using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Daeyang_Manager : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    RectTransform rectTransform;
    Vector3 Initial_pos;
    public Sprite Initial_face;
    bool check;

    public void OnPointerDown(PointerEventData eventData)
    {
        check = true;
        GameObject.Find("MainManager").GetComponent<MainManager>().SetRandomFace();
        Debug.Log("클릭");
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        Initial_pos = rectTransform.anchoredPosition;

        check = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!check)
        {
            rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(rectTransform.anchoredPosition.x, Initial_pos.x, 1), Mathf.Lerp(rectTransform.anchoredPosition.y, Initial_pos.y, 1));
        }
        */
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //check = false;
        rectTransform.anchoredPosition = Initial_pos;
        GetComponent<Image>().sprite = Initial_face;
        Debug.Log("끝");
    }
}
