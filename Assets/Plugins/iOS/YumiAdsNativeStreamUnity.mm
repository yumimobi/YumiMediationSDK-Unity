//
//  YumiAdsNativeStreamUnity.m
//  Unity-iPhone

#import "YumiAdsNativeStreamUnity.h"
#import <YumiMediationSDK/YumiMediationNativeModel.h>

#if defined(__cplusplus)
extern "C"{
#endif
    extern void UnitySendMessage(const char *, const char *, const char *);
    extern NSString* _CreateNSString (const char* string);
#if defined(__cplusplus)
}
#endif

@implementation YumiAdsNativeStreamUnity

#pragma mark - YumiMediationNativeAdDelegate

/// Tells the delegate that an ad has been successfully loaded.
- (void)yumiMediationNativeAdDidLoad:(NSArray<YumiMediationNativeModel *> *)nativeAdArray{
    
}

/// Tells the delegate that a request failed.
- (void)yumiMediationNativeAd:(YumiMediationNativeAd *)nativeAd didFailWithError:(YumiMediationError *)error{
    
}

/// Tells the delegate that the Native view has been clicked.
- (void)yumiMediationNativeAdDidClick:(YumiMediationNativeModel *)nativeAd{
    
}

@end

YumiAdsNativeStreamUnity *nativeStreamDelegate = nil;
YumiMediationNativeAd  *nativeStream = nil;
extern "C" {
    void  _initYumiAdsNativeStream(const char* placementID,const char* channelID,const char* versionID){
        if (!nativeStreamDelegate) {
            nativeStreamDelegate = [[YumiAdsNativeStreamUnity alloc]init];
        }
        if (!nativeStream) {
            nativeStream = [[YumiMediationNativeAd alloc] initWithPlacementID:[NSString stringWithCString:placementID encoding:NSUTF8StringEncoding] channelID:[NSString stringWithCString:channelID encoding:NSUTF8StringEncoding]  versionID:[NSString stringWithCString:versionID encoding:NSUTF8StringEncoding]];
        }
        nativeStream.delegate = nativeStreamDelegate;
    }
    
//    void _loadAd(const int adCount){
//        [nativeStream loadAd:adCount];
//    }
    
   /*
    NSError *error;
    NSData *streamData = [NSJSONSerialization dataWithJSONObject:streamDict options:kNilOptions error:&error];
    NSString *streamString =[[NSString alloc] initWithData:streamData encoding:NSUTF8StringEncoding];
    
    const  char  *streamChar = [streamString UTF8String];
    char  *sendChar =  ( char *)malloc(strlen(streamChar) +1);
    strcpy(sendChar, streamChar);
    
    return sendChar;
    */
    
}
