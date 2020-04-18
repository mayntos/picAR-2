using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : MonoBehaviour
{
    TouchScreenKeyboard kbRef;
    Polaroid currPolaroid;

    // Update is called once per frame
    private void Update()
    {
        if (kbRef != null)
        {
            if (kbRef.status == TouchScreenKeyboard.Status.Done)
                kbRef = null;

            currPolaroid.SetPicText(kbRef.text);
        }
    }

    public void SetPicText(string origText)
    {
        kbRef = TouchScreenKeyboard.Open(origText, TouchScreenKeyboardType.Default, true, true, false, false, "", 80);
    }
}
