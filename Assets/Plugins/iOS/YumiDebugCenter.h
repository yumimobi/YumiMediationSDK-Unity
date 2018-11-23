//
//  YumiDebugCenter.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/23.
//

#import <Foundation/Foundation.h>
#import <YumiMediationDebugCenter-iOS/YumiMediationDebugController.h>


@interface YumiDebugCenter : NSObject

- (void)presentWithBannerPlacementID:(NSString *)bannerPlacementID
             interstitialPlacementID:(NSString *)interstitialPlacementID
                    videoPlacementID:(NSString *)videoPlacementID
                   nativePlacementID:(NSString *)nativePlacementID
                           channelID:(NSString *)channelID
                           versionID:(NSString *)versionID;

@end

