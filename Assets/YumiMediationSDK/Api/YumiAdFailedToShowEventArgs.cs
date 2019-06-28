using System;

namespace YumiMediationSDK.Api
{
    /// <summary>
    ///  Event that occurs when an ad fails to show.
    /// </summary>
    public class YumiAdFailedToShowEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
