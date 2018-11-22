package com.zplay.unity.adsyumi;
import android.app.Activity;
import android.util.Log;

import com.yumi.android.sdk.ads.publish.YumiInterstitial;
import com.yumi.android.sdk.ads.publish.YumiMedia;
import com.yumi.android.sdk.ads.publish.enumbean.LayerErrorCode;
import com.yumi.android.sdk.ads.publish.enumbean.MediaStatus;
import com.yumi.android.sdk.ads.publish.listener.IYumiInterstititalListener;
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
                    public void onMediaExposure() {
                        Log.d(TAG, "reward video has loaded");
                        if (adListener != null) {
                            new Thread(new Runnable() {
                                @Override
                                public void run() {
                                    if (adListener != null) {
                                        adListener.onAdOpening();
                                    }
                                    if (adListener != null){
                                        adListener.onAdStartPlaying();
                                    }
                                }
                            }).start();
                        }
                    }

                    @Override
                    public void onMediaClicked() {

                    }

                    @Override
                    public void onMediaClosed() {
                        Log.d(TAG, "reward video has been closed ");
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
                    public void onMediaIncentived() {
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
        if (rewardVideo != null){
            Log.d(TAG, "play RewardVideo ");
            rewardVideo.showMedia();
        }
    }
    public  void  destroyRewardVideo(){
        Log.d(TAG, "destroy RewardVideo ");
        if (rewardVideo != null){
            rewardVideo.onDestory();
        }
    }
}
