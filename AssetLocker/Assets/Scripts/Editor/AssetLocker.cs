using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using Unity.EditorCoroutines.Editor;
using System.Text;

[InitializeOnLoad]
public static class AssetLocker 
{
    public class Asset
    {
        public string guid;
        public Object obj;
        public string holder;
        public bool locked  ;
    }

    static AddHandler add = null;
    static IsTrackedHandler isTracked = null;
    static IsLockedHandler isLocked = null;
    static LockHandler lck = null;
    static UnlockHandler unlock = null;
    static GetInfoHandler getInfo = null;

    static EditorCoroutine cor = null;

    /// <summary>
    /// key : guid, value : locked
    /// </summary>
    static Dictionary<string, Asset> lockedInfo = null;
    
    static AssetLocker()
    {
        EditorApplication.update += Update;
        Selection.selectionChanged += OnSelectionChanged;

        add = new AddHandler();
        isTracked = new IsTrackedHandler();
        lck = new LockHandler();
        unlock = new UnlockHandler();
        isLocked = new IsLockedHandler();
        getInfo = new GetInfoHandler();

        lockedInfo = new Dictionary<string, Asset>();
        Debug.Log("AssetLocker initialized");
    }

    static void OnSelectionChanged()
    {
        Debug.Log("OnSelectionChanged");

        if (null != cor)
            EditorCoroutineUtility.StopCoroutine(cor);

        if(0 < Selection.assetGUIDs.Length)
            cor = EditorCoroutineUtility.StartCoroutineOwnerless(C_OnSelectionChanged(Selection.assetGUIDs[0]));
    }

    static IEnumerator C_OnSelectionChanged(string guid)
    {
        yield return EditorCoroutineUtility.StartCoroutineOwnerless(isTracked.Request());
        if(!isTracked.succeed)
        {
            yield return EditorCoroutineUtility.StartCoroutineOwnerless(add.Request());
        }

        yield return EditorCoroutineUtility.StartCoroutineOwnerless(getInfo.Request());

        Asset info = lockedInfo[guid];
        if(info.locked && info.holder != AssetLockerOptions.ID)
        {
            if(EditorUtility.DisplayDialog($"Locked",$"Is locked by {lockedInfo[guid].holder}", "OK"))
            {
                Selection.activeObject = null;
            }
        }
    }

    static void Update()
    {
                
    }

    public static void SetInfo(string guid, string holder, bool isLocked)
    {
        Asset founded = null;
        if (!lockedInfo.TryGetValue(guid, out founded))
        {
            founded = new Asset();
            founded.guid = guid;
            lockedInfo.Add(guid, founded);
            Debug.Log($"{guid} is not existed, so inserted");
        }

        founded.locked = isLocked;
        founded.holder = holder;
        Debug.Log($"{guid} is setted");
    }

    [MenuItem("Assets/Lock")]
    public static void Lock()
    {
        EditorCoroutineUtility.StartCoroutineOwnerless(lck.Request());
    }

    [MenuItem("Assets/Unlock")]
    public static void Unlock()
    {
        EditorCoroutineUtility.StartCoroutineOwnerless(unlock.Request());
    }

    public static bool isLockedByCached(string guid)
    {
        Asset result = null;
        if (!lockedInfo.TryGetValue(guid, out result))
            return false;
        else
            return result.locked;
    }
}

public class EmptyCretificator:CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}