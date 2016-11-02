using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMaker : MonoBehaviour {

	// Use this for initialization
	void Start () {

        string url = "https://api.ct1-staging.luminopia.com/patients/recommended_content";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        string passwd = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyYjEzMTgxNy05YTI1LTRkYWQtYjMyZi01N2JmNDk0ZDI1YjEiLCIkdHlwZSI6InBhdGllbnQiLCJpYXQiOjE0NzgwOTM1Mjd9.S9UBG75AvYcQBTt-pQsUPAlWHMXH5ZMh9ql-evQGrxo";
        headers.Add("Authorization", passwd);

        GetRequest(url, headers);


        headers = new Dictionary<string, string>();
        passwd = "wrongasswd";
        headers.Add("Authorization", passwd);

        GetRequest(url, headers);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private System.Collections.IEnumerator WaitForRequest(WWW www)
    {
        
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("Request failed with: " + www.error + "\nError details: " + www.text);
        }
    }


    

    void GetRequest(string url, Dictionary<string, string> headers)
    {
        StartCoroutine(WaitForRequest(new WWW(url, null, headers))); 
    }
}
