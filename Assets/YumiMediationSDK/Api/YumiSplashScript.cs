using UnityEngine;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
using System;
using UnityEngine.SceneManagement;

public class YumiSplashScript : MonoBehaviour
{

    private YumiSplashAd splashAd;
    private string SplashPlacementId = "";
    private string GameVersionId = "";
    private string ChannelId = "";

    void Start()
    {
        GameVersionId = YumiMediationSDKSetting.GetGameVersion;
        ChannelId = YumiMediationSDKSetting.ChannelId();
        SplashPlacementId = YumiMediationSDKSetting.SplashPlacementId();

        LoadSplash();
    }

    public void Dispose()
    {
        if (splashAd != null)
        {
            splashAd.DestroySplashAd();
            splashAd = null;
        }
    }

    private void LoadSplash()
    {
        if (splashAd == null)
        {

            YumiSplashOptionsBuilder builder = new YumiSplashOptionsBuilder().setAdBottomViewHeight(100);
            YumiSplashOptions splashOptions = new YumiSplashOptions(builder);

            splashAd = new YumiSplashAd(SplashPlacementId, ChannelId, GameVersionId, splashOptions);
            // add splash event
            splashAd.OnAdSuccessToShow += HandleSplashAdSuccssToShow;
            splashAd.OnAdFailedToShow += HandleSplashAdFailToShow;
            splashAd.OnAdClicked += HandleSplashAdClicked;
            splashAd.OnAdClosed += HandleSplashAdClosed;
        }

        splashAd.LoadAdAndShow();
    }

    private void InputMainSence()
    {
        SceneManager.LoadScene("YumiMainDemoScene");
    }
    #region  splash callback handlers

    public void HandleSplashAdSuccssToShow(object sender, EventArgs args)
    {
        Logger.Log("HandleSplashSuccssToShow event success to show");
    }

    public void HandleSplashAdFailToShow(object sender, YumiAdFailedToShowEventArgs args)
    {
       
        Logger.Log("HandleSplashAdFailToShow + fail error is =  " + args.Message);
        InputMainSence();
    }

    public void HandleSplashAdClicked(object sender, EventArgs args)
    {
        Logger.Log("HandleSplashAdClicked clicked");
    }
    public void HandleSplashAdClosed(object sender, EventArgs args)
    {
        Logger.Log("HandleSplashAdClosed Ad closed ");
        InputMainSence();
    }

    #endregion
}
