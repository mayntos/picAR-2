using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    PhotoAccessManager pamRef;

    [SerializeField]
    PolaroidManager pmRef;

    [SerializeField]
    KeyboardManager kbmRef;

    [SerializeField]
    Transform previewCameraTrans;

    [SerializeField]
    GameObject previewPanel;

    public void Awake()
    {
        pamRef.ImageProcessed += (s, ee) => kbmRef.SetPicText(ee.polaroid);
    }

    public void ActivatePreviewPanel()
    {
        InitializePolaroidPreview();
        previewPanel.SetActive(true);
    }

    private void InitializePolaroidPreview()
    {
        Vector3 v = GetPreviewPosition();
        Quaternion q = GetPreviewRotation();

        pmRef.SpawnPolaroid(v, q);
    }

    private Vector3 GetPreviewPosition()
    {
        Vector3 previewPos = new Vector3(previewCameraTrans.position.x + (-0.009f),
                                         previewCameraTrans.position.y + .09f,
                                         previewCameraTrans.position.z + .55f);
        return previewPos;
    }

    private Quaternion GetPreviewRotation()
    {
        Quaternion previewRotation = previewCameraTrans.rotation;
        return previewRotation;
    }
}
