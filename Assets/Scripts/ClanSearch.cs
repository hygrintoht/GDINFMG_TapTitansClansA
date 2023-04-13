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

        GameObject clone = Instantiate(clanPrefab, transform.position, Quaternion.identity, clanSearchContent.transform);
        Clan clan = clone.AddComponent<Clan>();
        clan.clanID = 1;
        clan.clanName = "Test";
    }
}
