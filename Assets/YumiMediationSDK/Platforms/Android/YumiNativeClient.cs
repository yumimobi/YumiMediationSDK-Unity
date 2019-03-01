#if UNITY_ANDROID
using System;
using UnityEngine;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;

namespace YumiMediationSDK.Android
{
    public class YumiNativeClient : AndroidJavaProxy, IYumiNativeClient
    {
        private AndroidJavaObject nativeAd;
        private GameObject currentGameObject;

        public YumiNativeClient() : base(YumiUtils.UnityNativeAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(YumiUtils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            this.nativeAd = new AndroidJavaObject(
                YumiUtils.NativeAdClassName, activity, this);
        }

        public event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        public event EventHandler<EventArgs> OnAdClick;

        public void CreateNativeAd(string placementId, string channelId, string versionId)
        {
            nativeAd.Call("create", placementId, channelId, versionId);
        }

        public void DestroyNativeAd()
        {
            nativeAd.Call("destroy");
        }

        public void LoadAd(int adCount)
        {
            nativeAd.Call("loadAd", adCount);
        }

        public void RegisterGameObjectsForInteraction(GameObject gameObject, RectTransform adViewRectTransform, RectTransform mediaViewRectTransform, RectTransform iconViewRectTransform, RectTransform ctaViewRectTransform)
        {
            this.currentGameObject = gameObject;
            Camera camera = Camera.main;

            Rect adViewRect = getGameObjectRect(adViewRectTransform, camera);
            Rect mediaViewRect = getGameObjectRect(mediaViewRectTransform, camera);
            Rect iconViewRect = getGameObjectRect(iconViewRectTransform, camera);
            Rect ctaViewRect = getGameObjectRect(ctaViewRectTransform, camera);

            nativeAd.Call("fillViews", 0,
                (int)adViewRect.x, (int)adViewRect.y, (int)adViewRect.width, (int)adViewRect.height,
                (int)iconViewRect.x, (int)iconViewRect.y, (int)iconViewRect.width, (int)iconViewRect.height,
                (int)mediaViewRect.x, (int)mediaViewRect.y, (int)mediaViewRect.width, (int)mediaViewRect.height,
                (int)ctaViewRect.x, (int)ctaViewRect.y, (int)ctaViewRect.width, (int)ctaViewRect.height);
        }

        public void ReportClick(YumiNativeData nativeData)
        {
            Logger.Log("ReportClick");
        }

        public void ReportImpression(YumiNativeData nativeData)
        {
            Logger.Log("ReportImpression");
        }

        public void UnregisterView(YumiNativeData nativeData)
        {
            Logger.Log("ReportImpression");
        }

        private Rect getGameObjectRect(RectTransform rectTransform, Camera camera)
        {
            if (rectTransform == null)
            {
                return Rect.zero;
            }

            Vector3[] worldCorners = new Vector3[4];
            Canvas canvas = getCanvas(this.currentGameObject);

            rectTransform.GetWorldCorners(worldCorners);
            Vector3 gameObjectBottomLeft = worldCorners[0];
            Vector3 gameObjectTopRight = worldCorners[2];
            Vector3 cameraBottomLeft = camera.pixelRect.min;
            Vector3 cameraTopRight = camera.pixelRect.max;

            if (canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            {
                gameObjectBottomLeft = camera.WorldToScreenPoint(gameObjectBottomLeft);
                gameObjectTopRight = camera.WorldToScreenPoint(gameObjectTopRight);
            }

            return new Rect(Mathf.Round(gameObjectBottomLeft.x),
                            Mathf.Floor((cameraTopRight.y - gameObjectTopRight.y)),
                            Mathf.Ceil((gameObjectTopRight.x - gameObjectBottomLeft.x)),
                            Mathf.Round((gameObjectTopRight.y - gameObjectBottomLeft.y)));
        }

        private Canvas getCanvas(GameObject gameObject)
        {
            if (gameObject.GetComponent<Canvas>() != null)
            {
                return gameObject.GetComponent<Canvas>();
            }
            else
            {
                if (gameObject.transform.parent != null)
                {
                    return getCanvas(gameObject.transform.parent.gameObject);
                }
            }
            return null;
        }

        #region Callbacks from UnityBannerAdListener.
        void onLayerPrepared(int count)
        {
            OnNativeAdLoaded(this, new YumiNativeToLoadEventArgs());
        }

        void onLayerFailed(String errorMsg)
        {
            YumiAdFailedToLoadEventArgs args = new YumiAdFailedToLoadEventArgs()
            {
                Message = errorMsg
            };
            OnAdFailedToLoad(this, args);
        }

        void onLayerClick()
        {
            OnAdClick(this, EventArgs.Empty);
        }
        #endregion
    }
}
#endif