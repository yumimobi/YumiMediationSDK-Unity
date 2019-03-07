//
//  YumiNative.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/1/9.
//

#import "YumiNative.h"

@interface YumiNative()<YumiMediationNativeAdDelegate>

@property (nonatomic) NSMutableDictionary<NSString *,YumiMediationNativeModel *>  *nativeADDataMapping;
@property (nonatomic) UIView  *adView;

@end

@implementation YumiNative

- (void)dealloc{
    _nativeAd.delegate = nil;
    _nativeAd = nil;
}

- (instancetype)initWithNativeClientReference:(YumiTypeNativeClientRef *)nativeClientRef
                                  placementID:(NSString *)placementID
                                    channelID:(NSString *)channelID
                                    versionID:(NSString *)versionID{
    self = [super init];
    if (self) {
        _nativeClient = nativeClientRef;
        YumiMediationNativeAdConfiguration *configuration = [[YumiMediationNativeAdConfiguration alloc] init];
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
    
    __weak __typeof__(self) weakSelf = self;
    dispatch_async(dispatch_get_main_queue(), ^{
        
        YumiMediationNativeModel *model = [weakSelf getCurrentNativeModel:nativeId];
        
        UIView *mainView = UnityGetGLView();
        weakSelf.adView = [[UIView alloc] initWithFrame:adViewRect];
        weakSelf.adView.hidden = YES; // default is Yes
        
        UIImageView *iconView = [[UIImageView alloc] initWithFrame:iconViewRect];
        UIImageView *mediaView = [[UIImageView alloc] initWithFrame:mediaViewRect];
        UILabel *actLab = [[UILabel alloc] initWithFrame:ctaViewRect];
        
        UILabel *titleLab = [[UILabel alloc] initWithFrame:titleRect];
        UILabel *descLab = [[UILabel alloc] initWithFrame:descRect];
        
        [mainView addSubview:weakSelf.adView];
        [weakSelf.adView addSubview:iconView];
        [weakSelf.adView addSubview:mediaView];
        [weakSelf.adView addSubview:actLab];
        [weakSelf.adView addSubview:titleLab];
        [weakSelf.adView addSubview:descLab];
        
        // render value to view
        titleLab.numberOfLines = 0;
        descLab.numberOfLines = 0;
        titleLab.font = [UIFont systemFontOfSize:15.0];
        descLab.font = [UIFont systemFontOfSize:13.0];
        // image
        
        
        [weakSelf.nativeAd registerViewForInteraction:weakSelf.adView
                                  clickableAssetViews:@{
                                                        YumiMediationUnifiedNativeTitleAsset : titleLab,
                                                        YumiMediationUnifiedNativeDescAsset : descLab,
                                                        YumiMediationUnifiedNativeCoverImageAsset : mediaView,
                                                        YumiMediationUnifiedNativeMediaViewAsset : mediaView,
                                                        YumiMediationUnifiedNativeIconAsset : iconView,
                                                        YumiMediationUnifiedNativeCallToActionAsset : actLab
                                                        }
                                   withViewController:UnityGetGLViewController()
                                             nativeAd:model];
    });
    
}

- (void)unRegisterView:(NSString *)uniqueId{
    
    if (self.adView) {
        [self.adView removeFromSuperview];
    }
    
}
- (BOOL)isAdInvalidated:(NSString *)uniqueId{
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    return [model isExpired];
}
- (void)showView:(NSString *)uniqueId{
    self.adView.hidden = NO;
    ///report impression
    YumiMediationNativeModel *model = [self getCurrentNativeModel:uniqueId];
    [self.nativeAd reportImpression:model view:self.adView];
}
- (void)hideView:(NSString *)uniqueId{
    self.adView.hidden = YES;
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

@end
