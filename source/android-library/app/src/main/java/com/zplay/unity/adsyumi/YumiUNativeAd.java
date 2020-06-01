package com.zplay.unity.adsyumi;

import android.app.Activity;
import android.text.TextUtils;
import android.util.Log;
import android.view.Gravity;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.ImageView;
import android.widget.TextView;

import com.yumi.android.sdk.ads.formats.YumiNativeAdOptions;
import com.yumi.android.sdk.ads.formats.YumiNativeAdView;
import com.yumi.android.sdk.ads.publish.AdError;
import com.yumi.android.sdk.ads.publish.NativeContent;
import com.yumi.android.sdk.ads.publish.YumiNative;
import com.yumi.android.sdk.ads.publish.enumbean.ExpressAdSize;
import com.yumi.android.sdk.ads.publish.listener.IYumiNativeListener;
import com.yumi.android.sdk.ads.utils.device.WindowSizeUtils;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import static android.view.ViewGroup.LayoutParams.MATCH_PARENT;
import static android.view.ViewGroup.LayoutParams.WRAP_CONTENT;

public class YumiUNativeAd {
    private static final String TAG = "YumiUNativeAd";

    private static final String FAKE_URL = "http://www.facebook.com";

    private static final String FACEBOOK_NAME = "Facebook";

    private YumiNative mNativeAd;
    private YumiUNativeAdListener mNativeAdListener;

    private Activity mUnityPlayerActivity;
    private Map<String, NativeContent> mNativeContents;
    private Map<String, View> mNativeViews = new HashMap<>(5);
    //LayoutParams object that holds the current native ad, used when displaying native ads
    private Map<String, LayoutParams> mNativeLayoutParams = new HashMap<>(5);
    //Records whether the current native ad view is added to the phone Window interface
    private Map<String, Boolean> mNativeHasAddContentView = new HashMap<>(5);
    private NativeAdOptions mAdOptions;

    public YumiUNativeAd(Activity activity, YumiUNativeAdListener listener) {
        mUnityPlayerActivity = activity;
        mNativeAdListener = listener;
        mNativeContents = new HashMap<>();
    }

    public void create(final String slotId, final String channelId, final String versionId,
                       int adChosePosition,
                       int attriPosition, String attriText, int attriTextSize, String attriTextColor, String attriTextBackgroundColor,
                       int titleSize, String titleColor, String titleBackgroundColor,
                       int descSize, String descColor, String descBackgroundColor,
                       int ctaSize, String ctaColor, String ctaBackgroundColor,
                       int iconScaleType, int coverImageScaleType, final int expressAdViewWidth, final int expressAdViewHeight) {
        Log.d(TAG, "expressAdViewWidth: " + expressAdViewWidth + ",expressAdViewHeight: " + expressAdViewHeight);
        mAdOptions = new NativeAdOptions(
                adChosePosition,
                attriPosition, attriText, attriTextSize, attriTextColor, attriTextBackgroundColor,
                titleSize, titleColor, titleBackgroundColor,
                descSize, descColor, descBackgroundColor,
                ctaSize, ctaColor, ctaBackgroundColor,
                iconScaleType, coverImageScaleType);

        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                YumiNativeAdOptions nativeAdOptions = new YumiNativeAdOptions.Builder()
                        .setIsDownloadImage(true)
                        .setAdChoicesPosition(mAdOptions.getAdChosePosition())
                        .setAdAttributionPosition(mAdOptions.getAttriPosition())
                        .setAdAttributionText(mAdOptions.getAttriText())
                        .setAdAttributionTextColor(mAdOptions.getAttriTextColor())
                        .setAdAttributionBackgroundColor(mAdOptions.getAttriTextBackgroundColor())
                        .setAdAttributionTextSize(mAdOptions.getAttriTextSize())
                        .setHideAdAttribution(false)
                        .setExpressAdSize(new ExpressAdSize(WindowSizeUtils.px2dip(expressAdViewWidth), WindowSizeUtils.px2dip(expressAdViewHeight)))
                        .build();
                mNativeAd = new YumiNative(mUnityPlayerActivity, slotId, nativeAdOptions);
                mNativeAd.setNativeEventListener(new IYumiNativeListener() {
                    @Override
                    public void onLayerPrepared(List<NativeContent> list) {
                        if (list == null) {
                            return;
                        }

                        StringBuilder uniqueIds = new StringBuilder();
                        for (NativeContent content : list) {
                            final String uniqueId = UUID.randomUUID().toString();
                            uniqueIds.append(uniqueId).append(",");
                            mNativeContents.put(uniqueId, content);
                        }

                        if (uniqueIds.length() > 0) {
                            uniqueIds.deleteCharAt(uniqueIds.lastIndexOf(","));
                        }

                        if (mNativeAdListener != null) {
                            mNativeAdListener.onLayerPrepared(uniqueIds.toString());
                        }
                    }

                    @Override
                    public void onLayerFailed(AdError adError) {
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onLayerFailed(adError.toString());
                        }
                    }

                    @Override
                    public void onLayerClick() {
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onLayerClick();
                        }
                    }

                    @Override
                    public void onExpressAdRenderFail(NativeContent nativeContent, String errorMsg) {
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onExpressAdRenderFail(getUniqueId(nativeContent), errorMsg);
                        }
                    }

