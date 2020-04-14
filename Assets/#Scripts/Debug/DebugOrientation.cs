using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class DebugOrientation : MonoBehaviour
{
    [SerializeField]
    List<string> sourcePaths = new List<string>();

    [SerializeField]
    PhotoAccessManager pamRef;

    [SerializeField]
    PolaroidManager pmRef;

    Texture2D testTexture;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(StoreTexture(@"C:\Users\MAYNARD\Downloads\image5.jpeg"));
            pmRef.SetStoredTexture(testTexture);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            pmRef.SpawnPolaroid(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            testTexture = rotateTexture(testTexture, false);
            pmRef.SetStoredTexture(testTexture);
        }

    }

    private IEnumerator StoreTexture(string source)
    { 

        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(source))
        {
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError || uwr.isHttpError)
                Debug.Log(uwr.error);

            else
            {
                testTexture = DownloadHandlerTexture.GetContent(uwr);
            }
        }
    }

    Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }
}
