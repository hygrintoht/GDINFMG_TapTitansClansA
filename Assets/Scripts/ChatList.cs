using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatList : MonoBehaviour
{
    [SerializeField] List<Chat> chats;
    [SerializeField] private GameObject chatPrefab;
    [SerializeField] private GameObject chatPanel;

    public void SetupRaidsList(string rawResponse)
    {
        JSONNode node = JSON.Parse(rawResponse);

        GameObject chat = GameObject.Instantiate(this.chatPrefab, this.chatPanel.transform);
        Chat chatScript = chat.GetComponent<Chat>();

        chatScript.chatID = node[0][0];
        this.chats.Add(chatScript);

    }
}
