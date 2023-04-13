using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    [Space(10)]
    public Clan clan;
    public List<Player> clanMembers = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
