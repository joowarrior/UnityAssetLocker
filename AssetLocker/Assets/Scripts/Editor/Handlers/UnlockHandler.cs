using System;
using UnityEditor;

public class UnlockHandler : HandlerBase
{
    protected override string API => "Unlock";

    protected override string PostBody
    {
        get
        {
            LockPacket p = new LockPacket();
            p.Guid = Selection.assetGUIDs[0];
            p.Holder = AssetLockerOptions.ID;

            return p.ToJson();
        }
    }
}
