using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainManager : MonoBehaviour
{
	public GameObject[] m_pages;
	public Image[] m_navigation_icons;
	public Sprite[] m_icon_on;
	public Sprite[] m_icon_off;

	public GameObject intro_canvas;
	public GameObject intro1;
	bool play_introbutton = true;
	float a = 0.01f;
	public GameObject intro2;

    public Sprite[] m_expression_images;
    public GameObject daeyang;

    public GameObject SOOJLE_in_img;
    public GameObject SOOJLE_in_text;

    public GameObject notice_canvas;
    public GameObject popup_canvas;
    float timeSpan;
    float checkTime;

    public void SetRandomFace()
    {
        var random = Random.Range(0, 8);
        daeyang.GetComponent<Image>().sprite = m_expression_images[random];
    }

    public void SetDaeyangFace(int num)
    {
        daeyang.GetComponent<Image>().sprite = m_expression_images[num];
    }

    public void ChangePage(int current)
	{
        for (var i = 0; i < m_navigation_icons.Length; i++)
        {
            m_navigation_icons[i].sprite = m_icon_off[i];
            if (i != 3)
            {
                m_pages[i].SetActive(false);
            }
        }
        m_navigation_icons[current].sprite = m_icon_on[current];

        if (current == 3)   // At SOOJLE page, set false
        {
            SOOJLE_in_img.SetActive(false);
            SOOJLE_in_text.SetActive(false);

            m_pages[3].GetComponent<Canvas>().sortingOrder = 4;
        }
        else   // At Other pages, set true
        {
            m_pages[current].SetActive(true);

            SOOJLE_in_img.SetActive(true);
            SOOJLE_in_text.SetActive(true);

            m_pages[3].GetComponent<Canvas>().sortingOrder = -1;
        }
    }

    public void Intro1()
    {
        play_introbutton = false;

        intro1.transform.parent.gameObject.SetActive(false);
        intro2.SetActive(true);
    }

    public void GoMain()
    {
        intro_canvas.SetActive(false);
        // Main
        ChangePage(2);
    }

    public void GoCamera()
    {
        intro_canvas.SetActive(false);
        // Cameara
        ChangePage(4);
    }

    public void GoIntro()
    {
        for (var i = 0; i < m_navigation_icons.Length; i++)
        {
            m_navigation_icons[i].sprite = m_icon_off[i];
            m_pages[i].SetActive(false);
        }

        GameObject.Find("Notice_Canvas").GetComponent<Canvas>().sortingOrder= -1;
        GameObject.Find("FAQ_Popup_Canvas").GetComponent<Canvas>().sortingOrder = -1;
        GameObject.Find("Soojle_Canvas").GetComponent<Canvas>().sortingOrder = -1;

        intro_canvas.SetActive(true);
        play_introbutton = true;
        intro1.transform.parent.gameObject.SetActive(true);
        intro2.SetActive(false);

    }

    private void Start()
	{
        timeSpan = 0.0f;
        checkTime = 180.0f;  // 특정시간을 180초로 지정


    }

    private void Update()
	{
        if (play_introbutton)
        {
			intro1.GetComponent<Image>().color = new Color(255, 255, 255, intro1.GetComponent<Image>().color.a + a);

            if (intro1.GetComponent<Image>().color.a >= 1 && a > 0)
            {
				a *= -1;
            }
            if (intro1.GetComponent<Image>().color.a < 0.3 && a < 0)
            {
				a *= -1;
            }
        }
        timeSpan += Time.deltaTime; // 경과 시간 등록
        if (Input.GetMouseButtonDown(0))
        {
            timeSpan = 0.0f;
        }
        if (timeSpan >= checkTime)
        {
            GoIntro();
        }
    }
}
