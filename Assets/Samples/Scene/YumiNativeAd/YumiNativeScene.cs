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
    }

    private void Log(string s)
    {

        Logger.Log(s);
    }

    // Next button
    public void NextScene()
    {
        SceneManager.LoadScene("YumiScene");
    }
}
