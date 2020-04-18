using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleApi : MonoBehaviour
{
    public RawImage img;

    string url;

    public float lat;
    public float lon;

    LocationInfo li;

    public int zoom = 20;
    public int mapWidth = 640;
    public int mapHeight = 640;

    public enum mapType { roadmap, satellite, hybrid, terrain }
    public mapType mapSelected;
    public int scale;

    public void SetLocation(int number)
    {
        if(number == 1)
        {
            lat = 37.548824f; 
            lon = 127.074574f;
        }
        else if (number == 2)
        {
            lat = 37.549108f;
            lon = 127.073766f;
        }
        else if (number == 3)
        {
            lat = 37.549449f;
            lon = 127.075064f;
        }
        else if (number == 4)
        {
            lat = 37.549555f;
            lon = 127.073678f;
        }
        else if (number == 5)
        {
            lat = 37.549938f;
            lon = 127.074579f;
        }
        else if (number == 6)
        {
            lat = 37.550019f;
            lon = 127.073283f;
        }
        else if (number == 7)
        {
            lat = 37.550507f;
            lon = 127.072948f;
        }
        else if (number == 8)
        {
            lat = 37.550806f;
            lon = 127.073910f;
        }
        else if (number == 9)
        {
            lat = 37.550890f;
            lon = 127.073236f;

        }
        else if (number == 10)
        {
            lat = 37.551299f;
            lon = 127.073333f;
        }
        else if (number == 11)
        {
            lat = 37.550996f;
            lon = 127.075792f;
        }
        else if (number == 12)
        {
            lat = 37.551479f;
            lon = 127.075213f;
        }
        else if (number == 13)
        {
            lat = 37.551845f;
            lon = 127.074685f;
        }
        else if (number == 14)
        {
            lat = 37.551631f;
            lon = 127.074323f;
        }
        else if (number == 15)
        {
            lat = 37.551821f;
            lon = 127.074029f;
        }
        else if (number == 16)
        {
            lat = 37.552255f;
            lon = 127.074014f;
        }
        else if (number == 17)
        {
            lat = 37.552442f;
            lon = 127.073421f;
        }
        else if (number == 18)
        {
            lat = 37.552545f;
            lon = 127.074157f;
        }
        StartCoroutine(Map());
    }
    public void PlusZoom()
    {
        zoom++;
        img = gameObject.GetComponent<RawImage>();
        StartCoroutine(Map());
    }
    public void MinusZoom()
    {
        zoom--;
        img = gameObject.GetComponent<RawImage>();
        StartCoroutine(Map());
    }
    public void SetLat(float dist)
    {
        lat += dist;
        img = gameObject.GetComponent<RawImage>();
        StartCoroutine(Map());
    }
    public void SetLon(float dist)
    {
        lon += dist;
        img = gameObject.GetComponent<RawImage>();
        StartCoroutine(Map());
    }

    IEnumerator Map()
    {
        url = "https://maps.googleapis.com/maps/api/staticmap?center=" + lat + "," + lon +
            "&zoom=" + zoom + "&size=" + mapWidth + "x" + mapHeight + "&scale=" + scale
            + "&maptype=" + mapSelected +
            "&markers=color:blue%7Clabel:S%7C40.702147,-74.015794&markers=color:green%7Clabel:G%7C40.711614,-74.012318&markers=color:red%7Clabel:C%7C40.718217,-73.998284&key=AIzaSyAOl0LVTXiuaSbC_qzmL1rJO5RWbYIrQAo";
        WWW www = new WWW(url);
        yield return www;
        img.texture = www.texture;
        img.SetNativeSize();

        img.GetComponent<RectTransform>().sizeDelta = new Vector2(1080, 1080);

    }
    // Use this for initialization
    void Start()
    {
        img = gameObject.GetComponent<RawImage>();
        StartCoroutine(Map());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
