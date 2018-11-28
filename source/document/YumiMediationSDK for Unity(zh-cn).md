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
      * [Interstitial](#interstitial)
         * [初始化及请求插屏](#初始化及请求插屏)
         * [展示 Interstitial](#展示-interstitial)
         * [销毁 Interstitial](#销毁-interstitial)
      * [Rewarded Video](#rewarded-video)
         * [初始化及请求视频](#初始化及请求视频)
         * [判断视频是否准备好](#判断视频是否准备好)
         * [展示 Rewarded Video](#展示-rewarded-video)
         * [销毁 Rewarded Video](#销毁-rewarded-video)
   * [调试模式](#调试模式)
      * [调用调试模式](#调用调试模式)

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

[下载YumiMediationSDK Unity插件]()

[查看源码](https://github.com/yumimobi/YumiMediationSDK-Unity)

## 导入 YumiMediationSDK Unity 插件

在 Unity 编辑器中打开您的项目。选择**Assets> Import Package> Custom Package**，找到您下载的 YumiMediationSDKPlugin.unitypackage 文件。

![img](resources/01.png)

确保选中所有文件，然后单击 **Import**.

![img](resources/02.png)

## 集成 YumiMediationSDK

YumiMediationSDK Unity 插件随着 [Unity Play Services Resolver library](https://github.com/googlesamples/unity-jar-resolver) 一起发布。该库主要供访问 Android 特定库（例如，AAR）或 iOS CocoaPods 的任何 Unity 插件使用。它为 Unity 插件提供了声明依赖关系的能力，然后自动解析并复制到 Unity 项目中。请按照下面列出的步骤确保您的项目包含 YumiMediationSDK。

### 部署 iOS 项目

将 YumiMediationSDK 集成到 Unity 项目中无需其他步骤。

构建完成，打开 **xcworkspace** 工程。

**注意：使用 CocoaPods 识别 iOS 依赖项。 CocoaPods 作为后期构建过程步骤运行。**

### 部署 Android 项目

在 Unity 编辑器中，选择 **Assets> Play Services Resolver> Android Resolver>Force Resolve**。 Unity Play 服务解析器库会将声明的依赖项复制到 Unity 应用程序的 **Assets/Plugins/Android** 目录中。

![img](resources/03.png)



注意: YumiMediationSDK Unity 插件依赖项列在 **Assets/YumiMediationSDK/Editor/YumiMobileAdsDependencies.xml** 中

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
    if ( this.bannerView != null )
    {
      this.bannerView.Destroy();
    }

    this.bannerView = new YumiBannerView( bannerPlacementId, channelId, gameVersionId, YumiAdPosition.Bottom );

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
bool IsSmartBanner = true;//如果设置 isSmartBanner 为 YES ,YumiMediationBannerView 将会自动根据设备的尺寸进行适配。
this.bannerView.LoadAd(IsSmartBanner); 
```

#### 隐藏 Banner

```C#
this.bannerView.Hide();
```

#### 显示隐藏的 Banner

```C#
this.bannerView.Show();
```

#### 销毁 Banner

```C#
this.bannerView.Destroy();
```

### Interstitial

#### 初始化及请求插屏

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
    if (this.interstitialAd != null) 
    {
      this.interstitialAd.DestroyInterstitial();
    }
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

建议先调用```this.interstitialAd.IsInterstitialReady()```判断插屏是否准备好

```C#
 if(this.interstitialAd.IsInterstitialReady())
 {
  this.interstitialAd.ShowInterstitial();
 }
```

#### 销毁 Interstitial

```c#
this.interstitialAd.DestroyInterstitial();
```

### Rewarded Video

#### 初始化及请求视频

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
    if (this.rewardVideoAd != null) 
    {
      this.rewardVideoAd.DestroyRewardVideo();
    }
    this.rewardVideoAd = new YumiRewardVideoAd();
    this.rewardVideoAd.OnAdOpening += HandleRewardVideoAdOpened;
    this.rewardVideoAd.OnAdStartPlaying += HandleRewardVideoAdStartPlaying;
    this.rewardVideoAd.OnAdRewarded += HandleRewardVideoAdReward;
    this.rewardVideoAd.OnAdClosed += HandleRewardVideoAdClosed;
    // load ad
    this.rewardVideoAd.LoadRewardVideoAd(rewardVideoPlacementId, channelId, gameVersionId);
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
 this.rewardVideoAd.IsRewardVideoReady();
```

#### 展示 Rewarded Video

```c#
 if(this.rewardVideoAd.IsRewardVideoReady())
 {
  this.rewardVideoAd.PlayRewardVideo();
 } 
```

#### 销毁 Rewarded Video

```c#
this.rewardVideoAd.DestroyRewardVideo();
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
        this.debugCenter.PresentYumiMediationDebugCenter("YOUR_BANNER_PLACEMENT_ID", "YOUR_INTERSTITIAL_PLACEMENT_ID", "YOUR_REWARDVIDEO_PLACEMENT_ID", "YOUR_CHANNEL_ID", "YOUR_VERSION_ID");
    }
}
```







