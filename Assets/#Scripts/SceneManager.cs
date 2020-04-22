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

    public void Awake()
    {
        pamRef.ImageProcessed += (s, ee) => kbmRef.SetPicText(ee.polaroid);
    }

    public void ConfigurePolaroid()
    {
        pamRef.OpenImagePicker_Helper();
    }
}
