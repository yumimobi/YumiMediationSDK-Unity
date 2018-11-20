//
//  YumiObjectCache.h
//  Unity-iPhone
//
//  Created by Michael Tang on 2018/11/19.
//

#import <Foundation/Foundation.h>

@interface YumiObjectCache : NSObject

+ (instancetype)sharedInstance;

/// References to objects Google Mobile ads objects created from Unity.
@property(nonatomic, strong) NSMutableDictionary *references;

@end
@interface NSObject (YumiOwnershipAdditions)

/// Returns a key used to lookup a Google Mobile Ads object. This method is intended to only be used
/// by Google Mobile Ads objects.
- (NSString *)yumi_referenceKey;

@end
