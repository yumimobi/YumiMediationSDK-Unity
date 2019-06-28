package com.zplay.unity.adsyumi;

public interface YumiUInterstitialListener {
    void onAdLoaded();
    void onAdFailedToLoad(String errorReason);
    void onAdOpening();
    void onAdStartPlaying();
    void onAdFailedToShow(String errorReason);
    void onAdClick();
    void onAdClosed();
}
