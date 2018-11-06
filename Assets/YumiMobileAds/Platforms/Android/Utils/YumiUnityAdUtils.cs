using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    /// <summary>
    /// Android YumiAd plugin Utils 
    /// </summary>
public class YumiUnityAdUtils
{
    private AndroidJavaObject ajo;
    //Yumi AdID
    private string banner_slotID = "";
    private string interstitial_slotID = "";
    private string media_slotID = "";
    // setChannelID . (Recommend)
    private string channelStr = "";
    // setVersionName . (Recommend)
    private string versionStr = "";

    public YumiUnityAdUtils()
    {
    }

    /// <summary>
    /// Android YumiAd plugin Utils Constructor
    /// </summary>
    /// <param name="adID">Yumi AdID</param>
    /// <param name="channelStr">setChannelID . (Recommend)</param>
    /// <param name="versionStr">setVersionName . (Recommend)</param>
    public YumiUnityAdUtils(string banner_slotID, string interstitial_slotID, string media_slotID, string channelStr, string versionStr)
    {
        this.banner_slotID = banner_slotID;
        this.interstitial_slotID = interstitial_slotID;
        this.media_slotID = media_slotID;
        this.channelStr = channelStr;
        this.versionStr = versionStr;
        using (AndroidJavaClass ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            ajo = ajc.GetStatic<AndroidJavaObject>("currentActivity");
        }

    }

    #region Banner method


    /// <summary> 
    // Show Banner
    /// </summary>
    /// <param name="callbackName">GameObject Name</param>
    /// <param name="isMatchWindowWidth">banner Width Match Window</param>
    public void AddBannerAd(string callbackName = "YumiAdHelp", Boolean isMatchWindowWidth = false)
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils AddBannerAd error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("addBannerAd", ajo, banner_slotID, channelStr, versionStr, isMatchWindowWidth, callbackName);
        }
    }

    /// <summary>
    /// Resume Banner
    /// </summary>
    public void ResumeBanner()
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils ResumeBanner error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("resumeBanner", ajo);
        }
    }

    /// <summary>
    /// Dismiss Banner
    /// </summary>
    public void DismissBanner()
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils DismissBanner error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("dismissBanner", ajo);
        }
    }
    #endregion

    #region initInterstitialAd method

    /// <summary> 
    // Init Interstitial
    /// </summary>
    /// <param name="callbackName">GameObject Name</param>
    public void InitInterstitialAd(string callbackName = "YumiAdHelp")
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils InitInterstitialAd error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("initInterstitialAd", ajo, interstitial_slotID, channelStr, versionStr, callbackName);
        }
    }

    /// <summary>
    /// Request Interstitial
    /// </summary>
    public void RequestInterstitial()
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils RequestInterstitial error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("requestInterstitial", ajo);
        }
    }

    /// <summary>
    /// Show Interstitial
    /// </summary>
    public void ShowInterstitialAd()
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils ShowInterstitialAd error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("showInterstitialAd", ajo);
        }
    }
    #endregion


    #region MediaAd method

    /// <summary> 
    // Init Media
    /// </summary>
    /// <param name="callbackName">GameObject Name</param>
    public void InitMedia(string callbackName = "YumiAdHelp")
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils InitMedia error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("InitMedia", ajo, media_slotID, channelStr, versionStr, callbackName);
        }
    }

    /// <summary>
    /// Request Media
    /// </summary>
    public void RequestMedia()
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils RequestMedia error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("requestMedia", ajo);
        }
    }


    /// <summary>
    /// Media IsMediaPrepared
    /// </summary>
    public bool IsMediaPrepared()
    {       
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
           return jc.CallStatic<bool>("IsMediaPrepared");
        }
    }

    /// <summary>
    /// Show Media
    /// </summary>
    public void ShowMedia()
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils ShowMedia error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("showMedia", ajo);
        }
    }
    #endregion

    /// <summary>
    /// Start Debugging Activity
    /// </summary>
    public void StartDebugging()
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils StartDebugging error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("startDebugging", ajo, banner_slotID, interstitial_slotID, media_slotID, channelStr, versionStr);
        }
    }

    /// <summary>
    /// show android log
    /// </summary>
    /// <param name="data"></param>
    public void ShowLog(string data)
    {
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("showLog", "UnityAd", data);
        }
    }


    /// <summary>
    /// show android Toast(Bounced prompt)
    /// </summary>
    /// <param name="data"></param>
    public void ShowToast(string data)
    {
        if (null == ajo)
        {
            UnityEngine.Debug.LogError("UnityAdUtils ShowToast error : AndroidJavaObject ajo is null");
            return;
        }
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("showToast", ajo, data);
        }
    }

    /// <summary>
    /// Remind users to get permission
    /// </summary>
    /// <param name="data"></param>
    public void CheckPermission()
    {
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("CheckPermission");
        }
    }

    /// <summary>
    ///Setting The APP Is for GooglePlay released
    /// </summary>
    /// <param name="data"></param>
    public void SetAppIsGooglePlayVersions()
    {
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("setAppIsGooglePlayVersions");
        }
    }

    #region Callback interface
    /// <summary>
    /// BannerAd Callback interface
    /// </summary>
    public interface BannerAdCallbackListener
    {
        void onBannerPreparedFailed(string data);
        void onBannerPrepared(string data);
        void onBannerExposure(string data);
        void onBannerClosed(string data);
        void onBannerClicked(string data);
    }

    /// <summary>
    /// InterstitialAd Callback interface
    /// </summary>
    public interface InterstitialAdCallbackListener
    {
        void onInterstitialPreparedFailed(string data);
        void onInterstitialPrepared(string data);
        void onInterstitialExposure(string data);
        void onInterstitialClosed(string data);
        void onInterstitialClicked(string data);
    }

    /// <summary>
    /// MediaAd Callback interface
    /// </summary>
    public interface MediaAdCallbackListener
    {
        void onMediaIncentived(string data);
        void onMediaExposure(string data);
        void onMediaClosed(string data);
        void onMediaClicked(string data);
    }
    #endregion


    #region  life cycle method
    public void AdDestroy()
    {
        using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
        {
            jc.CallStatic("AdDestroy");
        }
    }
    #endregion
}

