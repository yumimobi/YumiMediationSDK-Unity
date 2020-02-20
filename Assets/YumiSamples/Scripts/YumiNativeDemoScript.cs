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
public class YumiNativeDemoScript : MonoBehaviour
{
    //private NativeAd nativeAd;
    private YumiNativeAd nativeAd;

    private string NativePlacementId = "";
    private string GameVersionId = "";
    private string ChannelId = "";

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

    void Start()
    {
        NativePlacementId = YumiMediationSDKSetting.NativeAdPlacementId();
        GameVersionId = YumiMediationSDKSetting.GetGameVersion;
        ChannelId = YumiMediationSDKSetting.ChannelId();
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
        if (nativeAd == null)
        {
            // you must set native  express ad view  transform if you want to support native express ad
            NativeAdOptionsBuilder builder = new NativeAdOptionsBuilder();
            builder.setExpressAdViewTransform(adPanel.transform);

            YumiNativeAdOptions options = new YumiNativeAdOptions(builder);
            //YumiNativeAdOptions options = new NativeAdOptionsBuilder().Build(); // only native 

            nativeAd = new YumiNativeAd(NativePlacementId, ChannelId, GameVersionId, gameObject,options);

            nativeAd.OnNativeAdLoaded += HandleNativeAdLoaded;
            nativeAd.OnAdFailedToLoad += HandleNativeAdFailedToLoad;
            nativeAd.OnAdClick += HandleNativeAdClicked;
            nativeAd.OnExpressAdRenderSuccess += HandleNativeExpressAdRenderSuccess;
            nativeAd.OnExpressAdRenderFail += HandleNativeExpressAdRenderFail;
            nativeAd.OnExpressAdClickCloseButton += HandleNativeExpressAdClickCloseButton;
        }

        UnregisterNativeViews();

        nativeAd.LoadAd(1);
    }

    private void RegisterNativeViews()
    {

        Dictionary<NativeElemetType, Transform> elementsDictionary = new Dictionary<NativeElemetType, Transform>();
        elementsDictionary.Add(NativeElemetType.PANEL, adPanel.transform);
        elementsDictionary.Add(NativeElemetType.TITLE, title.transform);
        elementsDictionary.Add(NativeElemetType.DESCRIPTION, body.transform);
        elementsDictionary.Add(NativeElemetType.ICON, iconImage.transform);
        elementsDictionary.Add(NativeElemetType.COVER_IMAGE, mediaView.transform);
        elementsDictionary.Add(NativeElemetType.CALL_TO_ACTION, callToActionButton.transform);

        nativeAd.RegisterNativeDataForInteraction(yumiNativeData, elementsDictionary);

    }

    private void ShowNatveAd(YumiNativeData nativeData)
    {
        statusText.text = "Show native ad ";

        nativeAd.ShowView(nativeData);
    }

    private void ShowNativeExpressAd(YumiNativeData nativeData)
    {
        statusText.text = "Show native express ad ";

        nativeAd.ShowView(nativeData);
    }

    public void ShowAd()
    {
        if (nativeAd.IsAdInvalidated(yumiNativeData))
        {
            statusText.text = "Native Data is invalidated";
            return;
        }

        statusText.text = "Register native views";
       
        // the ad is native ad
        if (!yumiNativeData.isExpressAdView)
        {
            ShowNatveAd(yumiNativeData);
        }

        // if the ad is native express view please show ad in HandleNativeExpressAdRenderSuccess

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
    // Next button
    public void NextScene()
    {
        if(nativeAd != null){
            this.nativeAd.UnregisterView(yumiNativeData);
            this.nativeAd.Destroy();
        }

        SceneManager.LoadScene("YumiMainDemoScene");

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
        RegisterNativeViews();
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

    public void HandleNativeExpressAdRenderSuccess(object sender , YumiNativeDataEventArgs args)
    {
        statusText.text = "HandleNativeExpressAdRenderSuccess";

        ShowNativeExpressAd(args.nativeData);

        Logger.Log("HandleNativeExpressAdRenderSuccess");
    }
    public void HandleNativeExpressAdRenderFail(object sender, YumiAdFailedToRenderEventArgs args)
    {
        statusText.text = "HandleNativeExpressAdRenderFail" + args.Message +"data id is " + args.nativeData.uniqueId;
        Logger.Log("HandleNativeExpressAdRenderFail" + args.Message + "data id is " + args.nativeData.uniqueId);
    }
    public void HandleNativeExpressAdClickCloseButton(object sender, YumiNativeDataEventArgs args)
    {
        statusText.text = "HandleNativeExpressAdClickCloseButton";
        Logger.Log("HandleNativeExpressAdClickCloseButton" + args.nativeData.uniqueId);

         UnregisterNativeViews();
    }

    #endregion
}
