//
//  YumiBanner.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/19.
//

#import <Foundation/Foundation.h>
#import "YumiTypes.h"
#import <YumiMediationSDK/YumiMediationBannerView.h>

@interface YumiBanner : NSObject

- (instancetype)initWithBannerClientReference:(YumiTypeBannerClientRef *)bannerClientRef
                        placementID:(NSString *)placementID
                          channelID:(NSString *)channelID
                          versionID:(NSString *)versionID
                           position:(YumiMediationBannerPosition)position;

/// A reference to the Unity banner client.
@property(nonatomic, assign) YumiTypeBannerClientRef *bannerClient;

/// A YumiMediationBannerView which contains the ad.
@property(nonatomic, strong) YumiMediationBannerView *bannerView;

/// The ad received callback into Unity.
@property(nonatomic, assign) YumiBannerDidReceiveAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) YumiBannerDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The ad clicked callback into Unity.
@property(nonatomic, assign) YumiBannerDidClickCallback adClickedCallback;

/// Makes an ad request. Additional targeting options can be supplied with a request object.
- (void)loadAd:(BOOL)isSmart;

/// Makes the YumiBannerView hidden on the screen.
- (void)hideBannerView;

/// Makes the YumiBannerView visible on the screen.
- (void)showBannerView;
/// Removes the GADBannerView from the view hierarchy.
- (void)removeBannerView;

- (void)setBannerAdSize:(YumiMediationAdViewBannerSize)bannerSize;

- (void)disableAutoRefresh;

@end
