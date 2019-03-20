using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YumiMediationSDK.Common
{
    public class YumiMediationSDKSetting : ScriptableObject
    {

        private const string settingsAssetName = "YumiMediationSDKSetting";
        private const string settingsPath = "YumiMediationSDK/Resources";
        private const string settingsAssetExtension = ".asset";

        private static YumiMediationSDKSetting instance;

        [Header("Debug")]
        [SerializeField]
        private bool DebugMode;

        [Header("Version")]
        [SerializeField]
        private string GameVersion = "";

        [Header("Android")]
        [SerializeField]
        private string Android_ChannelId = "";
        [SerializeField]
        private string Android_RewardedVideoPlacementId = "";
        [SerializeField]
        private string Android_InterstitialsPlacementId = "";
        [SerializeField]
        private string Android_BannerPlacementId = "";
        [SerializeField]
        private string Android_NativeAdPlacementId = "";


        [Header("IOS")]
        [SerializeField]
        private string IOS_ChannelId = "";
        [SerializeField]
        private string IOS_RewardedVideoPlacementId = "";
        [SerializeField]
        private string IOS_InterstitialsPlacementId = "";
        [SerializeField]
        private string IOS_BannerPlacementId = "";
        [SerializeField]
        private string IOS_NativeAdPlacementId = "";

        [Header("Banner Self-adaptation")]
        [SerializeField]
        private bool AutomaticAdaptionBanner;


        //Debug 
        public static bool GetDebugMode { get { return Instance.DebugMode; } }

        //Version
        public static string GetGameVersion { get { return Instance.GameVersion; } }

        //Banner Self-adaptation
        public static bool GetAutomaticAdaptionBanner { get { return instance.AutomaticAdaptionBanner; } }

        public static YumiMediationSDKSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load(settingsAssetName) as YumiMediationSDKSetting;

                    if (instance == null)
                    {
                        // If not found, autocreate the asset object.
                        instance = CreateInstance<YumiMediationSDKSetting>();
#if UNITY_EDITOR
                        string properPath = Path.Combine(Application.dataPath, settingsPath);
                        if (!Directory.Exists(properPath))
                        {
                            UnityEditor.AssetDatabase.CreateFolder("Assets/YumiMediationSDK", "Resources");
                        }

                        string fullPath = Path.Combine(Path.Combine("Assets", settingsPath),
                                        settingsAssetName + settingsAssetExtension);
                        UnityEditor.AssetDatabase.CreateAsset(instance, fullPath);
#endif
                    }
                }
                return instance;
            }
        }

        public static string BannerPlacementId()
        {
#if UNITY_ANDROID
            return Instance.Android_BannerPlacementId;
#elif UNITY_IOS
            return  Instance.IOS_BannerPlacementId;
#else
            return "unknown";
#endif
        }

        public static string InterstitialPlacementId()
        {
#if UNITY_ANDROID
            return Instance.Android_InterstitialsPlacementId;
#elif UNITY_IOS
            return  Instance.IOS_InterstitialsPlacementId;
#else
            return "unknown";
#endif
        }

        public static string RewardVideoPlacementId()
        {
#if UNITY_ANDROID
            return Instance.Android_RewardedVideoPlacementId;

#elif UNITY_IOS
            return  Instance.IOS_RewardedVideoPlacementId;
#else
            return "unknown";
#endif
        }

        public static string NativeAdPlacementId()
        {
#if UNITY_ANDROID
            return Instance.Android_NativeAdPlacementId;
#elif UNITY_IOS
            return  Instance.IOS_NativeAdPlacementId;
#else
            return "unknown";
#endif
        }

        public static string ChannelId()
        {
#if UNITY_ANDROID
            return Instance.Android_ChannelId;
#elif UNITY_IOS
            return  Instance.IOS_ChannelId;
#else
            return "unknown";
#endif
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Window/YumiMediationSDK/YumiMediationAd Settings")]
        public static void HandleYumiMediationSDKSettings()
        {
            Logger.Log("HandleYumiMediationSDKSettings");
            UnityEditor.Selection.activeObject = Instance;

        }
#endif
    }
}
