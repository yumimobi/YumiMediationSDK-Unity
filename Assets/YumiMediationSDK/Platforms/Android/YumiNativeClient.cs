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

        public void LoadAd(int adCount)
        {
            nativeAd.Call("loadAd", adCount);
        }

        public void RegisterGameObjectsForInteraction(YumiNativeData yumiNaitveData, GameObject gameObject, Dictionary<NativeElemetType, Transform> elements)
        {
            Logger.Log("YumiNativeClient: RegisterGameObjectsForInteraction " + yumiNaitveData.uniqueId);
            currentGameObject = gameObject;
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
            return nativeAd.Call<bool>("isAdInvalidated", nativeData.uniqueId );
        }

        public void ShowView(YumiNativeData nativeData)
        {
            nativeAd.Call("showView", nativeData.uniqueId );
        }

        public void HideView(YumiNativeData nativeData)
        {
            nativeAd.Call("hideView",  nativeData.uniqueId );
        }

        public void UnregisterView(YumiNativeData nativeData)
        {
            nativeAd.Call("removeView",  nativeData.uniqueId );
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
                nativeData = getNativeData(uniqueIds)
            };
            OnNativeAdLoaded(this, args);
        }

        private List<YumiNativeData> getNativeData(string uniqueIds)
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

                YumiNativeData e = new YumiNativeData
                {
                    uniqueId = uId,
                    title = title,
                    desc = description,
                    iconURL = iconUrl,
                    coverImageURL = coverImageUrl,
                    callToAction = callToAction,
                    appPrice = price,
                    appRating = starRating,
                    other = other
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
        #endregion
    }
}
#endif