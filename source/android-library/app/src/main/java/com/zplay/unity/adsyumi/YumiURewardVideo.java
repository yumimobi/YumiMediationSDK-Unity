package com.zplay.unity.adsyumi;
import android.app.Activity;
import android.util.Log;

import com.yumi.android.sdk.ads.publish.AdError;
import com.yumi.android.sdk.ads.publish.YumiMedia;
import com.yumi.android.sdk.ads.publish.listener.IYumiMediaListener;

public class YumiURewardVideo {
    private final String TAG = "zplayPluginActivity";
    /**
     * The {@link YumiMedia}.
     */
    private YumiMedia rewardVideo;

    /**
     * The {@code Activity} on which the interstitial will display.
     */
    private Activity activity;

    /**
     * A listener implemented in Unity via {@code AndroidJavaProxy} to receive ad events.
     */
    private YumiURewardVideoListener adListener;

    public YumiURewardVideo(Activity activity, YumiURewardVideoListener listener) {
        this.activity = activity;
        this.adListener = listener;
    }

    public void  create(){
        // android  not need this function ,create reward video ad at requestRewardVideoAd
    }

    public void requestRewardVideoAd(final String placementId, final String channelId,final String versionId) {
        Log.d(TAG, "create rewardVideo and request ad");
        if (null != rewardVideo) {
            rewardVideo.onDestory();
            rewardVideo = null;
        }
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                rewardVideo = new  YumiMedia(activity,placementId);
                rewardVideo.setChannelID(channelId);
                rewardVideo.setVersionName(versionId);
                rewardVideo.setMediaEventListner(new IYumiMediaListener() {
                    @Override
                    public void onMediaPrepared() {
                        Log.d(TAG, "reward video has prepared");
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
                    public void onMediaPreparedFailed(AdError adError) {
                        Log.d(TAG, "reward video has prepared failed");
                        if (adListener != null) {
                            final String errmsg = adError.getMsg();
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
                    public void onMediaExposure() {
                        Log.d(TAG, "reward video has exposure");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdOpening();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onMediaExposureFailed(AdError adError) {
                        Log.d(TAG, "reward video exposure failed");
                        if (adListener != null) {
                            final String errmsg = adError.getMsg();
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdFailedToShow(errmsg);
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onMediaClicked() {
                        Log.d(TAG, "reward video clicked");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdClicked();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onMediaClosed(final boolean isRewarded) {
                        Log.d(TAG, "reward video has been closed");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdClosed(isRewarded);
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onMediaRewarded() {
                        Log.d(TAG, "reward video has been rewarded");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdRewarded();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onMediaStartPlaying() {
                        Log.d(TAG, "reward video start playing");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null){
                                        adListener.onAdStartPlaying();
                                    }
                                }
                            }).start();
                        }
                    }

                });

                rewardVideo.requestYumiMedia();
            }
        });
    }
    public boolean isReady(){
        if (rewardVideo != null)
            return rewardVideo.isMediaPrepared();
        return false;
    }
    public  void playRewardVideo(){
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (rewardVideo != null){
                    Log.d(TAG, "play RewardVideo ");
                    rewardVideo.showMedia();
                }
            }
        });

    }
    public  void  destroyRewardVideo(){
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.d(TAG, "destroy RewardVideo ");
                if (rewardVideo != null){
                    rewardVideo.onDestory();
                }
            }
        });

    }
}
