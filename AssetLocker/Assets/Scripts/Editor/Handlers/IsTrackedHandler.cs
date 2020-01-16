using System;
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;

public class IsTrackedHandler : HandlerBase
{
    protected override string API => "GetInfo";

    protected override string PostBody
    {
        get
        {
            GuidOnlyPacket getInfoPack = new GuidOnlyPacket();
            getInfoPack.Guid = Util.GetGuid(Selection.activeObject);

            return getInfoPack.ToJson();
        }
    }
}
