package com.zplay.unity.adsyumi;

import android.text.TextUtils;
import android.util.Log;
import android.widget.ImageView;

import static android.widget.ImageView.ScaleType.CENTER_CROP;
import static android.widget.ImageView.ScaleType.CENTER_INSIDE;
import static android.widget.ImageView.ScaleType.FIT_CENTER;
import static android.widget.ImageView.ScaleType.FIT_XY;
import static com.yumi.android.sdk.ads.formats.YumiNativeAdOptions.POSITION_BOTTOM_LEFT;
import static com.yumi.android.sdk.ads.formats.YumiNativeAdOptions.POSITION_BOTTOM_RIGHT;
import static com.yumi.android.sdk.ads.formats.YumiNativeAdOptions.POSITION_TOP_LEFT;
import static com.yumi.android.sdk.ads.formats.YumiNativeAdOptions.POSITION_TOP_RIGHT;

/**
 * Description:
 * <p>
 * Created by lgd on 2019/3/8.
 */
class NativeAdOptions {
    private static final String TAG = "NativeAdOptions";
    private static int DEFAULT_COLOR = 0xff222222;
    private static int DEFAULT_BACKGROUND_COLOR = 0x00000000;

    private int mAdChosePosition;

    private int mAttriPosition;
    private String mAttriText;
    private int mAttriTextSize;
    private int mAttriTextColor;
    private int mAttriTextBackgroundColor;

    private int mTitleSize;
    private int mTitleColor;
    private int mTitleBackgroundColor;

    private int mDescSize;
    private int mDescColor;
    private int mDescBackgroundColor;

    private int mCtaSize;
    private int mCtaColor;
    private int mCtaBackgroundColor;

    private int mIconScaleType;
    private int mCoverImageScaleType;


    NativeAdOptions(int adChosePosition,
                    int attriPosition, String attriText, int attriTextSize, String attriTextColor, String attriTextBackgroundColor,
                    int titleSize, String titleColor, String titleBackgroundColor,
                    int descSize, String descColor, String descBackgroundColor,
                    int ctaSize, String ctaColor, String ctaBackgroundColor,
                    int iconScaleType, int coverImageScaleType) {
        mAdChosePosition = adChosePosition;

        mAttriPosition = attriPosition;
        mAttriText = attriText;
        mAttriTextSize = attriTextSize;
        mAttriTextColor = getColorFromString(attriTextColor, DEFAULT_COLOR);
        mAttriTextBackgroundColor = getColorFromString(attriTextBackgroundColor, DEFAULT_BACKGROUND_COLOR);

        mTitleSize = titleSize;
        mTitleColor = getColorFromString(titleColor, DEFAULT_COLOR);
        mTitleBackgroundColor = getColorFromString(titleBackgroundColor, DEFAULT_BACKGROUND_COLOR);

        mDescSize = descSize;
        mDescColor = getColorFromString(descColor, DEFAULT_COLOR);
        mDescBackgroundColor = getColorFromString(descBackgroundColor, DEFAULT_BACKGROUND_COLOR);

        mCtaSize = ctaSize;
        mCtaColor = getColorFromString(ctaColor, DEFAULT_COLOR);
        mCtaBackgroundColor = getColorFromString(ctaBackgroundColor, DEFAULT_BACKGROUND_COLOR);

        mIconScaleType = iconScaleType;
        mCoverImageScaleType = coverImageScaleType;

    }

    private int getColorFromString(String hexString, int defaultColor) {
        try {
            return Long.valueOf(hexString, 16).intValue();
        } catch (NumberFormatException e) {
            Log.d(TAG, "getColorFromString: ", e);
            return defaultColor;
        }
    }

    int getAdChosePosition() {
        switch (mAdChosePosition) {
            case 0:
                return POSITION_TOP_LEFT;
            case 1:
                return POSITION_TOP_RIGHT;
            case 2:
                return POSITION_BOTTOM_RIGHT;
            case 3:
                return POSITION_BOTTOM_LEFT;
            default:
                return POSITION_TOP_RIGHT;
        }
    }

    int getAttriPosition() {
        switch (mAttriPosition) {
            case 0:
                return POSITION_TOP_LEFT;
            case 1:
                return POSITION_TOP_RIGHT;
            case 2:
                return POSITION_BOTTOM_RIGHT;
            case 3:
                return POSITION_BOTTOM_LEFT;
            default:
                return POSITION_TOP_LEFT;
        }
    }

    String getAttriText() {
        return TextUtils.isEmpty(mAttriText) ? "ad" : mAttriText;
    }

    int getAttriTextSize() {
        return mAttriTextSize < 0 ? 0 : mAttriTextSize;
    }

    int getAttriTextColor() {
        return mAttriTextColor;
    }

    int getAttriTextBackgroundColor() {
        return mAttriTextBackgroundColor;
    }

    int getTitleSize() {
        return mTitleSize < 0 ? 0 : mTitleSize;
    }

    int getTitleColor() {
        return mTitleColor;
    }

    int getTitleBackgroundColor() {
        return mTitleBackgroundColor;
    }

    int getDescSize() {
        return mDescSize < 0 ? 0 : mDescSize;
    }

    int getDescColor() {
        return mDescColor;
    }

    int getDescBackgroundColor() {
        return mDescBackgroundColor;
    }

    int getCtaSize() {
        return mCtaSize < 0 ? 0 : mCtaSize;
    }

    int getCtaColor() {
        return mCtaColor;
    }

    int getCtaBackgroundColor() {
        return mCtaBackgroundColor;
    }

    ImageView.ScaleType getIconScaleType() {
        switch (mIconScaleType) {
            case 0:
                return FIT_XY;
            case 1:
                return CENTER_INSIDE;
            default:
                return CENTER_CROP;
        }
    }

    ImageView.ScaleType getCoverImageScaleType() {
        Log.d(TAG, "getCoverImageScaleType: " + mCoverImageScaleType);
        switch (mCoverImageScaleType) {
            case 0:
                return FIT_XY;
            case 1:
                return CENTER_INSIDE;
            default:
                return CENTER_CROP;
        }
    }
}
