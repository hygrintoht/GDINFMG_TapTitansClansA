using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using SimpleJSON;

public class ServerTalker : MonoBehaviour
{
    public static ServerTalker Instance { get; private set; }

    const string address = "localhost:3000";
    public string query;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        
    }

    //READ-GET
    public IEnumerator GetPlayerStats(string query, string username)
    {
        Debug.Log("Query Player");
        Debug.Log(address + query + "/" + username);

        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + username);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    //CREATE

    //CREATE PLAYER
    public IEnumerator CreatePlayer(string query, List<string> _submittedDataList)
    {
        JSONObject body = new JSONObject();
        body.Add("username", _submittedDataList[0]);
        body.Add("email", _submittedDataList[1]);
        body.Add("country", _submittedDataList[2]);
        body.Add("title", _submittedDataList[3]);
        body.Add("installation_date", _submittedDataList[4]);

        byte[] requestData = Encoding.UTF8.GetBytes(body.ToString());

        UnityWebRequest request = UnityWebRequest.Post(address + query, body.ToString());
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(requestData);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        request.Dispose();
        
    }

    //CREATE CLAN
    public IEnumerator CreateClan(string query, string clanName)
    {
        JSONObject body = new JSONObject();
        body.Add("name", clanName);

        byte[] requestData = Encoding.UTF8.GetBytes(body.ToString());

        UnityWebRequest request = UnityWebRequest.Post(address + query, body.ToString());
        request.SetRequestHeader("Content-Type", "application/json");  
        request.uploadHandler = new UploadHandlerRaw(requestData);     
        request.downloadHandler = new DownloadHandlerBuffer();         

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        request.Dispose();
    }
}
