#if UNITY_IOS
using System;
using YumiMediationSDK.Common;
using YumiMediationSDK.Api;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

namespace YumiMediationSDK.iOS
{
    public class YumiNativeClient : IYumiNativeClient
    {

        private IntPtr nativeAdPtr;

        private IntPtr nativeClientPtr;

        private GameObject currentGameObject;

        // options 
        private YumiNativeAdOptions adOptions;

#region Banner callback types

        internal delegate void YumiNativeAdDidReceiveAdCallback(IntPtr nativeClient, string nativeDataKey);

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
        public void CreateNativeAd(string placementId, string channelId, string versionId, YumiNativeAdOptions option)
        {
            this.nativeClientPtr = (IntPtr)GCHandle.Alloc(this);
            this.NativeAdPtr = YumiExterns.InitYumiNativeAd(this.nativeClientPtr, placementId, channelId, versionId, (int)option.adChoiseViewPosition,
                                                            (int)option.adAttribution.AdOptionsPosition, option.adAttribution.text, option.adAttribution.textColor,
                                                            option.adAttribution.backgroundColor, option.adAttribution.textSize, option.adAttribution.hide);
            adOptions = option;
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

        // Destroys native ad object.
        public void DestroyNativeAd()
        {
            this.NativeAdPtr = IntPtr.Zero;
            this.currentGameObject = null;
        }

        public void RegisterGameObjectsForInteraction(YumiNativeData yumiNaitveData, GameObject gameObject, Dictionary<NativeElemetType, Transform> elements){
            Logger.Log("RegisterGameObjectsForInteraction");
            this.currentGameObject = gameObject;
            Camera camera = Camera.main;
            
            RectTransform panelRectTransform = elements[NativeElemetType.PANEL] as RectTransform;
            RectTransform titleRectTransform = elements[NativeElemetType.TITLE] as RectTransform;
            RectTransform iconRectTransform = elements[NativeElemetType.ICON] as RectTransform;
            RectTransform coverImageRectTransform = elements[NativeElemetType.COVER_IMAGE] as RectTransform;
            RectTransform callToActionRectTransform = elements[NativeElemetType.CALL_TO_ACTION] as RectTransform;
            if (panelRectTransform == null || titleRectTransform == null || iconRectTransform == null || coverImageRectTransform == null || callToActionRectTransform == null)
            {
                Logger.Log("Yumi Native Ad requires the following transforms: panel, title, icon, coverImage, callToAction.");
                return;
            }
            RectTransform descriptionRectTransform = elements[NativeElemetType.DESCRIPTION] as RectTransform;
            if (descriptionRectTransform == null)
            {
                descriptionRectTransform = new RectTransform();
            }

            Rect adViewRect = getGameObjectRect(panelRectTransform, camera);

            Rect mediaViewRect = getGameObjectRect(coverImageRectTransform, camera);
            Rect iconViewRect = getGameObjectRect(iconRectTransform, camera);
            Rect ctaViewRect = getGameObjectRect(callToActionRectTransform, camera);
            Rect titleRect = getGameObjectRect(titleRectTransform, camera);
            Rect descRect = getGameObjectRect(descriptionRectTransform, camera);

            RegisterAssetObjectsForInteraction(yumiNaitveData,adViewRect, mediaViewRect, iconViewRect, ctaViewRect,titleRect,descRect);
        }

        public bool IsAdInvalidated(YumiNativeData nativeData){
            return YumiExterns.IsAdInvalidated(this.NativeAdPtr,nativeData.uniqueId);
        }

        public void ShowView(YumiNativeData nativeData){
            YumiExterns.ShowView(this.NativeAdPtr, nativeData.uniqueId);
        }

        public void HideView(YumiNativeData nativeData){
            YumiExterns.HideView(this.NativeAdPtr, nativeData.uniqueId);
        }

        public void UnregisterView(YumiNativeData nativeData)
        {
            YumiExterns.UnregisterView(this.NativeAdPtr, nativeData.uniqueId);
        }

        public void Dispose()
        {
            this.DestroyNativeAd();
            ((GCHandle)this.nativeClientPtr).Free();
        }
        ~YumiNativeClient()
        {
            this.Dispose();
        }
        #endregion

        #region private method
        // private method
        private void RegisterAssetObjectsForInteraction(YumiNativeData yumiNaitveData, Rect adViewRect, Rect mediaViewRect, Rect iconViewRect, Rect ctaViewRect, Rect titleRect, Rect descRect)
        {

            //set view style
            YumiExterns.RenderingTitleText(NativeAdPtr,adOptions.titleTextOptions.textColor, adOptions.titleTextOptions.backgroundColor, adOptions.titleTextOptions.textSize);
            YumiExterns.RenderingDescText(NativeAdPtr, adOptions.descTextOptions.textColor, adOptions.descTextOptions.backgroundColor, adOptions.descTextOptions.textSize);
            YumiExterns.RenderingCallToActionText(NativeAdPtr, adOptions.callToActionTextOptions.textColor, adOptions.callToActionTextOptions.backgroundColor, adOptions.callToActionTextOptions.textSize);
            YumiExterns.RenderingIconScaleType(NativeAdPtr,(int)adOptions.iconScaleType);
            YumiExterns.RenderingCoverImageScaleType(NativeAdPtr, (int)adOptions.coverImageScaleType);

            YumiExterns.RegisterAssetViewsForInteraction(this.NativeAdPtr, yumiNaitveData.uniqueId,
                    (int)adViewRect.x, (int)adViewRect.y, (int)adViewRect.width, (int)adViewRect.height,
                    (int)mediaViewRect.x, (int)mediaViewRect.y, (int)mediaViewRect.width, (int)mediaViewRect.height,
                    (int)iconViewRect.x, (int)iconViewRect.y, (int)iconViewRect.width, (int)iconViewRect.height,
                    (int)ctaViewRect.x, (int)ctaViewRect.y, (int)ctaViewRect.width, (int)ctaViewRect.height,
                     (int)titleRect.x, (int)titleRect.y, (int)titleRect.width, (int)titleRect.height,
                     (int)descRect.x, (int)descRect.y, (int)descRect.width, (int)descRect.height);
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

        private YumiNativeData GetNativeAdData(string adUniqueId){

            YumiNativeData nativeAdData = new YumiNativeData
            {
                uniqueId = adUniqueId,
                title = YumiExterns.YumiNativeAdBridgeGetTitle(this.NativeAdPtr, adUniqueId),
                desc = YumiExterns.YumiNativeAdBridgeGetDesc(this.NativeAdPtr, adUniqueId),
                iconURL = YumiExterns.YumiNativeAdBridgeGetIconUrl(this.NativeAdPtr, adUniqueId),
                coverImageURL = YumiExterns.YumiNativeAdBridgeGetCoverImageURL(this.NativeAdPtr, adUniqueId),
                callToAction = YumiExterns.YumiNativeAdBridgeGetCallToAction(this.NativeAdPtr, adUniqueId),
                price = YumiExterns.YumiNativeAdBridgeGetPrice(this.NativeAdPtr, adUniqueId),
                starRating = YumiExterns.YumiNativeAdBridgeGetStarRating(this.NativeAdPtr, adUniqueId),
                other = YumiExterns.YumiNativeAdBridgeGetOther(this.NativeAdPtr, adUniqueId)
            };

            return nativeAdData;
        }
#endregion


#region  native ad  callback methods

        [MonoPInvokeCallback(typeof(YumiNativeAdDidReceiveAdCallback))]
        private static void NativeDidReceiveAdCallback(IntPtr nativeClient, string nativeDataKey)
        {
            YumiNativeClient client = IntPtrToNativeClient(nativeClient);
            if (client.OnNativeAdLoaded != null)
            {
                List<YumiNativeData> nativeList = new List<YumiNativeData>();

                if (nativeDataKey != null)
                {
                    string[] keys = nativeDataKey.Split(',');
                   
                    foreach (var adUniqueId in keys)
                    {
                        
                        YumiNativeData model = client.GetNativeAdData(adUniqueId);
                        nativeList.Add(model);
                    }
                }
               
                YumiNativeToLoadEventArgs args = new YumiNativeToLoadEventArgs()
                {
                   
                    nativeData = nativeList
                };

                Debug.LogFormat("adcount = {0}", nativeDataKey);

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
#endif