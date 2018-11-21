using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YumiMediationSDK.Common
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


        [Header("IOS")]
        [SerializeField]
        private string IOS_ChannelId = "";
        [SerializeField]
        private string IOS_RewardedVideoPlacementId = "";
        [SerializeField]
        private string IOS_InterstitialsPlacementId = "";
        [SerializeField]
        private string IOS_BannerPlacementId = "";

        [Header("Banner Self-adaptation")]
        [SerializeField]
        private bool AutomaticAdaptionBanner; 


        //Debug 
        public static bool GetDebugMode { get { return Instance.DebugMode; } }

        //Version
        public static string GetGameVersion { get { return Instance.GameVersion; } }

        //Android
        public static string GetAndroidZChannelId { get { return Instance.Android_ChannelId; } }
        public static string GetAndroidZRewardedVideoPlacementId { get { return Instance.Android_RewardedVideoPlacementId; } }
        public static string GetAndroidZInterstitialsPlacementId { get { return Instance.Android_InterstitialsPlacementId; } }
        public static string GetAndroidZBannelPlacementId { get { return Instance.Android_BannerPlacementId; } }

        //IOS
        public static string GetIOSZChannelId { get { return Instance.IOS_ChannelId; } }
        public static string GetIOSZRewardedVideoPlacementId { get { return Instance.IOS_RewardedVideoPlacementId; } }
        public static string GetIOSZInterstitialsPlacementId { get { return Instance.IOS_InterstitialsPlacementId; } }
        public static string GetIOSZBannelPlacementId { get { return Instance.IOS_BannerPlacementId; } }

        //Banner Self-adaptation
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
