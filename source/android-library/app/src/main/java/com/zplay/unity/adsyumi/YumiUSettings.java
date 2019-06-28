package com.zplay.unity.adsyumi;

import android.util.Log;

import com.yumi.android.sdk.ads.publish.YumiSettings;
import com.yumi.android.sdk.ads.publish.enumbean.YumiGDPRStatus;

public class YumiUSettings {
    private final String TAG = "zplayPluginActivity";
    private int PERSONALIZED = 0;
    private int NONPERSONALIZED  = 1;

    public YumiUSettings(){
    }

    public void setGDPRConsent(int status){
        Log.d(TAG, "setGDPRConsent status :" + status);
        if(status == PERSONALIZED){
            YumiSettings.setGDPRConsent(YumiGDPRStatus.PERSONALIZED);
        }else if(status == NONPERSONALIZED){
            YumiSettings.setGDPRConsent(YumiGDPRStatus.NON_PERSONALIZED);
        }else{
            YumiSettings.setGDPRConsent(YumiGDPRStatus.UNKNOWN);
        }
    }

}
