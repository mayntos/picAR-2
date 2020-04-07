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

    public PolaroidManager pmRef;

    public void OpenImagePicker_Helper()
    {
        OpenImagePicker(gameObject.name, "StoreTexture");
    }

    private IEnumerator StoreTexture(string uri)
    {
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
                PassTexture(DownloadHandlerTexture.GetContent(uwr));
            }
        }
    }

    private void ReadOrientation(string sourcePath)
    {
        int orientationValue;

        using (ExifReader reader = new ExifReader(sourcePath))
        {
            if (reader.GetTagValue<int>(ExifTags.Orientation, out orientationValue))
            {
                // Relevant ExifOrientations and their values:
                // TopRight = Rotate 90 CW = 6
                // BottomLeft = Rotate 270 CW = 8
                // BottomRight = Rotate 180 = 3
                // EXIF Tag info: https://exiftool.org/TagNames/EXIF.html

                switch (orientationValue)
                {
                    case 6:
                        break;
                    case 8:
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}