using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AddYumiIOSPrefab{
	
	[MenuItem("AddYumiIOSPrefab/AddYumiIOSPrefab")]

	private static void CreateYumiMobi()
	{		
		GameObject obj = new GameObject("YumiMobiPrefab_iOS");
		obj.AddComponent<YumiSDKAdapter>();
		obj.AddComponent<YumiMediationSDKEventListener>();
		obj.AddComponent<YumiMediationSDKManager>();

	}
}

