using System;
using UnityEngine;
namespace YumiMediationSDK.Api
{
    public class YumiNativeAdOptions
    {
        /// <summary>
        /// Gets the ad choise view position.
        /// </summary>
        /// <value>The ad choise view position.</value>
        public AdOptionViewPosition adChoiseViewPosition { get; private set; }
        /// <summary>
        /// Gets the ad attribution.
        /// </summary>
        /// <value>The ad attribution.</value>
        public AdAttribution adAttribution { get; private set; }
        /// <summary>
        /// Gets the title text options.
        /// </summary>
        /// <value>The title text options.</value>
        public TextOptions titleTextOptions { get; private set; }
        /// <summary>
        /// Gets the desc text options.
        /// </summary>
        /// <value>The desc text options.</value>
        public TextOptions descTextOptions { get; private set; }
        /// <summary>
        /// Gets the call to action text options.
        /// </summary>
        /// <value>The call to action text options.</value>
        public TextOptions callToActionTextOptions { get; private set; }
        /// <summary>
        /// Gets the type of the icon scale.
        /// </summary>
        /// <value>The type of the icon scale.</value>
        public ScaleType iconScaleType { get; private set; }
        /// <summary>
        /// Gets the type of the cover image scale.
        /// </summary>
        /// <value>The type of the cover image scale.</value>
        public ScaleType coverImageScaleType { get; private set; }
        /// <summary>
        /// Gets express ad view transform
        /// </summary>
        public Transform expressAdViewTransform { get; private set; }

        internal YumiNativeAdOptions(NativeAdOptionsBuilder builder)
        {
            adChoiseViewPosition = builder.adChoiseViewPosition;
            adAttribution = builder.adAttribution;
            titleTextOptions = builder.titleTextOptions;
            descTextOptions = builder.descTextOptions;
            callToActionTextOptions = builder.callToActionTextOptions;
            iconScaleType = builder.iconScaleType;
            coverImageScaleType = builder.coverImageScaleType;
            expressAdViewTransform = builder.expressAdViewTransform;
        }
    }

    public class NativeAdOptionsBuilder
    {
        internal AdOptionViewPosition adChoiseViewPosition = AdOptionViewPosition.TOP_RIGHT;
        internal AdAttribution adAttribution = new AdAttribution
        {
            AdOptionsPosition = AdOptionViewPosition.TOP_LEFT,
            text = " Ad ",
            textColor = 0xff222222,
            backgroundColor = 0x11eeeeee,
            textSize = 8,
            hide = false
        };

        internal TextOptions titleTextOptions = new TextOptions
        {
            textSize = 15,
            textColor = 0xff222222,
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
        internal Transform expressAdViewTransform;
        /// <summary>
        /// Sets the ad choices position
        /// </summary>
        /// <returns>The NativeAdOptionsBuilder instance.</returns>
        /// <param name="position">Position.</param>
        public NativeAdOptionsBuilder setAdChoices(AdOptionViewPosition position)
        {
            adChoiseViewPosition = position;
            return this;
        }
        /// <summary>
        /// Sets the ad attribution.
        /// </summary>
        /// <returns>The NativeAdOptionsBuilder instance.</returns>
        /// <param name="position">Position.</param>
        /// <param name="text">Text.</param>
        /// <param name="textSize">Text size.</param>
        /// <param name="textColor">Text color.</param>
        /// <param name="backgroundColor">Background color.</param>
        /// <param name="hide">If set to <c>true</c> hide.</param>
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
        /// <summary>
        /// Sets the title text options.
        /// </summary>
        /// <returns>The NativeAdOptionsBuilder instance.</returns>
        /// <param name="textSize">Text size.</param>
        /// <param name="textColor">Text color.</param>
        /// <param name="backgroundColor">Background color.</param>
        public NativeAdOptionsBuilder setTitleTextOptions(int textSize, uint textColor, uint backgroundColor)
        {
            titleTextOptions.textSize = textSize;
            titleTextOptions.textColor = textColor;
            titleTextOptions.backgroundColor = backgroundColor;
            return this;
        }
        /// <summary>
        /// Sets the desc text options.
        /// </summary>
        /// <returns>The NativeAdOptionsBuilder instance.</returns>
        /// <param name="textSize">Text size.</param>
        /// <param name="textColor">Text color.</param>
        /// <param name="backgroundColor">Background color.</param>
        public NativeAdOptionsBuilder setDescTextOptions(int textSize, uint textColor, uint backgroundColor)
        {
            descTextOptions.textSize = textSize;
            descTextOptions.textColor = textColor;
            descTextOptions.backgroundColor = backgroundColor;
            return this;
        }
        /// <summary>
        /// Sets the call to action text options.
        /// </summary>
        /// <returns>The NativeAdOptionsBuilder instance.</returns>
        /// <param name="textSize">Text size.</param>
        /// <param name="textColor">Text color.</param>
        /// <param name="backgroundColor">Background color.</param>
        public NativeAdOptionsBuilder setCallToActionTextOptions(int textSize, uint textColor, uint backgroundColor)
        {
            callToActionTextOptions.textSize = textSize;
            callToActionTextOptions.textColor = textColor;
            callToActionTextOptions.backgroundColor = backgroundColor;
            return this;
        }
        /// <summary>
        /// Sets the type of the icon scale.
        /// </summary>
        /// <returns>The NativeAdOptionsBuilder instance.</returns>
        /// <param name="scaleType">Scale type.</param>
        public NativeAdOptionsBuilder setIconScaleType(ScaleType scaleType)
        {
            iconScaleType = scaleType;
            return this;
        }
        /// <summary>
        /// Sets the type of the cover image scale.
        /// </summary>
        /// <returns>The NativeAdOptionsBuilder instance.</returns>
        /// <param name="scaleType">Scale type.</param>
        public NativeAdOptionsBuilder setCoverImageScaleType(ScaleType scaleType)
        {
            coverImageScaleType = scaleType;
            return this;
        }

        public NativeAdOptionsBuilder setExpressAdViewTransform(Transform adViewTransform)
        {
            
            expressAdViewTransform = adViewTransform;
            return this;
        }
        /// <summary>
        /// Build this instance.
        /// </summary>
        /// <returns>The YumiNativeAdOptions instance.</returns>
        public YumiNativeAdOptions Build()
        {
            return new YumiNativeAdOptions(this);
        }
    }

    public enum AdOptionViewPosition
    {
        /// <summary>
        /// The top left.
        /// </summary>
        TOP_LEFT,
        /// <summary>
        /// The top right.
        /// </summary>
        TOP_RIGHT,
        /// <summary>
        /// The bottom left.
        /// </summary>
        BOTTOM_LEFT,
        /// <summary>
        /// The bottom right.
        /// </summary>
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

    public struct AdAttribution
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

    public struct TextOptions
    {
        internal int textSize;
        internal uint textColor;
        internal uint backgroundColor;
    }
}
