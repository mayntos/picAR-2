using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPanel : MonoBehaviour
{
    [SerializeField] Button picFramePreview;
    [SerializeField] Text picTextPreview;

    private TouchScreenKeyboard kbRef;

    private void Start()
    {
        PhotoAccessController.Instance.ImageProcessed += (s, eArgs) => SetPicFrame(eArgs.picTexture);
    }

    private void Update()
    {
        if (kbRef != null)
        {
            picTextPreview.text = kbRef.text;
        }
    }

    public void ActivatePreviewPanel()
    {
        gameObject.SetActive(true);
    }

    public void ConfigPicFrame()
    {
        PhotoAccessController.Instance.OpenImagePicker_Helper();
    }

    public void ConfigPicText()
    {
        kbRef = TouchScreenKeyboard.Open(picTextPreview.text);
    }

    private void SetPicFrame(Texture2D t)
    {
        picFramePreview.GetComponent<RawImage>().texture = t;
    }


}
