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

- (void)registerNativeForInteraction:(NSString *)nativeId adViewRect:(CGRect)adViewRect mediaViewRect:(CGRect)mediaViewRect iconViewRect:(CGRect)iconViewRect ctaViewRect:(CGRect)ctaViewRect titleRect:(CGRect)titleRect descRect:(CGRect)descRect;

- (void)unRegisterView:(NSString *)uniqueId;
// expired
- (BOOL)isAdInvalidated:(NSString *)uniqueId;
- (void)showView:(NSString *)uniqueId;
- (void)hideView:(NSString *)uniqueId;

/// get native data property
- (NSString *)getTitle:(NSString *)uniqueId;
- (NSString *)getDesc:(NSString *)uniqueId;
- (NSString *)getCallToAction:(NSString *)uniqueId;
- (NSString *)getIconUrl:(NSString *)uniqueId;
- (NSString *)getCoverImageUrl:(NSString *)uniqueId;
- (NSString *)getPrice:(NSString *)uniqueId;
- (NSString *)getStarRating:(NSString *)uniqueId;
- (NSString *)getOther:(NSString *)uniqueId;
- (BOOL)getHasVideoContent:(NSString *)uniqueId;

@end

NS_ASSUME_NONNULL_END
