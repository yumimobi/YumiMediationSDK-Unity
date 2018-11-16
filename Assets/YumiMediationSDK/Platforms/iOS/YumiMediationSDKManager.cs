using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class YumiMediationSDKManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //// bannerEvent
    //public static event Action yumiMediationBannerViewDidLoadEvent;
    //public static event Action<string> yumiMediationSDKDidFailToReceiveAdEvent;
    //public static event Action yumiMediationBannerViewDidClickEvent;

    //// InterstitalEvent
    //public static event Action yumiMediationInterstitialDidReceiveAdEvent;
    //public static event Action<string> yumiMediationInterstitialDidFailToReceiveAdEvent;
    //public static event Action yumiMediationInterstitialWillDismissScreenEvent;
    //public static event Action yumiMediationInterstitialDidClickEvent;

    //// videoEvent
    //public static event Action yumiMediationVideoDidOpenEvent;
    //public static event Action yumiMediationVideoDidStartPlayingEvent;
    //public static event Action yumiMediationVideoDidCloseEvent;
    //public static event Action yumiMediationVideoDidRewardEvent;

    ////splash
    //public static event Action yumiAdsSplashDidLoadEvent;
    //public static event Action<string> yumiAdsSplashDidFailToLoadEvent;
    //public static event Action yumiAdsSplashDidClickEvent;
    //public static event Action yumiAdsSplashDidClosedEvent;
    //public static event Func<Image> yumiAdsSplashDefaultImageEvent;

    void Awake()
    {
        // Set the GameObject name to the class name for easy access from Obj-C
        gameObject.name = this.GetType().ToString();
    }

    //banner delegate
    public void yumiMediationBannerViewDidLoad()
    {

        YumiSDKEventHandler.OnYumiAdBannerDidLoad();
    }

    public void yumiMediationSDKDidFailToReceiveAd(string error)
    {

        YumiSDKEventHandler.OnYumiAdBannerDidFailToLoad(error);

    }

    public void yumiMediationBannerViewDidClick()
    {

        YumiSDKEventHandler.OnYumiAdBannerDidClick();

    }


    // interstital delegate
    public void yumiMediationInterstitialDidReceiveAd()
    {

        YumiSDKEventHandler.OnYumiAdInterstitialDidLoad();

    }

    public void yumiMediationInterstitialDidFailToReceiveAd(string error)
    {

        YumiSDKEventHandler.OnYumiAdInterstitialDidFailToLoad(error);

    }

    public void yumiMediationInterstitialWillDismissScreen()
    {

        YumiSDKEventHandler.OnYumiAdInterstitialDidClose();

    }

    public void yumiMediationInterstitialDidClick()
    {

        YumiSDKEventHandler.OnYumiAdInterstitialDidClick();

    }

    // video delegate
    public void yumiMediationVideoDidOpen()
    {

        YumiSDKEventHandler.OnYumiAdVideoDidOpen();

    }

    public void yumiMediationVideoDidStartPlaying()
    {

        YumiSDKEventHandler.OnYumiAdVideoDidStartPlaying();

    }

    public void yumiMediationVideoDidClose()
    {

        YumiSDKEventHandler.OnYumiAdVideoDidClose();

    }

    public void yumiMediationVideoDidReward()
    {

        YumiSDKEventHandler.OnYumiAdVideoDidReward();

    }

    // splash
#if UNITY_IOS
    public void yumiAdsSplashDidLoad()
    {

        YumiSDKEventHandler.OnYumiAdSplashDidLoad();

    }
    public void yumiAdsSplashDidFailToLoad(string error)
    {

        YumiSDKEventHandler.OnYumiAdSplashDidFailToLoad(error);

    }
    public void yumiAdsSplashDidClick()
    {

        YumiSDKEventHandler.OnYumiAdSplashDidClick();

    }
    public void yumiAdsSplashDidClosed()
    {

        YumiSDKEventHandler.OnYumiAdSplashDidClose();

    }
    public Image yumiAdsSplashDefaultImage()
    {

        return null;
    }
#endif

}
