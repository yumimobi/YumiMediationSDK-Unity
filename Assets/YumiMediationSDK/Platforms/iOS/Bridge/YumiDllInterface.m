//
//  YumiDllInterface.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/19.
//

#import <Foundation/Foundation.h>
#import "YumiTypes.h"
#import "YumiBanner.h"
#import "YumiObjectCache.h"
#import "YumiInterstital.h"
#import "YumiRewardVideo.h"
#import "YumiDebugCenter.h"
#import "YumiNative.h"
#import "YumiAdBridgeTool.h"
#import <YumiMediationSDK/YumiTest.h>
#import "YumiSplash.h"
#import <YumiMediationSDK/YumiMediationGDPRManager.h>

/// Returns an NSString copying the characters from |bytes|, a C array of UTF8-encoded bytes.
/// Returns nil if |bytes| is NULL.
static NSString *YumiStringFromUTF8String(const char *bytes) { return bytes ? @(bytes) : nil; }

/// Returns a C string from a C array of UTF8-encoded bytes.
static const char *cStringCopy(const char *string) {
    if (!string) {
        return NULL;
    }
    char *res = (char *)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

/// Returns a C string from a C array of UTF8-encoded bytes.
static const char **cStringArrayCopy(NSArray *array) {
    if (array == nil) {
        return nil;
    }
    
    const char **stringArray;
    
    stringArray = calloc(array.count, sizeof(char *));
    for (int i = 0; i < array.count; i++) {
        stringArray[i] = cStringCopy([array[i] UTF8String]);
    }
    return stringArray;
}

static CGFloat iPhonePlusHeight = 736.0;

#pragma mark: banner method
/// Creates a full-width BannerView in the current orientation. Returns a reference to the
/// YumiBannerView.
YumiTypeBannerRef InitYumiBannerAd(YumiTypeBannerClientRef *bannerClient,const char * placementID, const char * channelID, const char * versionID, int position){
    
    YumiBanner *banner = [[YumiBanner alloc] initWithBannerClientReference:bannerClient placementID:YumiStringFromUTF8String(placementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID) position:position];
    YumiObjectCache *cache = [YumiObjectCache sharedInstance];
    [cache.references setObject:banner forKey:[banner yumi_referenceKey]];
    return (__bridge YumiTypeBannerRef)banner;
}
void ShowBannerView(YumiTypeBannerRef bannerView){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    [internalBanner showBannerView];
}
void HideBannerView(YumiTypeBannerRef bannerView){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    [internalBanner hideBannerView];
}
void DestroyBannerView(YumiTypeBannerRef bannerView){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    [internalBanner removeBannerView];
}
void SetBannerAdSize(YumiTypeBannerRef bannerView,YumiMediationAdViewBannerSize bannerSize){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    [internalBanner setBannerAdSize:bannerSize];
}
void DisableAutoRefresh(YumiTypeBannerRef bannerView){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    [internalBanner disableAutoRefresh];
}
/// Sets the banner callback methods to be invoked during banner ad events.
void SetBannerCallbacks(
                        YumiTypeBannerRef bannerView,
                        YumiBannerDidReceiveAdCallback adReceivedCallback,
                        YumiBannerDidFailToReceiveAdWithErrorCallback adFailedCallback,
                        YumiBannerDidClickCallback adClickedCallback){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    // set banner property
    internalBanner.adReceivedCallback = adReceivedCallback;
    internalBanner.adFailedCallback = adFailedCallback;
    internalBanner.adClickedCallback = adClickedCallback;
}

void RequestBannerAd( YumiTypeBannerRef bannerView,BOOL isSmart){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    [internalBanner loadAd:isSmart];
}

#pragma mark: interstitial method
YumiTypeInterstitialRef InitYumiInterstitial(YumiTypeInterstitialClientRef *interstitialClient,const char * placementID, const char * channelID, const char * versionID){
    
    YumiInterstital *interstitial = [[YumiInterstital alloc] initWithInterstitialClientReference:interstitialClient placementID:YumiStringFromUTF8String(placementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID)];
    // save pointer
    YumiObjectCache *cache = [YumiObjectCache sharedInstance];
    [cache.references setObject:interstitial forKey:[interstitial yumi_referenceKey]];
    return (__bridge YumiTypeInterstitialRef)interstitial;
}
BOOL IsInterstitialReady(YumiTypeInterstitialRef interstitial){
    YumiInterstital *internalInterstitial = (__bridge YumiInterstital *)interstitial;
    return [internalInterstitial isReady];
}
void PresentInterstitial(YumiTypeInterstitialRef interstitial){
    YumiInterstital *internalInterstitial = (__bridge YumiInterstital *)interstitial;
    [internalInterstitial present];
}
void SetInterstitiaCallbacks(YumiTypeInterstitialRef interstitial,
                             YumiInterstitialDidReceiveAdCallback adReceivedCallback,
                             YumiInterstitialDidFailToReceiveAdWithErrorCallback adFailCallback,
                             YumiInterstitialDidClickCallback adClickedCallback,
                             YumiInterstitialDidCloseCallback adClosedCallback,
                        YumiInterstitialDidFailToShowAdWithErrorCallback adFailToShowCallback,
                             YumiInterstitialDidOpenCallback adOpenedCallback,
                             YumiInterstitialDidStartPlayingCallback adStartPlayingCallback
                             ){
    YumiInterstital *internalInterstitial = (__bridge YumiInterstital *)interstitial;
    internalInterstitial.adReceivedCallback = adReceivedCallback;
    internalInterstitial.adFailedToLoadCallback = adFailCallback;
    internalInterstitial.adClickCallback = adClickedCallback;
    internalInterstitial.adCloseCallback = adClosedCallback;
    internalInterstitial.adFailToShowCallback = adFailToShowCallback;
    internalInterstitial.adOpenedCallback =adOpenedCallback;
    internalInterstitial.adStartPlayingCallback = adStartPlayingCallback;
}

#pragma mark - reward video
YumiTypeRewardVideoRef CreateYumiRewardVideo(YumiTypeRewardVideoClientRef *rewardVideoClientRef){
    YumiRewardVideo *rewardVideo = [[YumiRewardVideo alloc] initWithRewardVideoClientReference:rewardVideoClientRef];
    // save pointer
    YumiObjectCache *cache = [YumiObjectCache sharedInstance];
    [cache.references setObject:rewardVideo forKey:[rewardVideo yumi_referenceKey]];
    return (__bridge YumiTypeRewardVideoRef)rewardVideo;
}

void LoadYumiRewardVideo(YumiTypeRewardVideoRef rewardVideo,const char * placementID, const char * channelID, const char * versionID){
    YumiRewardVideo *internalRewardVideo = (__bridge YumiRewardVideo *)rewardVideo;
    [internalRewardVideo loadAdWithPlacementID:YumiStringFromUTF8String(placementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID)];
}
BOOL IsRewardVideoReady(YumiTypeRewardVideoRef rewardVideo){
     YumiRewardVideo *internalRewardVideo = (__bridge YumiRewardVideo *)rewardVideo;
    return [internalRewardVideo isReady];
}
void PlayRewardVideo(YumiTypeRewardVideoRef rewardVideo){
    YumiRewardVideo *internalRewardVideo = (__bridge YumiRewardVideo *)rewardVideo;
    [internalRewardVideo playRewardVideo];
}
void SetRewardVideoCallbacks(YumiTypeRewardVideoRef rewardVideo,
                             YumiRewardVideoDidOpenAdCallback adOpenedCallback,
                             YumiRewardVideoDidStartPlayingCallback adStartPlaying,
                             YumiRewardVideoDidRewardCallback adRewardedCallback,
                             YumiRewardVideoDidCloseCallback adClosedCallback,
                             YumiRewardVideoDidReceiveAdCallback adReceivedCallback,
                             YumiRewardVideoDidFailToReceiveAdWithErrorCallback adFailToLoadCallback,
                             YumiRewardVideoDidFailToShowAdWithErrorCallback adFailToShowCallback,
                             YumiRewardVideoDidClickAdCallback adClickCallBack
                             ){
    YumiRewardVideo *internalRewardVideo = (__bridge YumiRewardVideo *)rewardVideo;
    internalRewardVideo.adOpenedCallback = adOpenedCallback;
    internalRewardVideo.adStartPlayingCallback = adStartPlaying;
    internalRewardVideo.adRewardedCallback = adRewardedCallback;
    internalRewardVideo.adClosedCallback = adClosedCallback;
    internalRewardVideo.adReceivedCallback = adReceivedCallback;
    internalRewardVideo.adFailToLoadCallback = adFailToLoadCallback;
    internalRewardVideo.adFailToShowCallback = adFailToShowCallback;
    internalRewardVideo.adClickCallBack = adClickCallBack;
}

#pragma mark - Other methods
/// Removes an object from the cache.
void YumiRelease(YumiTypeRef ref) {
    if (ref) {
        YumiObjectCache *cache = [YumiObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref yumi_referenceKey]];
    }
}

