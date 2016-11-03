using System;
using System.Collections.Generic;
using UnityEngine;
using LitJson;


public class MenuMaker : MonoBehaviour {

    [Serializable]
    public class Category
    {
        public List<string> video_uris;
        public string name;
    }


    public static string passwd = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIyYjEzMTgxNy05YTI1LTRkYWQtYjMyZi01N2JmNDk0ZDI1YjEiLCIkdHlwZSI6InBhdGllbnQiLCJpYXQiOjE0NzgwOTM1Mjd9.S9UBG75AvYcQBTt-pQsUPAlWHMXH5ZMh9ql-evQGrxo";
    List<Category> categories = new List<Category>();

    // Use this for initialization
    void Start () {
        string url = "https://api.ct1-staging.luminopia.com/patients/recommended_content";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Authorization", passwd);

        StartCoroutine(WaitForRequest(new WWW(url, null, headers)));

    }

    // Update is called once per frame
    void Update () {
	
	}

    /*
    {
      "COMEDY": {
        "video_uris": [...],
        "name": "Comedy"
      },
      "SPORTS": {
        "video_uris": [...],
        "name": "Sports"
      }
    }
    */

    public void receiveCategoriesInformation(string myCategories)
    {
        JsonData myData = JsonMapper.ToObject(myCategories);

        for(int i = 0; i < myData.Keys.Count; i++)
        {
            Category tempCategory = new Category();

            tempCategory.name = myData[i]["name"].ToString();
            Debug.Log(myData[i]["name"].ToString());

            for (int j = 0; j < myData[i]["video_uris"].Count; j++)
            {
                tempCategory.video_uris.Add(myData[i]["video_uris"][j].ToString());
            }
            categories.Add(tempCategory);
        }
    }

    private System.Collections.IEnumerator GET_Categories()
    {

        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
            receiveCategoriesInformation(www.text);
        }
        else
        {
            Debug.Log("Request failed with: " + www.error + "\nError details: " + www.text);
        }
    }

    private System.Collections.IEnumerator WaitForRequest(WWW www)
    {
        
        yield return www;
        // check for errors
        if (www.error == null)
        {
            Debug.Log(www.text);
            receiveCategoriesInformation(www.text);
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
