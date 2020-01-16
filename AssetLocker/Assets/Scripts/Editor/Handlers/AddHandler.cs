using System;
using UnityEditor;
using UnityEngine;

public class AddHandler : HandlerBase
{
    protected override string API => "Add";

    protected override string PostBody
    {
        get
        {
            AddPacket p = new AddPacket();
            p.Guid = Util.GetGuid(Selection.activeObject);

            return p.ToJson();
        }
    }

    public override void OnSucceed(string data)
    {
        Debug.Log("Success");
    }
}
