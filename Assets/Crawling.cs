using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Crawling : MonoBehaviour
{
    bool isStart = true;
    int debug = 0;
    const int MAX_NOTICE_LENGTH = 10;
    Dictionary<string, List<string>> notices = new Dictionary<string, List<string>>() {
        { "index", new List<string>() },
        { "date", new List<string>() },
        { "href", new List<string>() }
    };

    public void op()
    {
        debug++;
        if (debug == MAX_NOTICE_LENGTH) debug = 0;
        Application.OpenURL(notices["href"][0]);
    }

    void FindElements(UnityWebRequest webRequest, string keyword)
    {
        var handler = webRequest.downloadHandler;
        var htmlStr = handler.text;
         
        // Initialize Start Index
        int start = htmlStr.IndexOf("tbody", 0);
        start = htmlStr.IndexOf(keyword, start);
        for (var index = 0; start != -1 && index <= 10; index++)
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
    }

    void setNoticeButtons(UnityWebRequest webRequest)
    {
        FindElements(webRequest, "index");
        FindElements(webRequest, "date");
        FindElements(webRequest, "href");
    }

    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=333"));

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
                setNoticeButtons(webRequest);
                GameObject.Find("Debug").GetComponent<Text>().text = webRequest.downloadHandler.text;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
