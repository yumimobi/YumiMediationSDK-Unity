package com.zplay.unity.adsyumi;

import android.app.Activity;
import android.util.Log;

import com.yumi.android.sdk.ads.publish.YumiSettings;

public class YumiUDebugCenter {
    private final String TAG = "zplayPluginActivity";
    /**
     * The {@code Activity} on which the debugcenter will display.
     */
    private Activity activity;

    public YumiUDebugCenter(Activity activity) {
        this.activity = activity;
    }

    public void presentDebugCenter(final String bannerPlacementId, final String interstitialPlacementId, final String rewardVideoPlacementId, final String channelId, final String versionId) {
        Log.d(TAG, "present debug center");
        presentDebugCenter(bannerPlacementId, interstitialPlacementId, rewardVideoPlacementId, "", channelId, versionId);
    }

    public void presentDebugCenter(final String bannerPlacementId, final String interstitialPlacementId, final String rewardVideoPlacementId, final String nativePlacementId, final String channelId, final String versionId) {
        Log.d(TAG, "present debug center");
        activity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                YumiSettings.startDebugging(YumiUDebugCenter.this.activity, bannerPlacementId, interstitialPlacementId, rewardVideoPlacementId, nativePlacementId, channelId, versionId);

            }
        });
    }
}
