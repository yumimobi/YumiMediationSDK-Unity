//
//  YumiMediationVideoUnity.m
//  Unity-iPhone
//

#import "YumiMediationVideoUnity.h"

#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
    extern NSString* _CreateNSString (const char* string);
#if defined(__cplusplus)
}
#endif

@implementation YumiMediationVideoUnity

#pragma mark - YumiMediationVideoDelegate
/// Tells the delegate that the video ad opened.
- (void)yumiMediationVideoDidOpen:(YumiMediationVideo *)video{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationVideoDidOpen","");
}

/// Tells the delegate that the video ad started playing.
- (void)yumiMediationVideoDidStartPlaying:(YumiMediationVideo *)video{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationVideoDidStartPlaying","");
}

/// Tells the delegate that the video ad closed.
- (void)yumiMediationVideoDidClose:(YumiMediationVideo *)video{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationVideoDidClose","");
}

/// Tells the delegate that the video ad has rewarded the user.
- (void)yumiMediationVideoDidReward:(YumiMediationVideo *)video{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationVideoDidReward","");
}

- (UIViewController *)viewControllerVideoModalView{
    return UnityGetGLViewController();
}

@end

YumiMediationVideoUnity *videoDelegate = nil;
extern "C" {
    void _loadYumiMediationVideo(const char* placementID,const char* channelID,const char* versionID){
        if (!videoDelegate) {
            videoDelegate = [[YumiMediationVideoUnity alloc]init];
        }
        [[YumiMediationVideo sharedInstance]loadAdWithPlacementID:[NSString stringWithCString:placementID encoding:NSUTF8StringEncoding] channelID:[NSString stringWithCString:channelID encoding:NSUTF8StringEncoding] versionID:[NSString stringWithCString:versionID encoding:NSUTF8StringEncoding]];
        [YumiMediationVideo sharedInstance].delegate = videoDelegate;
    }
    const bool _isVideoReady(){
        return [[YumiMediationVideo sharedInstance]isReady];
    }
    void _playVideo(){
        [[YumiMediationVideo sharedInstance]presentFromRootViewController:UnityGetGLViewController()];
    }
    
}

