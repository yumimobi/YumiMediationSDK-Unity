//
//  YumiInterstital.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/21.
//

#import <Foundation/Foundation.h>
#import "YumiTypes.h"
#import <YumiMediationSDK/YumiMediationInterstitial.h>

@interface YumiInterstital : NSObject

- (instancetype)initWithInterstitialClientReference:(YumiTypeInterstitialClientRef *)interstitialClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID;

/// The interstitial ad.
@property(nonatomic, strong) YumiMediationInterstitial *interstitial;

/// A reference to the Unity interstitial client.
@property(nonatomic, assign) YumiTypeInterstitialClientRef *interstitialClientRef;

/// The ad received callback into Unity.
@property(nonatomic, assign) YumiInterstitialDidReceiveAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) YumiInterstitialDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The ad clicked callback into Unity.
@property(nonatomic, assign) YumiInterstitialDidClickCallback adClickCallback;

/// The did dismiss screen callback into Unity.
@property(nonatomic, assign) YumiInterstitialDidCloseCallback adCloseCallback;

/// Returns YES if the interstitial is ready to be displayed.
- (BOOL)isReady;

/// Presents the interstitial ad from the view controller passed in in initialization method.
- (void)present;

@end

