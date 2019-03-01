package com.zplay.unity.adsyumi;

public interface YumiUNativeAdListener {

    void onLayerPrepared(int count);

    void onLayerFailed(String errorMsg);

    void onLayerClick();

}
