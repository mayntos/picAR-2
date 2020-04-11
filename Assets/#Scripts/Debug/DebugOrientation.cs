using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DebugOrientation : MonoBehaviour
{
    [SerializeField]
    List<string> sourcePaths = new List<string>();

    [SerializeField]
    PhotoAccessManager pamRef;

    [SerializeField]
    PolaroidManager pmRef;

    int count = 0;

    private void Start()
    {
        sourcePaths.Add(@"C:\Users\MAYNARD\Downloads\image5.jpeg");
        sourcePaths.Add(@"C:\Users\MAYNARD\Downloads\image4.jpeg");
        sourcePaths.Add(@"C:\Users\MAYNARD\Downloads\image0.jpeg");
        sourcePaths.Add(@"C:\Users\MAYNARD\Downloads\image1 (4).jpeg");
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pmRef.SpawnPolaroid(new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            debug(sourcePaths[count++]);
        }
    }

    void debug(string source)
    {
        pmRef.RotatePicFrame(pamRef.ReadExifOrientation(source));
    }
}
