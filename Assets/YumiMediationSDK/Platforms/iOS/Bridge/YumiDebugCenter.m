//
//  YumiDebugCenter.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/23.
//

#import "YumiDebugCenter.h"

@implementation YumiDebugCenter

- (void)presentWithBannerPlacementID:(NSString *)bannerPlacementID
             interstitialPlacementID:(NSString *)interstitialPlacementID
                    videoPlacementID:(NSString *)videoPlacementID
                   nativePlacementID:(NSString *)nativePlacementID
                    splashPlacementID:(NSString *)splashPlacementID
                           channelID:(NSString *)channelID
                           versionID:(NSString *)versionID{
    
    [[YumiMediationDebugController sharedInstance] presentWithBannerPlacementID:bannerPlacementID interstitialPlacementID:interstitialPlacementID videoPlacementID:videoPlacementID nativePlacementID:nativePlacementID
                      splashPlacementID:splashPlacementID  channelID:channelID versionID:versionID rootViewController:UnityGetGLViewController()];
}

@end
