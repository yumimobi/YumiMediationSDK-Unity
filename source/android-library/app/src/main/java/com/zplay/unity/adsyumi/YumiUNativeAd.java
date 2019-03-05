package com.zplay.unity.adsyumi;

import android.app.Activity;
import android.graphics.Color;
import android.util.Log;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
import android.widget.ImageView;
import android.widget.Toast;

import com.yumi.android.sdk.ads.formats.YumiNativeAdOptions;
import com.yumi.android.sdk.ads.formats.YumiNativeAdView;
import com.yumi.android.sdk.ads.publish.NativeContent;
import com.yumi.android.sdk.ads.publish.YumiDebug;
import com.yumi.android.sdk.ads.publish.YumiNative;
import com.yumi.android.sdk.ads.publish.enumbean.LayerErrorCode;
import com.yumi.android.sdk.ads.publish.listener.IYumiNativeListener;

import java.util.List;

import static android.view.ViewGroup.LayoutParams.MATCH_PARENT;

public class YumiUNativeAd {
    private static final String TAG = "YumiUNativeAd";

    private YumiNative mNativeAd;
    private YumiUNativeAdListener mNativeAdListener;

    private Activity mUnityPlayerActivity;
    private List<NativeContent> mNativeContents;

    public YumiUNativeAd(Activity activity, YumiUNativeAdListener listener) {
        mUnityPlayerActivity = activity;
        mNativeAdListener = listener;
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
                        mNativeContents = list;
                        if (mNativeAdListener != null && list != null) {
                            mNativeAdListener.onLayerPrepared(list.size());
                        }
                    }

                    @Override
                    public void onLayerFailed(LayerErrorCode layerErrorCode) {
                        if (mNativeAdListener != null) {
                            mNativeAdListener.onLayerFailed(layerErrorCode.toString());
                        }
                    }

                    @Override
                    public void onLayerClick() {
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
                mNativeAd.requestYumiNative(count);
            }
        });
    }


    public void fillViews(final int index,
                          final int containerX, final int containerY, final int containerWidth, final int containerHeight,
                          final int iconX, final int iconY, final int iconWidth, final int iconHeight,
                          final int imgX, final int imgY, final int imgWidth, final int imgHeight,
                          final int actionX, final int actionY, final int actionWidth, final int actionHeight) {
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                FrameLayout adPlaceHolder = new FrameLayout(mUnityPlayerActivity);
                LayoutParams adPlaceHolderLayout = new LayoutParams(containerWidth, containerHeight);
                adPlaceHolderLayout.leftMargin = containerX;
                adPlaceHolderLayout.topMargin = containerY;

                YumiNativeAdView adView = new YumiNativeAdView(mUnityPlayerActivity);
                LayoutParams containerLayout = new LayoutParams(MATCH_PARENT, MATCH_PARENT);

                ImageView iconView = new ImageView(adView.getContext());
                LayoutParams iconLayout = new LayoutParams(iconWidth, iconHeight);
                iconLayout.leftMargin = iconX - containerX;
                iconLayout.topMargin = iconY - containerY;
                adView.addView(iconView, iconLayout);

                final ImageView imgView = new ImageView(adView.getContext());
                imgView.setClickable(true);
                LayoutParams imgLayout = new LayoutParams(imgWidth, imgHeight);
                imgLayout.leftMargin = imgX - containerX;
                imgLayout.topMargin = imgY - containerY;
                imgView.setScaleType(ImageView.ScaleType.CENTER_CROP);
                adView.addView(imgView, imgLayout);

                Button actionView = new Button(adView.getContext());
                LayoutParams actionLayout = new LayoutParams(actionWidth, actionHeight);
                actionLayout.leftMargin = actionX - containerX;
                actionLayout.topMargin = actionY - containerY;
                adView.addView(actionView, actionLayout);

                adView.setIconView(iconView);
                adView.setImageView(imgView);
                adView.setCallToActionView(actionView);

                adPlaceHolder.addView(adView, containerLayout);


                if (mNativeContents == null || mNativeContents.isEmpty()) {
                    Log.d(TAG, "cannot found native ad data");
                    return;
                }
                if (mNativeContents.size() < index - 1) {
                    Log.d(TAG, "require index is out of native ad array.");
                    return;
                }

                NativeContent nativeContent = mNativeContents.get(index);
                if (nativeContent.getIcon() != null) {
                    ((ImageView) adView.getIconView()).setImageDrawable(nativeContent.getIcon().getDrawable());
                }

                if (nativeContent.getImage() != null) {
                    ((ImageView) adView.getImageView()).setImageDrawable(nativeContent.getImage().getDrawable());
                }

                if (nativeContent.getCallToAction() == null) {
                    adView.getCallToActionView().setVisibility(View.INVISIBLE);
                } else {
                    ((Button) adView.getCallToActionView()).setText(nativeContent.getCallToAction());
                }

                adView.setNativeAd(nativeContent);
                mUnityPlayerActivity.addContentView(adPlaceHolder, adPlaceHolderLayout);
            }
        });
    }

    public void destroy() {
        if (mNativeContents != null) {
            mNativeContents.clear();
            mNativeContents = null;
        }

        if (mNativeAd != null) {
            mNativeAd.onDestroy();
        }

        mUnityPlayerActivity = null;
        mNativeAdListener = null;
    }
}
