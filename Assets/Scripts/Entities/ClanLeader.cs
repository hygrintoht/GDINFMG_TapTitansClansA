using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClanLeader : MonoBehaviour
{
    public string clanName;
    public string leaderName;

    [SerializeField] private TMP_Text clanNameText;
    [SerializeField] private TMP_Text leaderNameText;

    public void AssignText()
    {
        this.clanNameText.text = clanName;
        this.leaderNameText.text = leaderName;
    }
}
