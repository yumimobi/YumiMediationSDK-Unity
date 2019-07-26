using System;
namespace YumiMediationSDK.Api
{
    public class YumiNativeDataEventArgs : EventArgs
    {
        /// <summary>
        /// yumi native data
        /// </summary>
        public YumiNativeData nativeData { get; set; }
    }
}
