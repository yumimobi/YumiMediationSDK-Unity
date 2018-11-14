using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class YumiMediationSDKEventListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	
	
		void Update () {
	
	}
	
	void OnEnable()
	{
		// banner
		YumiMediationSDKManager.yumiMediationBannerViewDidLoadEvent += yumiMediationBannerViewDidLoadEvent;
		YumiMediationSDKManager.yumiMediationSDKDidFailToReceiveAdEvent += yumiMediationSDKDidFailToReceiveAdEvent;
		YumiMediationSDKManager.yumiMediationBannerViewDidClickEvent += yumiMediationBannerViewDidClickEvent;

		// interstital
		YumiMediationSDKManager.yumiMediationInterstitialDidReceiveAdEvent += yumiMediationInterstitialDidReceiveAdEvent;
		YumiMediationSDKManager.yumiMediationInterstitialDidFailToReceiveAdEvent += yumiMediationInterstitialDidFailToReceiveAdEvent;
		YumiMediationSDKManager.yumiMediationInterstitialWillDismissScreenEvent += yumiMediationInterstitialWillDismissScreenEvent;
		YumiMediationSDKManager.yumiMediationInterstitialDidClickEvent += yumiMediationInterstitialDidClickEvent;

		// video
		YumiMediationSDKManager.yumiMediationVideoDidOpenEvent += yumiMediationVideoDidOpenEvent;
		YumiMediationSDKManager.yumiMediationVideoDidStartPlayingEvent += yumiMediationVideoDidStartPlayingEvent;
		YumiMediationSDKManager.yumiMediationVideoDidCloseEvent += yumiMediationVideoDidCloseEvent;
		YumiMediationSDKManager.yumiMediationVideoDidRewardEvent += yumiMediationVideoDidRewardEvent;

		//splash
		YumiMediationSDKManager.yumiAdsSplashDidLoadEvent += yumiAdsSplashDidLoadEvent;
		YumiMediationSDKManager.yumiAdsSplashDidFailToLoadEvent += yumiAdsSplashDidFailToLoadEvent;
		YumiMediationSDKManager.yumiAdsSplashDidClickEvent += yumiAdsSplashDidClickEvent;
		YumiMediationSDKManager.yumiAdsSplashDidClosedEvent += yumiAdsSplashDidClosedEvent;

	}

	void OnDisable()
	{
		// Remove banner
		YumiMediationSDKManager.yumiMediationBannerViewDidLoadEvent -= yumiMediationBannerViewDidLoadEvent;
		YumiMediationSDKManager.yumiMediationSDKDidFailToReceiveAdEvent -= yumiMediationSDKDidFailToReceiveAdEvent;
		YumiMediationSDKManager.yumiMediationBannerViewDidClickEvent -= yumiMediationBannerViewDidClickEvent;

		// Remove interstital
		YumiMediationSDKManager.yumiMediationInterstitialDidReceiveAdEvent -= yumiMediationInterstitialDidReceiveAdEvent;
		YumiMediationSDKManager.yumiMediationInterstitialDidFailToReceiveAdEvent -= yumiMediationInterstitialDidFailToReceiveAdEvent;
		YumiMediationSDKManager.yumiMediationInterstitialWillDismissScreenEvent -= yumiMediationInterstitialWillDismissScreenEvent;
		YumiMediationSDKManager.yumiMediationInterstitialDidClickEvent -= yumiMediationInterstitialDidClickEvent;

		// Remove video
		YumiMediationSDKManager.yumiMediationVideoDidOpenEvent -= yumiMediationVideoDidOpenEvent;
		YumiMediationSDKManager.yumiMediationVideoDidStartPlayingEvent -= yumiMediationVideoDidStartPlayingEvent;
		YumiMediationSDKManager.yumiMediationVideoDidCloseEvent += yumiMediationVideoDidCloseEvent;
		YumiMediationSDKManager.yumiMediationVideoDidRewardEvent += yumiMediationVideoDidRewardEvent;
		//splash
		YumiMediationSDKManager.yumiAdsSplashDidLoadEvent -= yumiAdsSplashDidLoadEvent;
		YumiMediationSDKManager.yumiAdsSplashDidFailToLoadEvent -= yumiAdsSplashDidFailToLoadEvent;
		YumiMediationSDKManager.yumiAdsSplashDidClickEvent -= yumiAdsSplashDidClickEvent;
		YumiMediationSDKManager.yumiAdsSplashDidClosedEvent -= yumiAdsSplashDidClosedEvent;

	}

	// banner
	void yumiMediationBannerViewDidLoadEvent()
	{
		Debug.Log("YumiMediationSDKBanner,didLoaded");
	}
	void yumiMediationSDKDidFailToReceiveAdEvent(string error)
	{
		Debug.Log("YumiMediationSDKBanner,didFailToReceiveAd");
	}
	void yumiMediationBannerViewDidClickEvent()
	{
		Debug.Log("YumiMediationSDKBanner,didClickedAd");
	}


	// interstital
	void yumiMediationInterstitialDidReceiveAdEvent(){
		Debug.Log ("YumiMediationInterstital, DidReceiveAd");
	}
	void yumiMediationInterstitialDidFailToReceiveAdEvent(string error){
		Debug.Log ("YumiMediationInterstital, DidFailToReceiveAd");
	}
	void yumiMediationInterstitialWillDismissScreenEvent(){
		Debug.Log ("YumiMediationInterstital, WillDismissScreen");
	}
	void yumiMediationInterstitialDidClickEvent() {
		Debug.Log ("YumiMediationInterstital, DidClicked");
	}

	//video
	void yumiMediationVideoDidOpenEvent(){
		Debug.Log ("YumiMediationVideo, DidOpen");
	}
	void yumiMediationVideoDidStartPlayingEvent(){
		Debug.Log ("YumiMediationVideo, DidStartPlaying");
	}
	void yumiMediationVideoDidCloseEvent(){
		Debug.Log ("YumiMediationVideo, DidClosed");
	}
	void yumiMediationVideoDidRewardEvent(){
		Debug.Log ("YumiMediationVideo, DidRewarded");
	}

	//splash
	public void yumiAdsSplashDidLoadEvent()
	{
		Debug.Log ("YumiAdsSplash,DidLoad");
	}
	public void yumiAdsSplashDidFailToLoadEvent(string error)
	{
		string errorString = string.Format ("YumiAdsSplashDidFailToLoad {0}" ,error);
		Debug.Log (errorString);
	}
	public void yumiAdsSplashDidClickEvent()
	{
		Debug.Log ("YumiAdsSplash ,DidClick");
	}
	public void yumiAdsSplashDidClosedEvent()
	{
		Debug.Log ("yumiAdsSplash ,DidClosed");
	}
	public Image yumiAdsSplashDefaultImageEvent()
	{
		Debug.Log ("yumiAdsSplash ,set default image");
//		Image newImage = Image.FromFile("Splash.png");
		return null;
	}

}
