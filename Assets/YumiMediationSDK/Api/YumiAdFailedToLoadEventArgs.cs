using System;
namespace YumiMediationSDK.Api
{

    /// <summary>
    /// Event that occurs when an ad fails to load.
    /// </summary>
    public class YumiAdFailedToLoadEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
