package com.zplay.unity.adsyumi;

import com.yumi.android.sdk.ads.publish.YumiInterstitial;
import com.yumi.android.sdk.ads.publish.enumbean.LayerErrorCode;
import com.yumi.android.sdk.ads.publish.listener.IYumiInterstititalListener;
import android.app.Activity;
import android.util.Log;
import com.yumi.android.sdk.ads.publish.enumbean.LayerErrorCode;

public class YumiUInterstitial {
    private final String TAG = "zplayPluginActivity";
    /**
     * The {@link YumiInterstitial}.
     */
    private YumiInterstitial interstitial;

    /**
     * The {@code Activity} on which the interstitial will display.
     */
    private Activity activity;

    /**
     * A listener implemented in Unity via {@code AndroidJavaProxy} to receive ad events.
     */
    private YumiUInterstitialListener adListener;
    /**
     * Whether or not the {@link YumiInterstitial} is ready to be shown.
     */
    private boolean isReady;

    public YumiUInterstitial(Activity activity, YumiUInterstitialListener listener) {
        this.activity = activity;
        this.adListener = listener;
        this.isReady = false;
    }
    public void create(final String placementId, final String channelId,final String versionId){
        Log.d(TAG, "create interstitial");
        if (null != interstitial) {
            interstitial.onDestory();
            interstitial = null;
        }
        this.isReady = false;
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                interstitial = new YumiInterstitial(activity, placementId, true);
                interstitial.setChannelID(channelId);
                interstitial.setVersionName(versionId);
                interstitial.setInterstitialEventListener(new IYumiInterstititalListener() {
                    @Override
                    public void onInterstitialPrepared() {
                        Log.d(TAG, "interstitial has loaded");
                        isReady = true;
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdLoaded();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onInterstitialPreparedFailed(LayerErrorCode layerErrorCode) {
                        Log.d(TAG, "on interstitial load failed " + layerErrorCode);
                        isReady = false;
                        if (adListener != null) {
                            final String errmsg = layerErrorCode.getMsg();
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdFailedToLoad(errmsg);
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onInterstitialExposure() {

                    }

                    @Override
                    public void onInterstitialClicked() {
                        Log.d(TAG, "on interstitial clicked ");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdClick();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onInterstitialClosed() {
                        Log.d(TAG, "on interstitial closed ");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdClosed();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onInterstitialExposureFailed() {

                    }
                });

                // load ad
                interstitial.requestYumiInterstitial();
            }
        });
    }

    public void showInterstitial(){
        Log.d(TAG, "show Interstitial");
        if (interstitial != null){
            if (isReady){
                isReady = false;
            }
            interstitial.showInterstitial(false);
        }
    }

    public  void  destroyInterstitial(){
        Log.d(TAG, "destroy Interstitial ");
        if (interstitial != null){
            interstitial.onDestory();
        }
    }
    /**
     * Returns {@code True} if the interstitial has loaded.
     */
    public boolean isReady(){
        return  isReady;
    }

}
