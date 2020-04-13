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

    public void SetPicFrameRotation(Vector3 newRotation)
    {
        picFrameTransRef.localEulerAngles = new Vector3(0, 0, 0);
        picFrameTransRef.Rotate(newRotation);
    }

    public void SetPicFrameImage(Texture2D texture)
    {
        picFrameMeshRendRef.material.mainTexture = texture;
    }
}
