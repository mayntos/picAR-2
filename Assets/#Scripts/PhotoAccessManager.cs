using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PhotoAccessManager : MonoBehaviour
{ 

    [DllImport("__Internal")]
    private static extern void OpenImagePicker(string gameObjectName, string functionName);

    public void OpenImagePicker_Helper()
    {
        OpenImagePicker(gameObject.name, "AssignImage");
    }

    // TODO: determine how the image should be assigned to AR game object.
    private void AssignImage(string path)
    {

    }
}