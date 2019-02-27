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
                             YumiInterstitialDidCloseCallback adClosedCallback
                             ){
    YumiInterstital *internalInterstitial = (__bridge YumiInterstital *)interstitial;
    internalInterstitial.adReceivedCallback = adReceivedCallback;
    internalInterstitial.adFailedCallback = adFailCallback;
    internalInterstitial.adClickCallback = adClickedCallback;
    internalInterstitial.adCloseCallback = adClosedCallback;
    
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
                             YumiRewardVideoDidCloseCallback adClosedCallback
                             ){
    YumiRewardVideo *internalRewardVideo = (__bridge YumiRewardVideo *)rewardVideo;
    internalRewardVideo.adOpenedCallback = adOpenedCallback;
    internalRewardVideo.adStartPlayingCallback = adStartPlaying;
    internalRewardVideo.adRewardedCallback = adRewardedCallback;
    internalRewardVideo.adClosedCallback = adClosedCallback;
    
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

void PresentDebugCenter(const char * bannerPlacementID, const char * interstitialPlacementID, const char * videoPlacementID, const char * nativePlacementID,const char * channelID, const char * versionID){
    YumiDebugCenter *debugcenter = [[YumiDebugCenter alloc] init];
    
    [debugcenter presentWithBannerPlacementID:YumiStringFromUTF8String(bannerPlacementID) interstitialPlacementID:YumiStringFromUTF8String(interstitialPlacementID) videoPlacementID:YumiStringFromUTF8String(videoPlacementID) nativePlacementID:YumiStringFromUTF8String(nativePlacementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID)];
}

#pragma  mark: native  method
YumiTypeNativeAdRef InitYumiNativeAd(YumiTypeNativeClientRef *nativeClient,const char * placementID, const char * channelID, const char * versionID){
    YumiNative *nativeAd = [[YumiNative alloc] initWithNativeClientReference:nativeClient placementID:YumiStringFromUTF8String(placementID) channelID:YumiStringFromUTF8String(channelID) versionID:YumiStringFromUTF8String(versionID)];
    // save pointer
    YumiObjectCache *cache = [YumiObjectCache sharedInstance];
    [cache.references setObject:nativeAd forKey:[nativeAd yumi_referenceKey]];
    return (__bridge YumiTypeNativeAdRef)nativeAd;
}

void RequestNativeAd(YumiTypeNativeAdRef nativeAd, int adCount){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    
    [internalNativeAd loadNativeAd:adCount];
}

void ReportImpression(YumiTypeNativeAdRef nativeAd){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd reportImpression];
}
void ReportClick(YumiTypeNativeAdRef nativeAd){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    [internalNativeAd reportClick];
    
}
void RegisterAssetViewsForInteraction(
                                           YumiTypeNativeAdRef nativeAd,int uniqueId,
                                           int adViewX, int adViewY, int adViewWidth, int adViewHeight,
                                           int mediaViewX, int mediaViewY, int mediaViewWidth, int mediaViewHeight,
                                           int iconViewX, int iconViewY, int iconViewWidth, int iconViewHeight,
                                           int ctaViewX, int ctaViewY, int ctaViewWidth, int ctaViewHeight){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    CGRect adViewRect = CGRectMake(adViewX, adViewY, adViewWidth, adViewHeight);
    CGRect mediaViewRect = CGRectMake(mediaViewX, mediaViewY, mediaViewWidth, mediaViewHeight);
    CGRect iconViewRect = CGRectMake(iconViewX, iconViewY, iconViewWidth, iconViewHeight);
     CGRect ctaViewRect = CGRectMake(ctaViewX, ctaViewY, ctaViewWidth,ctaViewHeight);
    
    [internalNativeAd registerNativeForInteraction:uniqueId adViewRect:adViewRect mediaViewRect:mediaViewRect iconViewRect:iconViewRect ctaViewRect:ctaViewRect];
    
}
void SetNativeCallbacks(YumiTypeNativeAdRef nativeAd ,
                        YumiNativeAdDidReceiveAdCallback adReceivedCallback,
                        YumiNativeAdDidFailToReceiveAdWithErrorCallback adFailCallback,
                        YumiNativeAdDidClickCallback adClickedCallback){
    YumiNative *internalNativeAd = (__bridge YumiNative *)nativeAd;
    internalNativeAd.adReceivedCallback = adReceivedCallback;
    internalNativeAd.adFailedCallback = adFailCallback;
    internalNativeAd.adClickedCallback = adClickedCallback;
}
