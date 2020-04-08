using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ExifLib;

public class PhotoAccessManager : MonoBehaviour
{ 
    [DllImport("__Internal")]
    private static extern void OpenImagePicker(string gameObjectName, string functionName);

    [SerializeField]
    PolaroidManager pmRef;

    public void OpenImagePicker_Helper()
    {
        OpenImagePicker(gameObject.name, "StoreTexture");
    }

    // This function is doing more than just storing a texture.
    private IEnumerator StoreTexture(string uri)
    {
        // TODO: Profiling, storing JPEG from URI and processing; as opposed to accessing the URI twice.
        Texture2D imageTexture = Texture2D.blackTexture; // default is a plain, black texture.
        yield return StartCoroutine(TryGenerateTexture(uri, outputTexture => imageTexture = outputTexture));
        pmRef.SetStoredTexture(imageTexture);

        pmRef.RotatePicFrame(ReadExifOrientation(uri));
    }

    private IEnumerator TryGenerateTexture(string source, System.Action<Texture2D> PassTexture)
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

    private int ReadExifOrientation(string sourcePath)
    {
        using (ExifReader reader = new ExifReader(sourcePath))
        {
            if (reader.GetTagValue<int>(ExifTags.Orientation, out int orientationValue))
                return orientationValue;
            else
                return 0; // indicates that no orientation value was located.
        }
    }
}