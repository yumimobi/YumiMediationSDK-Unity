//
//  YumiMediationBannerUnity.m
//  Unity-iPhone
//

#import "YumiMediationBannerUnity.h"

struct YumiMediationBannerSize{
    float bannerWidth;
    float bannerHeight;
}YumiMediationBannerSize;

#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
    extern NSString* _CreateNSString (const char* string);
#if defined(__cplusplus)
}
#endif

@implementation YumiMediationBannerUnity

#pragma mark - YumiMediationBannerViewDelegate

/// Tells the delegate that an ad has been successfully loaded.
- (void)yumiMediationBannerViewDidLoad:(YumiMediationBannerView *)adView{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationBannerViewDidLoad","");
}

/// Tells the delegate that a request failed.
- (void)yumiMediationBannerView:(YumiMediationBannerView *)adView didFailWithError:(YumiMediationError *)error{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationSDKDidFailToReceiveAd","");
}

/// Tells the delegate that the banner view has been clicked.
- (void)yumiMediationBannerViewDidClick:(YumiMediationBannerView *)adView{
    UnitySendMessage("YumiMediationSDKManager","yumiMediationBannerViewDidClick","");
}

@end

YumiMediationBannerView *bannerView = nil;
YumiMediationBannerUnity *bannerDelegate = nil;
UIView *mainView = nil;


extern "C" {
    void _initYumiMediationBanner(const char* placementID,const char* channelID,const char* versionID,YumiMediationBannerPosition position){
        
        if (!bannerDelegate) {
            bannerDelegate = [[YumiMediationBannerUnity alloc]init];
        }
        mainView = UnityGetGLView();
        if (!bannerView) {
            bannerView = [[YumiMediationBannerView alloc]initWithPlacementID:[NSString stringWithCString:placementID encoding:NSUTF8StringEncoding]    channelID:[NSString stringWithCString:channelID encoding:NSUTF8StringEncoding]versionID:[NSString stringWithCString:versionID encoding:NSUTF8StringEncoding]   position:position   rootViewController:UnityGetGLViewController()];
        }
            bannerView.delegate = bannerDelegate;
            [mainView addSubview:bannerView];
        
    }

    void _disableAutoRefresh(){
        if(bannerView){
            [bannerView disableAutoRefresh];
        }
     }
                                                                                                                                                       
     void _loadAd(const bool isSmartBanner){
         if(bannerView){
             [bannerView loadAd:isSmartBanner];
         }
     }

    const char* _fetchBannerAdSize(){
        NSString *adSizeString = [NSString stringWithFormat:@"%f_%f", ([bannerView fetchBannerAdSize].width ?:0.0) , ([bannerView fetchBannerAdSize].height?: 0.0)];
        const  char  *streamChar = [adSizeString UTF8String];
        char  *sendChar =  ( char *)malloc(strlen(streamChar) +1);
        strcpy(sendChar, streamChar);
        
        return sendChar;
    }
    
    void _hiddenYumiMediationBanner(const bool ishidden){
        if(!bannerView){
            return;
        }
        bannerView.hidden = ishidden;
    }
    
    void _removeBanner(){
        if(!bannerView){
            return;
        }
        bannerView.delegate = nil;
        [bannerView removeFromSuperview];
        bannerDelegate = nil;
        bannerView = nil;
    }
    /// set banner size
    void _setBannerAdSize(YumiMediationAdViewBannerSize bannerSize){
        if(!bannerView){
            return;
        }
        
        bannerView.bannerSize = bannerSize;
    }
      
}
