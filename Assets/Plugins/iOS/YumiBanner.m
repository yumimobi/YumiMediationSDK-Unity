//
//  YumiBanner.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/19.
//

#import "YumiBanner.h"

@interface YumiBanner()<YumiMediationBannerViewDelegate>


@end

@implementation YumiBanner

- (void)dealloc{
    self.bannerView.delegate = nil;
}

- (instancetype)initWithBannerClientReference:(YumiTypeBannerClientRef *)bannerClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID
                                     position:(YumiMediationBannerPosition)position{
    self = [super init];
    if (self) {
        _bannerClient = bannerClientRef;
        _bannerView = [[YumiMediationBannerView alloc] initWithPlacementID:placementID channelID:channelID versionID:versionID position:position rootViewController:UnityGetGLViewController()];
        _bannerView.delegate = self;
        // add subview
        UIView *mainView = UnityGetGLView();
        [mainView  addSubview:_bannerView];
    }
    
    return self;
    
}

- (void)loadAd:(BOOL)isSmart{
    if (!self.bannerView) {
        [self printLogIfError];
        return;
    }
    [self.bannerView loadAd:isSmart];
}

/// Makes the YumiBannerView hidden on the screen.
- (void)hideBannerView{
    if (!self.bannerView) {
        [self printLogIfError];
        return;
    }
    self.bannerView.hidden = YES;
}

/// Makes the YumiBannerView visible on the screen.
- (void)showBannerView{
    if (!self.bannerView) {
        [self printLogIfError];
        return;
    }
    self.bannerView.hidden = NO;
}
- (void)removeBannerView {
    if (!self.bannerView) {
       [self printLogIfError];
        return;
    }
    [self.bannerView removeFromSuperview];
    self.bannerView.delegate = nil;
    self.bannerView = nil;
    
}
- (void)setBannerAdSize:(YumiMediationAdViewBannerSize)bannerSize{
    if (!self.bannerView) {
        [self printLogIfError];
        return;
    }
    self.bannerView.bannerSize = bannerSize;
}
- (void)printLogIfError{
    NSLog(@"YumiMobileAdsPlugin: BannerView is nil. Ignoring ad request.");
}
#pragma mark - YumiMediationBannerViewDelegate

/// Tells the delegate that an ad has been successfully loaded.
- (void)yumiMediationBannerViewDidLoad:(YumiMediationBannerView *)adView{
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.bannerClient);
    }
}

/// Tells the delegate that a request failed.
- (void)yumiMediationBannerView:(YumiMediationBannerView *)adView didFailWithError:(YumiMediationError *)error{
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString
                              stringWithFormat:@"Failed to receive ad with error: %@", [error localizedFailureReason]];
        self.adFailedCallback(self.bannerClient, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that the banner view has been clicked.
- (void)yumiMediationBannerViewDidClick:(YumiMediationBannerView *)adView{
    if (self.adClickedCallback) {
        self.adClickedCallback(self.bannerClient);
    }
}
@end
