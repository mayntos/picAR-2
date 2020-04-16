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

    public async void ConfigurePolaroid()
    {
        await pamRef.OpenImagePicker_Helper();
        kbmRef.SetPicText(pmRef.GetCurrentPolaroid());
    }
}
