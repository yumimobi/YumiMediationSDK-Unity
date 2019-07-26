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

/// A reference to the Unity native client.
@property(nonatomic, assign) YumiTypeNativeClientRef *nativeClient;

/// A YumiMediationNativeAd which contains the ad.
@property(nonatomic, strong) YumiMediationNativeAd *nativeAd;

/// The ad received callback into Unity.
@property(nonatomic, assign) YumiNativeAdDidReceiveAdCallback adReceivedCallback;

/// The ad failed callback into Unity.
@property(nonatomic, assign) YumiNativeAdDidFailToReceiveAdWithErrorCallback adFailedCallback;

/// The ad clicked callback into Unity.
@property(nonatomic, assign) YumiNativeAdDidClickCallback adClickedCallback;

/// The ad clicked callback into Unity.
@property(nonatomic, assign)  YumiNativeExpressAdRenderSuccessDidCallback adRenderSuccessCallBack;
/// The ad clicked callback into Unity.
@property(nonatomic, assign) YumiNativeExpressAdDidRenderFailCallback adRenderFailCallBack;
/// The ad clicked callback into Unity.
@property(nonatomic, assign) YumiNativeExpressAdDidClickCloseButtonCallback adClickCloseButtonCallBack;


- (instancetype)initWithNativeClientReference:(YumiTypeNativeClientRef *)nativeClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID
                                     configuration:(YumiMediationNativeAdConfiguration *)configuration;

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
- (BOOL)getIsExpressAdView:(NSString *)uniqueId;

#pragma mark: set ad view style
- (void)setTitleTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize;
- (void)setDescTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize;
- (void)setCallToActionTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize;
- (void)setIconScaleType:(int)scaleType;
- (void)setCoverImageScaleType:(int)scaleType;

@end

NS_ASSUME_NONNULL_END
