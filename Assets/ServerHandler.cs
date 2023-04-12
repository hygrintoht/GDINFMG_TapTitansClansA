using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using Newtonsoft.Json;
using System.Text;

public class ServerHandler : MonoBehaviour
{
    const string address = "localhost:3000";
    const string query = "/college";

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(GetStudentsData(address, query));

        //StartCoroutine(CreateCollege(address, query, college));

        //StartCoroutine(DeleteCollege(address, query, "MySchool3"));
    }

    void ParseInfo(string rawJson)
    {
      
        JSONNode node = JSON.Parse(rawJson);
        Debug.Log(node[0].Count);
        //Debug.Log(node[0]["cName"]);
    }

    IEnumerator GetStudentsData(string address, string query)
    {
        UnityWebRequest www = UnityWebRequest.Get(address + query);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            ParseInfo(www.downloadHandler.text);
        }
    }

    IEnumerator CreateCollege(string address, string query)
    {

        JSONObject obj = new JSONObject();
        obj.Add("name", "MySchool5");
        obj.Add("city", "MyCity5");
        obj.Add("enrollment", 5);

        byte[] requestData = Encoding.UTF8.GetBytes(obj.ToString());        // Convert string into bytes

        UnityWebRequest request = UnityWebRequest.Post(address + query, obj.ToString());
        request.SetRequestHeader("Content-Type", "application/json");  // Set what type of data is in the request
        request.uploadHandler = new UploadHandlerRaw(requestData);     // Add the request data using UploadHandler
        request.downloadHandler = new DownloadHandlerBuffer();         // Create a receiver for the response later

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

    IEnumerator DeleteCollege(string address, string query, string collegeName)
    {
        JSONObject obj = new JSONObject();
        obj.Add("name", collegeName);

        byte[] requestData = Encoding.UTF8.GetBytes(obj.ToString());        // Convert string into bytes

        UnityWebRequest request = UnityWebRequest.Delete(address + query);
        request.SetRequestHeader("Content-Type", "application/json");  // Set what type of data is in the request
        request.uploadHandler = new UploadHandlerRaw(requestData);     // Add the request data using UploadHandler
        request.downloadHandler = new DownloadHandlerBuffer();         // Create a receiver for the response later

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
        }
        else
        {
            Debug.Log(collegeName + " Deleted!");
        }
    }
}
