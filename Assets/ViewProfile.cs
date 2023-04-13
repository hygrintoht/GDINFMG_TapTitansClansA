using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewProfile : MonoBehaviour
{
    public enum ProfileData
    {
        Username = 0,
        Email,
        Country,
        PlayerId,
        LastPlayed,
        JoinedOn,
        PlayTime,
        ClanId,
        ClanTitle,
        MaxPrestigeStage,
        SkillPointsOwned,
        ArtifactsCollected,
        CraftingPower,
        TotalPetLevels
    };

    
    [SerializeField] private TextMeshProUGUI[] dataText; //Text Gameobjects in the right column of the profile viewer
    private List<string> _retrievedData; //List of strings retrieved from the SERVER
    void OnEnable()
    {

    }

    //TODO - SERVER-SIDE
    private void RetrieveData(int playerIndex)
    {
        //Add player[i]'s data
            //_retrievedData.Add();
            //_retrievedData.Add();
            //_retrievedData.Add();
        
        
        //print data
        for(int i = 0; i < dataText.Length; i++)
        {
            dataText[i].text = _retrievedData[i];
        }
    }
}
