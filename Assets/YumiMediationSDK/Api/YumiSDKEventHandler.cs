using UnityEngine;
using System;

public class YumiSDKEventHandler{

    #region Banner ad  call back 

    public static void OnYumiAdBannerDidLoad(){
        Logger.Log("OnYumiAdBannerDidLoad");
    }
    public static void OnYumiAdBannerDidFailToLoad(string error){
        Logger.Log("OnYumiAdBannerDidFailToLoad" + error);
    }
     public static void OnYumiAdBannerDidClick(){
        Logger.Log("OnYumiAdBannerDidClick");
    }
    // android platforms had close and exposure
#if UNITY_ANDROID
     public static void OnYumiAdBannerDidClose(){
        Logger.Log("OnYumiAdBannerDidClose");
    }
    public static void OnYumiAdBannerExposure(){
        Logger.Log("OnYumiAdBannerExposure");
    }
#endif
    #endregion

    #region Interstitial ad  call back 

    public static void OnYumiAdInterstitialDidLoad(){
        Logger.Log("OnYumiAdInterstitialDidLoad");
    }
    public static void OnYumiAdInterstitialDidFailToLoad(string error){
        Logger.Log("OnYumiAdInterstitialDidFailToLoad" + error);
    }
    public static void OnYumiAdInterstitialDidClick(){
        Logger.Log("OnYumiAdInterstitialDidClick");
    }
    public static void OnYumiAdInterstitialDidClose(){
        Logger.Log("OnYumiAdInterstitialDidClose");
    }
    // android platforms had exposure
    #if UNITY_ANDROID
    
    public static void OnYumiAdInterstitialExposure(){
        Logger.Log("OnYumiAdInterstitialExposure");
    }
    #endif
    #endregion

    #region video ad  call back 

    public static void OnYumiAdVideoDidOpen(){
        Logger.Log("OnYumiAdVideoDidOpen");
    }
    public static void OnYumiAdVideoDidStartPlaying(){
        Logger.Log("OnYumiAdVideoDidStartPlaying");
    }
    public static void OnYumiAdVideoDidClose(){
        Logger.Log("OnYumiAdVideoDidClose");
    }
    public static void OnYumiAdVideoDidReward(){
        Logger.Log("OnYumiAdVideoDidReward");
    }
    // android platforms had click
    #if UNITY_ANDROID
    
    public static void OnYumiAdVideoDidClick(){
        Logger.Log("OnYumiAdVideoDidClick");
    }
    #endif
    #endregion
}