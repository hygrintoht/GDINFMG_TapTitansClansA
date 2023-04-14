using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Raid : MonoBehaviour
{
    public int raidID;
    public string titanName;
    public int clanID;

    [SerializeField] TMP_Text raidIDText;
    [SerializeField] TMP_Text titanNameText;

    public void AssignText()
    {
        //raidIDText.text = raidID.ToString(); // get statement from nodejs only returns raidID
        titanNameText.text = titanName;
    }

}
