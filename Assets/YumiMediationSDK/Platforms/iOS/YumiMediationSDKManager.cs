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
