using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_ANDROID
public class AddYumiAndroidPrefab
{
[MenuItem("AddYumiAndroidPrefab/AddYumiAndroidPrefab")]
	private static void CreateYumiMobi()
    {
        GameObject obj = new GameObject("YumiMobiPrefab_Android");
		obj.AddComponent<YumiSDKAdapter>();
        obj.AddComponent<BannerAdCallbackListener>();
        obj.AddComponent<MediaAdCallbackListener>();
        obj.AddComponent<InterstitialAdCallbackListener>();
    }
}
#endif