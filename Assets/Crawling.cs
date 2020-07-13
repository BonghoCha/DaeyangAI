using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Crawling : MonoBehaviour
{
    const int MAX_NOTICE_LENGTH = 10;
    // 전체공지
    Dictionary<string, List<string>> all_notices = new Dictionary<string, List<string>>() {
        { "name", new List<string>() },
        { "index", new List<string>() },
        { "date", new List<string>() },
        { "href", new List<string>() },
    };
    // 학사
    Dictionary<string, List<string>> ba_notices = new Dictionary<string, List<string>>() {
        { "name", new List<string>() },
        { "index", new List<string>() },
        { "date", new List<string>() },
        { "href", new List<string>() },
    };
    // 국제교류
    Dictionary<string, List<string>> ie_notices = new Dictionary<string, List<string>>() {
        { "name", new List<string>() },
        { "index", new List<string>() },
        { "date", new List<string>() },
        { "href", new List<string>() },
    };
    // 취업/교내모집
    Dictionary<string, List<string>> ic_notices = new Dictionary<string, List<string>>() {
        { "name", new List<string>() },
        { "index", new List<string>() },
        { "date", new List<string>() },
        { "href", new List<string>() },
    };
    // 교내모집
    Dictionary<string, List<string>> cr_notices = new Dictionary<string, List<string>>() {
        { "name", new List<string>() },
        { "index", new List<string>() },
        { "date", new List<string>() },
        { "href", new List<string>() },
    };
    string today;
    public GameObject[] updates;
    public GameObject[] popup_updates;

    void FindElements(UnityWebRequest webRequest, string keyword, Dictionary<string, List<string>> notices)
    {
        var handler = webRequest.downloadHandler;
        var htmlStr = handler.text;
   
        // Initialize Start Index
        int start = htmlStr.IndexOf("tbody", 0);
        start = htmlStr.IndexOf(keyword, start);
        for (var index = 0; start != -1 && index <= MAX_NOTICE_LENGTH; index++)
        {
            switch (keyword)
            {
                case "href":
                    {
                        start = htmlStr.IndexOf("board", start);
                        var url = "http://board.sejong.ac.kr/" + htmlStr.Substring(start, (htmlStr.IndexOf(">", start) - 1) - start);
                        notices[keyword].Add(url.Replace('^', '&'));
                        break;
                    }
                default:
                    {
                        start = htmlStr.IndexOf(">", start) + 1;
                        notices[keyword].Add(htmlStr.Substring(start, htmlStr.IndexOf("<", start) - start));
                        break;
                    }
            }
            start = htmlStr.IndexOf(keyword, start);
        }

        if (keyword == "date")
        {
            var count = 0;
            for (var i = 0; i < MAX_NOTICE_LENGTH; i++)
            {
                if (notices["date"][i] == today)
                {
                    count++;
                }
            }
            if (count > 0) {
                if (notices["name"][0].Equals("all")){
                    updates[0].SetActive(true);
                    updates[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";

                    popup_updates[0].SetActive(true);
                    popup_updates[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";
                }
                else if (notices["name"][0].Equals("ba"))
                {
                    updates[1].SetActive(true);
                    updates[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";

                    popup_updates[1].SetActive(true);
                    popup_updates[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";
                }
                else if (notices["name"][0].Equals("ie"))
                {
                    updates[2].SetActive(true);
                    updates[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";

                    popup_updates[2].SetActive(true);
                    popup_updates[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";
                }
                else if (notices["name"][0].Equals("ic"))
                {
                    updates[3].SetActive(true);
                    updates[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";

                    popup_updates[3].SetActive(true);
                    popup_updates[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";
                }
                else if (notices["name"][0].Equals("cr"))
                {
                    updates[4].SetActive(true);
                    updates[4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";

                    popup_updates[4].SetActive(true);
                    popup_updates[4].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = count + "";
                }
            }
        }
    }

    void setNoticeButtons(UnityWebRequest webRequest, Dictionary<string, List<string>> notice)
    {
        FindElements(webRequest, "index", notice);
        FindElements(webRequest, "date", notice);
        FindElements(webRequest, "href", notice);
    }

    string ConvertDate()
    {
        var Date = System.DateTime.Now;

        string year = Date.Year + "";
        string month = Date.Month + "";
        if (int.Parse(month) < 10) month = "0" + month;
        string day = Date.Day + "";
        if (int.Parse(day) < 10) day = "0" + day;

        return year  + "." + month + "." + day;
    }

    void Start()
    {
        today = ConvertDate();

        // A correct website page.
        // 전체공지
        StartCoroutine(GetRequest("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=333"));
        // 학사
        StartCoroutine(GetRequest("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=335"));
        // 국제교류
        StartCoroutine(GetRequest("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=674"));
        // 취업/장학
        StartCoroutine(GetRequest("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=337"));
        // 교내모집
        StartCoroutine(GetRequest("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=339"));

        // A non-existing page.
        StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                if (uri == "http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=333")
                {
                    all_notices["name"].Add("all");
                    setNoticeButtons(webRequest, all_notices);
                }
                else if (uri == "http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=335")
                {
                    ba_notices["name"].Add("ba");
                    setNoticeButtons(webRequest, ba_notices);
                }
                else if (uri == "http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=674")
                {
                    ie_notices["name"].Add("ie");
                    setNoticeButtons(webRequest, ie_notices);
                }
                else if (uri == "http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=337")
                {
                    ic_notices["name"].Add("ic");
                    setNoticeButtons(webRequest, ic_notices);
                }
                else if (uri == "http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=339")
                {
                    cr_notices["name"].Add("cr");
                    setNoticeButtons(webRequest, cr_notices);
                }
            }
        }
    }

    // Update is called once per frame
    // void Update(){}
}