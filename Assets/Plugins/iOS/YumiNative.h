//
//  YumiNative.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/1/9.
//

#import <Foundation/Foundation.h>
#import "YumiTypes.h"
#import <YumiMediationSDK/YumiMediationNativeAd.h>

NS_ASSUME_NONNULL_BEGIN

@interface YumiNative : NSObject

/// A reference to the Unity banner client.
@property(nonatomic, assign) YumiTypeNativeClientRef *nativeClient;

/// A YumiMediationNativeAd which contains the ad.
@property(nonatomic, strong) YumiMediationNativeAd *nativeAd;

/// The ad received callback into Unity.
@property(nonatomic, assign) YumiNativeAdDidReceiveAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) YumiNativeAdDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The ad clicked callback into Unity.
@property(nonatomic, assign) YumiNativeAdDidClickCallback adClickedCallback;


- (instancetype)initWithNativeClientReference:(YumiTypeNativeClientRef *)nativeClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID;

/// Begins loading the YumiMediationNativeAd with the count you wanted.
- (void)loadNativeAd:(NSUInteger)adCount;

///report impression
- (void)reportImpression;
/// report click
- (void)reportClick;

@end

NS_ASSUME_NONNULL_END
