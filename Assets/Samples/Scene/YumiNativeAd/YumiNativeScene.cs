using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;

[RequireComponent(typeof(CanvasRenderer))]
[RequireComponent(typeof(RectTransform))]
public class YumiNativeScene : MonoBehaviour
{
    //private NativeAd nativeAd;
    private YumiNativeAd nativeAd;
#if UNITY_ANDROID
    private String NativePlacementId = "zsvrfeni";
    private String GameVersionId = "";
    private String ChannelId = "";
#elif UNITY_IOS
    private String NativePlacementId = "atb3ke1i";
    private String GameVersionId = "";
    private String ChannelId = "";
#else
    private String NativePlacementId = "";
    private String GameVersionId = "";
    private String ChannelId = "";
#endif

    // UI elements in scene
    public Text statusText;
    [Header("Text:")]
    public Text title;
    public Text body;
    [Header("Images:")]
    public GameObject mediaView;
    public GameObject iconImage;
    [Header("Buttons:")]
    // This doesn't be a button - it can also be an image
    public Button callToActionButton;

    // ad panel
    public GameObject adPanel;

    private YumiNativeData yumiNativeData = new YumiNativeData();

    void Awake()
    {
        Logger.Log("Native ad ready to load.");
    }

    void OnDestroy()
    {
        // Dispose of native ad when the scene is destroyed
        //if (this.nativeAd) {
        //    this.nativeAd.Dispose();
        //}
        Logger.Log("NativeAdTest was destroyed!");
    }

    // Load Ad button
    public void LoadAd()
    {
        statusText.text = "LoadAd";
        if (this.nativeAd == null)
        {
            this.nativeAd = new YumiNativeAd(NativePlacementId, ChannelId, GameVersionId);

            this.nativeAd.OnNativeAdLoaded += HandleNativeAdLoaded;
            this.nativeAd.OnAdFailedToLoad += HandleNativeAdFailedToLoad;
            this.nativeAd.OnAdClick += HandleNativeAdClicked;
        }

        this.nativeAd.LoadNativeAd(1);
    }

    public void RegisterNativeViews()
    {
        statusText.text = "RegisterNativeViews";
        Dictionary<NativeElemetType, Transform> elementsDictionary = new Dictionary<NativeElemetType, Transform>();
        elementsDictionary.Add(NativeElemetType.PANEL, adPanel.transform);
        elementsDictionary.Add(NativeElemetType.TITLE, title.transform);
        elementsDictionary.Add(NativeElemetType.DESCRIPTION, body.transform);
        elementsDictionary.Add(NativeElemetType.ICON, iconImage.transform);
        elementsDictionary.Add(NativeElemetType.COVER_IMAGE, mediaView.transform);
        elementsDictionary.Add(NativeElemetType.CALL_TO_ACTION, callToActionButton.transform);
        nativeAd.RegisterGameObjectsForInteraction(yumiNativeData, gameObject, elementsDictionary);
    }

    public void ShowAd()
    {
        statusText.text = "ShowAd";
        if (!nativeAd.IsAdInvalidated(yumiNativeData))
        {
            statusText.text = "Native Data is invalidated";
            return;
        }
        nativeAd.ShowView(yumiNativeData);
    }

    public void HideAd()
    {
        statusText.text = "HideAd";
        nativeAd.HideView(yumiNativeData);
    }

    public void UnregisterNativeViews()
    {
        statusText.text = "UnregisterNativeViews";
        nativeAd.UnregisterView(yumiNativeData);
        yumiNativeData = new YumiNativeData();
    }

    private void Log(string s)
    {
        Logger.Log(s);
    }

#region native call back handles

    public void HandleNativeAdLoaded(object sender, YumiNativeToLoadEventArgs args)
    {
        Logger.Log("HandleNativeAdLoaded event opened");
        if (nativeAd == null)
        {
            statusText.text = "nativeAd is null";
            return;
        }

        if (args == null || args.nativeData == null || args.nativeData.Count == 0)
        {
            statusText.text = "nativeAd data not found.";
            return;
        }
        statusText.text = "HandleNativeAdLoaded";
        yumiNativeData = args.nativeData[0];
    }
    public void HandleNativeAdFailedToLoad(object sender, YumiAdFailedToLoadEventArgs args)
    {
        statusText.text = "HandleNativeAdFailedToLoad: " + args.Message;
        Logger.Log("HandleNativeAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleNativeAdClicked(object sender, EventArgs args)
    {
        statusText.text = "HandleNativeAdClicked";
        Logger.Log("HandleNativeAdClicked");
    }

#endregion
}
