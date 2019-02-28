//
//  YumiNative.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/1/9.
//

#import "YumiNative.h"

@interface YumiNative()<YumiMediationNativeAdDelegate>

@property (nonatomic) YumiMediationNativeModel  *currentModel;

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

- (void)registerNativeForInteraction:(int)nativeId adViewRect:(CGRect)adViewRect mediaViewRect:(CGRect)mediaViewRect iconViewRect:(CGRect)iconViewRect ctaViewRect:(CGRect)ctaViewRect{
    
    UIView *mainView = UnityGetGLView();
    UIView *adView = [[UIView alloc] initWithFrame:adViewRect];
    UIImageView *iconView = [[UIImageView alloc] initWithFrame:iconViewRect];
    UIImageView *mediaView = [[UIImageView alloc] initWithFrame:mediaViewRect];
    UILabel *actLab = [[UILabel alloc] initWithFrame:ctaViewRect];
    
    [mainView addSubview:adView];
    [mainView addSubview:iconView];
    [mainView addSubview:mediaView];
    [mainView addSubview:actLab];
    
    [self.nativeAd registerViewForInteraction:adView withViewController:UnityGetGLViewController() nativeAd:self.currentModel];
    
}

- (void)UnregisterView:(int)uniqueId{
    UIView *mainView = UnityGetGLView();
    
    for (UIView *subView in mainView.subviews) {
        [subView removeFromSuperview];
    }
    
}
- (void)printLogIfError{
    NSLog(@"YumiMobileAdsPlugin: NativeAd is nil or adCount <= 0. Ignoring ad request.");
}

#pragma mark:YumiMediationNativeAdDelegate

/// Tells the delegate that an ad has been successfully loaded.
- (void)yumiMediationNativeAdDidLoad:(NSArray<YumiMediationNativeModel *> *)nativeAdArray{
    if (nativeAdArray.count > 0) {
        self.currentModel = nativeAdArray[0];
    }
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.nativeClient,(int)nativeAdArray.count);
    }
   
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
    if (self.adClickedCallback) {
        self.adClickedCallback(self.nativeClient);
    }
}
@end
