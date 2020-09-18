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
        { "category", new List<string>() }
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

    public GameObject prefab;
    public GameObject prefab2;
    public GameObject[] professor_bag;

    public Text _Name;
    public Text _Major;
    public Text _Address;
    public Text _Contact;

    public GameObject info_obj;
    public GameObject info_btn;

    public GoogleApi mapAPI;

    public void onClick()
    {
        Search_Keyword = GameObject.Find("InputText").GetComponent<Text>().text;
        StopAllCoroutines();
        StartCoroutine(Search(Search_Keyword));
    }
    public void MapOnOff()
    {
        var map = mapAPI.gameObject.transform.parent.gameObject;
        if (!map.active) map.SetActive(true);
        else map.SetActive(false);
    }

    public void Btn1()
    {
        StopAllCoroutines();
        StartCoroutine(Search("인문과학"));
    }
    public void Btn2()
    {
        StopAllCoroutines();
        StartCoroutine(Search("사회과학"));
    }
    public void Btn3()
    {
        StopAllCoroutines();
        StartCoroutine(Search("경영경제"));
    }
    public void Btn4()
    {
        StopAllCoroutines();
        StartCoroutine(Search("호텔관광"));
    }
    public void Btn5()
    {
        StopAllCoroutines();
        StartCoroutine(Search("자연과학"));
    }
    public void Btn6()
    {
        StopAllCoroutines();
        StartCoroutine(Search("생명과학"));
    }
    public void Btn7()
    {
        StopAllCoroutines();
        StartCoroutine(Search("전자정보"));
    }
    public void Btn8()
    {
        StopAllCoroutines();
        StartCoroutine(Search("소프트웨어융합"));
    }
    public void Btn9()
    {
        StopAllCoroutines();
        StartCoroutine(Search("공과"));
    }
    public void Btn10()
    {
        StopAllCoroutines();
        StartCoroutine(Search("예체능"));
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
        string[] contact_store = new string[1000];
        int[] location_store = new int[1000];
        Debug.Log(Search_Keyword);

        prefab.SetActive(true);
        prefab2.SetActive(true);

        int index = 0;
        for (int i = 0; i < professor_bag.Length; i++)
        {
            Destroy(professor_bag[i]);
            Debug.Log("DESTROY");
        }
        Debug.Log("SEARCH!!!");
        Debug.Log("교수님 수: "+proflist["name"].Count);
        
        for (var i = 0; i < proflist["name"].Count - 1; i++)
        {
            Debug.Log("SEARCH!!!");
            if (proflist["name"][i].ToString().Contains(Search_Keyword) || proflist["major"][i].ToString().Contains(Search_Keyword) || proflist["location"][i].ToString().Contains(Search_Keyword) || proflist["contact"][i].ToString().Contains(Search_Keyword) || proflist["category"][i].ToString().Contains(Search_Keyword))
            {
                if (proflist["major"][i].Equals("&nbsp;") || proflist["location"][i].Equals("&nbsp;") || proflist["contact"][i].Equals("&nbsp;"))
                    continue;

                contact_store[index] += "이름  " + proflist["name"][i] + "\n" + "전공  " + proflist["major"][i] + "\n" + "주소  " + proflist["location"][i] + "\n" + "번호  " + proflist["contact"][i] + "\n";
                Debug.Log(contact_store[index]);
                var name = 1;
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
                else if (address[0] == "대양AI")
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
            }
        }
        professor_bag = new GameObject[index];
        int cnt = 0;
        for (var i = 0; i < index; i = i + 2)
        {
            var obj = Instantiate(prefab);
            obj.transform.parent = prefab.transform.parent;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(328, -80 - (142 * cnt), 0);
            obj.name = "TEXT" + i;
            obj.transform.GetChild(0).GetComponent<Text>().text = contact_store[i];
            obj.transform.GetChild(1).gameObject.name = location_store[i] + "";

            var name = location_store[i];

            Debug.Log(location_store[i]);
            obj.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                MapOnOff();
                mapAPI.SetLocation(name);
            });

            professor_bag[i] = obj;
            cnt++;
        }
        cnt = 0;
        for (var i = 1; i < index; i = i + 2)
        {
            var obj2 = Instantiate(prefab2);
            obj2.transform.parent = prefab2.transform.parent;
            obj2.GetComponent<RectTransform>().anchoredPosition = new Vector3(700.5f, -80 - (142 * cnt), 0);
            obj2.name = "TEXT" + i;
            obj2.transform.GetChild(0).GetComponent<Text>().text = contact_store[i];
            obj2.transform.GetChild(1).gameObject.name = location_store[i] + "";

            var name = location_store[i];

            Debug.Log(location_store[i]);
            obj2.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                MapOnOff();
                mapAPI.SetLocation(name);
            });
            professor_bag[i] = obj2;
            cnt++;
        }
        info_obj.GetComponent<RectTransform>().sizeDelta = new Vector2(948.2f, 143 * ((index + 1) / 2));


        prefab.SetActive(false);
        prefab2.SetActive(false);
        yield return new WaitForSeconds(0);
    }

    IEnumerator WebRequest(string address) //크롤링하여서 딕셔너리에 넣는 함수
    {
        UnityWebRequest request = new UnityWebRequest();

        using (request = UnityWebRequest.Get(address))
        {
            yield return request.SendWebRequest();
            if (request.isNetworkError)
            {
                Debug.Log("REQUEST ERROR" + request.error);
            }
            else
            {
                var html = request.downloadHandler.text;
                int start = html.IndexOf("prof_list", 0);
                int index = 0;
                //
                while (start != -1)
                {
                    int cnt = 0;
                    start = html.IndexOf("h4", start);
                    if (start == -1)
                        break;
                    int check1 = html.IndexOf(">", start);
                    int check2 = html.IndexOf("<", start);
                    int len = check2 - check1 - 1;
                    var name_store = html.Substring(check1 + 1, len);
                    if (name_store.Equals("안성청") || name_store.Equals("노용덕") || name_store.Equals("신수길") || name_store.Equals("홍익희"))
                        break;//과마다 연구실위치와 연락처가 없는 교수가 있는데 다 마지막에 저장되어있어서 그냥 이 교수들 이름 찾게되면 브레이크 걸어버림.
                    proflist["name"].Add(name_store);

                    start = html.IndexOf("교수", start);
                    check1 = html.IndexOf("dd>", start) + 2;
                    check2 = html.IndexOf("</dd", start);
                    len = check2 - check1 - 1;
                    var major_store = html.Substring(check1 + 1, len);
                    proflist["major"].Add(major_store);
                    if(major_store.Equals("국어국문학과") || major_store.Equals("영어영문학") || major_store.Equals("일어일문학") || major_store.Equals("중국통상학") || major_store.Equals("교육학과"))
                    {
                        Debug.Log("ADD인문과학!!!");
                        proflist["category"].Add("인문과학대학");
                    }
                    else if(major_store.Equals("행정학과") || major_store.Equals("미디어커뮤니케이션학과"))
                    {
                        Debug.Log("ADD사회과학대학!!!");
                        proflist["category"].Add("사회과학대학");
                    }
                    else if(major_store.Equals("경제학과") || major_store.Equals("경영학"))
                    {
                        Debug.Log("ADD경영경제대학!!!");
                        proflist["category"].Add("경영경제대학");
                    }
                    else if(major_store.Equals("호텔경영전공") || major_store.Equals("호텔관광경영학전공") || major_store.Equals("외식경영전공"))
                    {
                        Debug.Log("ADD호텔관광대학!!!");
                        proflist["category"].Add("호텔관광대학");
                    }
                    else if (major_store.Equals("수학통계학부") || major_store.Equals("물리천문학과") || major_store.Equals("화학과"))
                    {
                        Debug.Log("ADD자연과학대학!!!");
                        proflist["category"].Add("자연과학대학");
                    }
                    else if (major_store.Equals("식품생명공학") || major_store.Equals("바이오융합공학") || major_store.Equals("바이오산업자원공학") || major_store.Equals("스마트생명산업융합학과"))
                    {
                        Debug.Log("ADD생명과학대학!!!");
                        proflist["category"].Add("생명과학대학");
                    }
                    else if (major_store.Equals("전자정보통신공학과"))
                    {
                        Debug.Log("ADD전정대!!!");
                        proflist["category"].Add("전자정보공학대학");
                    }
                    else if (major_store.Equals("컴퓨터공학") || major_store.Equals("소프트웨어학과") || major_store.ToString().Contains("정보보호") || major_store.ToString().Contains("데이터사이언스") || major_store.ToString().Contains("지능기전공학") || major_store.ToString().Contains("스마트기기공학") || major_store.ToString().Contains("디자인이노베이션") || major_store.ToString().Contains("만화애니메이션"))
                    {
                        Debug.Log("ADD소융대!!!");
                        proflist["category"].Add("소프트웨어융합대학");
                    }
                    else if (major_store.Equals("건축공학") || major_store.Equals("건축학") || major_store.ToString().Contains("건설환경공학") || major_store.ToString().Contains("환경에너지공간융합") || major_store.ToString().Contains("지구자원시스템공학") || major_store.ToString().Contains("기계공학") || major_store.ToString().Contains("항공우주공학") || major_store.ToString().Contains("신소재공학") || major_store.ToString().Contains("양자원자력공학") || major_store.ToString().Contains("국방시스템공학") || major_store.ToString().Contains("항공시스템공학"))
                    {
                        Debug.Log("ADD공대!!!");
                        proflist["category"].Add("공과대학");
                    }
                    else if (major_store.Equals("회화과") || major_store.Equals("산업디자인학과") || major_store.ToString().Contains("패션디자인학과") || major_store.ToString().Contains("음악과") || major_store.ToString().Contains("체육학과") || major_store.ToString().Contains("무용과") || major_store.ToString().Contains("영화예술학과") || major_store.ToString().Contains("신소재공학") || major_store.ToString().Contains("양자원자력공학") || major_store.ToString().Contains("국방시스템공학") || major_store.ToString().Contains("항공시스템공학"))
                    {
                        Debug.Log("ADD예체능!!!");
                        proflist["category"].Add("예체능대학");
                    }
                    else
                    {
                        proflist["category"].Add("-");
                    }
                    start = html.IndexOf("연구실위치", start);
                    check1 = html.IndexOf("dd>", start) + 2;
                    check2 = html.IndexOf("</dd", start);
                    len = check2 - check1 - 1;
                    var location_store = html.Substring(check1 + 1, len);
                    if (location_store.Length > 3 && location_store[0] != '&')
                    {
                        if (location_store[0] == '광' && location_store[2] != '토')
                        {
                            var tmp = "광개토관";
                            var tmp3 = location_store.Substring(2, location_store.Length - 2);
                            location_store = tmp + ' ' + tmp3;
                        }
                        if (location_store[location_store.Length - 1] != '호')
                        {
                            location_store += '호';
                        }
                    }
                    Debug.Log(proflist["category"][index]);
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

                    if (name_store.Equals("MALIK ASIF IQBAL"))
                        break;
                }

                if (u_index < url.Length) StartCoroutine("WebRequest", url[u_index++]);
                //
            }
        }
    }
    void Start()
    {
        StartCoroutine("WebRequest", url[u_index++]);
    }
}

    
