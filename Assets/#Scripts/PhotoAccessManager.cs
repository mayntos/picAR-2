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

    public void OpenImagePicker_Helper()
    {
        OpenImagePicker(gameObject.name, "AssignImage");
    }

    // TODO: determine how the image should be assigned to AR game object.
    private IEnumerator AssignImage(string uri)
    {
        Texture2D imageTexture;
        yield return StartCoroutine(GenerateTexture(uri, outputTexture => imageTexture = outputTexture));
        // Pass texture reference Polaroid manager

    }

    private IEnumerator GenerateTexture(string source, System.Action<Texture2D> PassTexture)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(source))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
            {
                Debug.Log(uwr.error);
                yield break;

            }
            else
            {
                PassTexture(DownloadHandlerTexture.GetContent(uwr));
            }
        }
      
    }
}