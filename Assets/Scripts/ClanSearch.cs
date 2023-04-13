using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;

public class ClanSearch : MonoBehaviour
{
    public GameObject clanSearchContent;

    [SerializeField] private GameObject clanPrefab;
    public List<GameObject> clanList = new List<GameObject>();


    public void ClanSearchSetup(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);
        Debug.Log(node);
        Debug.Log(node.Count);
        Debug.Log(node[0].Count);

        for (int i = 0; i < node.Count; i++)
        {
            GameObject clone = Instantiate(clanPrefab, transform.position, Quaternion.identity, clanSearchContent.transform);
            clanList.Add(clone);

            Clan clan = clone.GetComponent<Clan>();
            clan.clanID = node[i][0];
            clan.clanInviteCode = node[i][1];
            clan.clanName = node[i][2];
            clan.numRaidCompleted = node[i][3];
            clan.numClanMorale = node[i][4];
            clan.requiredStage = node[i][5];
            clan.requiredRaidLevel = node[i][6];
            clan.privacy = node[i][7];

            clan.AssignText();

        }
        /*
        GameObject clone = Instantiate(clanPrefab, transform.position, Quaternion.identity, clanSearchContent.transform);
        Clan clan = clone.AddComponent<Clan>();
        clan.clanID = 1;
        clan.clanName = "Test";
        */
    }
}
