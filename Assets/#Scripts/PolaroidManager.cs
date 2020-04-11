using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidManager : MonoBehaviour
{
    // Perhaps I can use an object pool in the future?

    [SerializeField]
    Polaroid objectToPool;

    [SerializeField]
    int poolCount;
    Queue<Polaroid> polaroidPool;
    Polaroid currPolaroid;

    Texture2D storedTexture;

    public void Awake()
    {
        polaroidPool = new Queue<Polaroid>();
        AddPolaroids(poolCount);
    }

    private void AddPolaroids(int count)
    {
        for(int i = 0; i < count; i++)
        {
            Polaroid p = Instantiate(objectToPool);
            p.gameObject.SetActive(false);
            polaroidPool.Enqueue(p);
        }
    }

    private Polaroid GetPolaroid()
    {
        if (poolCount == 0)
            AddPolaroids(1);

        return polaroidPool.Dequeue();
    }

    public void SetStoredTexture(Texture2D texture)
    {
        storedTexture = texture;
    }

    public void SpawnPolaroid(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        currPolaroid = GetPolaroid();
        currPolaroid.gameObject.SetActive(true);
        MovePolaroid(spawnPosition);
        RotatePolaroid(spawnRotation);
        currPolaroid.SetPicFrameImage(storedTexture);
    }

    public void MovePolaroid(Vector3 newPosition)
    {
        currPolaroid.transform.position = newPosition;
    }

    public void RotatePolaroid(Quaternion newRotation)
    {
        currPolaroid.transform.rotation = newRotation;
    }

    public void DeselectPolaroid()
    {
        polaroidPool.Enqueue(currPolaroid);
        currPolaroid = null;
    }

    public void RotatePicFrame(short orientationValue)
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
                rotationToApply = new Vector3(0, 0, -90f);
                break;
            case 8:
                rotationToApply = new Vector3(0, 0, 90f);
                break;
            case 3:
                rotationToApply = new Vector3(0, 0, 180f);
                break;
            default:
                break;
        }

        currPolaroid.SetPicFrameRotation(rotationToApply);
    }
}
