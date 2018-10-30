using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Collections.Generic;

public class PostBuildProcess : MonoBehaviour
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        if (buildTarget == BuildTarget.iOS) {
            BuildForiOS(path);
        }
    }

    private static void BuildForiOS(string path)
    {
        string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";
        Debug.Log("Build iOS. path: " + projPath);

        PBXProject proj = new PBXProject();
        var file = File.ReadAllText(projPath);
        proj.ReadFromString(file);

        string target = proj.TargetGuidByName("Unity-iPhone");

		// 增加广告sdk
////		proj.AddAddFile();
//		string fguid = proj.AddFile("YumiMediationSDK/*/YumiMediationSDK.framework", "YumiMediationSDK/*/YumiMediationSDK.framework");
//		proj.AddFileToBuild(target, fguid);
//		string copyFilesPhaseGuid = proj.AddCopyFilesBuildPhase(targetGuid, "Embed Frameworks", "", "10");
//		proj.AddFileToBuildSection(target, copyFilesPhaseGuid, fguid); 

		proj.AddFrameworkToProject(target, "WebKit.framework", false);
		proj.AddFrameworkToProject(target, "MobileCoreServices.framework", false);
		proj.AddFrameworkToProject(target, "Security.framework", false);
		proj.AddFrameworkToProject(target, "CoreTelephony.framework", false);
		proj.AddFrameworkToProject(target, "AdSupport.framework", false);
		proj.AddFrameworkToProject(target, "StoreKit.framework", false);
		proj.AddFrameworkToProject(target, "AudioToolbox.framework", false);
		proj.AddFrameworkToProject(target, "AVFoundation.framework", false);
		proj.AddFrameworkToProject(target, "CoreGraphics.framework", false);
		proj.AddFrameworkToProject(target, "CoreLocation.framework", false);
		proj.AddFrameworkToProject(target, "CoreMedia.framework", false);
		proj.AddFrameworkToProject(target, "CoreMotion.framework", false);
		proj.AddFrameworkToProject(target, "Foundation.framework", false);
		proj.AddFrameworkToProject(target, "SystemConfiguration.framework", false);
		proj.AddFrameworkToProject(target, "UIKit.framework", false);
		proj.AddFrameworkToProject(target, "SafariServices.framework", false);
		proj.AddFrameworkToProject(target, "JavaScriptCore.framework", false);
		proj.AddFrameworkToProject(target, "GLKit.framework", false);


        AddUsrLib(proj, target, "libxml2.dylib");
		AddUsrLib(proj, target, "libc++.dylib");
		AddUsrLib(proj, target, "libz.1.2.5.dylib");
		AddUsrLib(proj, target, "libsqlite3.0.dylib");

        proj.AddBuildProperty(target, "OTHER_LDFLAGS", "-ObjC");// 加载oc的类和分类到静态库
        proj.SetBuildProperty(target, "CLANG_ENABLE_MODULES", "YES"); // 支持的模块，c和oc
        proj.SetBuildProperty(target, "ENABLE_BITCODE", "NO"); // bitcode 

        File.WriteAllText(projPath, proj.WriteToString());
    }

    private static void AddUsrLib(PBXProject proj, string targetGuid, string framework)
    {
        string fileGuid = proj.AddFile("usr/lib/"+framework, "Frameworks/"+framework, PBXSourceTree.Sdk);
        proj.AddFileToBuild(targetGuid, fileGuid);
    }
}

