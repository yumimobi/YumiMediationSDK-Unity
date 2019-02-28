using System;
using YumiMediationSDK.Common;
using YumiMediationSDK.Api;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace YumiMediationSDK.iOS
{
    public class YumiNativeClient : IYumiNativeClient
    {

        private IntPtr nativeAdPtr;

        private IntPtr nativeClientPtr;

        private GameObject currentGameObject;

        #region Banner callback types

        internal delegate void YumiNativeAdDidReceiveAdCallback(IntPtr nativeClient, int adCount);

        internal delegate void YumiNativeAdDidFailToReceiveAdWithErrorCallback(
                IntPtr nativeClient, string error);

        internal delegate void YumiNativeAdDidClickCallback(IntPtr nativeClient);

        #endregion

        // This property should be used when setting the bannerViewPtr.
        private IntPtr NativeAdPtr
        {
            get
            {
                return this.nativeAdPtr;
            }

            set
            {
                YumiExterns.YumiRelease(this.nativeAdPtr); // clear cache ,if existed
                this.nativeAdPtr = value;
            }
        }


        #region IYumiNativeClient event
        // Ad event fired when the native ad has been received.
        public event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        // Ad event fired when the native ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the native ad is click.
        public event EventHandler<EventArgs> OnAdClick;
        #endregion

        #region implement IYumiNativeClient interface 

        // Creates a native ad
        public void CreateNativeAd(string placementId, string channelId, string versionId)
        {
            this.nativeClientPtr = (IntPtr)GCHandle.Alloc(this);
            this.NativeAdPtr = YumiExterns.InitYumiNativeAd(this.nativeClientPtr, placementId, channelId, versionId);

            YumiExterns.SetNativeCallbacks(
                this.NativeAdPtr,
                NativeDidReceiveAdCallback,
                NativeDidFailToReceiveAdWithErrorCallback,
                NativeDidClickCallback);
        }

        // Begins loading the YumiMediationNativeAd with the count you wanted.
        public void LoadAd(int adCount)
        {
            YumiExterns.RequestNativeAd(this.NativeAdPtr, adCount);
        }

        //report Impression
        public void ReportImpression(YumiNativeData nativeData)
        {
            YumiExterns.ReportImpression(this.NativeAdPtr);
        }
        //report click
        public void ReportClick(YumiNativeData nativeData)
        {
            YumiExterns.ReportClick(this.NativeAdPtr);
        }
        // Destroys native ad object.
        public void DestroyNativeAd()
        {
            this.NativeAdPtr = IntPtr.Zero;
            this.currentGameObject = null;
        }

        public void RegisterGameObjectsForInteraction(GameObject gameObject, RectTransform adViewRectTransform,RectTransform mediaViewRectTransform, RectTransform iconViewRectTransform, RectTransform ctaViewRectTransform)
        {
            Logger.Log("RegisterGameObjectsForInteraction");
            this.currentGameObject = gameObject;
            Camera camera = Camera.main;

            Rect adViewRect = getGameObjectRect(adViewRectTransform, camera);
            Rect mediaViewRect = getGameObjectRect(mediaViewRectTransform, camera);
            Rect iconViewRect = getGameObjectRect(iconViewRectTransform, camera);
            Rect ctaViewRect = getGameObjectRect(ctaViewRectTransform, camera);

            RegisterAssetObjectsForInteraction(adViewRect,mediaViewRect, iconViewRect,ctaViewRect);
        }

        public void RegisterAssetObjectsForInteraction(Rect adViewRect, Rect mediaViewRect, Rect iconViewRect, Rect ctaViewRect)
        {
            int uniqueId = 0;
            YumiExterns.RegisterAssetViewsForInteraction(this.NativeAdPtr, uniqueId,
                    (int)adViewRect.x, (int)adViewRect.y, (int)adViewRect.width, (int)adViewRect.height,
                    (int)mediaViewRect.x, (int)mediaViewRect.y, (int)mediaViewRect.width, (int)mediaViewRect.height,
                    (int)iconViewRect.x, (int)iconViewRect.y, (int)iconViewRect.width, (int)iconViewRect.height,
                    (int)ctaViewRect.x, (int)ctaViewRect.y, (int)ctaViewRect.width, (int)ctaViewRect.height);
        }

        public void Dispose()
        {
            this.DestroyNativeAd();
            ((GCHandle)this.nativeClientPtr).Free();
        }

        public void UnregisterView(YumiNativeData nativeData){
            YumiExterns.UnregisterView(this.NativeAdPtr, nativeData.uniqueId);
        }

        ~YumiNativeClient()
        {
            this.Dispose();
        }
        #endregion

        #region private method
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
        #endregion


        #region  native ad  callback methods

        [MonoPInvokeCallback(typeof(YumiNativeAdDidReceiveAdCallback))]
        private static void NativeDidReceiveAdCallback(IntPtr nativeClient, int adCount)
        {
            YumiNativeClient client = IntPtrToNativeClient(nativeClient);
            if (client.OnNativeAdLoaded != null)
            {
                YumiNativeToLoadEventArgs args = new YumiNativeToLoadEventArgs()
                {
                    nativeData = null
                };
                Debug.LogFormat("adcount = {0}",adCount);

                client.OnNativeAdLoaded(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiNativeAdDidFailToReceiveAdWithErrorCallback))]
        private static void NativeDidFailToReceiveAdWithErrorCallback(IntPtr nativeClient, string error)
        {
            YumiNativeClient client = IntPtrToNativeClient(nativeClient);
            if (client.OnAdFailedToLoad != null)
            {
                YumiAdFailedToLoadEventArgs args = new YumiAdFailedToLoadEventArgs()
                {
                    Message = error
                };
                client.OnAdFailedToLoad(client, args);
            }
        }
        [MonoPInvokeCallback(typeof(YumiNativeAdDidClickCallback))]
        private static void NativeDidClickCallback(IntPtr nativeClient)
        {
            YumiNativeClient client = IntPtrToNativeClient(nativeClient);
            if (client.OnAdClick != null)
            {
                client.OnAdClick(client, EventArgs.Empty);
            }
        }


        private static YumiNativeClient IntPtrToNativeClient(IntPtr nativeClient)
        {
            GCHandle handle = (GCHandle)nativeClient;
            return handle.Target as YumiNativeClient;
        }

        #endregion

    }
}
