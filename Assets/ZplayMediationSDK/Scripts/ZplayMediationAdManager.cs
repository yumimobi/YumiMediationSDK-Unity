using Assets.ZplayMediationSDK.Scripts.SettingManager;
using System;
using UnityEngine;

namespace Assets.ZplayMediationSDK.Scripts
{
    public class ZplayMediationAdManager : MonoBehaviour
    {

        public enum YumiMediationBannerPosition
        {
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

        public static event Action<String> AdFinished;

        private string GameVersion = "";
        private String ChannelId = "";
        private String RewardedVideoSlotId = "";
        private String InterstitialsSlotId = "";
        private string BannerSlotId = "";

        public static ZplayMediationAdManager Instance;

        private String _adTag;
        private IAdMediationProxy _mediationProxy;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        void Start()
        {
            GameVersion = MediationManagerSetting.GetGameVersion;
#if UNITY_EDITOR
            _mediationProxy = new EditorAdMediationProxy();
#elif UNITY_IOS
            ChannelId = MediationManagerSetting.GetIOSZChannelId;
            RewardedVideoSlotId = MediationManagerSetting.GetIOSZRewardedVideoSlotId;
            InterstitialsSlotId = MediationManagerSetting.GetIOSZInterstitialsSlotId;
			BannerSlotId = MediationManagerSetting.GetIOSZBannelSlotId;
            _mediationProxy = iOSAdMediationProxy.Create();
#elif UNITY_ANDROID
            ChannelId = MediationManagerSetting.GetAndroidZChannelId;
            RewardedVideoSlotId = MediationManagerSetting.GetAndroidZRewardedVideoSlotId;
            InterstitialsSlotId = MediationManagerSetting.GetAndroidZInterstitialsSlotId;
            BannerSlotId = MediationManagerSetting.GetAndroidZBannelSlotId;
            _mediationProxy = AndroidAdMediationProxy.Create();
#endif
            _mediationProxy.Initialize(GameVersion, ChannelId, RewardedVideoSlotId, InterstitialsSlotId, BannerSlotId);
            _mediationProxy.RewardedVideoFinished += OnRewardedVideoFinished;
        }

        void OnGUI()
        {
            if (MediationManagerSetting.GetDebugMode)
            {
                GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button);
                myButtonStyle.fontSize = 20;

                if (GUI.Button(new Rect(300, 50, 200, 80), "YMDebugAD", myButtonStyle))
                {
                    _mediationProxy.ShowTestSuit();
                }
            }

        }

        public Boolean IsInterstitialAdReady
        {
            get
            {
                return  _mediationProxy.IsInterstitialAvailable();
            }
        }
        public Boolean IsRewardedVideoAdReady
        {
            get
            {
                return _mediationProxy.IsRewardedVideoAvailable();
            }
        }

        private void OnRewardedVideoFinished(Boolean isSuccess)
        {
            if (!isSuccess)
            {
                return;
            }

            Debug.Log("Ad finished");
            if (AdFinished != null)
            {
                AdFinished(_adTag);
            }
        }

        public void ShowRewardedAd(String adTag, Action<Boolean> beforeAdShownCallback)
        {
            if (_mediationProxy.IsRewardedVideoAvailable())
            {
                if (beforeAdShownCallback != null)
                {
                    beforeAdShownCallback(true);
                }
                _adTag = adTag;
                _mediationProxy.ShowRewardedVideo();
            }
            else
            {
                if (beforeAdShownCallback != null)
                {
                    beforeAdShownCallback(false);
                }
                Debug.LogError("Ad is unavailable when trying to ShowRewardedAd");
            }
        }

        public void ShowInterstitialAd()
        {
            _mediationProxy.ShowInterstitial();
        }

        public void ShowBannelAd()
        {
            _mediationProxy.ShowBanner(MediationManagerSetting.GetAutomaticAdaptionBanner);
        }
        public void HideBannelAd()
        {
            _mediationProxy.HideBanner();
        }
    }
}