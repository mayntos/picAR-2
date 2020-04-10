using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
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
    public IEnumerator StoreTexture(string uri)
    {
        // TODO: Profiling, storing JPEG from URI and processing; as opposed to accessing the URI twice.
        Texture2D imageTexture = Texture2D.blackTexture; // default is a plain, black texture.
        yield return StartCoroutine(TryGenerateTexture(uri, outputTexture => imageTexture = outputTexture));
        pmRef.SetStoredTexture(imageTexture);
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
                MemoryStream imgStream = new MemoryStream(uwr.downloadHandler.data);
                pmRef.RotatePicFrame(ReadExifOrientation(imgStream));
                PassTexture(DownloadHandlerTexture.GetContent(uwr));
            }
        }
    }

    private short ReadExifOrientation(MemoryStream source)
    {
        using (ExifReader reader = new ExifReader(source))
        {
            if (reader.GetTagValue(ExifTags.Orientation, out System.UInt16 orientationValue))
            {
                return System.Convert.ToInt16(orientationValue);
            }
            else
                return 0;
        }
    }

    public short ReadExifOrientation(string source)
    {
        using (ExifReader reader = new ExifReader(source))
        {
            if (reader.GetTagValue(ExifTags.Orientation, out System.UInt16 orientationValue))
            {
                return System.Convert.ToInt16(orientationValue);
            }
            else
                return 0;
        }
    }
}