//
//  YumiNative.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/1/9.
//

#import "YumiNative.h"
#import "YumiNativeBridgeView.h"

@interface YumiNative()<YumiMediationNativeAdDelegate>

@property (nonatomic) NSMutableDictionary<NSString *,YumiMediationNativeModel *>  *nativeADDataMapping;
@property (nonatomic) YumiNativeBridgeView  *nativeAdView;

@end

@implementation YumiNative

- (void)dealloc{
    _nativeAd.delegate = nil;
    _nativeAd = nil;
}

- (instancetype)initWithNativeClientReference:(YumiTypeNativeClientRef *)nativeClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID
                                    configuration:(YumiMediationNativeAdConfiguration *)configuration{
    self = [super init];
    if (self) {
        _nativeClient = nativeClientRef;
        _nativeAd = [[YumiMediationNativeAd alloc] initWithPlacementID:placementID channelID:channelID versionID:versionID configuration:configuration];
        _nativeAd.delegate = self;
    }
    
    [self.nativeADDataMapping removeAllObjects];
    
    return self;
}

- (void)loadNativeAd:(NSUInteger)adCount{
    if (!self.nativeAd || adCount <= 0) {
        [self printLogIfError];
        return;
    }
    [self.nativeAd loadAd:adCount];
}

- (void)registerNativeForInteraction:(NSString *)nativeId adViewRect:(CGRect)adViewRect mediaViewRect:(CGRect)mediaViewRect iconViewRect:(CGRect)iconViewRect ctaViewRect:(CGRect)ctaViewRect titleRect:(CGRect)titleRect descRect:(CGRect)descRect{
    
    YumiMediationNativeModel *model = [self getCurrentNativeModel:nativeId];
    if (!model) {
        return ;
    }
    
    __weak __typeof__(self) weakSelf = self;
    dispatch_async(dispatch_get_main_queue(), ^{
        
        UIView *mainView = UnityGetGLView();
        [mainView addSubview:weakSelf.nativeAdView];
        
        // set frame
        [weakSelf.nativeAdView setFrame:adViewRect];
        [weakSelf.nativeAdView.titleLab setFrame:titleRect];
        [weakSelf.nativeAdView.descLab setFrame:descRect];
        [weakSelf.nativeAdView.actLab setFrame:ctaViewRect];
        [weakSelf.nativeAdView.iconView setFrame:iconViewRect];
        [weakSelf.nativeAdView.mediaView setFrame:mediaViewRect];
        // set value
        weakSelf.nativeAdView.titleLab.text = model.title;
        weakSelf.nativeAdView.descLab.text = model.desc;
        weakSelf.nativeAdView.actLab.text = model.callToAction;
        weakSelf.nativeAdView.mediaView.image = model.coverImage.image;
        weakSelf.nativeAdView.iconView.image = model.icon.image;
        
        [weakSelf.nativeAd registerViewForInteraction:weakSelf.nativeAdView
                                  clickableAssetViews:@{
                                                        YumiMediationUnifiedNativeTitleAsset : weakSelf.nativeAdView.titleLab,
                                                        YumiMediationUnifiedNativeDescAsset : weakSelf.nativeAdView.descLab,
                                                        YumiMediationUnifiedNativeCoverImageAsset : weakSelf.nativeAdView.mediaView,
                                                        YumiMediationUnifiedNativeMediaViewAsset : weakSelf.nativeAdView.mediaView,
                                                        YumiMediationUnifiedNativeIconAsset : weakSelf.nativeAdView.iconView,
                                                        YumiMediationUnifiedNativeCallToActionAsset : weakSelf.nativeAdView.actLab
                                                        }
                                   withViewController:UnityGetGLViewController()
                                             nativeAd:model];
    });
    
}

- (void)unRegisterView:(NSString *)uniqueId{
    
    if (self.nativeAdView) {
        [self.nativeAdView removeFromSuperview];
        self.nativeAdView = nil;
    }
    
}
- (BOOL)isAdInvalidated:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    
    return [model isExpired];
}
- (void)showView:(NSString *)uniqueId{
    self.nativeAdView.hidden = NO;
    ///report impression
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    if (!model) {
        return;
    }
    [self.nativeAd reportImpression:model view:self.nativeAdView];
}
- (void)hideView:(NSString *)uniqueId{
    self.nativeAdView.hidden = YES;
}

