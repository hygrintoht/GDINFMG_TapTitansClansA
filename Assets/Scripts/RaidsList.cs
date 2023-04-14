using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidsList : MonoBehaviour
{
    [SerializeField] private List<Raid> raids;
    [SerializeField] private GameObject raidsPrefab;
    [SerializeField] private GameObject raidsPanel;

    public void SetupRaidsList(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse); //  response only returns ONE RAID from nodejs

        //foreach (KeyValuePair<string, JSONNode> entry in node)
        //{
        //    GameObject raid = GameObject.Instantiate(this.raidsPrefab, this.raidsPanel.transform);
        //    Raid raidScript = raid.GetComponent<Raid>();

        //    raidScript.raidID = entry.Value[0][0]; // (??)
        //}

        GameObject raid = GameObject.Instantiate(this.raidsPrefab, this.raidsPanel.transform);
        Raid raidScript = raid.GetComponent<Raid>();

        raidScript.raidID = node[0][0];
        this.raids.Add(raidScript);
    }
}
