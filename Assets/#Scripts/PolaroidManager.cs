using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidManager : MonoBehaviour
{
    public static PolaroidManager Instance { get; private set; }

    [SerializeField] GameObject arCamRef;
    public float hidePolaroidOffsetVal;

    public Vector3 polaroidSpawnOffsetVal;

    [SerializeField] Polaroid objectToPool;

    [SerializeField] int poolCount;
    Queue<Polaroid> polaroidPool;

    Polaroid currPolaroid;
    Texture2D storedTexture;
    short storedOrientation;
    string storedDescription;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

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

    public void LoadPolaroid()
    {
        currPolaroid = GetPolaroid();
    }

    private Polaroid GetPolaroid()
    {
        if (poolCount == 0)
            AddPolaroids(1);

        return polaroidPool.Dequeue();
    }

    /*
    public Polaroid GetCurrentPolaroid()
    {
        currPolaroid = GetPolaroid();

        if (currPolaroid.gameObject.activeSelf == true)
        {
            Vector3 behindCameraPos = arCamRef.transform.position;
            behindCameraPos.z -= hidePolaroidOffsetVal;
            MovePolaroid(behindCameraPos);
        }
        return currPolaroid;
    }
    */

    public void SetStoredTexture(Texture2D texture)
    {
        storedTexture = texture;
    }

    public void SetStoredOrientation(short val)
    {
        storedOrientation = val;
    }

    public void SetStoredDescription(string s)
    {
        storedDescription = s;
    }

    public void SpawnPolaroid(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        currPolaroid.gameObject.SetActive(true);
        MovePolaroid(spawnPosition);
        RotatePolaroid(spawnRotation);
        RotatePicFrame(storedOrientation);
        currPolaroid.SetPicFrameImage(storedTexture);
        currPolaroid.SetPicText(storedDescription);
    }

    public void MovePolaroid(Vector3 newPosition)
    {
        currPolaroid.transform.position = newPosition + polaroidSpawnOffsetVal;
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
                rotationToApply = new Vector3(0, 0, 0);
                break;
        }

            currPolaroid.SetPicFrameRotation(rotationToApply);
    }
}
