//
//  YumiAdsSplashUnity.h
//  Unity-iPhone

#import "YumiAdsSplashUnity.h"

#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
    extern NSString* _CreateNSString (const char* string);
#if defined(__cplusplus)
}
#endif

@implementation YumiAdsSplashUnity

#pragma mark - YumiAdsSplashDelegate

/// Tells the delegate that the splash ad did load.
- (void)yumiAdsSplashDidLoad:(YumiAdsSplash *)splash{
    
    UnitySendMessage("YumiMediationSDKManager","yumiAdsSplashDidLoad","");
}
/// Tells the delegate that the splash ad did fail to load.
- (void)yumiAdsSplash:(YumiAdsSplash *)splash DidFailToLoad:(NSError *)error{
     const char *errorChar = [[error localizedDescription] UTF8String];
    UnitySendMessage("YumiMediationSDKManager","yumiAdsSplashDidFailToLoad",errorChar);
}
/// Tells the delegate that the splash ad did clicked.
- (void)yumiAdsSplashDidClicked:(YumiAdsSplash *)splash{
    
    UnitySendMessage("YumiMediationSDKManager","yumiAdsSplashDidClick","");
}
/// Tells the delegate that the splash ad did closed.
- (void)yumiAdsSplashDidClosed:(YumiAdsSplash *)splash{
    UnitySendMessage("YumiMediationSDKManager","yumiAdsSplashDidClosed","");
}
/// return your lunchImages
- (nullable UIImage *)yumiAdsSplashDefaultImage{
    
     UnitySendMessage("YumiMediationSDKManager","yumiAdsSplashDefaultImage","");
    return nil;
}

@end

YumiAdsSplashUnity *splashDelegate = nil;

extern "C" {
    void _showYumiAdsSplash(const char* placementID,const char  *appKey ) {
        if (!splashDelegate) {
            splashDelegate = [[YumiAdsSplashUnity alloc]init];
        }
        [[YumiAdsSplash sharedInstance] showYumiAdsSplashWith:[NSString stringWithCString:placementID encoding:NSUTF8StringEncoding] appKey:[NSString stringWithCString:appKey encoding:NSUTF8StringEncoding]
       rootViewController:UnityGetGLViewController()
            delegate:splashDelegate];
    }
    
}
