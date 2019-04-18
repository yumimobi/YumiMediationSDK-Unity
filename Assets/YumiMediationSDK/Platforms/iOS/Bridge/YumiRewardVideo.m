//
//  YumiRewardVideo.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/21.
//

#import "YumiRewardVideo.h"

@interface YumiRewardVideo()<YumiMediationVideoDelegate>

@end

@implementation YumiRewardVideo

- (void)dealloc{
    _rewardVideo.delegate = nil;
    _rewardVideo = nil;
}

- (instancetype)initWithRewardVideoClientReference:(YumiTypeRewardVideoClientRef *)rewardVideoClientRef{
    self = [super init];
    if (self) {
        _rewardVideoAdClient = rewardVideoClientRef;
        _rewardVideo = [YumiMediationVideo sharedInstance];
        _rewardVideo.delegate = self;
    }
    return self;
}

- (void)loadAdWithPlacementID:(NSString *)placementID channelID:(NSString *)channelID versionID:(NSString *)versionID{
    if (!self.rewardVideo) {
        [self printLogIfError];
        return;
    }
    [self.rewardVideo loadAdWithPlacementID:placementID channelID:channelID versionID:versionID];
}

/// Indicates if the receiver is ready to be presented full screen.
- (BOOL)isReady{
    return [self.rewardVideo isReady];
}

/// play the video ad
- (void)playRewardVideo{
    if (!self.rewardVideo) {
        [self printLogIfError];
        return;
    }
    [self.rewardVideo presentFromRootViewController:UnityGetGLViewController()];
}

- (void)printLogIfError{
    NSLog(@"YumiMobileAdsPlugin: reward video is nil. Ignoring ad request.");
}
/// Tells the delegate that the video ad opened.
- (void)yumiMediationVideoDidOpen:(YumiMediationVideo *)video{
    if (self.adOpenedCallback) {
        self.adOpenedCallback(self.rewardVideoAdClient);
    }
}

/// Tells the delegate that the video ad started playing.
- (void)yumiMediationVideoDidStartPlaying:(YumiMediationVideo *)video{
    if (self.adStartPlayingCallback) {
        self.adStartPlayingCallback(self.rewardVideoAdClient);
    }
}

/// Tells the delegate that the video ad closed.
- (void)yumiMediationVideoDidClose:(YumiMediationVideo *)video{
    if (self.adClosedCallback) {
        self.adClosedCallback(self.rewardVideoAdClient);
    }
}

/// Tells the delegate that the video ad has rewarded the user.
- (void)yumiMediationVideoDidReward:(YumiMediationVideo *)video{
    if (self.adRewardedCallback) {
        self.adRewardedCallback(self.rewardVideoAdClient);
    }
}
@end
