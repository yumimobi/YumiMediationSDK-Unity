using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;
using System.Collections.Generic;

//#if UNITY_IPHONE && !UNITY_EDITOR

//using UnityEditor.iOS.Xcode;
//public class PostBuildProcess : MonoBehaviour
//{
//    [PostProcessBuild]
//    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
//    {
//        if (buildTarget == BuildTarget.iOS) {
//            BuildForiOS(path);
//        }
//    }
//
//    private static void BuildForiOS(string path)
//    {
//        string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
//        Debug.Log("Build iOS. path: " + projPath);
//
//        PBXProject proj = new PBXProject();
//        var file = File.ReadAllText(projPath);
//        proj.ReadFromString(file);
//
//        string target = proj.TargetGuidByName("Unity-iPhone");
//
//        proj.AddFrameworkToProject(target, "PassKit.framework", false);
//        proj.AddFrameworkToProject(target, "CoreData.framework", false);
//        proj.AddFrameworkToProject(target, "WebKit.framework", false);
//        proj.AddFrameworkToProject(target, "Social.framework", false);
//        proj.AddFrameworkToProject(target, "MobileCoreServices.framework", false);
//        proj.AddFrameworkToProject(target, "Security.framework", false);
//        proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
//        proj.AddFrameworkToProject(target, "AdSupport.framework", false);
//        proj.AddFrameworkToProject(target, "StoreKit.framework", false);
//        proj.AddFrameworkToProject(target, "MessageUI.framework", false);
//        proj.AddFrameworkToProject(target, "EventKit.framework", false);
//        proj.AddFrameworkToProject(target, "EventKitUI.framework", false);
//        proj.AddFrameworkToProject(target, "CoreText.framework", false);
//        proj.AddFrameworkToProject(target, "AudioToolbox.framework", false);
//        proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
//        proj.AddFrameworkToProject(target, "CFNetwork.framework", false);
//        proj.AddFrameworkToProject(target, "CoreGraphics.framework", false);
//        proj.AddFrameworkToProject(target, "CoreLocation.framework", false);
//        proj.AddFrameworkToProject(target, "CoreMedia.framework", false);
//        proj.AddFrameworkToProject(target, "CoreMotion.framework", false);
//        proj.AddFrameworkToProject(target, "CoreVideo.framework", false);
//        proj.AddFrameworkToProject(target, "Foundation.framework", false);
//        proj.AddFrameworkToProject(target, "iAd.framework", false);
//        proj.AddFrameworkToProject(target, "MediaPlayer.framework", false);
//        proj.AddFrameworkToProject(target, "OpenAL.framework", false);
//        proj.AddFrameworkToProject(target, "OpenGLES.framework", false);
//        proj.AddFrameworkToProject(target, "QuartzCore.framework", false);
//        proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
//        proj.AddFrameworkToProject(target, "UIKit.framework", false);
//		proj.AddFrameworkToProject(target, "GLKit.framework", false);
//
//        AddUsrLib(proj, target, "libxml2.dylib");
//		AddUsrLib(proj, target, "libc++.dylib");
//		AddUsrLib(proj, target, "libz.1.2.5.dylib");
//		AddUsrLib(proj, target, "libsqlite3.0.dylib");
//
//        proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");
//        proj.SetBuildProperty(target, "CLANG_ENABLE_MODULES", "YES");
//        proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO");
//
//        File.WriteAllText(projPath, proj.WriteToString());
//    }
//
//    private static void AddUsrLib(PBXProject proj, string targetGuid, string framework)
//    {
//        string fileGuid = proj.AddFile("usr/lib/"+framework, "Frameworks/"+framework, PBXSourceTree.Sdk);
//        proj.AddFileToBuild(targetGuid, fileGuid);
//    }
//}

//#endif