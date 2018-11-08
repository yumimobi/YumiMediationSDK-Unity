using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Assets.YumiMediationSDK.Common
{
    public class MediationManagerSetting : ScriptableObject
    {

        private const string settingsAssetName = "MediationManagerSetting";
		private const string settingsPath = "YumiMediationSDK/Resources";
        private const string settingsAssetExtension = ".asset";

        private static MediationManagerSetting instance;

        [Header("Debug")]
        [SerializeField]
        private bool DebugMode;

        [Header("RequestInterval")]
        [SerializeField]
        private int RewardedVideoRequestInterval = 1;

        [Header("Load")]
        [SerializeField]
        private bool ReadyRewardedVideoAds;
        [SerializeField]
        private bool ReadyInterstitialsAds;
        [SerializeField]
        private bool ReadyBannerAds;

        [Header("Version")]
        [SerializeField]
        private string GameVersion = "";

        [Header("Android")]
        [SerializeField]
        private string Android_ChannelId = "";
        [SerializeField]
        private string Android_RewardedVideoSlotId = "";
        [SerializeField]
        private string Android_InterstitialsSlotId = "";
        [SerializeField]
        private string Android_BannerSlotId = "";


        [Header("IOS")]
        [SerializeField]
        private string IOS_ChannelId = "";
        [SerializeField]
        private string IOS_RewardedVideoSlotId = "";
        [SerializeField]
        private string IOS_InterstitialsSlotId = "";
        [SerializeField]
        private string IOS_BannerSlotId = "";

        [Header("IOSBannerPosition")]
        [SerializeField]
        private bool IOS_BannerPositionTop;
        [SerializeField]
        private bool IOS_BannerPositionBottom;

        [Header("AutomaticAdaption")]
        [SerializeField]
        private bool AutomaticAdaptionBanner; 


        //Debug 
        public static bool GetDebugMode { get { return Instance.DebugMode; } }

        //RequestInterval
        public static int GetRewardedVideoRequestInterval { get { return Instance.RewardedVideoRequestInterval; } }

        //Load
        public static bool GetReadyRewardedVideoAds { get { return Instance.ReadyRewardedVideoAds; } }
        public static bool GetReadyInterstitialsAds { get { return Instance.ReadyInterstitialsAds; } }
        public static bool GetReadyBannerAds { get { return Instance.ReadyBannerAds; } }

        //Version
        public static string GetGameVersion { get { return Instance.GameVersion; } }

        //Android
        public static string GetAndroidZChannelId { get { return Instance.Android_ChannelId; } }
        public static string GetAndroidZRewardedVideoSlotId { get { return Instance.Android_RewardedVideoSlotId; } }
        public static string GetAndroidZInterstitialsSlotId { get { return Instance.Android_InterstitialsSlotId; } }
        public static string GetAndroidZBannelSlotId { get { return Instance.Android_BannerSlotId; } }

        //IOS
        public static string GetIOSZChannelId { get { return Instance.IOS_ChannelId; } }
        public static string GetIOSZRewardedVideoSlotId { get { return Instance.IOS_RewardedVideoSlotId; } }
        public static string GetIOSZInterstitialsSlotId { get { return Instance.IOS_InterstitialsSlotId; } }
        public static string GetIOSZBannelSlotId { get { return Instance.IOS_BannerSlotId; } }

        //IOSBannerPosition
        public static bool GetIOSBannerPositionTop { get { return Instance.IOS_BannerPositionTop; } }
        public static bool GetIOSBannnerPostionBottom { get { return Instance.IOS_BannerPositionBottom; } }

        //AutomaticAdaption
        public static bool GetAutomaticAdaptionBanner { get { return instance.AutomaticAdaptionBanner; } }

        public static MediationManagerSetting Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load(settingsAssetName) as MediationManagerSetting;

                    if (instance == null)
                    {
                        // If not found, autocreate the asset object.
                        instance = CreateInstance<MediationManagerSetting>();
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

#if UNITY_EDITOR
		[UnityEditor.MenuItem("Window/YumiMediationSDK/Edit Settings")]
        public static void ZplayPromoteHTSetting()
        {
            UnityEditor.Selection.activeObject = Instance;
			if (GameObject.Find("YumiSDKAdapter"))
            {
                Debug.Log("MediationManagerSetting game object already exists and does not need to be created");
            }
            else
            {
                GameObject gameObject = new GameObject();
				gameObject.name = "YumiSDKAdapter";
				gameObject.AddComponent<YumiSDKAdapter>();
            }
            UnityEngine.SceneManagement.Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(scene);
        }
#endif
    }
}
