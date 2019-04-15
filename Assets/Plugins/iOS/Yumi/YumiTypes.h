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
/// Callback for when an rewardVideo has just been closed.
typedef void (*YumiRewardVideoDidCloseCallback)(YumiTypeRewardVideoClientRef *rewardVideo);

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

