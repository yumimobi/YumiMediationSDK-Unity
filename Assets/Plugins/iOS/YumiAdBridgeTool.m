//
//  YumiAdBridgeTool.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2019/3/8.
//

#import "YumiAdBridgeTool.h"

@implementation YumiAdBridgeTool


+(UIColor *)colorWithARGB:(NSUInteger)ARGBValue
{
    return [UIColor colorWithRed:((float)((ARGBValue & 0xFF0000) >> 16)) / 255.0
                           green:((float)((ARGBValue & 0xFF00) >> 8)) / 255.0
                            blue:((float)(ARGBValue & 0xFF))/255.0
                           alpha:((float)((ARGBValue & 0xFF000000) >> 24)) / 255.0];
}

@end
