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

    
    public GameObject prefab;
    public GameObject prefab2;
    public GameObject[] contact_bag;
    
    public int check = 0;

    public Text _Name;
    public Text _Major;
    public Text _Address;
    public Text _Contact;
   
    public GameObject info_obj;
    public GameObject info_btn;
    
    public GoogleApi mapAPI;

    public GameObject btn_univ;
    public GameObject btn_prof;

    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("Contact");
    }

    public void onClick()
    {
        Search_Keyword = GameObject.Find("InputText").GetComponent<Text>().text;
        StopAllCoroutines();
        StartCoroutine(Search(Search_Keyword));
    }
    public void MapOnOff()
    {
        Debug.Log("클릭");
        var map = mapAPI.gameObject.transform.parent.gameObject;
        if (!map.active) map.SetActive(true);
        else map.SetActive(false);
    }

    public void Btn1()
    {
        StopAllCoroutines();
        StartCoroutine(Search("인문과학대학"));
    }
    public void Btn2()
    {
        StopAllCoroutines();
        StartCoroutine(Search("사회과학대학"));
    }
    public void Btn3()
    {
        StopAllCoroutines();
        StartCoroutine(Search("경영경제대학"));
    }
    public void Btn4()
    {
        StopAllCoroutines();
        StartCoroutine(Search("호텔관광대학"));
    }
    public void Btn5()
    {
        StopAllCoroutines();
        StartCoroutine(Search("자연과학대학"));
    }
    public void Btn6()
    {
        StopAllCoroutines();
        StartCoroutine(Search("생명과학대학"));
    }
    public void Btn7()
    {
        StopAllCoroutines();
        StartCoroutine(Search("전자정보공학대학"));
    }
    public void Btn8()
    {
        StopAllCoroutines();
        StartCoroutine(Search("소프트웨어융합대학"));
    }
    public void Btn9()
    {
        StopAllCoroutines();
        StartCoroutine(Search("공과대학"));
    }
    public void Btn10()
    {
        StopAllCoroutines();
        StartCoroutine(Search("예체능대학"));
    }
    public void Btn11()
    {
        StopAllCoroutines();
        StartCoroutine(Search("대양휴머니티칼리지"));
    }
    public void Btn12()
    {
        StopAllCoroutines();
        StartCoroutine(Search("법학"));
    }
    IEnumerator Search(string Search_Keyword)
    {
        string[] contact_store = new string[500];
        int[] location_store = new int[500];
        Debug.Log(Search_Keyword);
        List<Dictionary<string, object>> data = CSVReader.Read("Contact");
        
        prefab.SetActive(true);
        prefab2.SetActive(true);

        int index = 0;
        for(int i=0; i<contact_bag.Length; i++)
        {
            Destroy(contact_bag[i]);
            Debug.Log("DESTROY");
        }
        for (var i = 0; i < data.Count; i++)
        {
            if (data[i]["부서"].ToString().Contains(Search_Keyword) || data[i]["부"].ToString().Contains(Search_Keyword) || data[i]["주소"].ToString().Contains(Search_Keyword) || data[i]["번호"].ToString().Contains(Search_Keyword))
            {
                contact_store[index] += "부서  " + data[i]["부서"] + "\n" + "부      " + data[i]["부"] + "\n" + "주소  " + data[i]["주소"] + "\n" + "번호  " + data[i]["번호"] + "\n";

                var name = 1;
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
                Debug.Log(address[0]);

                location_store[index] = name;
                Debug.Log(location_store[index]);
                index++;
                //info_btn.transform.GetChild(index).gameObject.name = name + "";
                //info_btn.transform.GetChild(index).GetComponent<Button>().onClick.AddListener(() => {
                    //mapAPI.SetLocation(name);
                    //MapOnOff();
                //});
            }
        }
        contact_bag = new GameObject[index];
        int cnt = 0;
        for (var i=0; i<index; i=i+2)
        {
            var obj = Instantiate(prefab);
            obj.transform.parent = prefab.transform.parent;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(328,-80-(142*cnt),0);
            obj.name = "TEXT"+i;
            obj.transform.GetChild(0).GetComponent<Text>().text = contact_store[i];
            obj.transform.GetChild(1).gameObject.name = location_store[i] + "";

            var name = location_store[i];

            Debug.Log(obj.GetComponent<RectTransform>().anchoredPosition.x);

            obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                MapOnOff();
                mapAPI.SetLocation(name);
            });

            contact_bag[i] = obj;
            cnt++;
        }
        cnt = 0;
        for (var i = 1; i < index; i = i + 2)
        {
            var obj2 = Instantiate(prefab2);
            obj2.transform.parent = prefab2.transform.parent;
            obj2.GetComponent<RectTransform>().anchoredPosition = new Vector3(700.5f,-80 - (142 * cnt), 0);
            obj2.name = "TEXT"+i;
            obj2.transform.GetChild(0).GetComponent<Text>().text = contact_store[i];
            obj2.transform.GetChild(1).gameObject.name = location_store[i] + "";

            var name = location_store[i];
            
            obj2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                MapOnOff();
                mapAPI.SetLocation(name);
            });
            contact_bag[i] = obj2;
            cnt++;
        }
        info_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(948.2f, 143*((index+1)/2));


        prefab.SetActive(false);
        prefab2.SetActive(false);
        yield return new WaitForSeconds(0);
    }
    
    public void clickButton_univ()
    {
        btn_prof.SetActive(false);
        btn_univ.SetActive(true);
    }
    public void clickButton_prof()
    {
        btn_prof.SetActive(true);
        btn_univ.SetActive(false);
    }

}