#pragma mark: debugcenter

void PresentDebugCenter(const char * bannerPlacementID, const char * interstitialPlacementID, const char * videoPlacementID, const char * nativePlacementID,const char * splashPlacementID ,const char * channelID, const char * versionID){
    YumiDebugCenter *debugcenter = [[YumiDebugCenter alloc] init];
    
    [debugcenter presentWithBannerPlacementID:YumiStringFromUTF8String(bannerPlacementID) interstitialPlacementID:YumiStringFromUTF8String(interstitialPlacementID) videoPlacementID:YumiStringFromUTF8String(videoPlacementID) nativePlacementID:YumiStringFromUTF8String(nativePlacementID)
                            splashPlacementID:YumiStringFromUTF8String(splashPlacementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID)];
}

void EnableTestMode(){
    [YumiTest enableTestMode];
}

void UpdateNetworksConsentStatus(int consentStatus){
    
    [[YumiMediationGDPRManager sharedGDPRManager] updateNetworksConsentStatus:consentStatus];
}

#pragma  mark: native  method

YumiTypeNativeAdRef InitYumiNativeAd(YumiTypeNativeClientRef *nativeClient,const char * placementID, const char * channelID, const char * versionID,int preferredAdChoicesPosition,int preferredAdAttributionPosition,const char *preferredAdAttributionText,uint preferredAdAttributionTextColor,uint preferredAdAttributionTextBackgroundColor,int preferredAdAttributionTextFontSize,BOOL hideAdAttribution,int expressAdViewWidth,int expressAdViewHeight){
    
    YumiMediationNativeAdConfiguration *adConfiguration = [[YumiMediationNativeAdConfiguration alloc] init];
    switch (preferredAdChoicesPosition) {
        case 0:
            adConfiguration.preferredAdChoicesPosition = YumiMediationAdViewPositionTopLeftCorner;
            break;
        case 1:
             adConfiguration.preferredAdChoicesPosition = YumiMediationAdViewPositionTopRightCorner;
            break;
        case 2:
             adConfiguration.preferredAdChoicesPosition = YumiMediationAdViewPositionBottomLeftCorner;
            break;
        case 3:
             adConfiguration.preferredAdChoicesPosition = YumiMediationAdViewPositionBottomRightCorner;
            break;
            
        default:
            break;
    }
    switch (preferredAdAttributionPosition) {
        case 0:
            adConfiguration.preferredAdAttributionPosition = YumiMediationAdViewPositionTopLeftCorner;
            break;
        case 1:
            adConfiguration.preferredAdAttributionPosition = YumiMediationAdViewPositionTopRightCorner;
            break;
        case 2:
            adConfiguration.preferredAdAttributionPosition = YumiMediationAdViewPositionBottomLeftCorner;
            break;
        case 3:
            adConfiguration.preferredAdAttributionPosition = YumiMediationAdViewPositionBottomRightCorner;
            break;
            
        default:
            break;
    }
    adConfiguration.preferredAdAttributionText = YumiStringFromUTF8String(preferredAdAttributionText);
    adConfiguration.preferredAdAttributionTextFont = [UIFont systemFontOfSize:preferredAdAttributionTextFontSize];
    adConfiguration.hideAdAttribution = hideAdAttribution;
    adConfiguration.preferredAdAttributionTextColor = [YumiAdBridgeTool colorWithARGB:preferredAdAttributionTextColor];
    adConfiguration.preferredAdAttributionTextBackgroundColor = [YumiAdBridgeTool colorWithARGB:preferredAdAttributionTextBackgroundColor];
    
    CGFloat scale = [UIScreen mainScreen].scale;
    NSLog(@"[UIScreen mainScreen].scale = %f,",scale);
    if ([UIScreen mainScreen].bounds.size.height == iPhonePlusHeight) {
        scale = 2.6; // 6/7/8 plus的实际像素比是2.6。 屏幕宽高414:736  物理像素1080:1920
    }
    
    adConfiguration.expressAdSize = CGSizeMake(expressAdViewWidth /scale, expressAdViewHeight / scale);
    
    YumiNative *nativeAd = [[YumiNative alloc] initWithNativeClientReference:nativeClient placementID:YumiStringFromUTF8String(placementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID) configuration:adConfiguration];
    // save pointer
    YumiObjectCache *cache = [YumiObjectCache sharedInstance];
    [cache.references setObject:nativeAd forKey:[nativeAd yumi_referenceKey]];
    return (__bridge YumiTypeNativeAdRef)nativeAd;
}

