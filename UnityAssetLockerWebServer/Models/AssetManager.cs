using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;


public enum UnlockResult
{
    Success,
    WrongHolder,
    NotExist,
    AlreadyUnlocked,
    Count
}

public enum LockResult
{
    Success,
    AlreadyLocked,
    NotExist,
    Count
}

public enum AddResult
{
    Success,
    ExistGUID,
    Count
}

public enum GetResult
{
    Success,
    NotExistGUID,
}

public enum RemoveResult
{
    Success,
    NotExist,
}
[JsonObject(MemberSerialization.OptIn)]
public static class AssetManager
{
    const string DATA_PATH = "data.json";
    static Dictionary<string, Asset> dicAssets = new Dictionary<string, Asset>();

    static AssetManager()
    {
        if (File.Exists(DATA_PATH))
        {
            string json = File.ReadAllText(DATA_PATH);
            dicAssets = JsonConvert.DeserializeObject<Dictionary<string, Asset>>(json);
        }
    }

    public static void SaveData()
    {
        if (File.Exists(DATA_PATH))
            File.Delete(DATA_PATH);

        string json = JsonConvert.SerializeObject(dicAssets, Formatting.Indented);
        File.WriteAllText(DATA_PATH, json);
    }

    public static AddResult AddAsset(Asset newAsset)
    {
        if (dicAssets.ContainsKey(newAsset.Guid))
            return AddResult.ExistGUID;

        dicAssets.Add(newAsset.Guid, newAsset);
        SaveData();
        return AddResult.Success;
    }

    public static LockResult LockAsset(string guid, string holder)
    {
        Asset founded = null;
        if (!dicAssets.TryGetValue(guid, out founded))
            return LockResult.NotExist;

        return founded.Lock(holder);
    }

    public static UnlockResult UnlockAsset(string guid, string holder)
    {
        Asset founded = null;
        if (!dicAssets.TryGetValue(guid, out founded))
            return UnlockResult.NotExist;

        return founded.Unlock(holder);
    }

    public static bool TryGetAsset(string guid, out Asset founded)
    {
        if(!dicAssets.TryGetValue(guid,out founded))
            return false;    
            
        return true;
    }

    public static RemoveResult Remove(string guid)
    {
        if(dicAssets.Remove(guid))
            return RemoveResult.Success;
        else
            return RemoveResult.NotExist;
    }
}