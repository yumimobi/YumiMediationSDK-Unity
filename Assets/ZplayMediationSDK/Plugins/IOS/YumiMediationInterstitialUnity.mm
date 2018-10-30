//
//  YumiMediationInterstitialUnity.m
//  Unity-iPhone
//

#import "YumiMediationInterstitialUnity.h"

#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
    extern NSString* _CreateNSString (const char* string);
#if defined(__cplusplus)
}
#endif

@implementation YumiMediationInterstitialUnity

#pragma mark - YumiMediationInterstitialDelegate
/// Tells the delegate that the interstitial ad request succeeded.
- (void)yumiMediationInterstitialDidReceiveAd:(YumiMediationInterstitial *)interstitial{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationInterstitialDidReceiveAd","");
}

/// Tells the delegate that the interstitial ad request failed.
- (void)yumiMediationInterstitial:(YumiMediationInterstitial *)interstitial
                 didFailWithError:(YumiMediationError *)error{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationInterstitialDidFailToReceiveAd","");
}

/// Tells the delegate that the interstitial is to be animated off the screen.
- (void)yumiMediationInterstitialWillDismissScreen:(YumiMediationInterstitial *)interstitial{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationInterstitialWillDismissScreen","");
}

/// Tells the delegate that the interstitial ad has been clicked.
- (void)yumiMediationInterstitialDidClick:(YumiMediationInterstitial *)interstitial{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationInterstitialDidClick","");
}

@end

YumiMediationInterstitial *interstital = nil;
YumiMediationInterstitialUnity *interstitalDelegate = nil;


extern "C" {
    void _initYumiMediationInterstitial(const char* placementID,const char* channelID,const char* versionID){
        if (!interstitalDelegate) {
            interstitalDelegate = [[YumiMediationInterstitialUnity alloc]init];
        }
        interstital = [[YumiMediationInterstitial alloc]initWithPlacementID:[NSString stringWithCString:placementID encoding:NSUTF8StringEncoding]
                            channelID:[NSString stringWithCString:channelID encoding:NSUTF8StringEncoding]
                            versionID:[NSString stringWithCString:versionID encoding:NSUTF8StringEncoding]
                            rootViewController:UnityGetGLViewController()];
        interstital.delegate = interstitalDelegate;
    }
    const bool _isInterstitialReady(){
        if (!interstital) {
            return NO;
        }
        return [interstital isReady];
    }
    void _present(){
        if (interstital) {
            [interstital present];
        }
    }
    
}















