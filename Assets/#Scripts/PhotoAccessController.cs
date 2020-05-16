using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Networking;
using ExifLib;
using TMPro;

public class PhotoAccessController : MonoBehaviour
{
    public static PhotoAccessController Instance { get; private set; }

    [DllImport("__Internal")]
    private static extern void OpenImagePicker(string gameObjectName, string functionName);

    public event EventHandler<ImageProcessedArgs> ImageProcessed;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnImageProcessed(Texture2D t, short o)
    {
        EventHandler<ImageProcessedArgs> handler = ImageProcessed;
        ImageProcessedArgs picData = new ImageProcessedArgs(t, o);
        handler?.Invoke(this, picData);
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

                PolaroidManager.Instance.SetStoredTexture(imgTexture);
                PolaroidManager.Instance.SetStoredOrientation(imgOrientation);
                OnImageProcessed(imgTexture, imgOrientation);
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

    public class ImageProcessedArgs : EventArgs
    {
        public Texture2D picTexture { get; private set; }
        public short picOrientation { get; private set; }

        public ImageProcessedArgs(Texture2D t, short o)
        {
            picTexture = t;
            picOrientation = o;
        }
    }
}