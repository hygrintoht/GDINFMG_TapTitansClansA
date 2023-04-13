using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    [Header("Player")]
    public Player player;
    public ClanSearch clanSearch;
    public ClanProfile clanProfile;

    [Header("Panels")]
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject profileCreatePanel;
    [SerializeField] private GameObject clanSearchPanel;
    [SerializeField] private GameObject clanCreatePanel;
    [SerializeField] private GameObject clanProfilePanel;

    public void Start()
    {
        profileCreatePanel.SetActive(true);

        profilePanel.SetActive(false);
        clanSearchPanel.SetActive(false);
        clanCreatePanel.SetActive(false);
        clanProfilePanel.SetActive(false);
    }
    public void OpenClanProfile(Clan clan)
    {
        clanProfilePanel.SetActive(true);
        clanProfile.ClearMembers();
        clanProfile.clan = clan;
        clanProfile.SetupClanStats();
        
        StartCoroutine(ServerTalker.Instance.GetPlayersFromClan("/clan", clan.clanID));

    }

    public void SetupClanMembers(string rawResponse)
    {
        clanProfile.SetupClanMembers(rawResponse);
    }

    public void CloseClanProfile()
    {
        clanProfilePanel.SetActive(false);
    }

    public void OpenProfileCreatePanel()
    {
        profileCreatePanel.SetActive(true);
    }
    public void OpenClanSearchPanel()
    {
        clanSearchPanel.SetActive(true);
        StartCoroutine(ServerTalker.Instance.GetClans("/clan"));
    }
    public void OpenClanCreatePanel()
    {
        clanCreatePanel.SetActive(true);
    }

    public void OpenProfilePanel()
    {
        ServerTalker.Instance.StartCoroutine(ServerTalker.Instance.GetPlayerStats("/player/stats", player.playerID));
        profilePanel.SetActive(true);
    }

    public void InspectPlayer(Player other)
    {
        profilePanel.SetActive(true);
        Player player = profilePanel.GetComponent<Player>();

        player.playerID = other.playerID;
        player.username = other.username;
        player.email = other.email;
        player.clanID = other.clanID;
        player.country = other.country;
        player.title = other.title;
        player.maxPrestigeStage = other.maxPrestigeStage;
        player.artifactsCollected = other.artifactsCollected;
        player.craftingPower = other.craftingPower;
        player.totalPetLevels = other.totalPetLevels;
        player.skillPointsOwned = other.skillPointsOwned;

        player.AssignPlayerText();

    }

    public void AssignProfileText(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);
        Player player = profilePanel.GetComponent<Player>();

        player.playerID = node[0][0];
        player.username = node[0][1];
        player.email = node[0][2];
        player.clanID = node[0][3];
        player.country = node[0][4];
        player.title = node[0][5];
        player.maxPrestigeStage = node[0][6];
        player.artifactsCollected = node[0][7];
        player.craftingPower = node[0][8];
        player.totalPetLevels = node[0][9];
        player.skillPointsOwned = node[0][10];

        player.AssignPlayerText();
    }

    public void CloseProfileCreatePanel()
    {
        profileCreatePanel.SetActive(false);
    }
    public void CloseClanSearchPanel()
    {
        clanSearch.ClearClans();
        clanSearchPanel.SetActive(false);
    }
    public void CloseClanCreatePanel()
    {
        clanCreatePanel.SetActive(false);
    }
    public void CloseProfilePanel()
    {
        profilePanel.SetActive(false);
    }

}