                    @Override
                    public void onExpressAdRenderSuccess(NativeContent nativeContent) {
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onExpressAdRenderSuccess(getUniqueId(nativeContent));
                        }
                    }

                    @Override
                    public void onExpressAdClosed(NativeContent nativeContent) {
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onExpressAdClickCloseButton(getUniqueId(nativeContent));
                        }
                    }
                });
                mNativeAd.setChannelID(channelId);
                mNativeAd.setVersionName(versionId);
            }
        });
    }

    private String getUniqueId(NativeContent nativeContent) {

        if (mNativeContents != null && mNativeContents.containsValue(nativeContent)) {
            for (String uniqueId : mNativeContents.keySet()) {
                if (mNativeContents.get(uniqueId).equals(nativeContent)) {
                    return uniqueId;
                }
            }
        }
        return null;
    }


    public void loadAd(final int count) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                mNativeAd.requestYumiNative(count);
            }
        });
    }

    public void fillViews(final String uniqueId,
                          final int containerX, final int containerY, final int containerWidth, final int containerHeight,
                          final int titleX, final int titleY, final int titleWidth, final int titleHeight,
                          final int iconX, final int iconY, final int iconWidth, final int iconHeight,
                          final int imgX, final int imgY, final int imgWidth, final int imgHeight,
                          final int actionX, final int actionY, final int actionWidth, final int actionHeight,
                          final int descX, final int descY, final int descWidth, final int descHeight) {

        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mNativeContents == null || mNativeContents.isEmpty()) {
                    Log.d(TAG, "cannot found native ad data");
                    return;
                }

                if (mAdOptions == null) {
                    Log.d(TAG, "cannot found style options.");
                    return;
                }

                NativeContent nativeContent = mNativeContents.get(uniqueId);
                if (nativeContent == null) {
                    Log.d(TAG, "cannot fillViews without content.");
                    return;
                }

                if (mNativeViews.containsKey(uniqueId)) {
                    if (mNativeViews.get(uniqueId) != null) {
                        Log.d(TAG, "cannot fillViews uniqueId views is presence uniqueId: " + uniqueId);
                        return;
                    }
                }

                if (nativeContent.isExpressAdView()) {
                    FrameLayout adPlaceHolder = new FrameLayout(mUnityPlayerActivity);
                    LayoutParams adPlaceHolderLayout = new LayoutParams(containerWidth, containerHeight);
                    adPlaceHolderLayout.leftMargin = containerX;
                    adPlaceHolderLayout.topMargin = containerY;

                    YumiNativeAdView adView = new YumiNativeAdView(mUnityPlayerActivity);
                    LayoutParams adViewLayout = new LayoutParams(MATCH_PARENT, MATCH_PARENT);

                    FrameLayout.LayoutParams videoViewLayout = new FrameLayout.LayoutParams(WRAP_CONTENT, WRAP_CONTENT);
                    videoViewLayout.gravity = Gravity.CENTER;

                    adView.addView(nativeContent.getExpressAdView(), videoViewLayout);

                    adView.setNativeAd(nativeContent);
                    adPlaceHolder.addView(adView, adViewLayout);
                    mUnityPlayerActivity.addContentView(adPlaceHolder, adPlaceHolderLayout);

                    adPlaceHolder.setVisibility(View.GONE);
                    mNativeViews.put(uniqueId, adPlaceHolder);

                } else {
                    if (TextUtils.equals(FACEBOOK_NAME, nativeContent.getProviderName())) {
                        try {
                            nativeContent.getIcon().setUrl(FAKE_URL);
                            nativeContent.getCoverImage().setUrl(FAKE_URL);
                        } catch (NullPointerException ignore) {
                        }
                    }

                    FrameLayout adPlaceHolder = new FrameLayout(mUnityPlayerActivity);
                    LayoutParams adPlaceHolderLayout = new LayoutParams(containerWidth, containerHeight);
                    adPlaceHolderLayout.leftMargin = containerX;
                    adPlaceHolderLayout.topMargin = containerY;

                    YumiNativeAdView adView = new YumiNativeAdView(mUnityPlayerActivity);
                    LayoutParams adViewLayout = new LayoutParams(MATCH_PARENT, MATCH_PARENT);

                    TextView titleView = new TextView(adView.getContext());
                    LayoutParams titleLayout = new LayoutParams(titleWidth, titleHeight);
                    titleLayout.leftMargin = titleX - containerX;
                    titleLayout.topMargin = titleY - containerY;
                    titleView.setGravity(Gravity.CENTER_VERTICAL);
                    titleView.setTextSize(mAdOptions.getTitleSize());
                    titleView.setTextColor(mAdOptions.getTitleColor());
                    titleView.setBackgroundColor(mAdOptions.getTitleBackgroundColor());
                    titleView.setEllipsize(TextUtils.TruncateAt.END);
                    titleView.setIncludeFontPadding(false);
                    if (!TextUtils.isEmpty(nativeContent.getTitle())) {
                        titleView.setText(nativeContent.getTitle());
                    }
                    adView.addView(titleView, titleLayout);
                    adView.setTitleView(titleView);

                    ImageView iconView = new ImageView(adView.getContext());
                    LayoutParams iconLayout = new LayoutParams(iconWidth, iconHeight);
                    iconLayout.leftMargin = iconX - containerX;
                    iconLayout.topMargin = iconY - containerY;
                    iconView.setScaleType(mAdOptions.getIconScaleType());
                    if (nativeContent.getIcon() != null) {
                        iconView.setImageDrawable(nativeContent.getIcon().getDrawable());
                    } else {
                        Log.d(TAG, "icon image view is null");
                    }
                    adView.addView(iconView, iconLayout);
                    adView.setIconView(iconView);

                    if (nativeContent.getHasVideoContent()) {
                        FrameLayout videoContainer = new FrameLayout(mUnityPlayerActivity);
                        LayoutParams videoLayout = new LayoutParams(imgWidth, imgHeight);
                        videoLayout.leftMargin = imgX - containerX;
                        videoLayout.topMargin = imgY - containerY;

                        FrameLayout videoView = new FrameLayout(mUnityPlayerActivity);
                        LayoutParams videoViewLayout = new LayoutParams(WRAP_CONTENT, WRAP_CONTENT);
                        videoViewLayout.gravity = Gravity.CENTER;
                        videoContainer.addView(videoView, videoViewLayout);

                        adView.addView(videoContainer, videoLayout);

                        adView.setMediaLayout(videoView);
                    } else {
                        final ImageView imgView = new ImageView(adView.getContext());
                        LayoutParams imgLayout = new LayoutParams(imgWidth, imgHeight);
                        imgLayout.leftMargin = imgX - containerX;
                        imgLayout.topMargin = imgY - containerY;
                        imgView.setScaleType(mAdOptions.getCoverImageScaleType());
                        if (nativeContent.getCoverImage() != null) {
                            imgView.setImageDrawable(nativeContent.getCoverImage().getDrawable());
                        } else {
                            Log.d(TAG, "cover image view is null");
                        }
                        adView.addView(imgView, imgLayout);
                        adView.setCoverImageView(imgView);
                    }

                    TextView actionView = new TextView(adView.getContext());
                    LayoutParams actionLayout = new LayoutParams(actionWidth, actionHeight);
                    actionLayout.leftMargin = actionX - containerX;
                    actionLayout.topMargin = actionY - containerY;
                    actionView.setGravity(Gravity.CENTER);
                    actionView.setTextSize(mAdOptions.getCtaSize());
                    actionView.setTextColor(mAdOptions.getCtaColor());
                    actionView.setEllipsize(TextUtils.TruncateAt.END);
                    actionView.setBackgroundColor(mAdOptions.getCtaBackgroundColor());
                    actionView.setIncludeFontPadding(false);
                    if (TextUtils.isEmpty(nativeContent.getCallToAction())) {
                        actionView.setVisibility(View.GONE);
                    } else {
                        actionView.setText(nativeContent.getCallToAction());
                    }
                    adView.addView(actionView, actionLayout);
                    adView.setCallToActionView(actionView);

                    TextView descView = new TextView(adView.getContext());
                    LayoutParams descLayout = new LayoutParams(descWidth, descHeight);
                    descLayout.leftMargin = descX - containerX;
                    descLayout.topMargin = descY - containerY;
                    descView.setTextSize(mAdOptions.getDescSize());
                    descView.setTextColor(mAdOptions.getDescColor());
                    descView.setBackgroundColor(mAdOptions.getDescBackgroundColor());
                    descView.setEllipsize(TextUtils.TruncateAt.END);
                    descView.setIncludeFontPadding(false);
                    if (!TextUtils.isEmpty(nativeContent.getDesc())) {
                        descView.setText(nativeContent.getDesc());
                    }
                    adView.addView(descView, descLayout);
                    adView.setDescView(descView);

                    adView.setNativeAd(nativeContent);
                    adPlaceHolder.addView(adView, adViewLayout);

                    adPlaceHolder.setVisibility(View.GONE);

                    mNativeHasAddContentView.put(uniqueId, false);
                    mNativeViews.put(uniqueId, adPlaceHolder);
                    mNativeLayoutParams.put(uniqueId, adPlaceHolderLayout);
                }
            }
        });

    }

    public String getTitle(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).getTitle();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public String getDescription(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).getDesc();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public String getIconURL(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).getIcon().getUrl();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public String getCoverImageURL(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).getCoverImage().getUrl();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public String getCallToAction(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).getCallToAction();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public String getPrice(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).getPrice();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public String getStarRating(String uniqueId) {
        try {
            return "" + mNativeContents.get(uniqueId).getStarRating();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public String getOther(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).getOther();
        } catch (NullPointerException ignore) {
            return "";
        }
    }

    public boolean isExpressAdView(String uniqueId) {
        try {
            return mNativeContents.get(uniqueId).isExpressAdView();
        } catch (NullPointerException ignore) {
            return false;
        }
    }

    public void showView(final String uniqueId) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                View adView = mNativeViews.get(uniqueId);
                LayoutParams adViewLayoutParams = mNativeLayoutParams.get(uniqueId);
                boolean nativeHasaddContentView = mNativeHasAddContentView.get(uniqueId);
                if (adView == null || adViewLayoutParams == null) {
                    Log.d(TAG, "showView: cannot found the target view");
                    return;
                }

                if (!nativeHasaddContentView) {
                    mNativeHasAddContentView.put(uniqueId, true);
                    mUnityPlayerActivity.addContentView(adView, adViewLayoutParams);
                }
                adView.setVisibility(View.VISIBLE);
            }
        });
    }

    public void hideView(final String uniqueId) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                View adView = mNativeViews.get(uniqueId);
                if (adView == null) {
                    Log.d(TAG, "hideView: cannot found the target view");
                    return;
                }
                adView.setVisibility(View.GONE);
            }
        });
    }

    public boolean isAdInvalidated(final String uniqueId) {
        NativeContent content = mNativeContents.get(uniqueId);
        return content == null || content.isExpired();
    }

    public void removeView(final String uniqueId) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {

                View adView = mNativeViews.get(uniqueId);
                if (adView != null) {
                    Log.d(TAG, "removeView: uniqueId ：" + uniqueId);
                    adView.setVisibility(View.GONE);
                    if (adView.getParent() instanceof ViewGroup) {
                        ((ViewGroup) adView.getParent()).removeView(adView);
                    }
                }

                nativeContentDestroy(uniqueId);
                mNativeContents.remove(uniqueId);
                mNativeViews.remove(uniqueId);
                mNativeLayoutParams.remove(uniqueId);
            }
        });
    }

    public void destroy() {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (mNativeContents != null) {
                    for (String uniqueId : mNativeContents.keySet()) {
                        nativeContentDestroy(uniqueId);
                    }
                    mNativeContents.clear();
                }

                if (mNativeViews != null) {
                    for (Map.Entry<String, View> entry : mNativeViews.entrySet()) {
                        hideView(entry.getKey());
                    }
                    Log.d(TAG, "destroy: NativeViews clear ");
                    mNativeViews.clear();
                    mNativeLayoutParams.clear();
                }

                if (mNativeAd != null) {
                    mNativeAd.onDestroy();
                }
                mUnityPlayerActivity = null;
                mNativeAdListener = null;
            }
        });
    }

    private void nativeContentDestroy(String uniqueId) {
        if (mNativeContents != null) {
            NativeContent nativeContent = mNativeContents.get(uniqueId);
            if (nativeContent != null) {
                nativeContent.destroy();
            }
        }
    }
}
