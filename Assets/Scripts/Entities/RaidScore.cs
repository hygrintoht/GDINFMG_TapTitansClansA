using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaidScore : MonoBehaviour
{
    public int raidID;
    public int playerID;
    public string username;
    public int attacks;
    public int damage;

    [Header("Refereces")]
    [SerializeField] private TMP_Text usernameText;
    [SerializeField] private TMP_Text attacksText;
    [SerializeField] private TMP_Text damageText;

    public void AssignHolderText()
    {
        usernameText.text = username;
        attacksText.text = attacks.ToString();
        damageText.text = damage.ToString();
    }

}
