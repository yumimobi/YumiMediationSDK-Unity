package com.zplay.unity.adsyumi;

import android.app.Activity;
import android.graphics.Color;
import android.text.TextUtils;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.ImageView;
import android.widget.TextView;

import com.yumi.android.sdk.ads.formats.YumiNativeAdOptions;
import com.yumi.android.sdk.ads.formats.YumiNativeAdView;
import com.yumi.android.sdk.ads.publish.NativeContent;
import com.yumi.android.sdk.ads.publish.YumiDebug;
import com.yumi.android.sdk.ads.publish.YumiNative;
import com.yumi.android.sdk.ads.publish.enumbean.LayerErrorCode;
import com.yumi.android.sdk.ads.publish.listener.IYumiNativeListener;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.UUID;

import static android.util.TypedValue.COMPLEX_UNIT_PX;
import static android.view.ViewGroup.LayoutParams.MATCH_PARENT;

public class YumiUNativeAd {
    private static final String TAG = "YumiUNativeAd";

    private static final int TEXT_SIZE_DELTA = 2;

    private YumiNative mNativeAd;
    private YumiUNativeAdListener mNativeAdListener;

    private Activity mUnityPlayerActivity;
    private Map<String, NativeContent> mNativeContents;
    private Map<String, View> mNativeViews = new HashMap<>(5);

    public YumiUNativeAd(Activity activity, YumiUNativeAdListener listener) {
        mUnityPlayerActivity = activity;
        mNativeAdListener = listener;
        mNativeContents = new HashMap<>();
    }

