//
//  YumiSplash.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/6/19.
//

#import "YumiSplash.h"

@interface YumiSplash () <YumiMediationSplashAdDelegate>

@end

@implementation YumiSplash

/// initial splash object
- (instancetype)initWithSplashClientReference:(YumiTypeSplashClientRef *)splashClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID{
    
    self = [super init];
    if (self) {
        _splashClientRef = splashClientRef;
        _splash = [[YumiMediationSplash alloc] initWithPlacementID:placementID channelID:channelID versionID:versionID];
        _splash.delegate = self;
    }
    
    return self;
}

- (void)setInterfaceOrientation:(UIInterfaceOrientation)orientation{
    [self.splash setInterfaceOrientation:orientation];
}

- (void)setFetchTime:(NSUInteger)fetchTime{
    [self.splash setFetchTime:fetchTime];
}

- (void)loadAdAndShowWithBottomViewHeight:(CGFloat)height{
    
    if (height == 0) {
        [self.splash loadAdAndShowInWindow:[UIApplication sharedApplication].keyWindow];
        return;
    }
    
    UIView *bottomView = [[UIView alloc] initWithFrame:CGRectMake(0, 0, [UIScreen mainScreen].bounds.size.width, height)];
    [self.splash loadAdAndShowInWindow:[UIApplication sharedApplication].keyWindow withBottomView:bottomView];
}

#pragma mark: YumiMediationSplashAdDelegate

- (void)yumiMediationSplashAdSuccessToShow:(YumiMediationSplash *)splash{
    if (self.adSuccessToShowCallback) {
        self.adSuccessToShowCallback(self.splashClientRef);
    }
}

- (void)yumiMediationSplashAdFailToShow:(YumiMediationSplash *)splash withError:(NSError *)error{
    if (self.adFailToShowCallback) {
        NSString *errorMsg = [NSString
                              stringWithFormat:@"Failed to show ad with error: %@", [error localizedFailureReason]];
        self.adFailToShowCallback(self.splashClientRef, [errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

- (void)yumiMediationSplashAdDidClick:(YumiMediationSplash *)splash{
    if (self.adClickCallback) {
        self.adClickCallback(self.splashClientRef);
    }
}

- (void)yumiMediationSplashAdDidClose:(YumiMediationSplash *)splash{
    if (self.adCloseCallback) {
        self.adCloseCallback(self.splashClientRef);
    }
}

@end
