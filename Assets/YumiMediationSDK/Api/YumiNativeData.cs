using System;
namespace YumiMediationSDK.Api
{
    public struct YumiNativeData
    {
        /// <summary>
        /// native ad uniqueId 
        /// </summary>
        public string uniqueId;
        /// <summary>
        /// native ad title
        /// </summary>
        public string title;
        /// <summary>
        ///  ad description 
        /// </summary>
        public string desc;
        /// <summary>
        /// icon image url 
        /// </summary>
        public string iconURL;
        /// <summary>
        ///  cover image url
        /// </summary>
        public string coverImageURL;
        /// <summary>
        ///  price, for example "free"
        /// </summary>
        public string price;
        /// <summary>
        ///  rating (0 to 5).
        /// </summary>
        public string starRating;
        /// <summary>
        ///  call to action phrase of the ad, for example "download".
        /// </summary>
        public string callToAction;
        /// <summary>
        ///  other data
        /// </summary>
        public string other;
    }
}
