using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.ZplayMediationSDK.Scripts;

public class ZplayMediationDemo : MonoBehaviour {

    void Awake()
    {
        ZplayMediationAdManager.AdFinished += adFinished;
    }

    private void adFinished(string Tag)
    {

    }


    public void ShowRewardedAd()
    {
        bool IsRewardedVideoAdReady = ZplayMediationAdManager.Instance.IsRewardedVideoAdReady;
        ZplayMediationAdManager.Instance.ShowRewardedAd("TAG", result => {
            if (result)
            {

            }
        });
    }
    public void ShowInterstitialAd()
    {
        ZplayMediationAdManager.Instance.ShowInterstitialAd();
        bool IsInterstitialAdReady = ZplayMediationAdManager.Instance.IsInterstitialAdReady;
    }

    public void ShowBanner()
    {
        ZplayMediationAdManager.Instance.ShowBannelAd();
    }

    public void HideBanner()
    {
        ZplayMediationAdManager.Instance.HideBannelAd();
    }
}
