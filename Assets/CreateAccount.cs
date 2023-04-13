using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccount : MonoBehaviour
{
    public enum PersonalData
    {
        Username,
        Password,
        Country,
        Title,
    };

    [SerializeField] private TextMeshProUGUI username;
    [SerializeField] private TextMeshProUGUI email;
    [SerializeField] private TextMeshProUGUI country;
    [SerializeField] private TextMeshProUGUI title;
    
    private List<string> _submittedDataList = new List<string>();

    private PersonalData _personalData;

    public void SubmitPersonalData()
    {
        //clear current list
        _submittedDataList.Clear();
        
        //get data from textareas
        _submittedDataList.Add(username.text);
        _submittedDataList.Add(email.text);
        _submittedDataList.Add(country.text);
        _submittedDataList.Add(title.text);
        DateTime dateToday = DateTime.Now;
        _submittedDataList.Add(dateToday.ToString("yyyy/MM/dd"));
        
        //upload to database
        UploadData();


        for(int i = 0; i < _submittedDataList.Count; i++)
        {
            Debug.Log(_submittedDataList[i]);
        }
       
    }

    public void UploadData()
    {
        ServerTalker.Instance.StartCoroutine(ServerTalker.Instance.CreatePlayer("/player", _submittedDataList));
    }
}
