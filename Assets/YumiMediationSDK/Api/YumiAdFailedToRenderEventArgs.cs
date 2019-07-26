using System;
namespace YumiMediationSDK.Api
{
    public class YumiAdFailedToRenderEventArgs : EventArgs
    {
        /// <summary>
        /// yumi netive data
        /// </summary>
        public YumiNativeData nativeData { get; set; }
        /// <summary>
        /// error message
        /// </summary>
        public string Message { get; set; }
    }
}
