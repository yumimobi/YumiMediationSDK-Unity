using System;
namespace YumiMediationSDK.Api
{
    // Event that occurs when an ad closed
    public class YumiAdCloseEventArgs : EventArgs
    {
        public bool IsRewarded { get; set; }
    }
}
