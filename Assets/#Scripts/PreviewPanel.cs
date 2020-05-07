using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPanel : MonoBehaviour
{
    [SerializeField] Button picFramePreview;
    [SerializeField] Button picTextPreview;

    private string text;

    private void Start()
    {
        PhotoAccessController.Instance.ImageProcessed += (s, eArgs) => SetPicFrame(eArgs.picTexture);
    }

    public void ConfigPicFrame()
    {
        gameObject.SetActive(true);
        PhotoAccessController.Instance.OpenImagePicker_Helper();
    }

    private void SetPicFrame(Texture2D t)
    {
        picFramePreview.GetComponent<RawImage>().texture = t;
    }


}
