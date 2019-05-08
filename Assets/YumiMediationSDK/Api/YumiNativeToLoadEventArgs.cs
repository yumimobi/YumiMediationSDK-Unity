using System;
using System.Collections.Generic;

namespace YumiMediationSDK.Api
{
    /// <summary>
    /// the native ad load  event arguments.
    /// </summary>
    public class YumiNativeToLoadEventArgs : EventArgs
    {
        /// <summary>
        /// the native data list
        /// </summary>
        /// <value>The native data.</value>
        public List<YumiNativeData>  nativeData { get; set; }
    }
}
