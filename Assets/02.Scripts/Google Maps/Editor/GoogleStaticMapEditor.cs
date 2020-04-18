using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GoogleStaticMap))]
public class GoogleStaticMapEditor : Editor
{
	public override void OnInspectorGUI()
	{
		GoogleStaticMap googleStaticMap = (GoogleStaticMap)target;

		DrawDefaultInspector();

		string helpMessage = "The Google Static Maps API lets you embed a Google Maps image on your web page without requiring JavaScript or any dynamic page loading. The Google Static Maps API service creates your map based on URL parameters sent through a standard HTTP request and returns the map as an image you can display on your web page.";
		EditorGUILayout.HelpBox(helpMessage, MessageType.Info);

		if (GUILayout.Button("Get a API key"))
		{
			Application.OpenURL("https://developers.google.com/maps/documentation/static-maps/");
		}

		if (GUILayout.Button("Visit Google Maps API - Google Developers"))
		{
			Application.OpenURL("https://developers.google.com/maps/");
		}

		if (GUILayout.Button("Visit API Credentials - Google Cloud Console"))
		{
			Application.OpenURL("https://console.cloud.google.com/apis/credentials");
		}
	}
}
