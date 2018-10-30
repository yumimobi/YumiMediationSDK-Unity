using Assets.ZplayMediationSDK.Scripts.SettingManager;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.ZplayMediationSDK.Scripts
{
    public class AndroidAdMediationProxy : MonoBehaviour, IAdMediationProxy
    {
        private String GameVersion = "";
        private String ChannelId = "";
        private String RewardedVideoSlotId = "";
        private String InterstitialsSlotId = "";
        private String BannerSlotId = "";


        public event Action<Boolean> RewardedVideoFinished;

        private AndroidJavaObject _androidJavaObject;
        private Boolean _isRewardedVideoAvailable;
        private Boolean _isInterstitialAvailable;
        private Boolean _isBannerAvailable;
        private Boolean _isRewardedVideoIncentivized;

        public static IAdMediationProxy Create()
        {
            var gameObject = new GameObject("ZplayYUMIHelper");
            return gameObject.AddComponent<AndroidAdMediationProxy>();
        }

        #region IAdMediationProxy

        public void Initialize(string gameVersion,string channelId,string rewardedVideoSlotId,string interstitialsSlotId,string bannerSlotId)
        {
            //Obtain correlation parameters
            GameVersion = gameVersion;
            ChannelId = channelId;
            RewardedVideoSlotId = rewardedVideoSlotId;
            InterstitialsSlotId = interstitialsSlotId;
            BannerSlotId = bannerSlotId;

            using (var ajc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                _androidJavaObject = ajc.GetStatic<AndroidJavaObject>("currentActivity");
            }

            if (_androidJavaObject == null)
            {
                Debug.LogError("InitInterstitialAd error : AndroidJavaObject is null");
                return;
            }

            using (var javaClass = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                if (MediationManagerSetting.GetReadyRewardedVideoAds)
                {
                    javaClass.CallStatic("InitMedia", _androidJavaObject, RewardedVideoSlotId, ChannelId, GameVersion, gameObject.name);
                    //Request a video
                    FetchRewardedVideo();
                    StartCoroutine(CheckRewardedVideoAvailability());
                }
                else if (MediationManagerSetting.GetReadyInterstitialsAds)
                {
                    javaClass.CallStatic("initInterstitialAd", _androidJavaObject, InterstitialsSlotId, ChannelId, GameVersion, gameObject.name);
                    //Request a Interstitial
                    FetchInterstitial();
                }

                javaClass.CallStatic("setAppIsGooglePlayVersions");
            }
        }

        public Boolean IsInterstitialAvailable()
        {
            return _isInterstitialAvailable;
        }

        public Boolean IsRewardedVideoAvailable()
        {
            return _isRewardedVideoAvailable;
        }

        public Boolean IsBannerAvailable()
        {
            return _isBannerAvailable;
        }

        public void FetchInterstitial()
        {
            if (_androidJavaObject == null)
            {
                Debug.LogError("RequestInterstitial error : AndroidJavaObject ajo is null");
                return;
            }

            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                jc.CallStatic("requestInterstitial", _androidJavaObject);
            }
        }

        public void FetchRewardedVideo()
        {
            if (_androidJavaObject == null)
            {
                Debug.LogError("RequestMedia error : AndroidJavaObject ajo is null");
                return;
            }

            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                jc.CallStatic("requestMedia", _androidJavaObject);
            }
        }

        public void ShowInterstitial()
        {
            if (_androidJavaObject == null)
            {
                Debug.LogError("ShowInterstitialAd error : AndroidJavaObject ajo is null");
                return;
            }
            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                jc.CallStatic("showInterstitialAd", _androidJavaObject);
            }
        }

        public void ShowRewardedVideo()
        {
            if (_androidJavaObject == null)
            {
                Debug.LogError("ShowMedia error : AndroidJavaObject ajo is null");
                return;
            }
            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                jc.CallStatic("showMedia", _androidJavaObject);
            }
        }

        public void ShowBanner(Boolean AutomaticAdaption = false)
        {
            if (_androidJavaObject == null)
            {
                Debug.LogError("ShowBanner error : AndroidJavaObject ajo is null");
                return;
            }
            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                if (MediationManagerSetting.GetReadyBannerAds)
                    jc.CallStatic("addBannerAd", _androidJavaObject, BannerSlotId, ChannelId, GameVersion, AutomaticAdaption,gameObject.name);
            }
        }

        public void HideBanner()
        {
            if (_androidJavaObject == null)
            {
                Debug.LogError("HideBanner error : AndroidJavaObject ajo is null");
                return;
            }
            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                jc.CallStatic("dismissBanner", _androidJavaObject);
            }
        }

        public void ShowTestSuit()
        {
            if (_androidJavaObject == null)
            {
                Debug.LogError("StartDebugging error : AndroidJavaObject ajo is null");
                return;
            }
            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                jc.CallStatic("startDebugging", _androidJavaObject, BannerSlotId, InterstitialsSlotId, RewardedVideoSlotId, ChannelId, GameVersion);
            }
        }


        #endregion

        #region Callbacks

        public void onInterstitialPreparedFailed(string data)
        {
            Debug.LogError("yumiMobi SDK Interstitial Prepared Failed: " + data);
            _isInterstitialAvailable = false;
        }
        public void onInterstitialPrepared(string data)
        {
            Debug.LogError("yumiMobi SDK Interstitial Prepared Succeed: " + data);
            _isInterstitialAvailable = true;
        }

        public void onMediaIncentived(string data)
        {
            Debug.LogError("yumiMobi SDK Media Incentived Succeed callBack: " + data);
            _isRewardedVideoIncentivized = true;
        }

        public void onMediaClosed(string data)
        {
            Debug.LogError("yumiMobi SDK Media Closed");
            if (RewardedVideoFinished != null)
            {
                RewardedVideoFinished(_isRewardedVideoIncentivized);
            }
            _isRewardedVideoIncentivized = false;
            _isRewardedVideoAvailable = false;
            StartCoroutine(CheckRewardedVideoAvailability());
        }

        public void onIsMediaPrepared(string data)
        {
            Debug.LogError("yumiMobi SDK Media Is Prepared :" + data);
            if (Convert.ToBoolean(data))
            {
                _isRewardedVideoAvailable = true;
                _isRewardedVideoIncentivized = false;
            }
        }

        public void onBannerPreparedFailed(string data)
        {
            Debug.LogError("yumiMobi SDK Banner Prepared Failed: " + data);
            _isBannerAvailable = false;
        }
        public void onBannerPrepared(string data)
        {
            Debug.LogError("yumiMobi SDK Banner Prepared Succeed: " + data);
            _isBannerAvailable = true;
        }

        #endregion

        private IEnumerator CheckRewardedVideoAvailability()
        {
            while (!_isRewardedVideoAvailable)
            {
                yield return new WaitForSeconds(MediationManagerSetting.GetRewardedVideoRequestInterval);
                using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
                {
                    jc.CallStatic("IsMediaPrepared", _androidJavaObject, gameObject.name);
                }
            }
        }

        public void AdDestroy()
        {
            using (var jc = new AndroidJavaClass("com.zplay.adsyumi.unity.plugin.AdsPluginActivity"))
            {
                jc.CallStatic("AdDestroy");
            }
        }
    }
}