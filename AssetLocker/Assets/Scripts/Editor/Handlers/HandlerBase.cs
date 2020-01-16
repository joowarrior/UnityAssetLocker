using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
public abstract class HandlerBase
{
    public bool succeed { get; private set; }
    public virtual IEnumerator Request()
    {
        UnityWebRequest req = new UnityWebRequest(AssetLockerOptions.IP + API,"POST");
        req.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(PostBody));
        req.uploadHandler.contentType = "application/json";
        req.downloadHandler = new DownloadHandlerBuffer();
        req.certificateHandler = new EmptyCretificator();

        yield return req.SendWebRequest();

        if(req.isHttpError || req.isNetworkError)
        {
            //Debug.LogError(req.error);
            succeed = false;
            OnFailed(req.error);
        }
        else
        {
            succeed = true;
            OnSucceed(req.downloadHandler.text);
        }
    }

    protected abstract string API { get; }
    protected abstract string PostBody { get; }
    public virtual void OnSucceed(string data) { }
    public virtual void OnFailed(string error) { Debug.LogError($"{error}"); }
}
