//
//  YumiTypes.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/19.
//

/// Base type representing a GADU* pointer.
typedef const void *YumiTypeRef;
#pragma mark - banner ads
/// Type representing a Unity banner client.
typedef const void *YumiTypeBannerClientRef;
/// Type representing a YumiBanner.
typedef const void *YumiTypeBannerRef;

#pragma mark - banner ads callback
/// Callback for when a banner ad request was successfully loaded.
typedef void (*YumiBannerDidReceiveAdCallback)(YumiTypeBannerClientRef *bannerClient);

/// Callback for when a banner ad request failed.
typedef void (*YumiBannerDidFailToReceiveAdWithErrorCallback)(YumiTypeBannerClientRef *bannerClient, const char *error);

/// Callback for when a full screen view is about to be presented as a result of a banner click.
typedef void (*YumiBannerDidClickCallback)(YumiTypeBannerClientRef *bannerClient);

#pragma mark - Interstitial ads
/// Type representing a Unity interstitial client.
typedef const void *YumiTypeInterstitialClientRef;
/// Type representing a YumiInterstitialAd.
typedef const void *YumiTypeInterstitialRef;

#pragma mark - Interstitial ads callback
///Callback for when a interstitial ad request was successfully loaded.
typedef void (*YumiInterstitialDidReceiveAdCallback)(YumiTypeInterstitialClientRef *interstitial);
/// Callback for when an interstitial ad request failed.
typedef void (*YumiInterstitialDidFailToReceiveAdWithErrorCallback)(YumiTypeInterstitialClientRef *interstitial, const char *error);

/// Callback for when an  interstitial has clicked.
typedef void (*YumiInterstitialDidClickCallback)(YumiTypeInterstitialClientRef *interstitial);
/// Callback for when an interstitial has just been closed.
typedef void (*YumiInterstitialDidCloseCallback)(YumiTypeInterstitialClientRef *interstitial);
// v4.0.0
/// Callback for when an interstitial ad show fail
typedef void (*YumiInterstitialDidFailToShowAdWithErrorCallback)(YumiTypeInterstitialClientRef *interstitial, const char *error);
/// Callback for when an  interstitial has been opened
typedef void (*YumiInterstitialDidOpenCallback)(YumiTypeInterstitialClientRef *interstitial);
/// Callback for when an  interstitial has start playing.
typedef void (*YumiInterstitialDidStartPlayingCallback)(YumiTypeInterstitialClientRef *interstitial);

#pragma mark - RewardVideo ads
/// Type representing a Unity RewardVideo client.
typedef const void *YumiTypeRewardVideoClientRef;
/// Type representing a YumiRewardVideoAd.
typedef const void *YumiTypeRewardVideoRef;

#pragma mark - RewardVideo ads callback
///Callback for when a rewardVideo ad has been opened
typedef void (*YumiRewardVideoDidOpenAdCallback)(YumiTypeRewardVideoClientRef *rewardVideo);
/// Callback for when an rewardVideo ad has been start playing
typedef void (*YumiRewardVideoDidStartPlayingCallback)(YumiTypeRewardVideoClientRef *rewardVideo);

/// Callback for when an  rewardVideo ad has been reward
typedef void (*YumiRewardVideoDidRewardCallback)(YumiTypeRewardVideoClientRef *rewardVideo);

// v4.0.0

/// Callback for when an rewardVideo has been closed.
typedef void (*YumiRewardVideoDidCloseCallback)(YumiTypeRewardVideoClientRef *rewardVideo, BOOL isRewarded);

/// Callback for when an rewardVideo has just been load.
typedef void (*YumiRewardVideoDidReceiveAdCallback)(YumiTypeRewardVideoClientRef *rewardVideo);
/// Callback for when an rewardVideo ad request failed.
typedef void (*YumiRewardVideoDidFailToReceiveAdWithErrorCallback)(YumiTypeRewardVideoClientRef *rewardVideo, const char *error);
/// Callback for when an rewardVideo ad show fail
typedef void (*YumiRewardVideoDidFailToShowAdWithErrorCallback)(YumiTypeRewardVideoClientRef *rewardVideo, const char *error);
/// Callback for when an rewardVideo has been clicked.
typedef void (*YumiRewardVideoDidClickAdCallback)(YumiTypeRewardVideoClientRef *rewardVideo);

#pragma mark - native ads
/// Type representing a Unity native client.
typedef const void *YumiTypeNativeClientRef;
/// Type representing a YumiNative.
typedef const void *YumiTypeNativeAdRef;
#pragma mark - native call back
/// Callback for when a native ad request was successfully loaded.
typedef void (*YumiNativeAdDidReceiveAdCallback)(YumiTypeNativeClientRef *nativeClient ,  const char *adKeys);

/// Callback for when a native ad request failed.
typedef void (*YumiNativeAdDidFailToReceiveAdWithErrorCallback)(YumiTypeNativeClientRef *nativeClient, const char *error);

/// Callback for when an  native has clicked.
typedef void (*YumiNativeAdDidClickCallback)(YumiTypeNativeClientRef *nativeClient);
/// Callback for when an  native has been render success.
typedef void (*YumiNativeExpressAdRenderSuccessDidCallback)(YumiTypeNativeClientRef *nativeClient, const char *uniqueId);
/// Callback for when an  native has been render fail.
typedef void (*YumiNativeExpressAdDidRenderFailCallback)(YumiTypeNativeClientRef *nativeClient,const char *uniqueId,const char *errorMsg);
/// Callback for when an  native has been click close button.
typedef void (*YumiNativeExpressAdDidClickCloseButtonCallback)(YumiTypeNativeClientRef *nativeClient,const char *uniqueId);

#pragma mark - splash ads
/// Type representing a Unity splash client.
typedef const void *YumiTypeSplashClientRef;
/// Type representing a YumiSplash.
typedef const void *YumiTypeSplashAdRef;
#pragma mark - splash call back
/// Callback for when a splash ad success to show
typedef void (*YumiSplashAdSuccessToShowCallback)(YumiTypeSplashClientRef *splashClient);

///Callback for when a splash ad fail to show
typedef void (*YumiSplashAdDidFailToShowWithErrorCallback)(YumiTypeSplashClientRef *splashClient, const char *error);

/// Callback for when an  splash has clicked.
typedef void (*YumiSplashAdDidClickCallback)(YumiTypeSplashClientRef *splashClient);

/// Callback for when an  splash has closed.
typedef void (*YumiSplashAdDidCloseCallback)(YumiTypeSplashClientRef *splashClient);


