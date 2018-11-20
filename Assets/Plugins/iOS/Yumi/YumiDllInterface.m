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

#pragma mark: request ads
void RequestBannerAd( YumiTypeBannerRef bannerView,BOOL isSmart){
    YumiBanner *internalBanner = (__bridge YumiBanner *)bannerView;
    [internalBanner loadAd:isSmart];
}
#pragma mark - Other methods
/// Removes an object from the cache.
void YumiRelease(YumiTypeRef ref) {
    if (ref) {
        YumiObjectCache *cache = [YumiObjectCache sharedInstance];
        [cache.references removeObjectForKey:[(__bridge NSObject *)ref yumi_referenceKey]];
    }
}
