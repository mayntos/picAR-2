using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Polaroid : MonoBehaviour
{
    Transform picFrameTransRef;
    MeshRenderer picFrameMeshRendRef;

    TextMeshPro picTextRef;
    TouchScreenKeyboard kbRef;

    private void Awake()
    {
        // Retrieve the relevant component references
        // from this object's children.
        foreach(Transform t in transform)
        {
            if (t.gameObject.tag == "PictureFrame")
            {
                picFrameTransRef = t;
                picFrameMeshRendRef = t.gameObject.GetComponent<MeshRenderer>();
            }

            else if (t.gameObject.tag == "PictureText")
                picTextRef = t.gameObject.GetComponent<TextMeshPro>();
        }
    }

    private void Update()
    {
        if (kbRef != null)
            picTextRef.text = kbRef.text;
    }

    public void SetPicFrameRotation(Vector3 newRotation)
    {
        picFrameTransRef.localEulerAngles = new Vector3(0, 0, 0);
        picFrameTransRef.Rotate(newRotation);
    }

    public void SetPicFrameImage(Texture2D texture)
    {
        picFrameMeshRendRef.material.mainTexture = texture;
    }

    // SetPicText
    // 1. Open TouchScreenKeyboard
    // 2. Update the Polaroid's textbox (also preview) as the user types.
    public void SetPicText()
    {
        kbRef = TouchScreenKeyboard.Open(picTextRef.text, TouchScreenKeyboardType.Default, true, true, false, false, "", 80);
    }
}
