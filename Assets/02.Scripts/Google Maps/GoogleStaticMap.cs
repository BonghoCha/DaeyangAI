using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleStaticMap : MonoBehaviour
{
	public RawImage rawImage;
	[Range(0f, 1f)]
	public float transparency = 1f;
	public float mapCenterLatitude = 37.5665350f;
	public float mapCenterLongtitude = 126.9779690f;
	[Range(1, 20)]
	public int mapZoom = 14;
	public int mapWidth = 640;
	public int mapHeight = 640;
	public enum MapType
	{
		roadmap, satellite, terrain, hybrid,
	}
	public MapType mapType = MapType.roadmap;
	[Range(1, 4)]
	public int scale = 1;
	public float markerLatitude = 37.5665350f;
	public float markerLongtitude = 126.9779690f;
	public enum MarkerSize
	{
		tiny, mid, small,
	}
	public MarkerSize markerSize = MarkerSize.mid;
	public enum MarkerColor
	{
		black, brown, green, purple, yellow, blue, gray, orange, red, white,
	}
	public MarkerColor markerColor = MarkerColor.blue;
	public char label = 'C';
	public string apiKey;

	private string url;
	private Color rawImageColor = Color.white;

	IEnumerator Map()
	{
		rawImageColor.a = transparency;
		rawImage.color = rawImageColor;

		label = Char.ToUpper(label);

		url = "https://maps.googleapis.com/maps/api/staticmap"
			+ "?center=" + mapCenterLatitude + "," + mapCenterLongtitude
			+ "&zoom=" + mapZoom
			+ "&size=" + mapWidth + "x" + mapHeight
			+ "&scale=" + scale
			+ "&maptype=" + mapType
			+ "&markers=size:" + markerSize + "%7Ccolor:" + markerColor + "%7Clabel:" + label + "%7C" + markerLatitude + "," + markerLongtitude
			+ "&key=" + apiKey;
		WWW www = new WWW(url);
		yield return www;
		rawImage.texture = www.texture;
		rawImage.SetNativeSize();
	}

	public void RefreshMap()
	{
		if (gameObject.activeInHierarchy)
		{
			StartCoroutine(Map());
		}
	}

	private void Reset()
	{
		rawImage = gameObject.GetComponentInChildren<RawImage>();
		RefreshMap();
	}

	private void Start()
	{
		Reset();
	}

#if UNITY_EDITOR
	private void OnValidate()
	{
		RefreshMap();
	}
#endif
}
