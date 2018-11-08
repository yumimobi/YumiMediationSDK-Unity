using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class YumiMediationSDKManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// bannerEvent
	public static event Action yumiMediationBannerViewDidLoadEvent;
	public static event Action <string>yumiMediationSDKDidFailToReceiveAdEvent;
	public static event Action yumiMediationBannerViewDidClickEvent;

	// InterstitalEvent
	public static event Action yumiMediationInterstitialDidReceiveAdEvent;
	public static event Action <string>yumiMediationInterstitialDidFailToReceiveAdEvent;
	public static event Action yumiMediationInterstitialWillDismissScreenEvent;
	public static event Action yumiMediationInterstitialDidClickEvent;

	// videoEvent
	public static event Action yumiMediationVideoDidOpenEvent;
	public static event Action yumiMediationVideoDidStartPlayingEvent;
	public static event Action yumiMediationVideoDidCloseEvent;
	public static event Action yumiMediationVideoDidRewardEvent;

	//splash
	public static event Action yumiAdsSplashDidLoadEvent;
	public static event Action <string>yumiAdsSplashDidFailToLoadEvent;
	public static event Action yumiAdsSplashDidClickEvent;
	public static event Action yumiAdsSplashDidClosedEvent;
	public static event Func<Image> yumiAdsSplashDefaultImageEvent;

	void Awake()
	{
		// Set the GameObject name to the class name for easy access from Obj-C
		gameObject.name = this.GetType().ToString();
	}

	//banner delegate
	public void yumiMediationBannerViewDidLoad()
	{
		if( yumiMediationBannerViewDidLoadEvent != null )
			yumiMediationBannerViewDidLoadEvent();
	}

	public void yumiMediationSDKDidFailToReceiveAd(string error)
	{
		if(yumiMediationSDKDidFailToReceiveAdEvent != null){
			yumiMediationSDKDidFailToReceiveAdEvent(error);
		}
	}

	public void yumiMediationBannerViewDidClick()
	{
		if(yumiMediationBannerViewDidClickEvent != null){
			yumiMediationBannerViewDidClickEvent();
		}
	}


	// interstital delegate
	public void yumiMediationInterstitialDidReceiveAd()
	{
		if(yumiMediationInterstitialDidReceiveAdEvent != null){
			yumiMediationInterstitialDidReceiveAdEvent();
		}
	}

	public void yumiMediationInterstitialDidFailToReceiveAd(string error)
	{
		if(yumiMediationInterstitialDidFailToReceiveAdEvent != null){
			yumiMediationInterstitialDidFailToReceiveAdEvent(error);
		}
	}

	public void yumiMediationInterstitialWillDismissScreen()
	{
		if(yumiMediationInterstitialWillDismissScreenEvent != null){
			yumiMediationInterstitialWillDismissScreenEvent();
		}
	}

	public void yumiMediationInterstitialDidClick(){
		if(yumiMediationInterstitialDidClickEvent != null){
			yumiMediationInterstitialDidClickEvent();
		}
	}

	// video delegate
	public void yumiMediationVideoDidOpen()
	{
		if(yumiMediationVideoDidOpenEvent != null){
			yumiMediationVideoDidOpenEvent();
		}
	}

	public void yumiMediationVideoDidStartPlaying()
	{
		if(yumiMediationVideoDidStartPlayingEvent != null){
			yumiMediationVideoDidStartPlayingEvent();
		}
	}

	public void yumiMediationVideoDidClose()
	{
		if(yumiMediationVideoDidCloseEvent!= null){
			yumiMediationVideoDidCloseEvent ();
		}
	}

	public void yumiMediationVideoDidReward()
	{
		if(yumiMediationVideoDidRewardEvent != null){
			yumiMediationVideoDidRewardEvent();
		}
	}

	// splash
	public void yumiAdsSplashDidLoad()
	{
		if (yumiAdsSplashDidLoadEvent != null) 
		{
			yumiAdsSplashDidLoadEvent ();
		}
	}
	public void yumiAdsSplashDidFailToLoad(string error)
	{
		if (yumiAdsSplashDidFailToLoadEvent != null) 
		{
			yumiAdsSplashDidFailToLoadEvent (error);
		}
	}
	public void yumiAdsSplashDidClick()
	{
		if (yumiAdsSplashDidClickEvent != null) {
			yumiAdsSplashDidClickEvent ();
		}
	}
	public void yumiAdsSplashDidClosed()
	{
		if (yumiAdsSplashDidClosedEvent != null) {
			yumiAdsSplashDidClosedEvent ();
		}
	}
	public Image yumiAdsSplashDefaultImage()
	{
		if (yumiAdsSplashDefaultImageEvent != null) {
			return yumiAdsSplashDefaultImageEvent ();
		}
		return null;
	}

}
