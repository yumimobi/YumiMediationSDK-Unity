package com.zplay.unity.adsyumi;

public interface YumiURewardVideoListener {
    void onAdLoaded();

    void onAdFailedToLoad(String errorReason);

    void onAdOpening();

    void onAdStartPlaying();

    void onAdRewarded();

    void onRewardVideoAdClosed(boolean isRewarded);

    void onAdFailedToShow(String errorReason);

    void onAdClick();

}
