   * [YumiMediationSDK for Unity](#yumimediationsdk-for-unity)
      * [Summary](#summary)
      * [Download the YumiMediationSDK Unity plugin](#download-the-yumimediationsdk-unity-plugin)
      * [Import the YumiMediationSDK Unity plugin](#import-the-yumimediationsdk-unity-plugin)
      * [Include the YumiMediationSDK](#include-the-yumimediationsdk)
         * [Deploy iOS](#deploy-ios)
         * [Deploy Android](#deploy-android)
      * [Select an ad format](#select-an-ad-format)
         * [Banner](#banner)
            * [Initialize Banner](#initialize-banner)
            * [Request Banner](#request-banner)
            * [Hide Banner](#hide-banner)
            * [Show Banner](#show-banner)
            * [Destroy Banner](#destroy-banner)
            * [YumiBannerViewOptions](#yumibannerviewoptions)
         * [Interstitial](#interstitial)
            * [Initialization and Interstitial request](#initialization-and-interstitial-request)
            * [Show Interstitial](#show-interstitial)
            * [Destroy Interstitial](#destroy-interstitial)
         * [Reward Video](#reward-video)
            * [Initialization and Reward Video request](#initialization-and-reward-video-request)
            * [Determine if the video is ready](#determine-if-the-video-is-ready)
            * [Show Rewarded Video](#show-rewarded-video)
         * [Native Ad](#native-ad)
            * [Init Native Ad](#init-native-ad)
            * [YumiNativeAdOptions](#yuminativeadoptions)
            * [Request Native](#request-native)
            * [Create Your Native Ad Layout](#create-your-native-ad-layout)
            * [Populating your Layout Using the Ad's Metadata](#populating-your-layout-using-the-ads-metadata)
            * [Show Native Ad View](#show-native-ad-view)
            * [Hide Native Ad View](#hide-native-ad-view)
            * [Remove Native Ad View](#remove-native-ad-view)
            * [Destroy Native Ad View](#destroy-native-ad-view)
      * [Debug Mode](#debug-mode)
         * [Call Debug Mode](#call-debug-mode)
      * [Common issues of developer](#common-issues-of-developer)

# YumiMediationSDK for Unity

## Summary

1. To Readers

   This documentation is intended for developers who want to integrate Yumimobi SDK in Unity products.

2. Develop Environment

   - Unity 5.6 and above

   - To deploy to iOS

     Xcode 7.0 or higher

     iOS 8.0 and above

     [CocoaPods](https://guides.cocoapods.org/using/getting-started.html)

   - To deploy to Android

     Android SDK： > 4.1 (API level 16)

     [Demo ](https://github.com/yumimobi/YumiMediationSDK-Unity)   

## Download the YumiMediationSDK Unity plugin

The YumiMediationSDK Unity plugin enables Unity developers to easily serve Yumimobi Ads on Android and iOS apps without having to write Java or Objective-C code. The plugin provides a C# interface for requesting ads that is used by C# scripts in your Unity project. Use the links below to download the Unity package for the plugin or to take a look at its code on GitHub.

[Download the YumiMediationSDK Unity plugin](https://adsdk.yumimobi.com/Unity/3.6.0/YumiMediationSDKPlugin_v3.6.0.unitypackage)

[VIEW SOURCE](https://github.com/yumimobi/YumiMediationSDK-Unity)

## Import the YumiMediationSDK Unity plugin

Open your project in the Unity editor. Select **Assets> Import Package> Custom Package** and find the YumiMediationSDKPlugin.unitypackage file that you downloaded.

![img](resources/01.png)

Make sure all of the files are selected and click **Import**.

![img](resources/02.png)

## Include the YumiMediationSDK

The YumiMediationSDK Unity plugin is distributed with the [Unity Play Services Resolver library](https://github.com/googlesamples/unity-jar-resolver). This library is intended for use by any Unity plugin that requires access to Android specific libraries (e.g., AARs) or iOS CocoaPods. It provides Unity plugins the ability to declare dependencies, which are then automatically resolved and copied into your Unity project.

Follow the steps listed below to ensure your project includes the YumiMediationSDK Unity
### Deploy iOS 

No procedure are required to integrate the YumiMediationSDK into a Unity project.

The YumiMediationSDK Ads Unity plugin dependencies are listed in **Assets/YumiMediationSDK/Editor/YumiMobileAdsDependencies.xml**  .
iOS dependencies：

```xml
    <iosPods>
        <iosPod name="YumiMediationSDK" version="3.6.0" minTargetSdk="8.0">
            <sources>
                <source>https://github.com/CocoaPods/Specs</source>
            </sources>
        </iosPod>
        <!-- adapters -->
        <iosPod name="YumiMediationAdapters/AdColony" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/AdMob" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/AppLovin" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Baidu" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Chartboost" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Domob" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Facebook" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/GDT" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/InMobi" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/IronSource" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Unity" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Vungle" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Mintegral" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/OneWay" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/ZplayAds" version="3.6.0">
        </iosPod>
        <iosPod name="YumiMediationAdapters/IQzone" version="3.6.0">
        </iosPod>
        <!-- debugCenter -->
        <iosPod name="YumiMediationDebugCenter-iOS" version="3.6.0">
        </iosPod>
    </iosPods>
```

e.g., Delete `AdMob`, Delete `<iosPod name="YumiMediationAdapters/AdMob" version="3.6.0"></iosPod>`  

Complete the above procedure, Open **xcworkspace** project.

**Note：Use CocoaPods to identify iOS dependencies. CocoaPods runs as a post-build process step.**

### Deploy Android 

In the Unity editor, select **Assets> Play Services Resolver> Android Resolver>Force Resolve**. The Unity Play Services Resolver library will copy the declared dependencies into the  **Assets/Plugins/Android** directory of your Unity app.

![img](resources/03.png)

The YumiMediationSDK Ads Unity plugin dependencies are listed in **Assets/YumiMediationSDK/Editor/YumiMobileAdsDependencies.xml**.

Android dependencies:

```xml
<androidPackages>
        <androidPackage spec="com.yumimobi.ads:mediation:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:adcolony:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:applovin:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:playableads:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:admob:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:baidu:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:chartboost:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:facebook:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:gdt:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:inmobi:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:oneway:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:vungle:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:ironsource:3.6.0" />
        <androidPackage spec="com.yumimobi.ads.mediation:ksyun:3.6.0">
            <repositories>
                <repository>https://dl.bintray.com/yumimobi/thirdparty/</repository>
            </repositories>
        </androidPackage>
        <androidPackage spec="com.yumimobi.ads.mediation:mintegral:3.6.0" />
        <!--  If your app is only available in mainland China, use unity-china,else use Unity.   -->
        <androidPackage spec="com.yumimobi.ads.mediation:unity:3.6.0" />
       <!-- <androidPackage spec="com.yumimobi.ads.mediation:unity-china:3.6.0" />-->
        <repositories>
            <repository>https://jcenter.bintray.com/</repository>
        </repositories>
</androidPackages>
```
e.g., Delete  `admob`, Delete `<androidPackage spec="com.yumimobi.ads.mediation:admob:3.6.0" />`.

## Select an ad format

The YumiMediationSDK is now included in your Unity app when deploying to either the Android or iOS platform. You're now ready to implement an ad. YumiMediationSDK offers a number of different ad formats, so you can choose the one that best fits your user experience needs.

### Banner

#### Initialize Banner

```c#
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;

public class YumiSDKDemo : MonoBehaviour
{
  private YumiBannerView bannerView;

  void Start()
  {
    this.InitBanner();
  }

  private void InitBanner()
  {
    string  gameVersionId = "YOUR_VERSION_ID";
    string  channelId = "YOUR_CHANNEL_ID";

    #if UNITY_ANDROID
      string bannerPlacementId = "YOUR_BANNER_PLACEMENT_ID_ANDROID";
    #elif UNITY_IOS
      string bannerPlacementId = "YOUR_BANNER_PLACEMENT_ID_IOS";
    #else
      string bannerPlacementId = "unexpected_platform";
    #endif

    // You can set the banner size & banner position & autoRefresh & IsSmart in YumiBannerViewOptions
    // This file is described below.
    YumiBannerViewOptions bannerOptions = new YumiBannerViewOptionsBuilder().Build();
    this.bannerView = new YumiBannerView(BannerPlacementId, ChannelId, GameVersionId, bannerOptions);

    /* banner add ad event */
    this.bannerView.OnAdLoaded    += HandleAdLoaded;
    this.bannerView.OnAdFailedToLoad  += HandleAdFailedToLoad;
    this.bannerView.OnAdClick   += HandleAdClicked;
  }

  #region Banner callback handlers

  public void HandleAdLoaded( object sender, EventArgs args )
  {
    Logger.Log( "HandleAdLoaded event received" );
  }

  public void HandleAdFailedToLoad( object sender, YumiAdFailedToLoadEventArgs args )
  {
    Logger.Log( "HandleFailedToReceiveAd event received with message: " + args.Message );
  }

  public void HandleAdClicked( object sender, EventArgs args )
  {
    Logger.Log( "Handle Ad Clicked" );
  }

  #endregion
}
```

#### Request Banner

```C#
this.bannerView.LoadAd();
```

#### Hide Banner

```C#
this.bannerView.Hide();
```

#### Show Banner

```C#
this.bannerView.Show();
```

#### Destroy Banner

```C#
this.bannerView.Destroy();
```

#### YumiBannerViewOptions

`YumiBannerViewOptions` is the last parameter to init `YumiBannerView`, you can set it in `YumiBannerViewOptions` file.

- `AdPosition`

  Set the position of the banner in the superview.

  default is `BOTTOM`.
  
- `BannerSize`
  
  Set the banner size.

  default:
  - iPhone and iPod Touch ad size. Typically 320x50.
  - Leaderboard size for the iPad. Typically 728x90.

- `IsSmart`

  Set the banner to automatically adapter the screen width.

  default is `true`.

- `DisableAutoRefresh`

  default is `false`. banner will request next ad automatically.

  if you set it to `true`, then you should call `this.bannerView.LoadAd();` by manual.

### Interstitial

#### Initialization and Interstitial request

The interstitial placement will auto cached.

```C#
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
public class YumiSDKDemo : MonoBehaviour 
{
  private YumiInterstitialAd interstitialAd;
  void Start() 
  {
    this.RequestInterstitial();
  }
  private void RequestInterstitial() 
  {
    string gameVersionId = "YOUR_VERSION_ID";
    string channelId = "YOUR_CHANNEL_ID";
    #if UNITY_ANDROID
      string interstitialPlacementId = "YOUR_INTERSTITIAL_PLACEMENT_ID_ANDROID";
    #elif UNITY_IOS
      string interstitialPlacementId = "YOUR_INTERSTITIAL_PLACEMENT_ID_IOS";
    # else
      string interstitialPlacementId = "unexpected_platform";
    #endif
    this.interstitialAd = new YumiInterstitialAd(interstitialPlacementId, channelId, gameVersionId);

    // add interstitial event
    this.interstitialAd.OnAdLoaded += HandleInterstitialAdLoaded;
    this.interstitialAd.OnAdFailedToLoad += HandleInterstitialAdFailedToLoad;
    this.interstitialAd.OnAdClicked += HandleInterstitialAdClicked;
    this.interstitialAd.OnAdClosed += HandleInterstitialAdClosed;
  }
  
  #region interstitial callback handlers
  public void HandleInterstitialAdLoaded(object sender, EventArgs args) 
  {
    Logger.Log("HandleInterstitialAdLoaded event received");
  }
  public void HandleInterstitialAdFailedToLoad(object sender, YumiAdFailedToLoadEventArgs args) 
  {
    Logger.Log("HandleInterstitialAdFailedToLoad event received with message: " + args.Message);
  }
  public void HandleInterstitialAdClicked(object sender, EventArgs args) 
  {
    Logger.Log("HandleInterstitialAdClicked Clicked");
  }
  public void HandleInterstitialAdClosed(object sender, EventArgs args) 
  {
    Logger.Log("HandleInterstitialAdClosed Ad closed");
  }
  #endregion
}
```

#### Show Interstitial

It is recommended to call `this.interstitialAd.IsReady()` to determine if the screen is ready.

```C#
if(this.interstitialAd.IsReady())
{
  this.interstitialAd.Show();
}
```

#### Destroy Interstitial

```c#
this.interstitialAd.Destroy();
```

### Reward Video

#### Initialization and Reward Video request

The reward video placement will auto cached.

```C#
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;
public class YumiSDKDemo : MonoBehaviour 
{
  private YumiRewardVideoAd rewardVideoAd;
  void Start() 
  {
    this.RequestRewardVideo();
  }
  private void RequestRewardVideo() 
  {
    string gameVersionId = "YOUR_VERSION_ID";
    string channelId = "YOUR_CHANNEL_ID";
    #if UNITY_ANDROID
      string rewardVideoPlacementId = "YOUR_REWARDVIDEO_PLACEMENT_ID_ANDROID";
    #elif UNITY_IOS
      string rewardVideoPlacementId = "YOUR_REWARDVIDEO_PLACEMENT_ID_IOS";
    # else
      string rewardVideoPlacementId = "unexpected_platform";
    #endif
    this.rewardVideoAd = YumiRewardVideoAd.Instance;
    this.rewardVideoAd.OnAdOpening += HandleRewardVideoAdOpened;
    this.rewardVideoAd.OnAdStartPlaying += HandleRewardVideoAdStartPlaying;
    this.rewardVideoAd.OnAdRewarded += HandleRewardVideoAdReward;
    this.rewardVideoAd.OnAdClosed += HandleRewardVideoAdClosed;
    // load ad
    this.rewardVideoAd.LoadAd(rewardVideoPlacementId, channelId, gameVersionId);
  }
  
  #region reward video callback handlers
  public void HandleRewardVideoAdOpened(object sender, EventArgs args) 
  {
    Logger.Log("HandleRewardVideoAdOpened event opened");
  }
  public void HandleRewardVideoAdStartPlaying(object sender, EventArgs args) 
  {
    Logger.Log("HandleRewardVideoAdStartPlaying event start playing ");
  }
  public void HandleRewardVideoAdReward(object sender, EventArgs args) 
  {
    Logger.Log("HandleRewardVideoAdReward reward");
  }
  public void HandleRewardVideoAdClosed(object sender, EventArgs args) 
  {
    Logger.Log("HandleRewardVideoAdClosed Ad closed");
  }
  #endregion
}
```

#### Determine if the video is ready

```c#
this.rewardVideoAd.IsReady();
```

#### Show Rewarded Video

```c#
if(this.rewardVideoAd.IsReady())
{
  this.rewardVideoAd.Play();
}
```


### Native Ad

#### Init Native Ad

```C#
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;

public class YumiNativeScene : MonoBehaviour
{
    private YumiNativeAd nativeAd;
    private YumiNativeData yumiNativeData;
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

    // ad panel
    public GameObject adPanel;
  
    void Start()
    {
        this.InitNativeAd();
    }
    private void InitNativeAd()
    {
        string gameVersionId = "YOUR_VERSION_ID";
        string channelId = "YOUR_CHANNEL_ID";
        #if UNITY_ANDROID
          string nativePlacementId = "YOUR_NATIVE_PLACEMENT_ID_ANDROID";
        #elif UNITY_IOS
          string nativePlacementId = "YOUR_NATIVE_PLACEMENT_ID_IOS";
        #else
          string nativePlacementId = "unexpected_platform";
        #endif
        YumiNativeAdOptions options = new NativeAdOptionsBuilder().Build();
        this.nativeAd = new YumiNativeAd(nativePlacementId, channelId, gameVersionId, options);
        // callBack
        this.nativeAd.OnNativeAdLoaded += HandleNativeAdLoaded;
        this.nativeAd.OnAdFailedToLoad += HandleNativeAdFailedToLoad;
        this.nativeAd.OnAdClick += HandleNativeAdClicked;
    }
    #region native call back handles
    public void HandleNativeAdLoaded(object sender, YumiNativeToLoadEventArgs args)
    {
        Logger.Log("HandleNativeAdLoaded event opened");
        if (nativeAd == null)
        {
            Logger.Log("nativeAd is null");
            return;
        }

        if (args == null || args.nativeData == null || args.nativeData.Count == 0)
        {
            Logger.Log("nativeAd data not found.");
            return;
        }
        // args.nativeData is nativeAd data
      	yumiNativeData = args.nativeData[0];
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
```

#### YumiNativeAdOptions

`YumiNativeAdOptions` is the last parameter to init the `YumiNativeAd`, you can set the ad style by this.

```C#
// AdOptionViewPosition: TOP_LEFT,TOP_RIGHT,BOTTOM_LEFT,BOTTOM_RIGHT
internal AdOptionViewPosition adChoiseViewPosition;
// AdAttribution: AdOptionsPosition、text、textColor、backgroundColor、textSize、hide
internal AdAttribution adAttribution;
// TextOptions: textSize，textColor，backgroundColor
internal TextOptions titleTextOptions;
internal TextOptions descTextOptions;
internal TextOptions callToActionTextOptions;
// ScaleType: SCALE_TO_FILL、SCALE_ASPECT_FIT、SCALE_ASPECT_FILL
internal ScaleType iconScaleType;
internal ScaleType coverImageScaleType;
```

#### Request Native

```C#
int adCount = 1;// adCount: you can load more than one ad
this.nativeAd.LoadAd(adCount);
```

#### Create Your Native Ad Layout

```C#
public class YumiNativeScene : MonoBehaviour
  {
    private YumiNativeAd nativeAd;
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
    /// ...
  }
```

Here is how they can be associated with the views in the editor:

![image](./resources/nativeAd.png)

#### Populating Your Layout Using the Ad's Metadata

```C#
public class YumiNativeScene : MonoBehaviour
{
  private YumiNativeAd nativeAd;
  private YumiNativeData yumiNativeData;
  private void RegisterNativeViews()
    {
        Dictionary<NativeElemetType, Transform> elementsDictionary = new Dictionary<NativeElemetType, Transform>();
        elementsDictionary.Add(NativeElemetType.PANEL, adPanel.transform);
        elementsDictionary.Add(NativeElemetType.TITLE, title.transform);
        elementsDictionary.Add(NativeElemetType.DESCRIPTION, body.transform);
        elementsDictionary.Add(NativeElemetType.ICON, iconImage.transform);
        elementsDictionary.Add(NativeElemetType.COVER_IMAGE, mediaView.transform);
        elementsDictionary.Add(NativeElemetType.CALL_TO_ACTION, callToActionButton.transform);
        // This is a method to associate a YumiNativeData with the ad assets gameobject you will use to display the native ads.
        this.nativeAd.RegisterGameObjectsForInteraction(yumiNativeData, gameObject, elementsDictionary);

    }
}
```

#### Show Native Ad View

You should check whether the ad has been invalidated before displaying it.

```C#
// Determines whether nativeAd data is invalidated, if invalidated please reload
if (this.nativeAd.IsAdInvalidated(yumiNativeData))
  {
      Logger.Log("Native Data is invalidated");
      return;
  }
  this.nativeAd.ShowView(yumiNativeData);
```

#### Hide Native Ad View

```C#
this.nativeAd.HideView(yumiNativeData);// Hide nativeAd data associate view
```

#### Remove Native Ad View

```C#
this.nativeAd.UnregisterView(yumiNativeData);
```

#### Destroy Native Ad View

```C#
this.nativeAd.Destroy();
```

## Debug Mode

Please select debug mode if you want to test whether ad ruturn is available for an app. 

### Call Debug Mode

```C#
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;

public class YumiSDKDemo : MonoBehaviour
{
   private YumiDebugCenter debugCenter;
  
   private void CallDebugCenter(){
        if (this.debugCenter == null)
        {
            this.debugCenter = new YumiDebugCenter();
        }
        // Note: Fill in the ad slot information to distinguish between iOS and Android
        this.debugCenter.PresentYumiMediationDebugCenter("YOUR_BANNER_PLACEMENT_ID", "YOUR_INTERSTITIAL_PLACEMENT_ID", "YOUR_REWARDVIDEO_PLACEMENT_ID", "YOUR_CHANNEL_ID", "YOUR_VERSION_ID");
    }
}
```

## Common issues of developer 

**1.Apps with more than 64K (65536) Android configuration methods**

Reference Android official solution，[Click to view](https://developer.android.com/studio/build/multidex)


**2.Test ad placementIDs**

| Platform | Banner   | Interstitial | Rewarded Video |
| -------- | -------- | ------------ | -------------- |
| iOS      | l6ibkpae | onkkeg5i     | 5xmpgti4       |
| Android  | uz852t89 | 56ubk22h     | ew9hyvl4       |

