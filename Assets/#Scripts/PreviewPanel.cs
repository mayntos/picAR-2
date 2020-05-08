using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewPanel : MonoBehaviour
{
    private TouchScreenKeyboard kbRef;

    [SerializeField] RawImage picFramePreview;
    [SerializeField] Text picTextPreview;

    short picOrientation;

    private void Start()
    {
        PhotoAccessController.Instance.ImageProcessed += (s, eArgs) => SetPicFrame(eArgs.picTexture, eArgs.picOrientation);
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

    private void SetPicFrame(Texture2D t2D, short orientation)
    {
        picFramePreview.texture = t2D;
        picOrientation = orientation;
    }

    public void ConfirmPreview()
    {
        PolaroidManager.Instance.LoadPolaroid();
        PolaroidManager.Instance.SetStoredTexture((Texture2D)picFramePreview.texture);
        PolaroidManager.Instance.SetStoredOrientation(picOrientation);
        PolaroidManager.Instance.SetStoredDescription(picTextPreview.text);
    }
}
