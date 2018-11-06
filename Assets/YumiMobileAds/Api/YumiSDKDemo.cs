using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class YumiSDKDemo : MonoBehaviour {

	void Start () {
		// Whether to display log log
		Logger.SetDebug(true);
	}

	void OnGUI() {

		// Create style for a button
		GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
		myButtonStyle.fontSize = 25;

		// Load and set Font
		Font myFont = (Font)Resources.Load("Fonts/comic", typeof(Font));
		myButtonStyle.font = myFont;

		// Set color for selected and unselected buttons
		myButtonStyle.normal.textColor = Color.white;
		myButtonStyle.hover.textColor = Color.white;

		//Yumi banner
		int btnWidth = (Screen.width-40*2-10)/2;

		if(GUI.Button(new Rect(40,84,btnWidth,120),"show banner",myButtonStyle))
		{
			YumiSDKAdapter.Instance.ShowBanner (false);

		}  
		//remove banner
		if(GUI.Button(new Rect(40+btnWidth+10,84,btnWidth,120),"reomve banner",myButtonStyle))
		{
			// iOS get banner size for developer layout
			#if UNITY_IPHONE && !UNITY_EDITOR

			string bannerSize = YumiMediationSDK_Unity.fetchBannerAdSize ();
			string[] sizeArray = bannerSize.Split ('_');
			string width = "0.0";
			string height = "0.0";
			if(sizeArray.Length == 2)
			{
			width = sizeArray[0];
			height = sizeArray[1];
			}

			string sizeString = string.Format ("width = {0:N2} , height = {1:N2}",Convert.ToDouble(width),Convert.ToDouble(height));

			Debug.Log (sizeString);

			#endif

			YumiSDKAdapter.Instance.DismissBanner ();

		}  
			
		//Yumi interstital
		if(GUI.Button(new Rect(40,214,btnWidth,120),"init interstital",myButtonStyle))
		{
			
			YumiSDKAdapter.Instance.InitInterstitial ();

		}

		if(GUI.Button(new Rect(40+btnWidth+10,214,btnWidth,120),"present interstital",myButtonStyle))
		{
			
			YumiSDKAdapter.Instance.PresentInterstitial ();
		}
			
		//Yumi video
		if(GUI.Button(new Rect(40,344,btnWidth,120),"init video",myButtonStyle))
		{
			YumiSDKAdapter.Instance.InitVideo ();
		}

		if(GUI.Button(new Rect(40+btnWidth+10,344,btnWidth,120),"play video",myButtonStyle))
		{
			YumiSDKAdapter.Instance.PlayVideo ();
		}
	}
}
