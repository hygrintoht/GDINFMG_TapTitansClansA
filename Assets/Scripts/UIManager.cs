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
    public Clan clan;
    public ClanSearch clanSearch;
    public ClanProfile clanProfile;
    public RaidsList raidsList;

    [Header("Panels")]
    [SerializeField] private GameObject profilePanel;
    [SerializeField] private GameObject profileCreatePanel;
    [SerializeField] private GameObject clanSearchPanel;
    [SerializeField] private GameObject clanCreatePanel;
    [SerializeField] private GameObject clanProfilePanel;
    [SerializeField] private GameObject raidsListPanel;

    public void Start()
    {
        profileCreatePanel.SetActive(true);

        profilePanel.SetActive(false);
        clanSearchPanel.SetActive(false);
        clanCreatePanel.SetActive(false);
        clanProfilePanel.SetActive(false);
        raidsListPanel.SetActive(false);
    }
    public void OpenClanProfile(Clan clan)
    {
        clanProfilePanel.SetActive(true);
        clanProfile.ClearMembers();
        clanProfile.clan = clan;
        clanProfile.SetupClanStats();
        
        StartCoroutine(ServerTalker.Instance.GetPlayersFromClan("/clan", clan.clanID));

    }

    public void ProcessMyLeader(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);
        clanProfile.leader.leaderName = node[0][0].ToString();
    }

    public void ProcessMyClan(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);
        clan.clanID = node[0][0];
        clan.clanInviteCode = node[0][1];
        clan.clanName = node[0][2];
        clan.numRaidCompleted = node[0][3];
        clan.numClanMorale = node[0][4];
        clan.requiredStage = node[0][5];
        clan.requiredRaidLevel = node[0][6];
        clan.privacy = node[0][7];

        clanProfilePanel.SetActive(true);
        clanProfile.ClearMembers();
        clanProfile.clan = clan;
        clanProfile.SetupClanStats();
        StartCoroutine(ServerTalker.Instance.GetPlayersFromClan("/clan", clan.clanID));

    }

    public void JoinClan(Clan otherClan)
    {
        if (player.clanID == otherClan.clanID)
        {
            Debug.Log("same clan");
            return;
        }

        JSONObject obj = new JSONObject();
        obj.Add("clanID", otherClan.clanID);

        JSONArray array = new JSONArray();
        array.Add(obj);

        StartCoroutine(ServerTalker.Instance.UpdatePlayer("/player", player.playerID, array));

        clan.clanID = otherClan.clanID;

    }

    public void OpenMyClan()
    {
        if (clan.clanID == 0)
        {
            Debug.Log("No clan joined!");
            return;
        }

        StartCoroutine(ServerTalker.Instance.GetClanLeaderWithClanID("/clan_leaders/get_clan_leaders", clan.clanID));
        StartCoroutine(ServerTalker.Instance.GetClanByID("/clan", clan.clanID));

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

        this.player.playerID = player.playerID;
        this.player.username = player.username;
        this.player.email = player.email;
        this.player.clanID = player.clanID;
        this.player.country = player.country;
        this.player.title = player.title;
        this.player.maxPrestigeStage = player.maxPrestigeStage;
        this.player.artifactsCollected = player.artifactsCollected;
        this.player.craftingPower = player.craftingPower;
        this.player.totalPetLevels = player.totalPetLevels;
        this.player.skillPointsOwned = player.skillPointsOwned;

        player.AssignPlayerText();
    }

    public void OpenRaidsList()
    {
        raidsListPanel.SetActive(true);
    }

    public void CloseRaidsList()
    {
        raidsListPanel.SetActive(false);
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
