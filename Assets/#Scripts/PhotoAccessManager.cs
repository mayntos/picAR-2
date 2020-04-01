using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PhotoAccessManager : MonoBehaviour
{ 
    [DllImport("__Internal")]
    private static extern void OpenImagePicker(string gameObjectName, string functionName);

    public PolaroidManager pmRef;

    public void OpenImagePicker_Helper()
    {
        OpenImagePicker(gameObject.name, "StoreTexture");
    }

    private IEnumerator StoreTexture(string uri)
    {
        Texture2D imageTexture = Texture2D.blackTexture; // default is a plain, black texture.
        yield return StartCoroutine(AttemptToGenerateTexture(uri, outputTexture => imageTexture = outputTexture));
        pmRef.SetStoredTexture(imageTexture);
    }

    private IEnumerator AttemptToGenerateTexture(string source, System.Action<Texture2D> PassTexture)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(source))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
            }
            else
            {
                PassTexture(DownloadHandlerTexture.GetContent(uwr));
            }
        }
    }


}