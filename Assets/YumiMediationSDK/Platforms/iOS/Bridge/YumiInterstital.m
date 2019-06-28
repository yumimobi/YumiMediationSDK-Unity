//
//  YumiInterstital.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/21.
//

#import "YumiInterstital.h"

@interface YumiInterstital()<YumiMediationInterstitialDelegate>

@end

@implementation YumiInterstital

- (void)dealloc{
    _interstitial.delegate = nil;
    _interstitial = nil;
}

- (instancetype)initWithInterstitialClientReference:(YumiTypeInterstitialClientRef *)interstitialClientRef placementID:(NSString *)placementID channelID:(NSString *)channelID versionID:(NSString *)versionID{
    self = [super init];
    if (self) {
        _interstitialClientRef = interstitialClientRef;
        _interstitial = [[YumiMediationInterstitial alloc] initWithPlacementID:placementID channelID:channelID versionID:versionID rootViewController:UnityGetGLViewController()];
        _interstitial.delegate = self;
    }
    return self;
}

- (BOOL)isReady{
   
    return [self.interstitial isReady];
}

- (void)present{
    if (!self.interstitial) {
        [self printLogIfError];
        return;
    }
    [self.interstitial present];
}
- (void)printLogIfError{
    NSLog(@"YumiMobileAdsPlugin: interstitial is nil. Ignoring ad request.");
}

#pragma mark: YumiMediationInterstitialDelegate
/// Tells the delegate that the interstitial ad request succeeded.
- (void)yumiMediationInterstitialDidReceiveAd:(YumiMediationInterstitial *)interstitial{
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.interstitialClientRef);
    }
}

/// Tells the delegate that the interstitial ad request failed.
- (void)yumiMediationInterstitial:(YumiMediationInterstitial *)interstitial didFailToLoadWithError:(YumiMediationError *)error{
    if (self.adFailedToLoadCallback) {
        NSString *errorMsg = [NSString
                              stringWithFormat:@"Failed to receive ad with error: %@", [error localizedFailureReason]];
        self.adFailedToLoadCallback(self.interstitialClientRef, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that the interstitial is to be animated off the screen.
- (void)yumiMediationInterstitialDidClosed:(YumiMediationInterstitial *)interstitial{
    if (self.adCloseCallback) {
        self.adCloseCallback(self.interstitialClientRef);
    }
}

/// Tells the delegate that the interstitial ad has been clicked.
- (void)yumiMediationInterstitialDidClick:(YumiMediationInterstitial *)interstitial{
    if (self.adClickCallback) {
        self.adClickCallback(self.interstitialClientRef);
    }
}
/// Tells the delegate that the interstitial ad failed to show.
- (void)yumiMediationInterstitial:(YumiMediationInterstitial *)interstitial
           didFailToShowWithError:(YumiMediationError *)error{
    if (self.adFailToShowCallback) {
        NSString *errorMsg = [NSString
                              stringWithFormat:@"Failed to show ad with error: %@", [error localizedFailureReason]];
        self.adFailToShowCallback(self.interstitialClientRef, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that the interstitial ad opened.
- (void)yumiMediationInterstitialDidOpen:(YumiMediationInterstitial *)interstitial{
    if (self.adOpenedCallback) {
        self.adOpenedCallback(self.interstitialClientRef);
    }
}

/// Tells the delegate that the interstitial ad start playing.
- (void)yumiMediationInterstitialDidStartPlaying:(YumiMediationInterstitial *)interstitial{
    if (self.adStartPlayingCallback) {
        self.adStartPlayingCallback(self.interstitialClientRef);
    }
}
@end
