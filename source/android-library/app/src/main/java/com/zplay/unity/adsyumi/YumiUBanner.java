package com.zplay.unity.adsyumi;

import com.yumi.android.sdk.ads.publish.YumiBanner;
import com.yumi.android.sdk.ads.publish.listener.IYumiBannerListener;
import android.app.Activity;
import android.widget.FrameLayout;
import android.graphics.Point;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Gravity;
import android.content.Context;
import android.view.ViewGroup.LayoutParams;
import com.yumi.android.sdk.ads.publish.enumbean.LayerErrorCode;
import com.yumi.android.sdk.ads.publish.enumbean.AdSize;

public class YumiUBanner {

    private final String TAG = "zplayPluginActivity";
    private  FrameLayout zplay_view;
    private YumiBanner bannerAdView;
    private IYumiBannerListener bannerListener;

    /**
     * The {@code Activity} that the banner will be displayed in.
     */
    private Activity mUnityPlayerActivity;
    /**
     * A listener implemented in Unity via {@code AndroidJavaProxy} to receive ad events.
     */
    private YumiUBannerListener mUnityListener;

    public YumiUBanner(Activity activity, YumiUBannerListener listener) {
        this.mUnityPlayerActivity = activity;
        this.mUnityListener = listener;
    }

    /**
     * Creates an view  to hold banner ads.
     */
    public void create(final String placementId, final String channelId,final String versionId) {
        Log.d(TAG, "create banner");
        mUnityPlayerActivity.runOnUiThread(new Runnable() {

            @Override
            public void run() {
                bannerListener = new IYumiBannerListener() {

                    @Override
                    public void onBannerPreparedFailed(LayerErrorCode errorCode) {
                        Log.d(TAG, "on banner prepared failed " + errorCode);
                        final String errmsg = errorCode.getMsg();
                        if (mUnityListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (mUnityListener != null) {

                                        mUnityListener.onAdFailedToLoad(errmsg);
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onBannerPrepared() {
                        Log.d(TAG, "on banner prepared");
                        if (mUnityListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (mUnityListener != null) {

                                        mUnityListener.onAdLoaded();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onBannerExposure() {
                        Log.d(TAG, "on banner exposure");


                    }

                    @Override
                    public void onBannerClosed() {
                        Log.d(TAG, "on banner close ");

                    }

                    @Override
                    public void onBannerClicked() {
                        Log.d(TAG, "on banner clicked ");
                        if (mUnityListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (mUnityListener != null) {

                                        mUnityListener.onAdClick();
                                    }
                                }
                            }).start();
                        }
                    }
                };
                bannerAdView = new YumiBanner((Activity) mUnityPlayerActivity, placementId, true);

                // setChannelID . (Recommend)
                bannerAdView.setChannelID(channelId);
                // setVersionName . (Recommend)
                bannerAdView.setVersionName(versionId);
                // setBannerEventListener. (Recommend)
                bannerAdView.setBannerEventListener(bannerListener);

            }
        });
    }

    private void setBannerContentView(boolean isMatchWindowWidth){
        if (null != zplay_view) {
            zplay_view.removeAllViews();
        } else {
            zplay_view = new FrameLayout(mUnityPlayerActivity);
            zplay_view.setTag(1);
        }
        FrameLayout.LayoutParams params;
        int width = dip2px(mUnityPlayerActivity, 320);
        int height = dip2px(mUnityPlayerActivity, 50);
        Log.d(TAG, "run addbannerad");
        if (isTablet((Activity) mUnityPlayerActivity)) {
            // 锟斤拷ipad
            width = dip2px(mUnityPlayerActivity, 728);
            height = dip2px(mUnityPlayerActivity, 90);

        }
        if (isPortrait(mUnityPlayerActivity) && isMatchWindowWidth) {
            params = new FrameLayout.LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT);
        } else {
            params = new FrameLayout.LayoutParams(width, height);
        }
        params.gravity = Gravity.CENTER | Gravity.BOTTOM;

        if (((int) zplay_view.getTag()) == 1) {
            Log.d(TAG, "addContentView 1");
            ((Activity) mUnityPlayerActivity).getWindow().addContentView(zplay_view, params);
            zplay_view.setTag(2);
        } else {
            Log.d(TAG, "addContentView 2");
        }
    }

    public void requestAd(final boolean isSmart){
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.d(TAG, "Calling requestAd() on Android isSmart is " + isSmart);
                if (bannerAdView != null){
                    if (zplay_view == null){
                        setBannerContentView(isSmart);
                        // setBannerContainer
                        bannerAdView.setBannerContainer(zplay_view, AdSize.BANNER_SIZE_AUTO, isSmart);
                    }
                   
                    bannerAdView.requestYumiBanner();
                }
            }
        });
    }
    public  void showBannerView(){
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if(bannerAdView != null){

                    bannerAdView.resumeBanner();
                }
            }
        });
    }
    public  void  hideBanner(){
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (bannerAdView != null){
                    bannerAdView.dismissBanner();
                }
            }
        });

    }
    public void destroyBanner(){
        mUnityPlayerActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (bannerAdView != null){
                    bannerAdView.onDestroy();
                    bannerAdView = null;
                }
            }
        });
    }

    private final  boolean isApproximateTablet(Context context) {
        DisplayMetrics displayMetrics = context.getResources().getDisplayMetrics();
        int width = displayMetrics.widthPixels;
        int height = displayMetrics.heightPixels;
        float density = displayMetrics.density;
        double inch = Math.sqrt(Math.pow(width, 2) + Math.pow(height, 2)) / (160 * density);
        if (inch >= 8.0d) {
            return true;
        }
        return false;
    }

    private  int dip2px(Context context, float dipValue) {
        final float scale = context.getResources().getDisplayMetrics().density;
        return (int) (dipValue * scale + 0.5f);
    }

    private int px2dip(Context context, float pxValue) {
        final float scale = context.getResources().getDisplayMetrics().density;
        return (int) (pxValue / scale + 0.5f);
    }

    private  boolean isPortrait(Context context) {
        try {
            DisplayMetrics dm = context.getResources().getDisplayMetrics();
            if (dm.widthPixels <= dm.heightPixels) {
                return true;
            }
        } catch (Exception e) {
        }
        return false;
    }
    private boolean isTablet(Activity activity) {
        if (android.os.Build.VERSION.SDK_INT >= 17) {
            Point point = new Point();
            activity.getWindowManager().getDefaultDisplay().getRealSize(point);
            float density = activity.getResources().getDisplayMetrics().density;
            double inch = Math.sqrt(Math.pow(point.x, 2) + Math.pow(point.y, 2)) / (160 * density);
            if (inch >= 8.0d) {
                return true;
            }
            return false;
        }
        return isApproximateTablet(activity.getApplicationContext());
    }
}
