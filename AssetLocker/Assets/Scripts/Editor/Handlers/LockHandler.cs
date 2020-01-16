using System;
using UnityEditor;

public class LockHandler : HandlerBase
{
    protected override string API => "Lock";

    protected override string PostBody
    {
        get
        {
            LockPacket p = new LockPacket();
            p.Guid = Util.GetGuid(UnityEditor.Selection.activeObject);
            p.Holder = AssetLockerOptions.ID;
            return p.ToJson();
        }
    }

    public override void OnSucceed(string data)
    {
        base.OnSucceed(data);
        UnityEngine.Debug.Log($"Locked : name : {Selection.activeObject.name} guid : {Selection.assetGUIDs[0]}");

    }
}
