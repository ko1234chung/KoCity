using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class BuildAssetBundleEditor : MonoBehaviour
{
    [MenuItem("Build/Build All Asset Bundles")]
    static void BuildAssetBundle()
    {
        SetAllAssetBundleType();
        string assetBundleDirectory = "Assets/StreamingAssets";
        if (!Directory.Exists(assetBundleDirectory))
            Directory.CreateDirectory(assetBundleDirectory);

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
    }

    static void SetAllAssetBundleType()
    {
        string assetPath = Application.dataPath + "/BundleAsset/";
        string[] directoryEntries = Directory.GetDirectories(assetPath);
        foreach (string directoryPath in directoryEntries)
        {
            string assetBundleType = directoryPath.Substring(directoryPath.LastIndexOf("/") + 1);
            SetBundleTypeToDirectoryFile(directoryPath, assetBundleType);
        }
    }

    static void SetBundleTypeToDirectoryFile(string directoryPath, string assetBundleType)
    {
        string[] directoryEntries = Directory.GetDirectories(directoryPath);
        foreach (string directoryEntry in directoryEntries)
        {
            SetBundleTypeToDirectoryFile(directoryEntry, assetBundleType);
        }

        string[] fileEntries = Directory.GetFiles(directoryPath);
        foreach (string filePath in fileEntries)
        {
            if (filePath.EndsWith(".meta"))
                continue;

            string relativepath = "";
            relativepath = "Assets" + filePath.Substring(Application.dataPath.Length);
            var importer = UnityEditor.AssetImporter.GetAtPath(relativepath);
            Debug.Log($"Set file Path {relativepath} with type : {assetBundleType}");

            importer.assetBundleName = assetBundleType;
        }
    }
}
