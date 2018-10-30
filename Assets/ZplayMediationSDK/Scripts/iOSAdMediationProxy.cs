using Assets.ZplayMediationSDK.Scripts.SettingManager;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.ZplayMediationSDK.Scripts
{
    public class iOSAdMediationProxy : MonoBehaviour, IAdMediationProxy
    {
        private String GameVersion = "";
        private String ChannelId = "";
        private String RewardedVideoSlotId = "";
        private String InterstitialsSlotId = "";
        private String BannerSlotId = "";

        public event Action<Boolean> RewardedVideoFinished;
        private Boolean _isRewardedVideoAvailable;

        public static IAdMediationProxy Create()
        {
            var gameObject = new GameObject("YumiMediationSDKManager");
            return gameObject.AddComponent<iOSAdMediationProxy>();
        }

        #region NativeMethods

        // interstital
        [DllImport("__Internal")]
        private static extern void _initYumiMediationInterstitial(string placementID, string channelID, string versionID);
        [DllImport("__Internal")]
        private static extern bool _isInterstitialReady();
        [DllImport("__Internal")]
        private static extern void _present();

        //video
        [DllImport("__Internal")]
        private static extern void _loadYumiMediationVideo(string placementID, string channelID, string versionID);
        [DllImport("__Internal")]
        private static extern bool _isVideoReady();
        [DllImport("__Internal")]
        private static extern void _playVideo();

        //Bannel
        [DllImport("__Internal")]
        private static extern void _initYumiMediationBanner(string placementID, string channelID, string versionID, ZplayMediationAdManager.YumiMediationBannerPosition position);
        [DllImport("__Internal")]
        private static extern void _loadAd(bool isSmartBanner);
        [DllImport("__Internal")]
        private static extern void _hiddenYumiMediationBanner(bool ishidden);
        [DllImport("__Internal")]
        private static extern void _removeBanner();

        [DllImport("__Internal")]
        private static extern string _fetchBannerAdSize();
        [DllImport("__Internal")]
        private static extern void _setBannerAdSize(ZplayMediationAdManager.YumiMediationAdViewBannerSize bannerSize);

        //splash
        [DllImport("__Internal")]
        private static extern void _showYumiAdsSplash(string placementID, string appKey);

        // debugcenter 
        [DllImport("__Internal")]
        private static extern void _presentYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID, string nativePlacementID, string channelID, string versionID);
        [DllImport("__Internal")]
        private static extern void _setBannerSizeInDebugCenter(ZplayMediationAdManager.YumiMediationAdViewBannerSize bannerSize);
        #endregion

        #region IAdMediationProxy

        public void Initialize(string gameVersion, string channelId, string rewardedVideoSlotId, string interstitialsSlotId, string bannerSlotId)
        {

            //Obtain correlation parameters
            GameVersion = gameVersion;
            ChannelId = channelId;
            RewardedVideoSlotId = rewardedVideoSlotId;
            InterstitialsSlotId = interstitialsSlotId;
            BannerSlotId = bannerSlotId;

            if (MediationManagerSetting.GetReadyRewardedVideoAds)
            {
                _loadYumiMediationVideo(RewardedVideoSlotId, ChannelId, GameVersion);
                StartCoroutine(CheckRewardedVideoAvailability());
            }
            else if (MediationManagerSetting.GetReadyInterstitialsAds)
            {
                _initYumiMediationInterstitial(InterstitialsSlotId, ChannelId, GameVersion);
            }else if (MediationManagerSetting.GetReadyBannerAds)
            {
                if(MediationManagerSetting.GetIOSBannerPositionTop)
                    _initYumiMediationBanner(BannerSlotId,ChannelId,GameVersion,ZplayMediationAdManager.YumiMediationBannerPosition.YumiMediationBannerPositionTop);
                else if(MediationManagerSetting.GetIOSBannnerPostionBottom)
                    _initYumiMediationBanner(BannerSlotId, ChannelId, GameVersion, ZplayMediationAdManager.YumiMediationBannerPosition.YumiMediationBannerPositionBottom);
                else
                    _initYumiMediationBanner(BannerSlotId, ChannelId, GameVersion, ZplayMediationAdManager.YumiMediationBannerPosition.YumiMediationBannerPositionBottom);
            }
        }

        public Boolean IsInterstitialAvailable()
        {
            return _isInterstitialReady();
        }

        public Boolean IsRewardedVideoAvailable()
        {
            return _isRewardedVideoAvailable;
        }
        public Boolean IsBannerAvailable()
        {
            return false;
        }

        public void FetchInterstitial()
        {
        }

        public void FetchRewardedVideo()
        {
        }

        public void ShowInterstitial()
        {
            _present();
        }

        public void ShowRewardedVideo()
        {
            _playVideo();
        }
        public void ShowBanner(Boolean AutomaticAdaption = false)
        {
            _loadAd(AutomaticAdaption);
        }

        public void HideBanner()
        {
            _removeBanner();
        }

        public void ShowTestSuit()
        {
            _presentYumiMediationDebugCenter(BannerSlotId, InterstitialsSlotId, RewardedVideoSlotId, "", ChannelId, GameVersion);
        }

        #endregion

        #region Callbacks

        public void yumiMediationVideoDidClose()
        {
            Debug.Log("Rewarded video: closed");
            if (RewardedVideoFinished != null)
            {
                RewardedVideoFinished(false);
            }
            _isRewardedVideoAvailable = false;
            StartCoroutine(CheckRewardedVideoAvailability());
        }

        public void yumiMediationVideoDidReward()
        {
            Debug.Log("Rewarded video: reward available");
            if (RewardedVideoFinished != null)
            {
                RewardedVideoFinished(true);
            }
        }

        #endregion

        private IEnumerator CheckRewardedVideoAvailability()
        {
            while (!_isRewardedVideoAvailable)
            {
                yield return new WaitForSeconds(MediationManagerSetting.GetRewardedVideoRequestInterval);
                _isRewardedVideoAvailable = _isVideoReady();
            }
        }
    }
}