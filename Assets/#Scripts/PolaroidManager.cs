using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidManager : MonoBehaviour
{
    // Create an ImageFrameManager interface.

    // Perhaps I can use an object pool in the future?

    [SerializeField]
    [Tooltip("The prefab that should be spawned.")]
    GameObject objectToSpawn;

    private Texture2D storedTexture;
    private GameObject currPolaroid;

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

    public void RotatePolaroid(float rotationToApply)
    {

    }
}
