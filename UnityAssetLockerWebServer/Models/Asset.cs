using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Asset
{
    public string Guid { get; set; }
    public bool Locked { get; set; } = false;
    public string Holder { get; set; }

    public Asset()
    {

    }
    public Asset(string guid)
    {
        this.Guid = guid;
        Locked = false;
        Holder = "";
    }

    public LockResult Lock(string holder)
    {
        if (Locked)
            return LockResult.AlreadyLocked;

        Locked = true;
        this.Holder = holder;
        AssetManager.SaveData();
        return LockResult.Success;
    }

    public UnlockResult Unlock(string holder)
    {
        if (!Locked)
            return UnlockResult.AlreadyUnlocked;

        if (this.Holder != holder)
            return UnlockResult.WrongHolder;

        Locked = false;
        Holder = "";
        AssetManager.SaveData();
        return UnlockResult.Success;
    }
}