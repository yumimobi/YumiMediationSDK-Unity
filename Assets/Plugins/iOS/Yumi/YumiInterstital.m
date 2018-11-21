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
- (void)yumiMediationInterstitial:(YumiMediationInterstitial *)interstitial
                 didFailWithError:(YumiMediationError *)error{
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString
                              stringWithFormat:@"Failed to receive ad with error: %@", [error localizedFailureReason]];
        self.adFailedCallback(self.interstitialClientRef, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that the interstitial is to be animated off the screen.
- (void)yumiMediationInterstitialWillDismissScreen:(YumiMediationInterstitial *)interstitial{
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

@end
