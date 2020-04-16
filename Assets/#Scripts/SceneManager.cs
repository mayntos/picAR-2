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

    public void ConfigurePolaroid()
    {
        Polaroid p = pmRef.GetCurrentPolaroid();
        pamRef.OpenImagePicker_Helper();
        kbmRef.SetPicText(p);
    }
}
