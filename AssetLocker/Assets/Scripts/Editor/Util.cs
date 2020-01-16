using System;
using UnityEditor;

public static class Util
{
    public static string GetGuid(UnityEngine.Object obj)
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        string guid = AssetDatabase.AssetPathToGUID(path);

        return guid;
    }
}
