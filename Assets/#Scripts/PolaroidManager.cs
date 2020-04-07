using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidManager : MonoBehaviour
{
    // Create an ImageFrameManager interface.

    // Perhaps I can use an object pool in the future?

    [SerializeField]
    [Tooltip("The prefab that should be spawned.")]
    Polaroid objectToSpawn;

    private Texture2D storedTexture;
    private Polaroid currPolaroid;

    public void SetStoredTexture(Texture2D texture)
    {
        storedTexture = texture;
    }

    public void SpawnPolaroid(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        currPolaroid = Instantiate(objectToSpawn, spawnPosition, spawnRotation); 
        objectToSpawn.GetComponent<Renderer>().material.mainTexture = storedTexture;
    }

    public void MovePolaroid(Vector3 newPosition)
    {
        currPolaroid.transform.position = newPosition;
    }

    public void DeselectPolaroid()
    {
        currPolaroid = null;
    }

    public void RotatePicFrame(int orientationValue)
    {
        if (orientationValue == 0)
            return;

        // Relevant ExifOrientations and their values:
        // TopRight = Rotate 90 CW = 6
        // BottomLeft = Rotate 270 CW = 8
        // BottomRight = Rotate 180 = 3
        // EXIF Tag info: https://exiftool.org/TagNames/EXIF.html
        Vector3 rotationToApply = default;

        switch (orientationValue)
        {
            case 6:
                rotationToApply = new Vector3(90f, 90f, -90f);
                break;
            case 8:
                rotationToApply = new Vector3(90f, 90f, 90f);
                break;
            case 3:
                rotationToApply = new Vector3(90f, 90f, 180f);
                break;
            default:
                break;
        }

        currPolaroid.SetPicFrameRotation(rotationToApply);
    }
}
