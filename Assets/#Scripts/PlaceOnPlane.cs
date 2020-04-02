using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // can remove this when not debugging
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceOnPlane : MonoBehaviour
{
    [SerializeField]
    private PolaroidManager pmRef;

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    ARRaycastManager m_RaycastManager;

    GameObject spawnedObject;

    public Toggle toggleBegin;
    public Toggle toggleMoved;
    public Toggle toggleEnded;
    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

    //bool TryGetTouchPosition(out Vector2 touchPosition)
    //{
    //    if (Input.touchCount > 0)
    //    {
    //        touchPosition = Input.GetTouch(0).position;
    //        return true;
    //    }
    //    touchPosition = default;
    //    return false;
    //}

    bool TryGetTouchData(out Vector2 touchPosition, out TouchPhase touchPhase)
    {
        if (Input.touchCount > 0)
        {
            Touch currTouch = Input.GetTouch(0);
            touchPosition = currTouch.position;
            touchPhase = currTouch.phase;
            return true;
        }
        else
        {
            touchPosition = default;
            touchPhase = default;
            return false;
        }
    }

    void Update()
    {
        // so... I can get the position...although I also need to know the touch phase.
        // When the touch phase begins -> spawn object at that position.
        // When the moved phase begins -> move the polaroid object accordingly.
        // When the end   phase begins -> deselect the given polaroid objects.
        //if (!TryGetTouchPosition(out Vector2 touchPosition))
        //    return;

        if (!TryGetTouchData(out Vector2 touchPosition, out TouchPhase touchPhase))
            return;

        switch (touchPhase)
        {
            case TouchPhase.Began:
                toggleBegin.isOn = true;
                toggleEnded.isOn = false;
                break;
            case TouchPhase.Moved:
                toggleMoved.isOn = true;
                toggleBegin.isOn = false;
                break;
            case TouchPhase.Ended:
                toggleEnded.isOn = true;
                toggleMoved.isOn = false;
                break;
            default:
                break;
        }

        //if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        //{
        //    // Raycast hits are sorted by distance, so the first one
        //    // will be the closest hit.
        //    var hitPose = s_Hits[0].pose;
        //    if (!spawnedObject)
        //        pmRef.SpawnPolaroid(hitPose.position, hitPose.rotation);
        //}
    }
}
