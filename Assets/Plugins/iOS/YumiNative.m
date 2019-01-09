//
//  YumiNative.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/1/9.
//

#import "YumiNative.h"

@interface YumiNative()<YumiMediationNativeAdDelegate>



@end

@implementation YumiNative

- (void)dealloc{
    _nativeAd.delegate = nil;
    _nativeAd = nil;
}

- (instancetype)initWithNativeClientReference:(YumiTypeNativeClientRef *)nativeClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID{
    self = [super init];
    if (self) {
        _nativeClient = nativeClientRef;
        _nativeAd = [[YumiMediationNativeAd alloc] initWithPlacementID:placementID channelID:channelID versionID:versionID];
        _nativeAd.delegate = self;
    }
    return self;
}

- (void)loadNativeAd:(NSUInteger)adCount{
    if (!self.nativeAd || adCount <= 0) {
        [self printLogIfError];
        return;
    }
    [self.nativeAd loadAd:adCount];
}

- (void)reportImpression{
    
}

- (void)reportClick{
    
}

- (void)printLogIfError{
    NSLog(@"YumiMobileAdsPlugin: NativeAd is nil or adCount <= 0. Ignoring ad request.");
}

#pragma mark:YumiMediationNativeAdDelegate

/// Tells the delegate that an ad has been successfully loaded.
- (void)yumiMediationNativeAdDidLoad:(NSArray<YumiMediationNativeModel *> *)nativeAdArray{
    
}

/// Tells the delegate that a request failed.
- (void)yumiMediationNativeAd:(YumiMediationNativeAd *)nativeAd didFailWithError:(YumiMediationError *)error{
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString
                              stringWithFormat:@"Failed to receive ad with error: %@", [error localizedFailureReason]];
        self.adFailedCallback(self.nativeClient,[errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that the Native view has been clicked.
- (void)yumiMediationNativeAdDidClick:(YumiMediationNativeModel *)nativeAd{
    
}
@end