    public void create(final String slotId, final String channelId, final String versionId) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                YumiNativeAdOptions nativeAdOptions = new YumiNativeAdOptions.Builder()
                        .setIsDownloadImage(true)
                        .setAdChoicesPosition(YumiNativeAdOptions.POSITION_TOP_RIGHT)
                        .setAdAttributionPosition(YumiNativeAdOptions.POSITION_TOP_LEFT)
                        .setAdAttributionText("ad")
                        .setAdAttributionTextColor(Color.argb(255, 255, 255, 255))
                        .setAdAttributionBackgroundColor(Color.argb(90, 0, 0, 0))
                        .setAdAttributionTextSize(10)
                        .setHideAdAttribution(false).build();
                mNativeAd = new YumiNative(mUnityPlayerActivity, slotId, nativeAdOptions);
                mNativeAd.setNativeEventListener(new IYumiNativeListener() {
                    @Override
                    public void onLayerPrepared(List<NativeContent> list) {
                        Log.d(TAG, "onLayerPrepared: ");
                        if (list == null) {
                            return;
                        }
                        Log.d(TAG, "onLayerPrepared: " + list.size());

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
                    public void onLayerFailed(LayerErrorCode layerErrorCode) {
                        Log.d(TAG, "onLayerFailed: " + layerErrorCode);
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onLayerFailed(layerErrorCode.toString());
                        }
                    }

                    @Override
                    public void onLayerClick() {
                        Log.d(TAG, "onLayerClick: ");
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onLayerClick();
                        }
                    }
                });
                mNativeAd.setChannelID(channelId);
                mNativeAd.setVersionName(versionId);
            }
        });
    }

    public void loadAd(final int count) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.d(TAG, "loadAd: ");
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
                Log.d(TAG, "fillViews: ");

                if (mNativeContents == null || mNativeContents.isEmpty()) {
                    Log.d(TAG, "cannot found native ad data");
                    return;
                }

                NativeContent nativeContent = mNativeContents.get(uniqueId);
                if (nativeContent == null) {
                    Log.d(TAG, "cannot fillViews without content.");
                    return;
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
                titleView.setTextSize(COMPLEX_UNIT_PX, titleHeight - TEXT_SIZE_DELTA);
                titleView.setEllipsize(TextUtils.TruncateAt.END);
                titleView.setIncludeFontPadding(false);
                if (!TextUtils.isEmpty(nativeContent.getTitle())) {
                    titleView.setText(nativeContent.getTitle());
                }
                adView.addView(titleView, titleLayout);

                ImageView iconView = new ImageView(adView.getContext());
                LayoutParams iconLayout = new LayoutParams(iconWidth, iconHeight);
                iconLayout.leftMargin = iconX - containerX;
                iconLayout.topMargin = iconY - containerY;
                iconView.setScaleType(ImageView.ScaleType.CENTER_CROP);
                if (nativeContent.getIcon() != null) {
                    iconView.setImageDrawable(nativeContent.getIcon().getDrawable());
                } else {
                    Log.d(TAG, "icon image view is null");
                }
                adView.addView(iconView, iconLayout);

                final ImageView imgView = new ImageView(adView.getContext());
                LayoutParams imgLayout = new LayoutParams(imgWidth, imgHeight);
                imgLayout.leftMargin = imgX - containerX;
                imgLayout.topMargin = imgY - containerY;
                imgView.setScaleType(ImageView.ScaleType.CENTER_CROP);
                if (nativeContent.getCoverImage() != null) {
                    imgView.setImageDrawable(nativeContent.getCoverImage().getDrawable());
                } else {
                    Log.d(TAG, "cover image view is null");
                }
                adView.addView(imgView, imgLayout);

                TextView actionView = new Button(adView.getContext());
                LayoutParams actionLayout = new LayoutParams(actionWidth, actionHeight);
                actionLayout.leftMargin = actionX - containerX;
                actionLayout.topMargin = actionY - containerY;
                actionView.setPadding(0, 0, 0, 0);
                actionView.setTextSize(14);
                actionView.setIncludeFontPadding(false);
                actionView.setBackgroundColor(0xff33ff33);
                if (nativeContent.getCallToAction() == null) {
                    actionView.setVisibility(View.GONE);
                } else {
                    actionView.setText(nativeContent.getCallToAction());
                }
                adView.addView(actionView, actionLayout);

                TextView descView = new TextView(adView.getContext());
                LayoutParams descLayout = new LayoutParams(descWidth, descHeight);
                descLayout.leftMargin = descX - containerX;
                descLayout.topMargin = descY - containerY;
                descView.setTextSize(COMPLEX_UNIT_PX, descHeight - TEXT_SIZE_DELTA);
                descView.setEllipsize(TextUtils.TruncateAt.END);
                descView.setIncludeFontPadding(false);
                if (!TextUtils.isEmpty(nativeContent.getDesc())) {
                    descView.setText(nativeContent.getDesc());
                }
                adView.addView(descView, descLayout);

                adView.setTitleView(titleView);
                adView.setIconView(iconView);
                adView.setCoverImageView(imgView);
                adView.setCallToActionView(actionView);
                adView.setDescView(descView);

                adView.setNativeAd(nativeContent);
                adPlaceHolder.addView(adView, adViewLayout);
                mUnityPlayerActivity.addContentView(adPlaceHolder, adPlaceHolderLayout);

                adPlaceHolder.setVisibility(View.GONE);
                Log.d(TAG, "fills: " + uniqueId);
                mNativeViews.put(uniqueId, adPlaceHolder);
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

    public void showView(final String uniqueId) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                View adView = mNativeViews.get(uniqueId);
                if (adView == null) {
                    Log.d(TAG, "showView: cannot found the target view");
                    return;
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
        return content != null && !content.isExpired();
    }

    public void removeView(final String uniqueId) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                hideView(uniqueId);
                mNativeContents.remove(uniqueId);
                mNativeViews.remove(uniqueId);
            }
        });

    }

    public void destroy() {
        if (mNativeContents != null) {
            mNativeContents.clear();
        }

        if (mNativeViews != null) {
            mNativeViews.clear();
        }

        if (mNativeAd != null) {
            mNativeAd.onDestroy();
        }
        mUnityPlayerActivity = null;
        mNativeAdListener = null;
    }
}
