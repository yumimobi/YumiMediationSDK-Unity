//
//  YumiRewardVideo.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/21.
//

#import <Foundation/Foundation.h>
#import "YumiTypes.h"
#import <YumiMediationSDK/YumiMediationVideo.h>

@interface YumiRewardVideo : NSObject

- (instancetype)initWithRewardVideoClientReference:(YumiTypeRewardVideoClientRef *)rewardVideoClientRef;

/// The reward based video ad.
@property(nonatomic, strong) YumiMediationVideo *rewardVideo;

/// A reference to the Unity reward based video ad client.
@property(nonatomic, assign) YumiTypeRewardVideoClientRef *rewardVideoAdClient;

/// The ad was opened callback into Unity.
@property(nonatomic, assign) YumiRewardVideoDidOpenAdCallback adOpenedCallback;


/// The ad started playing callback into Unity.
@property(nonatomic, assign) YumiRewardVideoDidStartPlayingCallback adStartPlayingCallback;

/// The ad was reward callback into Unity.
@property(nonatomic, assign) YumiRewardVideoDidRewardCallback adRewardedCallback;

/// The ad was closed callback into Unity.
@property(nonatomic, assign) YumiRewardVideoDidCloseCallback adClosedCallback;
// v4.0.0
/// The ad was received callback into Unity.
@property(nonatomic, assign)  YumiRewardVideoDidReceiveAdCallback adReceivedCallback;
/// The ad was failed to load callback into Unity.
@property(nonatomic, assign) YumiRewardVideoDidFailToReceiveAdWithErrorCallback adFailToLoadCallback;
/// The ad  was failed to show callback into Unity.
@property(nonatomic, assign)  YumiRewardVideoDidFailToShowAdWithErrorCallback adFailToShowCallback;
/// The ad was clicked callback into Unity.
@property(nonatomic, assign) YumiRewardVideoDidClickAdCallback adClickCallBack;


/// load reward video
- (void)loadAdWithPlacementID:(NSString *)placementID channelID:(NSString *)channelID versionID:(NSString *)versionID;

/// Indicates if the receiver is ready to be presented full screen.
- (BOOL)isReady;

/// play the video ad
- (void)playRewardVideo;

@end

