//
//  YumiNativeBridgeView.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/3/8.
//

#import "YumiNativeBridgeView.h"
#import "YumiAdBridgeTool.h"

@implementation YumiNativeBridgeView

- (void)setTitleTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize{
    self.titleLab.textColor = [YumiAdBridgeTool colorWithARGB:textColor];
    self.titleLab.backgroundColor = [YumiAdBridgeTool colorWithARGB:textBgColor];
    self.titleLab.font = [UIFont systemFontOfSize:fontSize];
}
- (void)setDescTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize{
    self.descLab.textColor = [YumiAdBridgeTool colorWithARGB:textColor];
    self.descLab.backgroundColor = [YumiAdBridgeTool colorWithARGB:textBgColor];
    self.descLab.font = [UIFont systemFontOfSize:fontSize];
}
- (void)setCallToActionTextColor:(uint)textColor textBgColor:(uint)textBgColor fontSize:(int)fontSize{
    self.actLab.textColor = [YumiAdBridgeTool colorWithARGB:textColor];
    self.actLab.backgroundColor = [YumiAdBridgeTool colorWithARGB:textBgColor];
    self.actLab.font = [UIFont systemFontOfSize:fontSize];
}
- (void)setIconScaleType:(int)scaleType{
    switch (scaleType) {
        case 0:
            self.iconView.contentMode = UIViewContentModeScaleToFill;
            break;
        case 1:
            self.iconView.contentMode = UIViewContentModeScaleAspectFit;
            break;
        case 2:
            self.iconView.contentMode = UIViewContentModeScaleAspectFill;
            break;
            
        default:
            break;
    }
    
}
- (void)setCoverImageScaleType:(int)scaleType{
    switch (scaleType) {
        case 0:
            self.mediaView.contentMode = UIViewContentModeScaleToFill;
            break;
        case 1:
            self.mediaView.contentMode = UIViewContentModeScaleAspectFit;
            break;
        case 2:
            self.mediaView.contentMode = UIViewContentModeScaleAspectFill;
            break;
            
        default:
            break;
    }
}

#pragma mark: getter

-(UIImageView *)iconView{
    if (!_iconView) {
        _iconView = [[UIImageView alloc] init];
        [self addSubview:_iconView];
    }
    return _iconView;
}
- (UIImageView *)mediaView{
    if (!_mediaView) {
        _mediaView = [[UIImageView alloc] init];
        [self addSubview:_mediaView];
    }
    return _mediaView;
}
- (UILabel *)titleLab{
    if (!_titleLab) {
        _titleLab = [[UILabel alloc] init];
        _titleLab.numberOfLines = 0;
        [self addSubview:_titleLab];
    }
    return _titleLab;
}

- (UILabel *)descLab{
    if (!_descLab) {
        _descLab = [[UILabel alloc] init];
        _descLab.numberOfLines = 0;
        [self addSubview:_descLab];
    }
    return _descLab;
}
- (UILabel *)actLab{
    if (!_actLab) {
        _actLab = [[UILabel alloc] init];
        _actLab.numberOfLines = 0;
        _actLab.textAlignment = NSTextAlignmentCenter;
        [self addSubview:_actLab];
    }
    return _actLab;
}

@end
