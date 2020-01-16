using System;
using UnityEditor;

public static class AssetLockerOptions
{
    static AssetLockerOptionsAsset asset = null;
    static AssetLockerOptionsAsset Asset
    {
        get
        {
            if (null == asset)
            {
                asset = EditorGUIUtility.Load("AssetLockerOptions.asset") as AssetLockerOptionsAsset;
            }

            return asset;
        }
    }


    public static void ReloadAsset()
    {
        asset = EditorGUIUtility.Load("AssetLockerOptions.asset") as AssetLockerOptionsAsset;
    }

    public static string ID
    {
        get
        {
            return Asset.ID;
        }
    }
    public static string IP
    {
        get
        {
            return Asset.IP;
        }
    }
}
