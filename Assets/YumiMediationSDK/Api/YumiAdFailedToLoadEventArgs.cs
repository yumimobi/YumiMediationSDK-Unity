using System;
namespace YumiMediationSDK.Api
{

    // Event that occurs when an ad fails to load.
    public class YumiAdFailedToLoadEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
