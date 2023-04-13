using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SearchManager : MonoBehaviour
{
    [SerializeField] private GameObject contentHolder;
    [FormerlySerializedAs("element")] [SerializeField] private GameObject[] elements;
    [SerializeField] private GameObject searchBar;

    private int totalElements;

    private void Start()
    {
        totalElements = contentHolder.transform.childCount;

        elements = new GameObject[totalElements];

        for (int i = 0; i < totalElements; i++)
        {
            elements[i] = contentHolder.transform.GetChild(i).gameObject;
        }
    }

    public void Search()
    { 
        string searchText = searchBar.GetComponent<TMP_InputField>().text;
        int searchTextLength = searchText.Length;

        int searchedElements = 0;
        
        foreach (GameObject element in elements)
        {
            searchedElements += 1;

            if (element.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length >= searchTextLength)
            {
                if (searchText.ToLower() == element.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.ToLower()
                        .Substring(0, searchTextLength))
                {
                    element.SetActive(true);
                }
                else
                {
                    element.SetActive(false);
                }
            }

        }
    }
}
