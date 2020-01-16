using System;
using UnityEngine;
using System.Text;

[Serializable]
public class PacketBase
{
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public byte[] GetBytes()
    {
        return Encoding.UTF8.GetBytes(ToJson());
    }
}

[Serializable]
public class GuidOnlyPacket  : PacketBase
{
    public string Guid;
}

[Serializable]
public class AddPacket : PacketBase
{
    public string Guid;
    public bool Locked;
    public string Holder;
}

[Serializable]
public class LockPacket : PacketBase
{
    public string Guid;
    public string Holder;
}