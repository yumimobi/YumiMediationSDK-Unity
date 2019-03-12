using System;
namespace YumiMediationSDK.Api
{
    public class YumiNativeAdOptions
    {
        internal AdOptionViewPosition adChoiseViewPosition;
        internal AdAttribution adAttribution;
        internal TextOptions titleTextOptions;
        internal TextOptions descTextOptions;
        internal TextOptions callToActionTextOptions;
        internal ScaleType iconScaleType;
        internal ScaleType coverImageScaleType;

        internal YumiNativeAdOptions(NativeAdOptionsBuilder builder)
        {
            adChoiseViewPosition = builder.adChoiseViewPosition;
            adAttribution = builder.adAttribution;
            titleTextOptions = builder.titleTextOptions;
            descTextOptions = builder.descTextOptions;
            callToActionTextOptions = builder.callToActionTextOptions;
            iconScaleType = builder.iconScaleType;
            coverImageScaleType = builder.coverImageScaleType;
        }
    }

    public class NativeAdOptionsBuilder
    {
        internal AdOptionViewPosition adChoiseViewPosition = AdOptionViewPosition.TOP_RIGHT;
        internal AdAttribution adAttribution = new AdAttribution
        {
            AdOptionsPosition = AdOptionViewPosition.TOP_LEFT,
            text = "Ad",
            textColor = 0xff222222,
            backgroundColor = 0x11eeeeee,
            textSize = 8,
            hide = false
        };

        internal TextOptions titleTextOptions = new TextOptions
        {
            textSize = 15,
            textColor = 0x11000000,
            backgroundColor = 0x00000000

        };

        internal TextOptions descTextOptions = new TextOptions
        {
            textSize = 12,
            textColor = 0xff222222,
            backgroundColor = 0x00000000
        };

        internal TextOptions callToActionTextOptions = new TextOptions
        {
            textSize = 15,
            textColor = 0xff222222,
            backgroundColor = 0xff00ff00
        };

        internal ScaleType iconScaleType = ScaleType.SCALE_TO_FILL;
        internal ScaleType coverImageScaleType = ScaleType.SCALE_TO_FILL;


        public NativeAdOptionsBuilder setAdChoices(AdOptionViewPosition position)
        {
            adChoiseViewPosition = position;
            return this;
        }

        public NativeAdOptionsBuilder setAdAttribution(AdOptionViewPosition position, string text, int textSize, uint textColor, uint backgroundColor, bool hide)
        {
            adAttribution.AdOptionsPosition = position;
            adAttribution.text = text;
            adAttribution.textSize = textSize;
            adAttribution.textColor = textColor;
            adAttribution.backgroundColor = backgroundColor;
            adAttribution.hide = hide;
            return this;
        }

        public NativeAdOptionsBuilder setTitleTextOptions(int textSize, uint textColor, uint backgroundColor)
        {
            titleTextOptions.textSize = textSize;
            titleTextOptions.textColor = textColor;
            titleTextOptions.backgroundColor = backgroundColor;
            return this;
        }

        public NativeAdOptionsBuilder setDescTextOptions(int textSize, uint textColor, uint backgroundColor)
        {
            descTextOptions.textSize = textSize;
            descTextOptions.textColor = textColor;
            descTextOptions.backgroundColor = backgroundColor;
            return this;
        }

        public NativeAdOptionsBuilder setCallToActionTextOptions(int textSize, uint textColor, uint backgroundColor)
        {
            callToActionTextOptions.textSize = textSize;
            callToActionTextOptions.textColor = textColor;
            callToActionTextOptions.backgroundColor = backgroundColor;
            return this;
        }

        public NativeAdOptionsBuilder setIconScaleType(ScaleType scaleType)
        {
            iconScaleType = scaleType;
            return this;
        }

        public NativeAdOptionsBuilder setCoverImageScaleType(ScaleType scaleType)
        {
            coverImageScaleType = scaleType;
            return this;
        }

        public YumiNativeAdOptions Build()
        {
            return new YumiNativeAdOptions(this);
        }
    }

    public enum AdOptionViewPosition
    {
        TOP_LEFT,
        TOP_RIGHT,
        BOTTOM_LEFT,
        BOTTOM_RIGHT
    }

    public enum ScaleType
    {
        /// <summary>
        /// contents scaled to fill both with width and height. the content's aspect ratio may be changed 
        /// </summary>
        SCALE_TO_FILL,
        /// <summary>
        /// contents scaled to fit with fixed aspect. remainder is transparent
        /// </summary>
        SCALE_ASPECT_FIT,
        /// <summary>
        /// contents scaled to fill with fixed aspect. some portion of content may be clipped
        /// </summary>
        SCALE_ASPECT_FILL
    }

    struct AdAttribution
    {
        internal AdOptionViewPosition AdOptionsPosition;
        internal string text;
        /// <summary>
        /// ARGB format: 0xffffffff(0xAARRGGBB) refer to wihte
        /// </summary>
        internal uint textColor;
        internal uint backgroundColor;
        /// <summary>
        /// The size of the ad attribution text. 
        /// 1 = 1 point on iOS, 1 = 1sp on Android.
        /// </summary>
        internal int textSize;
        internal bool hide;
    }

    struct TextOptions
    {
        internal int textSize;
        internal uint textColor;
        internal uint backgroundColor;
    }
}
