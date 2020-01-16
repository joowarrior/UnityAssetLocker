using System;
using UnityEditor;

public class IsLockedHandler : HandlerBase
{
    protected override string API => "IsLocked";

    protected override string PostBody
    {
        get
        {
            LockPacket p = new LockPacket();
            p.Guid = Util.GetGuid(Selection.activeObject);
            p.Holder = AssetLockerOptions.ID;

            return p.ToJson();
        }
    }

    public override void OnSucceed(string data)
    {
        if (data != "Me")
        {
            UnityEngine.Debug.LogWarning($"{Selection.activeObject.name} is locked by {data}");
            Selection.activeObject = null;
        }
    }

    public override void OnFailed(string error)
    {
        //base.OnFailed(error);
    }
}
