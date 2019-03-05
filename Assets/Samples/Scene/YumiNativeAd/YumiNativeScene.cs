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

    private String NativePlacementId = "hxqd9uwr";
    private String GameVersionId = "";
    private String ChannelId = "";
    // UI elements in scene
    [Header("Text:")]
    public Text title;
    public Text body;
    [Header("Images:")]
    public GameObject mediaView;
    public GameObject iconImage;
    [Header("Buttons:")]
    // This doesn't be a button - it can also be an image
    public Button callToActionButton;
    //[Header("Ad Choices:")]
    //public AdChoices adChoices;

    // ad panel
    public GameObject adPanel;

    void Awake()
    {
        Logger.Log("Native ad ready to load.");
        this.title.text = "test title";
        this.body.text = "test body";
        callToActionButton.GetComponentInChildren<Text>().text = "安装";
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
        Logger.Log("load ad ");
        if (this.nativeAd == null)
        {
            this.nativeAd = new YumiNativeAd(NativePlacementId, ChannelId, GameVersionId);

            this.nativeAd.OnNativeAdLoaded += HandleNativeAdLoaded;
            this.nativeAd.OnAdFailedToLoad += HandleNativeAdFailedToLoad;
            this.nativeAd.OnAdClick += HandleNativeAdClicked;
        }

        this.nativeAd.LoadNativeAd(1);
    }

    private void Log(string s)
    {

        Logger.Log(s);
    }

    // Next button
    public void NextScene()
    {
        YumiNativeData data = new YumiNativeData();
        data.uniqueId = "";
        this.nativeAd.UnregisterView(data);
        SceneManager.LoadScene("YumiScene");
     
    }

    #region native call back handles

    public void HandleNativeAdLoaded(object sender, YumiNativeToLoadEventArgs args)
    {
        Logger.Log("HandleNativeAdLoaded event opened");
        if (this.nativeAd == null)
        {
            Logger.Log("nativeAd is null");

        }
        else
        {
            Logger.Log("nativeAd is not null");
            Dictionary<NativeElemetType, Transform> elementsDictionary = new Dictionary<NativeElemetType, Transform>();
            elementsDictionary.Add(NativeElemetType.PANEL, adPanel.transform);
            elementsDictionary.Add(NativeElemetType.TITLE, title.transform);
            elementsDictionary.Add(NativeElemetType.DESCRIPTION, body.transform);
            elementsDictionary.Add(NativeElemetType.ICON, iconImage.transform);
            elementsDictionary.Add(NativeElemetType.COVER_IMAGE, mediaView.transform);
            elementsDictionary.Add(NativeElemetType.CALL_TO_ACTION, callToActionButton.transform);
            nativeAd.RegisterGameObjectsForInteraction(new YumiNativeData(), gameObject, elementsDictionary);
        }
    }
    public void HandleNativeAdFailedToLoad(object sender, YumiAdFailedToLoadEventArgs args)
    {
        Logger.Log("HandleNativeAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleNativeAdClicked(object sender, EventArgs args)
    {
        Logger.Log("HandleNativeAdClicked");
    }

    #endregion
}