void RequestNativeAd(YumiTypeNativeAdRef nativeAd, int adCount){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    
    [internalNativeAd loadNativeAd:adCount];
}

void RegisterAssetViewsForInteraction(
                                           YumiTypeNativeAdRef nativeAd,const char *uniqueId,
                                           int adViewX, int adViewY, int adViewWidth, int adViewHeight,
                                           int mediaViewX, int mediaViewY, int mediaViewWidth, int mediaViewHeight,
                                           int iconViewX, int iconViewY, int iconViewWidth, int iconViewHeight,
                                           int ctaViewX, int ctaViewY, int ctaViewWidth, int ctaViewHeight, int titleX, int titleY, int titleWidth, int titleHeight,int descX, int descY, int descWidth, int descHeight){
    // adapte iphone size with screen scale
    CGFloat scale = [UIScreen mainScreen].scale;
    NSLog(@"[UIScreen mainScreen].scale = %f,",scale);
    if ([UIScreen mainScreen].bounds.size.height == iPhonePlusHeight) {
        scale = 2.6; // 6/7/8 plus的实际像素比是2.6。 屏幕宽高414:736  物理像素1080:1920
    }
    CGFloat adViewTop = adViewY/scale;
    CGFloat adViewLeft = adViewX / scale;
    
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    CGRect adViewRect = CGRectMake( adViewLeft, adViewTop, adViewWidth/scale, adViewHeight/scale);
    CGRect mediaViewRect = CGRectMake(mediaViewX/scale - adViewLeft, mediaViewY/scale - adViewTop, mediaViewWidth/scale, mediaViewHeight/scale);
    CGRect iconViewRect = CGRectMake(iconViewX/scale - adViewLeft, iconViewY/scale - adViewTop, iconViewWidth/scale, iconViewHeight/scale);
    CGRect ctaViewRect = CGRectMake(ctaViewX/scale - adViewLeft, ctaViewY/scale - adViewTop, ctaViewWidth/scale,ctaViewHeight/scale);
    
    CGRect titleRect = CGRectMake(titleX/scale - adViewLeft, titleY/scale - adViewTop, titleWidth/scale,titleHeight/scale);
    CGRect descRect = CGRectMake(descX/scale - adViewLeft, descY/scale - adViewTop, descWidth/scale,descHeight/scale);
    
    [internalNativeAd registerNativeForInteraction:YumiStringFromUTF8String(uniqueId) adViewRect:adViewRect mediaViewRect:mediaViewRect iconViewRect:iconViewRect ctaViewRect:ctaViewRect titleRect:titleRect descRect:descRect];
    
}
void UnregisterView(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd unRegisterView:YumiStringFromUTF8String(uniqueId)];
}
void SetNativeCallbacks(YumiTypeNativeAdRef nativeAd ,
                        YumiNativeAdDidReceiveAdCallback adReceivedCallback,
                        YumiNativeAdDidFailToReceiveAdWithErrorCallback adFailCallback,
                        YumiNativeAdDidClickCallback adClickedCallback,
                        YumiNativeExpressAdRenderSuccessDidCallback adRenderSuccessCallBack,
                        YumiNativeExpressAdDidRenderFailCallback adRenderFailCallBack,
                        YumiNativeExpressAdDidClickCloseButtonCallback adClickCloseButtonCallBack){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    internalNativeAd.adReceivedCallback = adReceivedCallback;
    internalNativeAd.adFailedCallback = adFailCallback;
    internalNativeAd.adClickedCallback = adClickedCallback;
    internalNativeAd.adRenderSuccessCallBack = adRenderSuccessCallBack;
    internalNativeAd.adRenderFailCallBack = adRenderFailCallBack;
    internalNativeAd.adClickCloseButtonCallBack = adClickCloseButtonCallBack;
}

