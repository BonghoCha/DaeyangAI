using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
			m_pages[i].SetActive(false);
		}
		m_navigation_icons[current].sprite = m_icon_on[current];
		m_pages[current].SetActive(true);
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

    private void Start()
	{

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
	}
}

