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

    [Header("Clan Raid References")]
    [SerializeField] private GameObject clanRaidPanel;
    [SerializeField] private TMP_Text titanNameText;
    [SerializeField] private TMP_Text titanHealthText;

    [Header("Clan Raid Score")]
    [SerializeField] private GameObject clanRaidScore;
    [SerializeField] private GameObject clanRaidContentTransform;

    [Space(10)]
    public Clan clan;
    public Raid raid;
    public List<GameObject> clanMembers = new List<GameObject>();
    public List<GameObject> clanRaidScores = new List<GameObject>();


    public void JoinClan()
    {
        UIManager.Instance.JoinClan(clan);
    }

    public void OpenClanStats()
    {
        clanProfileTab.SetActive(true);
        clanMembersPanel.SetActive(false);
        clanRaidPanel.SetActive(false);
    }

    public void OpenClanMembers()
    {
        clanMembersPanel.SetActive(true);
        clanProfileTab.SetActive(false);
        clanRaidPanel.SetActive(false);

    }

    public void OpenClanRaids()
    {
        clanMembersPanel.SetActive(false);
        clanProfileTab.SetActive(false);
        clanRaidPanel.SetActive(true);
    }

    public void SetupRaid()
    {
        titanNameText.text = raid.titanName;

        StartCoroutine(ServerTalker.Instance.GetRaidScores("/raid_score", raid.raidID));
    }

    public void SetupRaidScore(string rawResponse)
    {
        ClearRaidScores();

        JSONNode node = JSON.Parse(rawResponse);
        Debug.Log(node);
        Debug.Log(node.Count);
        Debug.Log(node[0].Count);

        int totalDamage = 0;

        for (int i = 0; i < node.Count; i++)
        {
            GameObject clone = Instantiate(clanRaidScore, transform.position, Quaternion.identity, clanRaidContentTransform.transform);
            clanRaidScores.Add(clone);

            RaidScore raidScore = clone.GetComponent<RaidScore>();

            foreach (GameObject obj in clanMembers)
            {
                Player player = obj.GetComponent<Player>();
                if (player.playerID == node[i][1])
                {
                    raidScore.playerID = node[i][1];
                    raidScore.username = player.username;
                }
            }
            raidScore.raidID = node[i][0];
            raidScore.attacks = node[i][2];
            raidScore.damage = node[i][3];

            totalDamage += raidScore.damage;

            raidScore.AssignHolderText();
        }

        titanHealthText.text = (raid.titanHealth - totalDamage).ToString() + "/" + raid.titanHealth;

    }

    public void AttackRaid()
    {
        foreach (GameObject obj in clanRaidScores)
        {
            RaidScore raidScore = obj.GetComponent<RaidScore>();
            if (raidScore.playerID == UIManager.Instance.player.playerID)
            {
                return;
            }
 
        }

        //Create
        List<int> data = new List<int>();
        data.Add(raid.raidID);
        data.Add(UIManager.Instance.player.playerID);
        data.Add(1);
        data.Add(Random.Range(1, 5));
        StartCoroutine(ServerTalker.Instance.CreateRaidScore("/raid_score", data));
    }

    public void ClearRaidScores()
    {
        foreach (GameObject obj in clanRaidScores)
        {
            Destroy(obj);
        }

        clanRaidScores.Clear();
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
            //player.clanID = node[i][3];
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
