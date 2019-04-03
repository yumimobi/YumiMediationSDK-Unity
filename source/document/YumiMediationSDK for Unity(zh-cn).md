   * [YumiMediationSDK for Unity](#yumimediationsdk-for-unity)
      * [概述](#概述)
      * [下载 YumiMediationSDK Unity 插件](#下载-yumimediationsdk-unity-插件)
      * [导入 YumiMediationSDK Unity 插件](#导入-yumimediationsdk-unity-插件)
      * [集成 YumiMediationSDK](#集成-yumimediationsdk)
         * [部署 iOS 项目](#部署-ios-项目)
         * [部署 Android 项目](#部署-android-项目)
      * [选择广告形式](#选择广告形式)
         * [Banner](#banner)
            * [初始化 Banner](#初始化-banner)
            * [请求 Banner](#请求-banner)
            * [隐藏 Banner](#隐藏-banner)
            * [显示隐藏的 Banner](#显示隐藏的-banner)
            * [销毁 Banner](#销毁-banner)
            * [YumiBannerViewOptions](#yumibannerviewoptions)
         * [Interstitial](#interstitial)
            * [初始化及请求插屏](#初始化及请求插屏)
            * [展示 Interstitial](#展示-interstitial)
            * [销毁 Interstitial](#销毁-interstitial)
         * [Rewarded Video](#rewarded-video)
            * [初始化及请求视频](#初始化及请求视频)
            * [判断视频是否准备好](#判断视频是否准备好)
            * [展示 Rewarded Video](#展示-rewarded-video)
         * [Native](#native)
            * [初始化 Native](#初始化-native)
            * [YumiNativeAdOptions](#yuminativeadoptions) 
            * [请求 Native](#请求-native)
            * [创建原生广告布局](#创建原生广告布局)
            * [使用广告元数据注册布局](#使用广告元数据注册布局)
            * [展示 Native View](#展示-native-view)
            * [隐藏 Native View](#隐藏-native-view)
            * [移除 Native View](#移除-native-view)
            * [销毁 Native](#销毁-native)
      * [调试模式](#调试模式)
         * [调用调试模式](#调用调试模式)
         * [图示](#图示) 
      * [常见问题](#常见问题)
        *  [1 TEST ID](#1-TEST-ID) 
        *  [2 Android打包失败](#2-Android-打包失败)
        

# YumiMediationSDK for Unity

## 概述

1. 面向人群

   本产品主要面向需要在 Unity 产品中接入玉米移动广告 SDK 的开发人员。

2. 先决条件

   - Unity 5.6 或更高版本

   - 部署 iOS
     
     Xcode 7.0 或更高版本
     
     iOS 8.0 或更高版本

     [CocoaPods](https://guides.cocoapods.org/using/getting-started.html)

   - 部署 Android

     Android SDK： > 4.1 (API level 16)

3. [Demo 获取地址](https://github.com/yumimobi/YumiMediationSDK-Unity)   

## 下载 YumiMediationSDK Unity 插件

Yumi 聚合广告 Unity 插件使 Unity 开发人员可以轻松地在 Android 和 iOS 应用上展示广告，无需编写 Java 或 Objective-C 代码。该插件提供了一个 C# 接口来请求广告。使用下面的链接下载插件的 Unity 包或在 GitHub 上查看其代码。

[下载YumiMediationSDK Unity插件](https://adsdk.yumimobi.com/Unity/3.6.3/YumiMediationSDKPlugin_v3.6.3.unitypackage)

[查看源码](https://github.com/yumimobi/YumiMediationSDK-Unity)

## 导入 YumiMediationSDK Unity 插件

在 Unity 编辑器中打开您的项目。选择**Assets> Import Package> Custom Package**，找到您下载的 YumiMediationSDKPlugin.unitypackage 文件。

![img](resources/01.png)

确保选中所有文件，然后单击 **Import**.

![img](resources/02.png)

## 集成 YumiMediationSDK

YumiMediationSDK Unity 插件随着 [Unity Play Services Resolver library](https://github.com/googlesamples/unity-jar-resolver) 一起发布。这个库适用于任何需要访问 Android 特定库(例如 AARs )或 iOS CocoaPods 的 Unity 插件。它为 Unity 插件提供了声明依赖关系的能力，然后自动解析并复制到 Unity 项目中。请按照下面列出的步骤确保您的项目包含 YumiMediationSDK。

### 部署 iOS 项目

将 YumiMediationSDK 集成到 Unity 项目中无需其他步骤。

如果你想要修改 YumiMediationSDK 依赖的库，请修改 **Assets/YumiMediationSDK/Editor/YumiMobileAdsDependencies.xml**  文件，iOS 依赖如下：

```xml
    <iosPods>
        <iosPod name="YumiMediationSDK" version="3.6.3" minTargetSdk="8.0">
            <sources>
                <source>https://github.com/CocoaPods/Specs</source>
            </sources>
        </iosPod>
        <!-- adapters -->
        <iosPod name="YumiMediationAdapters/AdColony" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/AdMob" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/AppLovin" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Baidu" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Chartboost" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Domob" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Facebook" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/GDT" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/InMobi" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/IronSource" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Unity" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Vungle" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/Mintegral" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/OneWay" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/ZplayAds" version="3.6.3">
        </iosPod>
        <iosPod name="YumiMediationAdapters/IQzone" version="3.6.3">
        </iosPod>
        <!-- debugCenter -->
        <iosPod name="YumiMediationDebugCenter-iOS" version="3.6.3">
        </iosPod>
    </iosPods>
```

比如删除 `AdMob` ，直接删除 ` <iosPod name="YumiMediationAdapters/AdMob" version="3.6.3"></iosPod>`  即可。

构建完成，打开 **xcworkspace** 工程。

**注意：使用 CocoaPods 识别 iOS 依赖项。 CocoaPods 作为后期构建过程步骤运行。**

### 部署 Android 项目

在 Unity 编辑器中，选择 **Assets> Play Services Resolver> Android Resolver>Force Resolve**。 Unity Play 服务解析器库会将声明的依赖项复制到 Unity 应用程序的 **Assets/Plugins/Android** 目录中。

![img](resources/03.png)

如果你想要修改 YumiMediationSDK 依赖的库，请修改 **Assets/YumiMediationSDK/Editor/YumiMobileAdsDependencies.xml**  文件，Android 依赖如下：

```xml
<androidPackages>
  <androidPackage spec="com.yumimobi.ads:mediation:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:adcolony:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:applovin:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:playableads:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:admob:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:baidu:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:chartboost:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:facebook:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:gdt:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:inmobi:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:oneway:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:vungle:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:ironsource:3.6.1" />
  <androidPackage spec="com.yumimobi.ads.mediation:iqzone:3.6.1">
      <repositories>
          <repository>https://dl.bintray.com/yumimobi/thirdparty/</repository>
          <repository>https://dl.bintray.com/yumimobi/ads/</repository>
          <repository>https://s3.amazonaws.com/moat-sdk-builds/</repository>
      </repositories>
  </androidPackage>

  <androidPackage spec="com.yumimobi.ads.mediation:ksyun:3.6.1" >
      <repositories>
          <repository>https://dl.bintray.com/yumimobi/thirdparty/</repository>
      </repositories>
  </androidPackage>
  <androidPackage spec="com.yumimobi.ads.mediation:mintegral:3.6.1" />
  <!--  If your app is only available in mainland China, use unity-china,else use Unity.   -->
  <androidPackage spec="com.yumimobi.ads.mediation:unity:3.6.1" />
  <!-- <androidPackage spec="com.yumimobi.ads.mediation:unity-china:3.6.1" />-->
  <repositories>
      <repository>https://jcenter.bintray.com/</repository>
      <repository>https://maven.google.com/</repository>
  </repositories>
</androidPackages>
```

比如删除 `admob`，直接删除 `<androidPackage spec="com.yumimobi.ads.mediation:admob:3.6.1" />` 即可。

## 选择广告形式

在部署到 Android 或 iOS 平台时，YumiMediationSDK 现在包含在 Unity 应用程序中。您现在已准备好实施广告。YumiMediationSDK 提供多种不同的广告格式，因此您可以选择最适合您的用户体验需求的广告格式。

### Banner

#### 初始化 Banner

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
    
    // YumiBannerViewOptions 可以配置 Banner 位置、大小、是否自适应和是否自刷新
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

#### 请求 Banner

```C#
this.bannerView.LoadAd(); 
```

#### 隐藏 Banner

```C#
this.bannerView.Hide();
```

#### 显示隐藏的 Banner

```C#
this.bannerView.Show();
```

####  销毁 Banner

```c#
this.bannerView.Destroy();
```
#### YumiBannerViewOptions
`YumiBannerViewOptions` 是初始化 `YumiBannerView` 时传入的最后一个参数，您可在 `YumiBannerViewOptions` 文件中进行设置：

- `AdPosition`

  设置 banner 广告位所处父视图的位置。默认为下方，居中显示。

- `BannerSize`

  设置 banner 广告的尺寸。

  在 iPhone 及 iPod Touch 上默认为 320 * 50。

  在 iPad 上默认为 728 * 90。

- `IsSmart`

  默认为 true。

  如果设置为 true，banner 会自适应屏幕宽度。

  如果设置为 false，banner 会展示广告位自身尺寸。

- `DisableAutoRefresh`

  默认为 false。
  
  如果设置为 false，banner 会自动请求下一条广告，您无需重复调用 `this.bannerView.LoadAd(); `。

  如果设置为 true，banner 不会进行下一次请求，您必须在恰当的时机再次调用 `this.bannerView.LoadAd();`。

### Interstitial

#### 初始化及请求插屏
插屏广告位会自动加载下一条广告，您无需重复调用
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

#### 展示 Interstitial

建议先调用 `this.interstitialAd.IsReady()` 判断插屏是否准备好

```C#
 if(this.interstitialAd.IsReady())
 {
  this.interstitialAd.Show();
 }
```

#### 销毁 Interstitial

```c#
this.interstitialAd.Destroy();
```

### Rewarded Video

#### 初始化及请求视频
视频广告位会自动加载下一条广告，您无需重复调用。
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

#### 判断视频是否准备好

```c#
 this.rewardVideoAd.IsReady();
```

#### 展示 Rewarded Video

```c#
 if(this.rewardVideoAd.IsReady())
 {
  this.rewardVideoAd.Play();
 } 
```

### Native

#### 初始化 Native

```c#
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

`YumiNativeAdOptions` 是初始化 `YumiNativeAd` 的最后一个参数，可以配置原生广告显示的样式，参数详情如下：

```c#
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

#### 请求 Native

```c#
int adCount = 1;// adCount: you can load more than one ad
this.nativeAd.LoadAd(adCount);
```

#### 创建原生广告布局

```c#
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

以下说明这些元素如何与编辑器中的视图关联：

![image](./resources/nativeAd.png)

#### 使用广告元数据注册布局

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

#### 展示 Native View

```C#
// Determines whether nativeAd data is invalidated, if invalidated please reload
if (this.nativeAd.IsAdInvalidated(yumiNativeData))
  {
      Logger.Log("Native Data is invalidated");
      return;
  }
  this.nativeAd.ShowView(yumiNativeData);
```

- 注意：显示广告前，您必须注册布局并检查广告是否已经无效。

#### 隐藏 Native View

```c#
this.nativeAd.HideView(yumiNativeData);// Hide nativeAd data associate view 
```

#### 移除 Native View

```c#
this.nativeAd.UnregisterView(yumiNativeData);
```

此方法的作用：从屏幕上移除当前广告视图，断开 View 和 广告元数据的注册。在显示一个新广告时，请先调用这个方法。

#### 销毁 Native

```c#
this.nativeAd.Destroy();
```

## 调试模式

如果您想调试平台 key 是否有广告返回，可选择调试模式。 

### 调用调试模式

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
        // 注意：填写的广告位信息要区分iOS和Android
        this.debugCenter.PresentYumiMediationDebugCenter("YOUR_BANNER_PLACEMENT_ID", "YOUR_INTERSTITIAL_PLACEMENT_ID", "YOUR_REWARDVIDEO_PLACEMENT_ID", "YOUR_NATIVE_PLACEMENT_ID","YOUR_CHANNEL_ID", "YOUR_VERSION_ID");
    }
}
```

### 图示

以 iOS 平台为例（Android 平台逻辑相同，UI 不同）。

<div align="center"><img width="200" height="352" src="resources/debug-1.png"/></div>

*<p align="center" size=1>选择平台类型</p>*

<div align="center"><img width="200" height="352" src="resources/debug-2.png"/></div>

*<p align="center" size=1>选择单一平台进行调试<br>如果您需要的平台未在列表中，则代表此平台未添加至工程中<br>绿色平台为已添加至工程并且已配置<br>灰色平台为已添加至工程但未配置</p>*

<div align="center"><img width="200" height="352" src="resources/debug-3.png"/></div>

*<p align="center" size=1>选择广告类型，调试单一平台</p>*

## 常见问题

### 1 TEST ID
 

| 系统    | 广告类型       | Slot(Placement) ID | 备注                                                                                                                                       |
| ------- | -------------- | ------------------ | ------------------------------------------------------------------------------------------------------------------------------------------ |
| Android | Banner         | uz852t89           | YUMI,AdMob,APPlovin,Baidu,IQzone 使用此test id,以上Network平台可测试到对应平台广告                                                         |
| Android | Interstitial   | 56ubk22h           | YUMI,AdMob,APPlovin,Baidu,IronSource,Inmobi,IQzone, untiy Ads，vungle, ZPLAYAds 使用此test id,以上Network平台可测试到对应平台广告          |
| Android | Rewarded Video | ew9hyvl4           | YUMI,AdMob,APPlovin,GDTMob,IronSource,Inmobi,IQzone, untiy Ads，vungle, ZPLAYAds 使用此test id,以上Network平台可测试到对应平台广告         |
| Android | Native         | dt62rndy           | YUMI,AdMob,Baidu,GDTMob,Facebook 使用此test id,以上Network平台可测试到对应平台广告                                                         |
| iOS     | Banner         | l6ibkpae           | YUMI,AdMob,APPlovin,Baidu,Facebook,GDTMob  使用此test id,以上Network平台可测试到对应平台广告                                               |
| iOS     | Interstitial   | onkkeg5i           | YUMI,AdMob,Baidu,Chartboost,GDTMob,IronSource,Inmobi,IQzone, untiy Ads，vungle, ZPLAYAds 使用此test id,以上Network平台可测试到对应平台广告 |
| iOS     | Rewarded Video | 5xmpgti4           | YUMI,AdMob,Adcolony, APPlovin,IronSource,Inmobi,Mintegral, untiy Ads，vungle, ZPLAYAds 使用此test id,以上Network平台可测试到对应平台广告   |
| iOS     | Native         | atb3ke1i           | YUMI,AdMob,Baidu,GDTMob,Facebook 使用此test id,以上Network平台可测试到对应平台广告                                                         |


### 2 Android 打包失败
#### 2.1 Failed to find Build Tools...
```
* What went wrong:
A problem occurred configuring root project 'gradleOut'.
> Failed to find Build Tools revision 29.0.0
```

**解决方法**

从 [mainTemplet](../../Assets/Plugins/Android/mainTemplate.gradle) 中删除 `buildToolsVersion '**BUILDTOOLS**'` 

#### 2.2 No toolchains found...
```
* What went wrong:
A problem occurred configuring root project 'gradleOut'.
> No toolchains found in the NDK toolchains folder for ABI with prefix: mips64el-linux-android
```

**解决方法**

修改 [mainTemplet](../../Assets/Plugins/Android/mainTemplate.gradle) 中 gradle plugin 版本，如将 `classpath 'com.android.tools.build:gradle:3.0.1'` 修改为 `classpath 'com.android.tools.build:gradle:3.2.1'`。

#### 2.3 Failed to apply plugin...
```
* What went wrong:
A problem occurred evaluating root project 'gradleOut'.
> Failed to apply plugin [id 'com.android.application']
   > Minimum supported Gradle version is 4.6. Current version is 4.2.1. If using the gradle wrapper, try editing the distributionUrl in
```

**解决方法（以下方法任选一个即可）**

1. 升级 gradle 版本至 4.6
2. 降级 gradle plugin 版本至 gradle 4.2.1 对应的版本。对照 [Update Gradle](https://developer.android.com/studio/releases/gradle-plugin#updating-gradle) 文档可知需要将 [mainTemplet](../../Assets/Plugins/Android/mainTemplate.gradle) 中 `classpath 'com.android.tools.build:gradle:x.x.x'` 修改为 `classpath 'com.android.tools.build:gradle:3.0.0+`