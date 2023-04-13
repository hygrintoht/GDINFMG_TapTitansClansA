using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    public int playerID;
    public string username;
    public string email;
    public int clanID;
    public string country;
    public string title;
    public int maxPrestigeStage;
    public int artifactsCollected;
    public int craftingPower;
    public int totalPetLevels;
    public int skillPointsOwned;

    [Header("References")]
    [SerializeField] private TMP_Text playerIDText;
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text emailText;
    [SerializeField] private TMP_Text clanIDText;
    [SerializeField] private TMP_Text countryText;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text maxPrestigeStageText;
    [SerializeField] private TMP_Text artifactsCollectedText;
    [SerializeField] private TMP_Text craftingPowerText;
    [SerializeField] private TMP_Text totalPetLevelsText;
    [SerializeField] private TMP_Text skillPointsOwnedText;

    [Header("Clan Member Reference")]
    [SerializeField] private TMP_Text playerName;

    public void AssignPlayerText()
    {
        playerIDText.text = playerID.ToString();
        usernameText.text = username.ToString();
        emailText.text = email.ToString();
        clanIDText.text = clanID.ToString();
        countryText.text = country.ToString();
        titleText.text = title.ToString();
        maxPrestigeStageText.text = maxPrestigeStage.ToString();
        artifactsCollectedText.text = artifactsCollected.ToString();
        craftingPowerText.text = craftingPower.ToString();
        totalPetLevelsText.text = totalPetLevels.ToString();
        skillPointsOwnedText.text = skillPointsOwned.ToString();
    }

    public void AssignMemberText()
    {
        playerName.text = username;
    }

    public void InspectPlayer()
    {
        UIManager.Instance.InspectPlayer(this);
    }
}
