//
//  YumiObjectCache.m
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/19.
//

#import "YumiObjectCache.h"

@implementation YumiObjectCache

+ (instancetype)sharedInstance {
    static YumiObjectCache *sharedInstance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        sharedInstance = [[self alloc] init];
    });
    return sharedInstance;
}

- (id)init {
    self = [super init];
    if (self) {
        _references = [[NSMutableDictionary alloc] init];
    }
    return self;
}

@end

@implementation NSObject (YumiOwnershipAdditions)

- (NSString *)yumi_referenceKey {
    return [NSString stringWithFormat:@"%p", (void *)self];
}

@end
