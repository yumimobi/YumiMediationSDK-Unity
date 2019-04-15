//
//  YumiNativeBridgeView.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/3/8.
//

#import <UIKit/UIKit.h>

NS_ASSUME_NONNULL_BEGIN

@interface YumiNativeBridgeView : UIView


@property (nonatomic) UIImageView *iconView;
@property (nonatomic) UIImageView *mediaView;
@property (nonatomic) UILabel  *actLab;
@property (nonatomic) UILabel  *titleLab;
@property (nonatomic) UILabel  *descLab;

- (void)setTitleTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize;
- (void)setDescTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize;
- (void)setCallToActionTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize;
- (void)setIconScaleType:(int)scaleType;
- (void)setCoverImageScaleType:(int)scaleType;

@end

NS_ASSUME_NONNULL_END
