using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Crawling_Professor : MonoBehaviour
{
    Dictionary<string, List<string>> proflist = new Dictionary<string, List<string>>() {
        { "name", new List<string>() },
        { "major", new List<string>() },
        { "location", new List<string>() },
        { "contact", new List<string>() },
    };

    public string[] url; //세종대 url 저장 변수
    public int u_index = 0; 
    public int m_index = 0;

    public string Search_Keyword = ""; //검색칸에 입력한 텍스트를 저장하는 변수
    public string t_name = ""; //교수 이름 저장하는 텍스트
    public string t_major = ""; //교수 전공 저장하는 텍스트
    public string t_location = ""; //연구실 위치 저장하는 텍스트
    public string t_contact = ""; //교수 연락처 저장하는 텍스트

    public int check = 0; //검색 했을 때 나오는 목록들을 카운트해ㅈ

    public Text _Name;
    public Text _Major;
    public Text _Address;
    public Text _Contact;

    public GameObject info_obj;
    public GameObject info_btn;

    public GoogleApi mapAPI;

    public void MapOnOff()
    {
        var map = mapAPI.gameObject.transform.parent.gameObject;
        if (!map.active) map.SetActive(true);
        else map.SetActive(false);
    }

    public void onClick() //검색 버튼 눌렀을 때
    {
        Search_Keyword = GameObject.Find("InputText").GetComponent<Text>().text; //검색칸에 입력한 텍스트를 가져옴
        check = 0;
        t_name = ""; //교수 이름 저장하는 텍스트
        t_major = ""; //교수 전공 저장하는 텍스트
        t_location = ""; //연구실 위치 저장하는 텍스트
        t_contact = ""; //교수 연락처 저장하는 텍스트
        string[] arr = new string[100000]; //전공이 달라지면 개행을 해주어야 하는데 그것을 체크하기 위한 변수

        int index = 0;

        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);
        Debug.Log(proflist["name"].Count);

        for (var i = 0; i < proflist["name"].Count - 1; i++)
        {
            if (i == 133) break;
            if (proflist["name"][i].ToString().Contains(Search_Keyword) || proflist["major"][i].ToString().Contains(Search_Keyword) || proflist["location"][i].ToString().Contains(Search_Keyword) || proflist["contact"][i].ToString().Contains(Search_Keyword))
            { //검색칸에 입력한 텍스트를 포함하는 딕셔너리를 찾음
                if (proflist["major"][i].Equals("&nbsp;") || proflist["location"][i].Equals("&nbsp;") || proflist["contact"][i].Equals("&nbsp;"))
                    continue; //전공과 연구실위치 연락처 없는 교수는 제외시킴
                
                arr[check] += proflist["major"][i]; //전공이 달라졌는지를 확인 하기 위해서 arr에 전공만 따로 저장함.
                //이제 전공이 달라져서 개행을 한번 더 해야되는지 확인하는 부분인데
                //달라졌는지를 확인 하는 부분에서 arr과 arr-1을 비교하는데 첫번째로 저장된 목록은 비교할 필요가 없기 때문에
                //check 1이상부터 arr과 arr-1를 비교하면 된다.
          
                if (check >= 1) //check가 1 이상.
                {
                    if (!arr[check].Equals(arr[check - 1])) //이전 arr과 같지 않다면 개행을 한번 더해줌
                    {
                        t_name += "\n" + proflist["name"][i] + "\n";
                        t_major += "\n" + proflist["major"][i] + "\n";
                        t_location += "\n" + proflist["location"][i] + "\n";
                        t_contact += "\n" + proflist["contact"][i] + "\n";

                        index++;
                        var name = 0;
                        var address = proflist["location"][i].ToString().Split(' ');
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
                    else // 아니면 그냥 한번만 개행 해줌
                    {
                        t_name += proflist["name"][i] + "\n";
                        t_major += proflist["major"][i] + "\n";
                        t_location += proflist["location"][i] + "\n";
                        t_contact += proflist["contact"][i] + "\n";

                        var name = 0;
                        var address = proflist["location"][i].ToString().Split(' ');
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
                else //첫번째 목록 추가.
                {
                    t_name += proflist["name"][i] + "\n";
                    t_major += proflist["major"][i] + "\n";
                    t_location += proflist["location"][i] + "\n";
                    t_contact += proflist["contact"][i] + "\n";

                    var name = 0;
                    var address = proflist["location"][i].ToString().Split(' ');
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
                check++; //check ++ 해줌
            }
        }

        info_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(777.7f, 30 * index);

        // Set Info
        _Name.text = t_name;
        _Major.text = t_major;
        _Address.text = t_location;
        _Contact.text = t_contact;
    }

    IEnumerator WebRequest(string address) //크롤링하여서 딕셔너리에 넣는 함수
    {
        UnityWebRequest request = new UnityWebRequest();
        
        using (request = UnityWebRequest.Get(address))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log("REQUEST ERROR"+request.error);
            }
            else
            {
                var html = request.downloadHandler.text;
                int start = html.IndexOf("prof_list", 0);
                int index = 0;
                //
                while(start != -1)
                {
                    int cnt = 0;
                    start = html.IndexOf("h4", start);
                    if (start == -1)
                        break;
                    int check1 = html.IndexOf(">", start);
                    int check2 = html.IndexOf("<", start);
                    int len = check2 - check1 - 1;
                    var name_store = html.Substring(check1 + 1, len);
                    if (name_store.Equals("안성청") || name_store.Equals("노용덕")||name_store.Equals("신수길") || name_store.Equals("홍익희"))  
                        break;//과마다 연구실위치와 연락처가 없는 교수가 있는데 다 마지막에 저장되어있어서 그냥 이 교수들 이름 찾게되면 브레이크 걸어버림.
                    proflist["name"].Add(name_store);

                    start = html.IndexOf("교수", start);
                    check1 = html.IndexOf("dd>", start) + 2;
                    check2 = html.IndexOf("</dd", start);
                    len = check2 - check1 - 1;
                    var major_store = html.Substring(check1 + 1, len);
                    proflist["major"].Add(major_store);

                    start = html.IndexOf("연구실위치", start);
                    check1 = html.IndexOf("dd>", start) + 2;
                    check2 = html.IndexOf("</dd", start);
                    len = check2 - check1 - 1;
                    var location_store = html.Substring(check1 + 1, len);
                    proflist["location"].Add(location_store);

                    start = html.IndexOf("연구실전화", start);
                    check1 = html.IndexOf("dd>", start) + 2;
                    check2 = html.IndexOf("</dd", start);
                    len = check2 - check1 - 1;
                    var contact_store = html.Substring(check1 + 1, len);
                    proflist["contact"].Add(contact_store);
                    m_index++;
                    index++;
                    cnt++;
                }

                if (u_index < url.Length) StartCoroutine("WebRequest", url[u_index++]);
                //
            }
        }
    }

    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WebRequest", url[u_index++]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
