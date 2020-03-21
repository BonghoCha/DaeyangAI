using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformLoginSystem : MonoBehaviour
{
    WebViewObject m_webViewObject = null;
    public GameObject m_blind;

    public GameObject[] m_outpopup_buttons;
    public GameObject[] m_inpopup_buttons;
    string m_url;
    int m_current_depth = 0;

    public virtual void OpenWebView(string web, int left, int top, int right, int bottom)
    {
        string strUrl = string.Format("{0}"
         , web);

        Debug.Log(strUrl);
        //Application.OpenURL(strUrl);

        // 오브젝트를 생성하고 멤버 변수로 등록한다. ( 쉽게 제거하기 위해서 )
        // WebViewObject 컴포넌트 등록 
        m_webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
        m_webViewObject.gameObject.name = "WebView";
        m_webViewObject.transform.parent = GameObject.Find("Notice_Canvas").transform;
        // 반드시 Init 호출 파라미터 : 웹페이지 
        // Html 형태를 등록 할 경우 페이지에 연결된 다른 페이지로 이동하지 않는 버그 있었음
        m_webViewObject.Init((msg) =>
        {
            Debug.Log(string.Format("CallFromJS[{0}]", msg));
        });

        // 마진값 : 화면으로부터 좌상우하 여백 값 
        m_webViewObject.SetMargins(left, top, right, bottom);
        m_webViewObject.LoadURL(strUrl);
        m_webViewObject.SetVisibility(true);
    }

    // 생성된 웹뷰 삭제
    public virtual void CloseWebView()
    {
        m_blind.SetActive(false);
        m_url = "";
        GameObject.Destroy(m_webViewObject);
    }

    public void OpenURL(string url)
    {
        m_blind.SetActive(true);
        m_url = url;
        OpenWebView(url, 90, 790, 200, 313);

        string name = EventSystem.current.currentSelectedGameObject.name;
        for (var i = 0; i < m_inpopup_buttons.Length; i++)
        {
            m_inpopup_buttons[i].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
            if (name.Equals(m_inpopup_buttons[i].name))
            {
                m_outpopup_buttons[i].transform.GetChild(0).gameObject.SetActive(false);
                m_inpopup_buttons[i].transform.GetChild(0).gameObject.SetActive(false);

                m_inpopup_buttons[i].GetComponent<TextMeshProUGUI>().color = new Color32(72+15, 126+15, 255, 255);
            }
        }
    }

    public void Change(string url)
    {
        // New Url
        string strUrl = string.Format("{0}", url);

        m_webViewObject.LoadURL(strUrl);

        string name = EventSystem.current.currentSelectedGameObject.name;
        for (var i = 0; i < m_inpopup_buttons.Length; i++)
        {
            m_inpopup_buttons[i].GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
            if (name.Equals(m_inpopup_buttons[i].name))
            {
                m_outpopup_buttons[i].transform.GetChild(0).gameObject.SetActive(false);
                m_inpopup_buttons[i].transform.GetChild(0).gameObject.SetActive(false);

                m_inpopup_buttons[i].GetComponent<TextMeshProUGUI>().color = new Color32(72+15, 126+15, 255, 255);
            }
        }
    }

    public void PrevPage()
    {
        m_webViewObject.GoBack();
    }

    public void NextPage()
    {
        m_webViewObject.GoForward();
    }

    void Start()
    {
    }
}