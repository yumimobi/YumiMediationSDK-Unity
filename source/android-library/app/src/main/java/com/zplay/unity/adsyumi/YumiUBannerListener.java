package com.zplay.unity.adsyumi;

public interface YumiUBannerListener {
    void onAdLoaded();
    void onAdFailedToLoad(String errorReason);
    void onAdClick();
}
