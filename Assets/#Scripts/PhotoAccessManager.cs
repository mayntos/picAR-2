using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;
using ExifLib;
using TMPro;

public class PhotoAccessManager : MonoBehaviour
{ 
    [DllImport("__Internal")]
    private static extern void OpenImagePicker(string gameObjectName, string functionName);

    [SerializeField]
    PolaroidManager pmRef;

    public event EventHandler<ImageProcessedArgs> ImageProcessed;

    protected virtual void OnImageProcessed(Polaroid p)
    {
        EventHandler<ImageProcessedArgs> handler = ImageProcessed;
        ImageProcessedArgs args = new ImageProcessedArgs(p);
        handler?.Invoke(this, args);
    }

    public void OpenImagePicker_Helper()
    {
        OpenImagePicker(gameObject.name, "TryProcessImage");
    }

    private IEnumerator TryProcessImage(string imgSource)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imgSource))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
                Debug.Log(uwr.error);

            else
            {
                Texture2D imgTexture = DownloadHandlerTexture.GetContent(uwr) ?? Texture2D.blackTexture;
                short imgOrientation = 1;

                using (MemoryStream imgStream = new MemoryStream(uwr.downloadHandler.data))
                {
                    imgOrientation = ReadExifOrientation(imgStream);
                }

                pmRef.SetStoredTexture(imgTexture);
                pmRef.SetStoredOrientation(imgOrientation);
                OnImageProcessed(pmRef.GetCurrentPolaroid());
            }
        }
    }

    private short ReadExifOrientation(MemoryStream source)
    {
        using (ExifReader reader = new ExifReader(source))
        {
            if (reader.GetTagValue(ExifTags.Orientation, out System.UInt16 orientationValue))
                return System.Convert.ToInt16(orientationValue);

            else
                return 0;
        }
    }

    // part of debug
    public short ReadExifOrientation(string source)
    {
        using (ExifReader reader = new ExifReader(source))
        {
            if (reader.GetTagValue(ExifTags.Orientation, out System.UInt16 orientationValue))
                return System.Convert.ToInt16(orientationValue);

            else
                return 0;
        }
    }

    public class ImageProcessedArgs : EventArgs
    {
        public string polaroidText { get; set; }

        public ImageProcessedArgs(Polaroid sourcePolaroid)
        {
            polaroidText = sourcePolaroid.GetPicText();
        }
    }
}