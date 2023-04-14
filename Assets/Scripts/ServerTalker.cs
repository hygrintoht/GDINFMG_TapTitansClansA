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

    //READ-GET

    public IEnumerator GetRaidByClanID(string query, int ID)
    {
        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + ID.ToString());
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log("RaidID" + request.downloadHandler.text);
            UIManager.Instance.ProcessRaid(request.downloadHandler.text);
        }

        request.Dispose();
    }

    public IEnumerator GetRaidScores(string query, int ID)
    {
        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + ID.ToString());
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log("RaidID" + request.downloadHandler.text);
            UIManager.Instance.clanProfile.SetupRaidScore(request.downloadHandler.text);
        }

        request.Dispose();
    }

    public IEnumerator GetClanByID(string query, int ID)
    {
        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + ID.ToString());
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("(Clan) Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            UIManager.Instance.ProcessMyClan(request.downloadHandler.text);
        }

        request.Dispose();
    }

    public IEnumerator GetClanLeaderWithClanID(string query, int ID)
    {
        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + ID.ToString());
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("(Leader) Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            //UIManager.Instance.ProcessMyLeader(request.downloadHandler.text);
        }

        request.Dispose();
    }

    public IEnumerator GetPlayerStats(string query, int ID)
    {
        Debug.Log("Query Player");
        Debug.Log(address + query + "/" + ID.ToString());

        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + ID.ToString());
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            UIManager.Instance.AssignProfileText(request.downloadHandler.text);
        }

        request.Dispose();
    }

    public IEnumerator GetPlayersFromClan(string query, int clanID)
    {
        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + clanID.ToString() + "/players");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log("Query Succesful");
            UIManager.Instance.SetupClanMembers(request.downloadHandler.text);
        }

        request.Dispose();
    }

    public IEnumerator GetPlayerID(string query, string username)
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
            JSONNode node = JSON.Parse(request.downloadHandler.text);
            UIManager.Instance.player.playerID = node[0][0];

        }

        request.Dispose();
    }


    public IEnumerator GetClans(string query)
    {
        Debug.Log("Get all Clans");

        UnityWebRequest request = UnityWebRequest.Get(address + query);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log("Query Succesful");
            UIManager.Instance.clanSearch.ClanSearchSetup(request.downloadHandler.text);
        }

        request.Dispose();
    }

    public IEnumerator GetRaidsList(string query, int ID)
    {
        UnityWebRequest request = UnityWebRequest.Get(address + query + "/" + ID.ToString());
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("(Raids) Something went wrong: " + request.error);
        }
        else
        {
            Debug.Log("Query Succesful");
      
        }

        request.Dispose();
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
            UIManager.Instance.player.username = _submittedDataList[0];
            StartCoroutine(GetPlayerID("/player/ID", _submittedDataList[0]));
            UIManager.Instance.CloseProfileCreatePanel();


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
            UIManager.Instance.CloseClanCreatePanel();
        }

        request.Dispose();
    }

    //CreateRaidScore
    public IEnumerator CreateRaidScore(string query, List<int> submittedData)
    {
        JSONObject body = new JSONObject();
        body.Add("raidId", submittedData[0]);
        body.Add("playerID", submittedData[1]);
        body.Add("attacks", submittedData[2]);
        body.Add("damage", submittedData[3]);

        Debug.Log(body);

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
            //process
        }

        request.Dispose();
    }

    // PATCH

    //PATCH PLAYER
    public IEnumerator UpdatePlayer(string query, int playerID, JSONArray updateVals)
    {
        
        JSONObject body = new JSONObject();
        body.Add("playerID", playerID);
        body.Add("updateVals", updateVals);

        byte[] requestData = Encoding.UTF8.GetBytes(body.ToString());

        UnityWebRequest request = UnityWebRequest.Put(address + query, requestData);
        request.SetRequestHeader("Content-Type", "application/json");
        request.method = "PATCH";
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
