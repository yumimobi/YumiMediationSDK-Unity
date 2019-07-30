#if UNITY_ANDROID
using System;
using System.Collections.Generic;
using UnityEngine;
using YumiMediationSDK.Api;
using YumiMediationSDK.Common;

namespace YumiMediationSDK.Android
{
    public class YumiNativeClient : AndroidJavaProxy, IYumiNativeClient
    {
        private AndroidJavaObject nativeAd;
        private GameObject currentGameObject;
        private YumiNativeAdOptions options;

        public YumiNativeClient() : base(YumiUtils.UnityNativeAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(YumiUtils.UnityActivityClassName);
            AndroidJavaObject activity =
                    playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            this.nativeAd = new AndroidJavaObject(
                YumiUtils.NativeAdClassName, activity, this);
        }
        // Ad event fired when the native ad has been received.
        public event EventHandler<YumiNativeToLoadEventArgs> OnNativeAdLoaded;
        // Ad event fired when the native ad has failed to load.
        public event EventHandler<YumiAdFailedToLoadEventArgs> OnAdFailedToLoad;
        // Ad event fired when the native ad is click.
        public event EventHandler<EventArgs> OnAdClick;
        /// Ad event fired when the native  express ad has been successed
        public event EventHandler<YumiNativeDataEventArgs> OnExpressAdRenderSuccess;
        /// Ad event fired when the native  express ad has been failed.
        public event EventHandler<YumiAdFailedToRenderEventArgs> OnExpressAdRenderFail;
        // Ad event fired when the native  express ad has been click close button.
        public event EventHandler<YumiNativeDataEventArgs> OnExpressAdClickCloseButton;

        public void CreateNativeAd(string placementId, string channelId, string versionId, GameObject gameObject, YumiNativeAdOptions options)
        {
            currentGameObject = gameObject;
            this.options = options;

            Camera camera = Camera.main;
            int expressAdViewWidth = 0;
            int expressAdViewHeight = 0;

            if (options.expressAdViewTransform != null)
            {
                Rect adViewRect = getGameObjectRect(options.expressAdViewTransform as RectTransform, camera);
                expressAdViewWidth = (int)adViewRect.width;
                expressAdViewHeight = (int)adViewRect.height;
            }

            AdAttribution aab = options.adAttribution;
            TextOptions titleOps = options.titleTextOptions;
            TextOptions ctaOps = options.callToActionTextOptions;
            TextOptions descOps = options.descTextOptions;

            nativeAd.Call("create", placementId, channelId, versionId,
                // int adChosePosition,
                (int)options.adChoiseViewPosition,
                // int attriPosition, String attriText, int attriTextSize, String attriTextColor, String attriTextBackgroundColor
                (int)aab.AdOptionsPosition, aab.text, aab.textSize, getColorString(aab.textColor), getColorString(aab.backgroundColor),
                // int titleSize, String titleColor, String titleBackgroundColor,
                titleOps.textSize, getColorString(titleOps.textColor), getColorString(titleOps.backgroundColor),
                // int descSize, String descColor, String descBackgroundColor
                descOps.textSize, getColorString(descOps.textColor), getColorString(descOps.backgroundColor),
                // int ctaSize, String ctaColor, String ctaBackgroundColor,
                ctaOps.textSize, getColorString(ctaOps.textColor), getColorString(ctaOps.backgroundColor),
                // int iconScaleType, int coverImageScaleType
                (int)options.iconScaleType, (int)options.coverImageScaleType, expressAdViewWidth, expressAdViewHeight);

            Logger.Log("YumiUNativeAd unity: create 2");
        }

        private String getColorString(uint color)
        {
            return color.ToString("x");
        }

        public void LoadAd(int adCount)
        {
            nativeAd.Call("loadAd", adCount);
        }

        public void RegisterNativeDataForInteraction(YumiNativeData yumiNaitveData, Dictionary<NativeElemetType, Transform> elements)
        {
            Logger.Log("YumiNativeClient: RegisterGameObjectsForInteraction " + yumiNaitveData.uniqueId);
            Camera camera = Camera.main;
            RectTransform panel = elements[NativeElemetType.PANEL] as RectTransform;
            RectTransform titile = elements[NativeElemetType.TITLE] as RectTransform;
            RectTransform icon = elements[NativeElemetType.ICON] as RectTransform;
            RectTransform coverImage = elements[NativeElemetType.COVER_IMAGE] as RectTransform;
            RectTransform callToAction = elements[NativeElemetType.CALL_TO_ACTION] as RectTransform;
            if (panel == null || titile == null || icon == null || coverImage == null || callToAction == null)
            {
                Logger.Log("Yumi Native Ad requires the following transforms: panel, titile, icon, coverImage, callToAction.");
                return;
            }
            RectTransform description = elements[NativeElemetType.DESCRIPTION] as RectTransform;
            if (description == null)
            {
                description = new RectTransform();
            }

            Rect panelRect = getGameObjectRect(panel, camera);
            Rect titileRect = getGameObjectRect(titile, camera);
            Rect iconRect = getGameObjectRect(icon, camera);
            Rect coverImageRect = getGameObjectRect(coverImage, camera);
            Rect callToActionRect = getGameObjectRect(callToAction, camera);
            Rect descriptionRect = getGameObjectRect(description, camera);
            nativeAd.Call("fillViews", yumiNaitveData.uniqueId,
                (int)panelRect.x, (int)panelRect.y, (int)panelRect.width, (int)panelRect.height,
                (int)titileRect.x, (int)titileRect.y, (int)titileRect.width, (int)titileRect.height,
                (int)iconRect.x, (int)iconRect.y, (int)iconRect.width, (int)iconRect.height,
                (int)coverImageRect.x, (int)coverImageRect.y, (int)coverImageRect.width, (int)coverImageRect.height,
                (int)callToActionRect.x, (int)callToActionRect.y, (int)callToActionRect.width, (int)callToActionRect.height,
                (int)descriptionRect.x, (int)descriptionRect.y, (int)descriptionRect.width, (int)descriptionRect.height);

        }

