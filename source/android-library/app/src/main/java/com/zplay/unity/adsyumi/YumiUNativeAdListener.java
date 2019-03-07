package com.zplay.unity.adsyumi;

public interface YumiUNativeAdListener {

    void onLayerPrepared(String uniqueIds);

    void onLayerFailed(String errorMsg);

    void onLayerClick();

}
