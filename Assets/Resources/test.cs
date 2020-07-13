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

    public GameObject info_obj;
    public GameObject info_btn;

    public GoogleApi mapAPI;

    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Contact");

        info_obj = GameObject.Find("key_Location").transform.parent.gameObject;
        info_btn = GameObject.Find("key_Button").transform.gameObject;

        for (var i = 0; i < data.Count; i++)
        {
            //Debug.Log("index " + (i).ToString() + " : " + data[i]["부서"] + " " + data[i]["부"] + " " + data[i]["주소"] + " " + data[i]["번호"]);
            Contact = Contact + "index " + (i).ToString() + " : " + data[i]["부서"] + " " + data[i]["부"] + " " + data[i]["주소"] + " " + data[i]["번호"] + "\n";
            if (data[i]["부서"].ToString().Contains(Keyword) || data[i]["부"].ToString().Contains(Keyword) || data[i]["주소"].ToString().Contains(Keyword) || data[i]["번호"].ToString().Contains(Keyword))
            {
                Store_Keyword += data[i]["부서"] + " " + data[i]["부"] + " " + data[i]["주소"] + " " + data[i]["번호"] + "\n";
            }
        }
        //GameObject.Find("test").GetComponent<Text>().text = Contact;

        //GameObject.Find("key").GetComponent<Text>().text = Store_Keyword;
    }

    public void onClick()
    {
        StopAllCoroutines();
        StartCoroutine(Search());
    }

    public void MapOnOff()
    {
        var map = mapAPI.gameObject.transform.parent.gameObject;
        if (!map.active) map.SetActive(true);
        else map.SetActive(false);
    }

    IEnumerator Search()
    {
        Search_Keyword = GameObject.Find("InputText").GetComponent<Text>().text;
        List<Dictionary<string, object>> data = CSVReader.Read("Contact");
        Store_Key_Part = "";
        Store_Key_Part2 = "";
        Store_Key_Location = "";
        Store_Key_Phone = "";
        check = 0;
        string[] arr = new string[100000];

        int index = 0;

        for (var i = 0; i < data.Count; i++)
        {
            if (data[i]["부서"].ToString().Contains(Search_Keyword) || data[i]["부"].ToString().Contains(Search_Keyword) || data[i]["주소"].ToString().Contains(Search_Keyword) || data[i]["번호"].ToString().Contains(Search_Keyword))
            {
                arr[check] += data[i]["부서"];
                if (check >= 1)
                {
                    if (!arr[check].Equals(arr[check - 1]))
                    {
                        Store_Key_Part += "\n" + data[i]["부서"] + "\n";
                        Store_Key_Part2 += "\n" + data[i]["부"] + "\n";
                        Store_Key_Location += "\n" + data[i]["주소"] + "\n";
                        Store_Key_Phone += "\n" + data[i]["번호"] + "\n";

                        index++;

                        var name = 0;
                        var address = data[i]["주소"].ToString().Split(' ');
                        if (address[0] == "대양홀")
                        {
                            name = 1;
                        }
                        else if (address[0] == "집현관")
                        {
                            name = 2;
                        }
                        else if (address[0] == "학생회관")
                        {
                            name = 3;
                        }
                        else if (address[0] == "군자관")
                        {
                            name = 4;
                        }
                        else if (address[0] == "세종관")
                        {
                            name = 5;
                        }
                        else if (address[0] == "광개토관")
                        {
                            name = 6;
                        }
                        else if (address[0] == "이당관")
                        {
                            name = 7;
                        }
                        else if (address[0] == "애지헌")
                        {
                            name = 8;
                        }
                        else if (address[0] == "진관홀")
                        {
                            name = 9;
                        }
                        else if (address[0] == "용덕관")
                        {
                            name = 10;
                        }
                        else if (address[0] == "대양AI센터")
                        {
                            name = 11;
                        }
                        else if (address[0] == "박물관")
                        {
                            name = 12;
                        }
                        else if (address[0] == "우정당")
                        {
                            name = 13;
                        }
                        else if (address[0] == "학술정보원")
                        {
                            name = 14;
                        }
                        else if (address[0] == "율곡관")
                        {
                            name = 15;
                        }
                        else if (address[0] == "충무관")
                        {
                            name = 16;
                        }
                        else if (address[0] == "영실관")
                        {
                            name = 17;
                        }
                        else if (address[0] == "다산관")
                        {
                            name = 18;
                        }
                        else if (address[0] == "모차르트홀")
                        {
                            name = 19;
                        }
                        info_btn.transform.GetChild(index).gameObject.name = name + "";
                        info_btn.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() => {
                            MapOnOff();
                            mapAPI.SetLocation(name);
                        });
                        index++;
                    }
                    else
                    {
                        Store_Key_Part += data[i]["부서"] + "\n";
                        Store_Key_Part2 += data[i]["부"] + "\n";
                        Store_Key_Location += data[i]["주소"] + "\n";
                        Store_Key_Phone += data[i]["번호"] + "\n";

                        var name = 0;
                        var address = data[i]["주소"].ToString().Split(' ');
                        if (address[0] == "대양홀")
                        {
                            name = 1;
                        }
                        else if (address[0] == "집현관")
                        {
                            name = 2;
                        }
                        else if (address[0] == "학생회관")
                        {
                            name = 3;
                        }
                        else if (address[0] == "군자관")
                        {
                            name = 4;
                        }
                        else if (address[0] == "세종관")
                        {
                            name = 5;
                        }
                        else if (address[0] == "광개토관")
                        {
                            name = 6;
                        }
                        else if (address[0] == "이당관")
                        {
                            name = 7;
                        }
                        else if (address[0] == "애지헌")
                        {
                            name = 8;
                        }
                        else if (address[0] == "진관홀")
                        {
                            name = 9;
                        }
                        else if (address[0] == "용덕관")
                        {
                            name = 10;
                        }
                        else if (address[0] == "대양AI센터")
                        {
                            name = 11;
                        }
                        else if (address[0] == "박물관")
                        {
                            name = 12;
                        }
                        else if (address[0] == "우정당")
                        {
                            name = 13;
                        }
                        else if (address[0] == "학술정보원")
                        {
                            name = 14;
                        }
                        else if (address[0] == "율곡관")
                        {
                            name = 15;
                        }
                        else if (address[0] == "충무관")
                        {
                            name = 16;
                        }
                        else if (address[0] == "영실관")
                        {
                            name = 17;
                        }
                        else if (address[0] == "다산관")
                        {
                            name = 18;
                        }
                        else if (address[0] == "모차르트홀")
                        {
                            name = 19;
                        }
                        info_btn.transform.GetChild(index).gameObject.name = name + "";
                        info_btn.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() => {
                            MapOnOff();
                            mapAPI.SetLocation(name);
                        });
                        index++;
                    }
                }
                else
                {

                    Store_Key_Part += data[i]["부서"] + "\n";
                    Store_Key_Part2 += data[i]["부"] + "\n";
                    Store_Key_Location += data[i]["주소"] + "\n";
                    Store_Key_Phone += data[i]["번호"] + "\n";

                    var name = 0;
                        var address = data[i]["주소"].ToString().Split(' ');
                    if (address[0] == "대양홀")
                    {
                        name = 1;
                    }
                    else if (address[0] == "집현관")
                    {
                        name = 2;
                    }
                    else if (address[0] == "학생회관")
                    {
                        name = 3;
                    }
                    else if (address[0] == "군자관")
                    {
                        name = 4;
                    }
                    else if (address[0] == "세종관")
                    {
                        name = 5;
                    }
                    else if (address[0] == "광개토관")
                    {
                        name = 6;
                    }
                    else if (address[0] == "이당관")
                    {
                        name = 7;
                    }
                    else if (address[0] == "애지헌")
                    {
                        name = 8;
                    }
                    else if (address[0] == "진관홀")
                    {
                        name = 9;
                    }
                    else if (address[0] == "용덕관")
                    {
                        name = 10;
                    }
                    else if (address[0] == "대양AI센터")
                    {
                        name = 11;
                    }
                    else if (address[0] == "박물관")
                    {
                        name = 12;
                    }
                    else if (address[0] == "우정당")
                    {
                        name = 13;
                    }
                    else if (address[0] == "학술정보원")
                    {
                        name = 14;
                    }
                    else if (address[0] == "율곡관")
                    {
                        name = 15;
                    }
                    else if (address[0] == "충무관")
                    {
                        name = 16;
                    }
                    else if (address[0] == "영실관")
                    {
                        name = 17;
                    }
                    else if (address[0] == "다산관")
                    {
                        name = 18;
                    }
                    else if (address[0] == "모차르트홀")
                    {
                        name = 19;
                    }
                    info_btn.transform.GetChild(index).gameObject.name = name + "";
                    info_btn.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() => {
                        MapOnOff();
                        mapAPI.SetLocation(name);
                    });
                    index++;
                }
                check++;
            }
        }

        info_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(777.7f, 30 * index);

        GameObject.Find("key_Part").GetComponent<Text>().text = Store_Key_Part;
        GameObject.Find("key_Part2").GetComponent<Text>().text = Store_Key_Part2;
        GameObject.Find("key_Location").GetComponent<Text>().text = Store_Key_Location;
        GameObject.Find("key_Phone").GetComponent<Text>().text = Store_Key_Phone;
        yield return new WaitForSeconds(0);
    }
}