BOOL IsAdInvalidated(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return [internalNativeAd isAdInvalidated:YumiStringFromUTF8String(uniqueId)];
}

void ShowView(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd showView:YumiStringFromUTF8String(uniqueId)];
}

void HideView(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd hideView:YumiStringFromUTF8String(uniqueId)];
}

#pragma mark: native Ad view options
void RenderingTitleText(YumiTypeNativeAdRef nativeAd,uint textColor,uint textBgColor ,int fontSize){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd setTitleTextColor:textColor textBgColor:textBgColor fontSize:fontSize];
}
void RenderingDescText(YumiTypeNativeAdRef nativeAd,uint textColor,uint textBgColor ,int fontSize){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd setDescTextColor:textColor textBgColor:textBgColor fontSize:fontSize];
}
void RenderingCallToActionText(YumiTypeNativeAdRef nativeAd,uint textColor,uint textBgColor ,int fontSize){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd setCallToActionTextColor:textColor textBgColor:textBgColor fontSize:fontSize];
}
void RenderingIconScaleType(YumiTypeNativeAdRef nativeAd,int scaleType){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd setIconScaleType:scaleType];
}
void RenderingCoverImageScaleType(YumiTypeNativeAdRef nativeAd,int scaleType){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd setCoverImageScaleType:scaleType];
}
#pragma mark: get native property value

