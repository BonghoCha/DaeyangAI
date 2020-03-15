using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
	public GameObject[] m_page_icons;

	public void ChangePage(int current)
	{
		for (var i = 0; i < m_page_icons.Length; i++)
		{
			m_page_icons[i].SetVisible(false);
		}
		m_page_icons[current].SetVisible(true);
	}

	private void Start()
	{

	}

    private void Update()
	{

	}
}

