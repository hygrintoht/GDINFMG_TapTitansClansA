using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Clan : MonoBehaviour
{
    public int clanID;
    public int clanInviteCode;
    public string clanName;
    public int numRaidCompleted;
    public int numClanMorale;
    public int requiredStage;
    public int requiredRaidLevel;
    public string privacy;

    [Header("References")]
    [SerializeField] private TMP_Text clanNameText;

    public void AssignText()
    {
        clanNameText.text = clanName;
    }

    public void OpenClanProfile()
    {
        UIManager.Instance.OpenClanProfile(this);
    }
}