        public bool IsAdInvalidated(YumiNativeData nativeData)
        {
            return nativeAd.Call<bool>("isAdInvalidated", nativeData.uniqueId);
        }

        public void ShowView(YumiNativeData nativeData)
        {
            nativeAd.Call("showView", nativeData.uniqueId);
        }

        public void HideView(YumiNativeData nativeData)
        {
            nativeAd.Call("hideView", nativeData.uniqueId);
        }

        public void UnregisterView(YumiNativeData nativeData)
        {
            nativeAd.Call("removeView", nativeData.uniqueId);
        }

        public void DestroyNativeAd()
        {
            nativeAd.Call("destroy");
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
        void onLayerPrepared(string uniqueIds)
        {
            if (uniqueIds == null || uniqueIds.Length == 0)
            {
                onLayerFailed("YumiBirdge: cannot found valiated uniqueIds");
                return;
            }
            YumiNativeToLoadEventArgs args = new YumiNativeToLoadEventArgs()
            {
                nativeData = getNativeDatas(uniqueIds)
            };
            OnNativeAdLoaded(this, args);
        }

        private List<YumiNativeData> getNativeDatas(string uniqueIds)
        {
            List<YumiNativeData> result = new List<YumiNativeData>();
            string[] uIds = uniqueIds.Split(',');
            foreach (string uId in uIds)
            {
                string title = nativeAd.Call<string>("getTitle", uId);
                string description = nativeAd.Call<string>("getDescription", uId);
                string iconUrl = nativeAd.Call<string>("getIconURL", uId);
                string coverImageUrl = nativeAd.Call<string>("getCoverImageURL", uId);
                string callToAction = nativeAd.Call<string>("getCallToAction", uId);
                string price = nativeAd.Call<string>("getPrice", uId);
                string starRating = nativeAd.Call<string>("getStarRating", uId);
                string other = nativeAd.Call<string>("getOther", uId);
                bool isExpressAdView = nativeAd.Call<bool>("isExpressAdView", uId);

                YumiNativeData e = new YumiNativeData
                {
                    uniqueId = uId,
                    title = title,
                    desc = description,
                    iconURL = iconUrl,
                    coverImageURL = coverImageUrl,
                    callToAction = callToAction,
                    price = price,
                    starRating = starRating,
                    other = other,
                    isExpressAdView = isExpressAdView
                };

                result.Add(e);
            }
            return result;
        }

        void onLayerFailed(string errorMsg)
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

        void onExpressAdRenderSuccess(string uniqueId) {
            if (uniqueId == null)
            {
                return;
            }
            YumiNativeDataEventArgs args = new YumiNativeDataEventArgs()
            {
                nativeData = getNativeData(uniqueId)
            };
            OnExpressAdRenderSuccess(this, args);
        }

        void onExpressAdRenderFail(string uniqueId,string errorMsg)
        {
            if (uniqueId == null)
            {
                return;
            }
            YumiAdFailedToRenderEventArgs args = new YumiAdFailedToRenderEventArgs()
            {
                nativeData = getNativeData(uniqueId),
                Message = errorMsg
            };
            OnExpressAdRenderFail(this, args);
        }

        void onExpressAdClickCloseButton(string uniqueId)
        {
            if (uniqueId == null)
            {
                return;
            }
            YumiNativeDataEventArgs args = new YumiNativeDataEventArgs()
            {
                nativeData = getNativeData(uniqueId)
            };
            OnExpressAdClickCloseButton(this, args);
        }

        private YumiNativeData getNativeData(string uniqueId)
        {
            string title = nativeAd.Call<string>("getTitle", uniqueId);
            string description = nativeAd.Call<string>("getDescription", uniqueId);
            string iconUrl = nativeAd.Call<string>("getIconURL", uniqueId);
            string coverImageUrl = nativeAd.Call<string>("getCoverImageURL", uniqueId);
            string callToAction = nativeAd.Call<string>("getCallToAction", uniqueId);
            string price = nativeAd.Call<string>("getPrice", uniqueId);
            string starRating = nativeAd.Call<string>("getStarRating", uniqueId);
            string other = nativeAd.Call<string>("getOther", uniqueId);
            bool isExpressAdView = nativeAd.Call<bool>("isExpressAdView", uniqueId);

            YumiNativeData e = new YumiNativeData
            {
                uniqueId = uniqueId,
                title = title,
                desc = description,
                iconURL = iconUrl,
                coverImageURL = coverImageUrl,
                callToAction = callToAction,
                price = price,
                starRating = starRating,
                other = other,
                isExpressAdView = isExpressAdView
            };

           return e;
        }

        #endregion
    }
}
#endif