using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PhotoAccessManager : MonoBehaviour
{
    [DllImport("_Internal")]
    private static extern void OpenImagePicker(string gameObjectName, string functionName);

    public void OpenImagePicker_Helper()
    {
        OpenImagePicker(gameObject.name, "AssignImage");
    }

    // TODO: determine how the image should be assigned to AR game object.
    // TODO: test whether the above extern function executes as expected.
    //private void AssignImage(string path)
    //{

    //}
