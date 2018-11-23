using System;
using YumiMediationSDK.Common;

namespace YumiMediationSDK
{
    public class YumiAdsClientFactory
    {
        public static IYumiBannerClient BuildBannerClient()
        {

#if UNITY_ANDROID
            return new YumiMediationSDK.Android.YumiBannerClient();

#elif UNITY_IOS
                return new YumiMediationSDK.iOS.YumiBannerClient();
#else
                return new YumiMediationSDK.Common.YumiDummyClient();
#endif
        }
        public static IYumiInterstitialClient BuildInterstitialClient()
        {

#if UNITY_ANDROID
            return new YumiMediationSDK.Android.YumiInterstitialClient();

#elif UNITY_IOS
            return new YumiMediationSDK.iOS.YumiInterstitialClient();
#else
                return new YumiMediationSDK.Common.YumiDummyClient();
#endif
        }
        public static IYumiRewardVideoClient BuildRewardVideoClient()
        {

#if UNITY_ANDROID
            return new YumiMediationSDK.Android.YumiRewardVideoClient();
           
#elif UNITY_IOS
            return new YumiMediationSDK.iOS.YumiRewardVideoClient();
#else
                return new YumiMediationSDK.Common.YumiDummyClient();
#endif
        }
        public static IYumiDebugCenterClient BuildDebugCenterClient()
        {

#if UNITY_ANDROID
            return new YumiMediationSDK.Android.YumiDebugCenterClient();
           
#elif UNITY_IOS
            return new YumiMediationSDK.iOS.YumiDebugCenterClient();
#else
                return new YumiMediationSDK.Common.YumiDummyClient();
#endif
        }
    }
}