const char *YumiNativeAdBridgeGetTitle(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    
    return cStringCopy([internalNativeAd getTitle:YumiStringFromUTF8String(uniqueId)].UTF8String);
    
}
const char *YumiNativeAdBridgeGetDesc(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return cStringCopy([internalNativeAd getDesc:YumiStringFromUTF8String(uniqueId)].UTF8String);
}
const char *YumiNativeAdBridgeGetIconUrl(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return cStringCopy([internalNativeAd getIconUrl:YumiStringFromUTF8String(uniqueId)].UTF8String);
}
const char *YumiNativeAdBridgeGetCoverImageURL(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return cStringCopy([internalNativeAd getCoverImageUrl:YumiStringFromUTF8String(uniqueId)].UTF8String);
}
const char *YumiNativeAdBridgeGetCallToAction(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return cStringCopy([internalNativeAd getCallToAction:YumiStringFromUTF8String(uniqueId)].UTF8String);
}
const char *YumiNativeAdBridgeGetPrice(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return cStringCopy([internalNativeAd getPrice:YumiStringFromUTF8String(uniqueId)].UTF8String);
}
const char *YumiNativeAdBridgeGetStarRating(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return cStringCopy([internalNativeAd getStarRating:YumiStringFromUTF8String(uniqueId)].UTF8String);
}
const char *YumiNativeAdBridgeGetOther(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return cStringCopy([internalNativeAd getOther:YumiStringFromUTF8String(uniqueId)].UTF8String);
}
BOOL YumiNativeAdBridgeHasVideoContent(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return [internalNativeAd getHasVideoContent:YumiStringFromUTF8String(uniqueId)];
}

BOOL YumiNativeAdBridgeIsExpressAdView(YumiTypeNativeAdRef nativeAd,const char * uniqueId){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    return [internalNativeAd getIsExpressAdView:YumiStringFromUTF8String(uniqueId)];
}

#pragma mark: Splash
YumiTypeSplashAdRef InitYumiSplash(YumiTypeSplashClientRef *splashClient,const char * placementID, const char * channelID, const char * versionID){
    
    YumiSplash *splash = [[YumiSplash alloc] initWithSplashClientReference:splashClient placementID:YumiStringFromUTF8String(placementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID)];
    // save pointer
    YumiObjectCache *cache = [YumiObjectCache sharedInstance];
    [cache.references setObject:splash forKey:[splash yumi_referenceKey]];
    return (__bridge YumiTypeSplashAdRef)splash;
}
void SetSplashOrientation(YumiTypeSplashAdRef splash , int adOrientation){
    YumiSplash *internalSplash = (__bridge YumiSplash *)splash;
    [internalSplash setInterfaceOrientation:adOrientation];
}
void SetSplashFetchTime(YumiTypeSplashAdRef splash , int adFetchDuration){
    YumiSplash *internalSplash = (__bridge YumiSplash *)splash;
    
    [internalSplash setFetchTime:adFetchDuration];
}

void LoadAdAndShowWithBottomViewHeight(YumiTypeSplashAdRef splash , double height){
    YumiSplash *internalSplash = (__bridge YumiSplash *)splash;
    
    [internalSplash loadAdAndShowWithBottomViewHeight:height];
}

void SetSplashCallbacks(YumiTypeSplashAdRef splash,
                             YumiSplashAdSuccessToShowCallback adSuccessToShowCallBack,
                            YumiSplashAdDidFailToShowWithErrorCallback adFailToShowCallBack,
                            YumiSplashAdDidClickCallback adClickCallBack,
                            YumiSplashAdDidCloseCallback adCloseCallBack){
    YumiSplash *internalSplash = (__bridge YumiSplash *)splash;
    
    internalSplash.adSuccessToShowCallback = adSuccessToShowCallBack;
    internalSplash.adFailToShowCallback = adFailToShowCallBack;
    internalSplash.adClickCallback = adClickCallBack;
    internalSplash.adCloseCallback = adCloseCallBack;
    
}
