package com.zplay.unity.adsyumi;

public interface YumiUSplashListener {
        void onAdSuccessToShow();
        void onAdFailedToShow(String errorReason);
        void onAdClosed();
        void onAdClicked();
}
