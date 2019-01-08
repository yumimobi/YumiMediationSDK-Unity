using System;
using System.Collections.Generic;

namespace YumiMediationSDK.Api
{
    public class YumiNativeToLoadEventArgs : EventArgs
    {
        public List<YumiNativeData>  nativeDatta { get; set; }
    }
}
