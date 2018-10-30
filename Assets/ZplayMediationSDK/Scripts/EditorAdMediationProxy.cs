using Assets.ZplayMediationSDK.Scripts.SettingManager;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.ZplayMediationSDK.Scripts
{
    public class EditorAdMediationProxy : IAdMediationProxy
    {
        public event Action<Boolean> RewardedVideoFinished;

        public void Initialize(string gameVersion, string channelId, string rewardedVideoSlotId, string interstitialsSlotId, string bannerSlotId)
        {
            Debug.Log("Editor ad Initialize Succeed");
        }

        public Boolean IsInterstitialAvailable()
        {
            return true;
        }

        public Boolean IsRewardedVideoAvailable()
        {
            return true;
        }

        public bool IsBannerAvailable()
        {
            return true;
        }
        public void FetchInterstitial()
        {
            Debug.Log("Editor ad Fetch Interstitial Succeed");
        }

        public void FetchRewardedVideo()
        {
            Debug.Log("Editor ad Fetch Rewarded Video Succeed");
        }

        public void ShowInterstitial()
        {
            Debug.Log("Editor ad Show Interstitial Succeed");
        }

        public void ShowRewardedVideo()
        {
            if (RewardedVideoFinished != null)
            {
                RewardedVideoFinished(true);
            }
        }
        public void ShowBanner(Boolean AutomaticAdaption = true)
        {
            Debug.Log("Editor ad Show Banner Succeed ="+ AutomaticAdaption);
        }

        public void HideBanner()
        {
            Debug.Log("Editor ad Hide Banner Succeed" );
        }
        public void ShowTestSuit()
        {
            Debug.Log("Editor ad Show TestSuit Succeed");
        }
    }
}