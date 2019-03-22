- [YumiMediationSDK for Unity](#yumimediationsdk-for-unity)
  - [概述](#%E6%A6%82%E8%BF%B0)
  - [下载 YumiMediationSDK Unity 插件](#%E4%B8%8B%E8%BD%BD-yumimediationsdk-unity-%E6%8F%92%E4%BB%B6)
  - [导入 YumiMediationSDK Unity 插件](#%E5%AF%BC%E5%85%A5-yumimediationsdk-unity-%E6%8F%92%E4%BB%B6)
  - [集成 YumiMediationSDK](#%E9%9B%86%E6%88%90-yumimediationsdk)
    - [部署 iOS 项目](#%E9%83%A8%E7%BD%B2-ios-%E9%A1%B9%E7%9B%AE)
    - [部署 Android 项目](#%E9%83%A8%E7%BD%B2-android-%E9%A1%B9%E7%9B%AE)
  - [选择广告形式](#%E9%80%89%E6%8B%A9%E5%B9%BF%E5%91%8A%E5%BD%A2%E5%BC%8F)
    - [Banner](#banner)
      - [初始化 Banner](#%E5%88%9D%E5%A7%8B%E5%8C%96-banner)
      - [请求 Banner](#%E8%AF%B7%E6%B1%82-banner)
      - [隐藏 Banner](#%E9%9A%90%E8%97%8F-banner)
      - [显示隐藏的 Banner](#%E6%98%BE%E7%A4%BA%E9%9A%90%E8%97%8F%E7%9A%84-banner)
      - [销毁 Banner](#%E9%94%80%E6%AF%81-banner)
    - [Interstitial](#interstitial)
      - [初始化及请求插屏](#%E5%88%9D%E5%A7%8B%E5%8C%96%E5%8F%8A%E8%AF%B7%E6%B1%82%E6%8F%92%E5%B1%8F)
      - [展示 Interstitial](#%E5%B1%95%E7%A4%BA-interstitial)
      - [销毁 Interstitial](#%E9%94%80%E6%AF%81-interstitial)
    - [Rewarded Video](#rewarded-video)
      - [初始化及请求视频](#%E5%88%9D%E5%A7%8B%E5%8C%96%E5%8F%8A%E8%AF%B7%E6%B1%82%E8%A7%86%E9%A2%91)
      - [判断视频是否准备好](#%E5%88%A4%E6%96%AD%E8%A7%86%E9%A2%91%E6%98%AF%E5%90%A6%E5%87%86%E5%A4%87%E5%A5%BD)
      - [展示 Rewarded Video](#%E5%B1%95%E7%A4%BA-rewarded-video)
    - [Native](#native)
      - [初始化 Native](#%E5%88%9D%E5%A7%8B%E5%8C%96-native)
      - [请求 Native](#%E8%AF%B7%E6%B1%82-native)
      - [创建原生广告布局](#%E5%88%9B%E5%BB%BA%E5%8E%9F%E7%94%9F%E5%B9%BF%E5%91%8A%E5%B8%83%E5%B1%80)
      - [使用广告元数据注册布局](#%E4%BD%BF%E7%94%A8%E5%B9%BF%E5%91%8A%E5%85%83%E6%95%B0%E6%8D%AE%E6%B3%A8%E5%86%8C%E5%B8%83%E5%B1%80)
      - [展示 Native View](#%E5%B1%95%E7%A4%BA-native-view)
      - [隐藏 Native View](#%E9%9A%90%E8%97%8F-native-view)
      - [移除 Native View](#%E7%A7%BB%E9%99%A4-native-view)
      - [销毁 Native](#%E9%94%80%E6%AF%81-native)
  - [调试模式](#%E8%B0%83%E8%AF%95%E6%A8%A1%E5%BC%8F)
    - [调用调试模式](#%E8%B0%83%E7%94%A8%E8%B0%83%E8%AF%95%E6%A8%A1%E5%BC%8F)
  - [常见问题](#%E5%B8%B8%E8%A7%81%E9%97%AE%E9%A2%98)


# YumiMediationSDK for Unity

## 概述

1.面向人群

本产品主要面向需要在 Unity 产品中接入玉米移动广告 SDK 的开发人员。

2.先决条件

- Unity 5.6 或更高版本


- 部署 iOS

   Xcode 7.0 或更高版本

   iOS 8.0 或更高版本

   [CocoaPods](https://guides.cocoapods.org/using/getting-started.html)

- 部署 Android

  Android SDK： > 4.1 (API level 16)

3.[Demo 获取地址](https://github.com/yumimobi/YumiMediationSDK-Unity)   

## 下载 YumiMediationSDK Unity 插件

Yumi 聚合广告 Unity 插件使 Unity 开发人员可以轻松地在 Android 和 iOS 应用上展示广告，无需编写 Java 或 Objective-C 代码。该插件提供了一个 C# 接口来请求广告。使用下面的链接下载插件的 Unity 包或在 GitHub 上查看其代码。

[下载YumiMediationSDK Unity插件](https://adsdk.yumimobi.com/Unity/3.6.0/YumiMediationSDKPlugin_v3.6.0.unitypackage)

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

比如删除 ```AdMob``` ，直接删除 ``` <iosPod name="YumiMediationAdapters/AdMob" version="3.6.0" />```  即可。

构建完成，打开 **xcworkspace** 工程。

**注意：使用 CocoaPods 识别 iOS 依赖项。 CocoaPods 作为后期构建过程步骤运行。**

### 部署 Android 项目

在 Unity 编辑器中，选择 **Assets> Play Services Resolver> Android Resolver>Force Resolve**。 Unity Play 服务解析器库会将声明的依赖项复制到 Unity 应用程序的 **Assets/Plugins/Android** 目录中。

![img](resources/03.png)

如果你想要修改 YumiMediationSDK 依赖的库，请修改 **Assets/YumiMediationSDK/Editor/YumiMobileAdsDependencies.xml**  文件，Android 依赖如下：

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

比如删除  ```admob```，直接删除 ```<androidPackage spec="com.yumimobi.ads.mediation:admob:3.6.0" />```  即可。

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
    this.bannerView.OnAdLoaded    += this.HandleAdLoaded;
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

建议先调用```this.interstitialAd.IsReady()```判断插屏是否准备好

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

YumiNativeAdOptions 可以配置原生广告显示的样式，参数详情如下：

```c#
internal AdOptionViewPosition adChoiseViewPosition;//AdOptionViewPosition: TOP_LEFT,TOP_RIGHT,BOTTOM_LEFT,BOTTOM_RIGHT
internal AdAttribution adAttribution;//AdAttribution: AdOptionsPosition、text、textColor、backgroundColor、textSize、hide
//TextOptions: textSize，textColor，backgroundColor
internal TextOptions titleTextOptions;
internal TextOptions descTextOptions;
internal TextOptions callToActionTextOptions;
// ScaleType: SCALE_TO_FILL、SCALE_ASPECT_FIT 、 SCALE_ASPECT_FILL
internal ScaleType iconScaleType;
internal ScaleType coverImageScaleType;
```

#### 请求 Native

```c#
int adCount = 1;// adCount is your request ad count
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

注意：显示广告前，您必须注册布局并检查广告是否已经无效。

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

## 常见问题

**1.Android 配置方法数超过 64K(65536) 的应用**

参考 Android 官方解决方案，[点击查看](https://developer.android.com/studio/build/multidex)


**2.测试广告位**

| 平台      | Banner   | Interstitial | Rewarded Video | Native   |
| ------- | -------- | ------------ | -------------- | -------- |
| iOS     | l6ibkpae | onkkeg5i     | 5xmpgti4       | atb3ke1i |
| Android | uz852t89 | 56ubk22h     | ew9hyvl4       | dt62rndy |

