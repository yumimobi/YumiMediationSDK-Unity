//
//  YumiSplash.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/6/19.
//

#import <Foundation/Foundation.h>
#import "YumiTypes.h"
#import <YumiMediationSDK/YumiMediationSplash.h>

@interface YumiSplash : NSObject

/// The splash ad.
@property(nonatomic, strong) YumiMediationSplash *splash;

/// A reference to the Unity splash client.
@property(nonatomic, assign) YumiTypeSplashClientRef *splashClientRef;

/// The ad success to show  callback into Unity.
@property(nonatomic, assign) YumiSplashAdSuccessToShowCallback adSuccessToShowCallback;

/// The ad success to show  callback into Unity.
@property(nonatomic, assign) YumiSplashAdDidFailToShowWithErrorCallback adFailToShowCallback;

/// The ad clicked callback into Unity.
@property(nonatomic, assign) YumiSplashAdDidClickCallback adClickCallback;

/// The ad closed callback into Unity.
@property(nonatomic, assign) YumiSplashAdDidCloseCallback adCloseCallback;

/// initial splash object
- (instancetype)initWithSplashClientReference:(YumiTypeSplashClientRef *)splashClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID;
/// set your app orientation
- (void)setInterfaceOrientation:(UIInterfaceOrientation)orientation;

/// set your fetch time.
- (void)setFetchTime:(NSUInteger)fetchTime;

/// load and show   splash with bottom view height
- (void)loadAdAndShowWithBottomViewHeight:(CGFloat)height;

@end
