using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class YumiMediationSDK_Unity 
{
	public enum YumiMediationBannerPosition{
		YumiMediationBannerPositionTop,
		YumiMediationBannerPositionBottom
	}

	public enum YumiMediationAdViewBannerSize
	{
		/// iPhone and iPod Touch ad size. Typically 320x50.
		kYumiMediationAdViewBanner320x50 = 1 << 0,
		// Leaderboard size for the iPad. Typically 728x90.
		kYumiMediationAdViewBanner728x90 = 1 << 1,
		// Represents the fixed banner ad size - 300pt by 250pt.
		kYumiMediationAdViewBanner300x250 = 1 << 2
	}

	// banner
	[DllImport ("__Internal")]
	private static extern void _initYumiMediationBanner(string placementID,string channelID,string versionID,YumiMediationBannerPosition position);
	[DllImport ("__Internal")]
	private static  extern void _loadAd(bool isSmartBanner);
	[DllImport ("__Internal")]
	private static  extern void _hiddenYumiMediationBanner(bool ishidden);
	[DllImport ("__Internal")]
	private static  extern void _removeBanner();

	[DllImport ("__Internal")]
	private static  extern string _fetchBannerAdSize();
	[DllImport ("__Internal")]
	private static  extern void _setBannerAdSize(YumiMediationAdViewBannerSize bannerSize);

	// interstital
	[DllImport ("__Internal")]
	private static extern void _initYumiMediationInterstitial(string placementID,string channelID,string versionID);
	[DllImport ("__Internal")]
	private static extern bool _isInterstitialReady();
	[DllImport ("__Internal")]
	private static extern void _present();

	//video
	[DllImport ("__Internal")]
	private static extern void _loadYumiMediationVideo(string placementID,string channelID,string versionID);
	[DllImport ("__Internal")]
	private static extern bool _isVideoReady();
	[DllImport ("__Internal")]
	private static extern void _playVideo();

	//splash
	[DllImport ("__Internal")]
	private static extern void _showYumiAdsSplash(string placementID ,string appKey);

	//banner
	public static void initYumiMediationBanner(string placementID,string channelID,string versionID,YumiMediationBannerPosition position)
	{
		_initYumiMediationBanner (placementID, channelID, versionID, position);
	}

	public static void loadAd(bool isSmartBanner){
		_loadAd (isSmartBanner);
	}

	public static void hiddenYumiMediationBanner(bool ishidden)
	{
		_hiddenYumiMediationBanner (ishidden);
	}

	public static void removeBanner()
	{
		_removeBanner ();
	}

	public static string fetchBannerAdSize()
	{
		return _fetchBannerAdSize ();
	}

	public static void setBannerAdSize(YumiMediationAdViewBannerSize bannerSize)
	{
		_setBannerAdSize (bannerSize);
	}

	//interstitial
	public static void initYumiMediationInterstitial(string placementID,string channelID,string versionID)
	{
		_initYumiMediationInterstitial (placementID,channelID,versionID);
	}

	public static bool isInterstitialReady()
	{
		return _isInterstitialReady ();
	}

	public static void present()
	{
		_present ();
	}

	//video
	public static void loadYumiMediationVideo(string placementID,string channelID,string versionID)
	{
		_loadYumiMediationVideo (placementID,channelID,versionID);
	}
		
	public static bool isVideoReady() 
	{
		return _isVideoReady ();
	}
		
	public static void playVideo()
	{
		_playVideo ();
	}

	// splash 
	public static void showYumiAdsSplash(string placementID,string appKey)
	{
		_showYumiAdsSplash (placementID,appKey);
	}
		
}



