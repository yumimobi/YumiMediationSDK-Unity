using System;
namespace YumiMediationSDK.Api
{
    public struct YumiNativeData
    {
        // native ad uniqueId 
        public string uniqueId;
        // title 
        public string title;
        // ad description 
        public string desc;
        // icon image url 
        public string iconURL;
        // cover image url
        public string coverImageURL;
        // price, for example "free"
        public string price;
        // rating (0 to 5).
        public string starRating;
        // call to action phrase of the ad, for example "download".
        public string callToAction;
        // other data
        public string other;
    }
}
