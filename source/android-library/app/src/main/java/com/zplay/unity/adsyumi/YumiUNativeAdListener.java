package com.zplay.unity.adsyumi;

public interface YumiUNativeAdListener {

    void onLayerPrepared(String uniqueIds);

    void onLayerFailed(String errorMsg);

    void onLayerClick();

    void onExpressAdRenderSuccess(String uniqueIds);

    void onExpressAdRenderFail(String uniqueId,String errorMsg);

    void onExpressAdClickCloseButton(String uniqueIds);
}
