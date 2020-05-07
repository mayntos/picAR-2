using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] PhotoAccessController pamRef;

    [SerializeField] KeyboardManager kbmRef;

    public void ConfigurePolaroid()
    {
        pamRef.OpenImagePicker_Helper();
    }
}