/// get native data property
- (NSString *)getTitle:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return model.title;
}
- (NSString *)getDesc:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return model.desc;
}
- (NSString *)getCallToAction:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return model.callToAction;
}
- (NSString *)getIconUrl:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return [NSString stringWithFormat:@"%@",model.icon.imageURL];
}
- (NSString *)getCoverImageUrl:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return [NSString stringWithFormat:@"%@",model.coverImage.imageURL];
}
- (NSString *)getPrice:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return model.price;
}
- (NSString *)getStarRating:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return model.starRating;
}
- (NSString *)getOther:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return model.other;
}
- (BOOL)getHasVideoContent:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return [model hasVideoContent];
}

- (void)printLogIfError{
    NSLog(@"YumiMobileAdsPlugin: NativeAd is nil or adCount <= 0. Ignoring ad request.");
}

- (NSString *)nativeDataUUIDKey{
    return  [[NSUUID UUID] UUIDString];
}

- (YumiMediationNativeModel *)getCurrentNativeModel:(NSString *)uniqueId{
    return self.nativeADDataMapping[uniqueId];
}
#pragma mark: native Ad view options
- (void)setTitleTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize{
    [self.nativeAdView setTitleTextColor:textColor textBgColor:textBgColor fontSize:fontSize];
}
- (void)setDescTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize{
    [self.nativeAdView setDescTextColor:textColor textBgColor:textBgColor fontSize:fontSize];
}
- (void)setCallToActionTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize{
    [self.nativeAdView setCallToActionTextColor:textColor textBgColor:textBgColor fontSize:fontSize];
}
- (void)setIconScaleType:(int)scaleType{
    [self.nativeAdView setIconScaleType:scaleType];
}
- (void)setCoverImageScaleType:(int)scaleType{
    [self.nativeAdView setCoverImageScaleType:scaleType];
}
#pragma mark:YumiMediationNativeAdDelegate

/// Tells the delegate that an ad has been successfully loaded.
- (void)yumiMediationNativeAdDidLoad:(NSArray<YumiMediationNativeModel *> *)nativeAdArray{
   
    __weak __typeof__(self) weakSelf = self;
    NSMutableArray *nativeKeys = [NSMutableArray arrayWithCapacity:1];
   // save native data
    [nativeAdArray enumerateObjectsUsingBlock:^(YumiMediationNativeModel * _Nonnull obj, NSUInteger idx, BOOL * _Nonnull stop) {
        if (!obj) {
            return ;
        }
        NSString *key = [weakSelf nativeDataUUIDKey];
        [weakSelf.nativeADDataMapping setValue:obj forKey:key];
        [nativeKeys addObject:key];
    }];
    
    NSString *keysString = [nativeKeys componentsJoinedByString:@","];
    
   // call back did load
    if (self.adReceivedCallback) {
        self.adReceivedCallback(self.nativeClient,[keysString cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that a request failed.
- (void)yumiMediationNativeAd:(YumiMediationNativeAd *)nativeAd didFailWithError:(YumiMediationError *)error{
    if (self.adFailedCallback) {
        NSString *errorMsg = [NSString
                              stringWithFormat:@"Failed to receive ad with error: %@", [error localizedFailureReason]];
        self.adFailedCallback(self.nativeClient,[errorMsg cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

/// Tells the delegate that the Native view has been clicked.
- (void)yumiMediationNativeAdDidClick:(YumiMediationNativeModel *)nativeAd{
    if (self.adClickedCallback) {
        self.adClickedCallback(self.nativeClient);
    }
}

#pragma mark: getter method
- (NSMutableDictionary<NSString *,YumiMediationNativeModel *> *)nativeADDataMapping{
    if (!_nativeADDataMapping) {
        _nativeADDataMapping = [NSMutableDictionary dictionaryWithCapacity:1];
    }
    return _nativeADDataMapping;
}

- (YumiNativeBridgeView *)nativeAdView{
    if (!_nativeAdView) {
        _nativeAdView = [[YumiNativeBridgeView alloc] init];
        _nativeAdView.hidden = YES;
    }
    return _nativeAdView;
}

@end
