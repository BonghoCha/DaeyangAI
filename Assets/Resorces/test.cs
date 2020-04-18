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

    public string Store_Key_Part = "";
    public string Store_Key_Part2 = "";
    public string Store_Key_Location = "";
    public string Store_Key_Phone = "";

    public int check = 0;
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
        List<Dictionary<string, object>> data = CSVReader.Read("Contact");
        Store_Key_Part = "부\n\n";
        Store_Key_Part2 = "부서\n\n";
        Store_Key_Location = "주소\n\n";
        Store_Key_Phone = "연락처\n\n";
        check = 0;
        string[] arr = new string[100000];
        for (var i = 0; i < data.Count; i++)
        {
            if (data[i]["부서"].ToString().Contains(Search_Keyword) || data[i]["부"].ToString().Contains(Search_Keyword) || data[i]["주소"].ToString().Contains(Search_Keyword) || data[i]["번호"].ToString().Contains(Search_Keyword))
            {
                arr[check] += data[i]["부서"];
                Debug.Log(arr[0]);
                Debug.Log(arr[1]);
                if (check >= 1)
                {
                    if (!arr[check].Equals(arr[check - 1]))
                    {
                        Store_Key_Part += "\n" + data[i]["부서"] + "\n";
                        Store_Key_Part2 += "\n" + data[i]["부"] + "\n";
                        Store_Key_Location += "\n" + data[i]["주소"] + "\n";
                        Store_Key_Phone += "\n" + data[i]["번호"] + "\n";
                        Debug.Log("Open");
                    }
                    else
                    {
                        Store_Key_Part += data[i]["부서"] + "\n";
                        Store_Key_Part2 += data[i]["부"] + "\n";
                        Store_Key_Location += data[i]["주소"] + "\n";
                        Store_Key_Phone += data[i]["번호"] + "\n";
                    }
                }
                else
                {
                    Store_Key_Part += data[i]["부서"] + "\n";
                    Store_Key_Part2 += data[i]["부"] + "\n";
                    Store_Key_Location += data[i]["주소"] + "\n";
                    Store_Key_Phone += data[i]["번호"] + "\n";
                }
                Debug.Log(Search_Keyword);
                check++;
            }
        }
        GameObject.Find("key_Part").GetComponent<Text>().text = Store_Key_Part;
        GameObject.Find("key_Part2").GetComponent<Text>().text = Store_Key_Part2;
        GameObject.Find("key_Location").GetComponent<Text>().text = Store_Key_Location;
        GameObject.Find("key_Phone").GetComponent<Text>().text = Store_Key_Phone;
        yield return new WaitForSeconds(0);
    }

    
}
