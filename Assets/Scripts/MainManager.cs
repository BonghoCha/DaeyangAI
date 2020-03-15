using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
	public GameObject[] m_pages;
	public Image[] m_navigation_icons;
	public Sprite[] m_icon_on;
	public Sprite[] m_icon_off;

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

	private void Start()
	{

	}

    private void Update()
	{

	}
}

