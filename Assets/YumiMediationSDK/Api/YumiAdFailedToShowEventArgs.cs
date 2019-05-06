using System;

namespace YumiMediationSDK.Api
{
    // Event that occurs when an ad fails to show.
    public class YumiAdFailedToShowEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
