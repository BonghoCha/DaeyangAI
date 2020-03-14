using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class test : MonoBehaviour
{

    public int _exp = 0;
    public string Contact = "";
    public string Keyword = "";
    public string Store_Keyword = "";
    public string Search_Keyword = "";
    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Contact");

        for (var i = 0; i < data.Count; i++)
        {
            //Debug.Log("index " + (i).ToString() + " : " + data[i]["부서"] + " " + data[i]["부"] + " " + data[i]["주소"] + " " + data[i]["번호"]);
            Contact = Contact + "index " + (i).ToString() + " : " + data[i]["부서"] + " " + data[i]["부"] + " " + data[i]["주소"] + " " + data[i]["번호"] + "\n";
            if (data[i]["부서"].ToString().Contains(Keyword) || data[i]["부"].ToString().Contains(Keyword) || data[i]["주소"].ToString().Contains(Keyword) || data[i]["번호"].ToString().Contains(Keyword))
            {
                Store_Keyword += data[i]["부서"] + " " + data[i]["부"] + " " + data[i]["주소"] + " " + data[i]["번호"] + "\n";
                Debug.Log(Store_Keyword);
            }
        }
        //GameObject.Find("test").GetComponent<Text>().text = Contact;

        GameObject.Find("key").GetComponent<Text>().text = Store_Keyword;
    }

    public void onClick()
    {
        Debug.Log("Click!");
        StopAllCoroutines();
        StartCoroutine(Search());
    }

    IEnumerator Search()
    {

        Search_Keyword = GameObject.Find("InputText").GetComponent<Text>().text;
        Store_Keyword = "";
        List<Dictionary<string, object>> data = CSVReader.Read("Contact");

        for (var i = 0; i < data.Count; i++)
        {
            if (data[i]["부서"].ToString().Contains(Search_Keyword) || data[i]["부"].ToString().Contains(Search_Keyword) || data[i]["주소"].ToString().Contains(Search_Keyword) || data[i]["번호"].ToString().Contains(Search_Keyword))
            {
                Store_Keyword += data[i]["부서"] + " " + data[i]["부"] + " " + data[i]["주소"] + " " + data[i]["번호"] + "\n";
                Debug.Log(Search_Keyword);
            }
        }
        GameObject.Find("key").GetComponent<Text>().text = Store_Keyword;
        yield return new WaitForSeconds(0);
    }

    
}
