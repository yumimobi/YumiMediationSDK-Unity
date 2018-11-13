//
//  YumiMediationDebugCenterUnity.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/6/4.
//

#import "YumiMediationDebugCenterUnity.h"
#import <YumiMediationDebugCenter-iOS/YumiMediationDebugController.h>

@implementation YumiMediationDebugCenterUnity

@end
extern "C" {
    
    void _presentYumiMediationDebugCenter(const char* bannerPlacementID,const char* interstitialPlacementID, const char* videoPlacementID, const char* nativePlacementID, const char* channelID , const char* versionID){
        [[YumiMediationDebugController sharedInstance] presentWithBannerPlacementID:[NSString stringWithCString:bannerPlacementID encoding:NSUTF8StringEncoding] interstitialPlacementID:[NSString stringWithCString:interstitialPlacementID encoding:NSUTF8StringEncoding] videoPlacementID:[NSString stringWithCString:videoPlacementID encoding:NSUTF8StringEncoding] nativePlacementID:[NSString stringWithCString:nativePlacementID encoding:NSUTF8StringEncoding] channelID:[NSString stringWithCString:channelID encoding:NSUTF8StringEncoding] versionID:[NSString stringWithCString:versionID encoding:NSUTF8StringEncoding] rootViewController:UnityGetGLViewController()];
    }
    /// set banner size
    void _setBannerSizeInDebugCenter(YumiMediationAdViewBannerSize bannerSize){
        [[YumiMediationDebugController sharedInstance] setupBannerSize:bannerSize];
    }
}
