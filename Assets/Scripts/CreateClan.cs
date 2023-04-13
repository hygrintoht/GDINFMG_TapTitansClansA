using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateClan : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clanName;

    public void SubmitClan()
    {
        ServerTalker.Instance.StartCoroutine(ServerTalker.Instance.CreateClan("/clan", clanName.text));
    }
}
