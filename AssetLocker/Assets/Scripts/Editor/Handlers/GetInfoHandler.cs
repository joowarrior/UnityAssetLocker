using System;
using UnityEditor;
using UnityEngine;

public class GetInfoHandler : HandlerBase
{
    protected override string API => "GetInfo";

    protected override string PostBody
    {
        get
        {
            GuidOnlyPacket p = new GuidOnlyPacket();
            p.Guid = Selection.assetGUIDs[0];

            return p.ToJson();
        }
    }

    public override void OnSucceed(string data)
    {
        AssetLocker.Asset asset = JsonUtility.FromJson<AssetLocker.Asset>(data);
        AssetLocker.SetInfo(asset.guid, asset.holder, asset.locked);
    }
}
