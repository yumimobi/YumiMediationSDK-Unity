using System;
namespace YumiMediationSDK.Api
{
    /// <summary>
    /// Event that occurs when an ad closed
    /// </summary>
    public class YumiAdCloseEventArgs : EventArgs
    {
        public bool IsRewarded { get; set; }
    }
}
