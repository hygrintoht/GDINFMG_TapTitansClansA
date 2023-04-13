using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleJSON;

public class ClanProfile : MonoBehaviour
{
    [Header("Clan Profile References")]
    [SerializeField] private GameObject clanProfileTab;
    [SerializeField] private TMP_Text clanIDText;
    [SerializeField] private TMP_Text clanNameText;
    [SerializeField] private TMP_Text inviteCode;
    [SerializeField] private TMP_Text raidCompletedText;
    [SerializeField] private TMP_Text clanMoraleText;
    [SerializeField] private TMP_Text requiredStageText;
    [SerializeField] private TMP_Text requiredRaidLevelText;
    [SerializeField] private TMP_Text privacyText;

    [Header("Clan Members References")]
    [SerializeField] private GameObject clanMembersPanel;
    [SerializeField] private GameObject clanMemberPrefab;
    [SerializeField] private GameObject clanMemberContentTransform;

    [Space(10)]
    public Clan clan;
    public List<GameObject> clanMembers = new List<GameObject>();

    public void OpenClanStats()
    {
        clanProfileTab.SetActive(true);
        clanMembersPanel.SetActive(false);
    }

    public void OpenClanMembers()
    {
        clanMembersPanel.SetActive(true);
        clanProfileTab.SetActive(false);
    }

    public void SetupClanStats()
    {
        clanProfileTab.SetActive(true);
        clanMembersPanel.SetActive(false);

        clanIDText.text = clan.clanID.ToString();
        clanNameText.text = clan.clanName;
        inviteCode.text = clan.clanInviteCode.ToString();
        raidCompletedText.text = clan.numRaidCompleted.ToString();
        clanMoraleText.text = clan.numClanMorale.ToString();
        requiredStageText.text = clan.requiredStage.ToString();
        requiredRaidLevelText.text = clan.requiredRaidLevel.ToString();
        privacyText.text = clan.privacy;

    }

    public void SetupClanMembers(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);
        Debug.Log(node);
        Debug.Log(node.Count);
        Debug.Log(node[0].Count);

        for (int i = 0; i < node.Count; i++)
        {
            GameObject clone = Instantiate(clanMemberPrefab, transform.position, Quaternion.identity, clanMemberContentTransform.transform);
            clanMembers.Add(clone);

            Player player = clone.GetComponent<Player>();

            player.playerID = node[i][0];
            player.username = node[i][1];
            player.email = node[i][2];
            player.clanID = node[i][3];
            player.country = node[i][4];
            player.maxPrestigeStage = node[i][5];
            player.artifactsCollected = node[i][6];
            player.craftingPower = node[i][7];
            player.totalPetLevels = node[i][8];
            player.skillPointsOwned = node[i][9];

            player.AssignMemberText();


        }
    }

    public void ClearMembers()
    {
        foreach (GameObject item in clanMembers)
        {
            Destroy(item);
        }

        clanMembers.Clear();
    }
}
