using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Crawling : MonoBehaviour
{
    bool isStart = true;
    Notice[] notices;
    int debug = 0;

    struct Notice
    {
        public string title;
        public string date;
        public string url;
    }

    public void op()
    {
        debug++;
        if (debug == 10) debug = 0;
        Application.OpenURL(notices[debug].url);
        Debug.Log(notices[debug].url);
    }

    void FindElements(UnityWebRequest webRequest, string keyword)
    {
        var handler = webRequest.downloadHandler;

        var index = 0;
         
        // Initial Start Index
        int start = handler.text.IndexOf("tbody", 0);
        start = handler.text.IndexOf(keyword, start);

        while (start != -1)
        {
            switch (keyword){
                case "date" : {
                    start = handler.text.IndexOf(">", start) + 1;
                    notices[index++].date = handler.text.Substring(start, handler.text.IndexOf("<", start) - start);
                    Debug.Log(notices[index].date + " ,"     +index);
                    break;
                }
                case "title":
                    {
                        start = handler.text.IndexOf(">", start) + 1;
                        notices[index++].date = handler.text.Substring(start, handler.text.IndexOf("<", start) - start);
                        Debug.Log(notices[index].date + " ," + index);
                        break;
                    }
                case "href":
                {
                    start = handler.text.IndexOf("board", start);
                    notices[index].url = "http://board.sejong.ac.kr/" + handler.text.Substring(start, (handler.text.IndexOf(">", start)-1) - start);
                    notices[index].url = notices[index].url.Replace('^', '&');

                    index++;
                    if (index == 10) return;
                    break;
                }
            }

            start = handler.text.IndexOf(keyword, start);
        }
    }
    void Start()
    {
        // A correct website page.
        StartCoroutine(GetRequest("http://board.sejong.ac.kr/boardlist.do?bbsConfigFK=333"));

        // A non-existing page.
        StartCoroutine(GetRequest("https://error.html"));

        notices = new Notice[20];
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

                FindElements(webRequest, "date");
                FindElements(webRequest, "href");
                for (var i = 0; i < 10; i++)
                {
                    Debug.Log(notices[i].date + "/" + notices[i].url);
                }
                GameObject.Find("Debug").GetComponent<Text>().text = webRequest.downloadHandler.text;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
