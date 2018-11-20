//
//  YumiTypes.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/19.
//


#pragma mark - yumi ads ref
/// Type representing a Unity banner client.
typedef const void *YumiTypeBannerClientRef;
/// Type representing a YumiBanner.
typedef const void *YumiTypeBannerRef;

#pragma mark - yumi ads callback
/// Callback for when a banner ad request was successfully loaded.
typedef void (*YumiBannerDidReceiveAdCallback)(YumiTypeBannerClientRef *bannerClient);

/// Callback for when a banner ad request failed.
typedef void (*YumiBannerDidFailToReceiveAdWithErrorCallback)(YumiTypeBannerClientRef *bannerClient, const char *error);

/// Callback for when a full screen view is about to be presented as a result of a banner click.
typedef void (*YumiBannerDidClickCallback)(YumiTypeBannerClientRef *bannerClient);

