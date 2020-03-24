using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolaroidManager : MonoBehaviour
{
    // PolaroidManager should be able to:
    // 1. Spawn AR polaroid objects.
    // 2. Store polaroid objects under this script's object.
    // 3. Pop (delete) recent object.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // What should go into spawning AR polaroid?
    // SpawnPolaroid should spawn in a game object at the specified location.
    // Q: How can the user specify the location they'd like to use?
    // A: Perhaps AR Foundation has a way of communicating world space?
    //    This would involve some kind of Vector3...How can I get Vector3 of position in Unity space?
    //    ACTUALLY how can I get Vector3 of a detected, vertical plane.

}
