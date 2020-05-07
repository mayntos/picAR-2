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
    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    ARRaycastManager m_RaycastManager;

    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

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
        if (!TryGetTouchData(out Vector2 touchPosition, out TouchPhase touchPhase))
            return;

        if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;

            // adjustment to the rotation, since ARRaycastHit generates a
            // point with a rotation perpendicular to its respective AR plane.
            hitPose.rotation *= Quaternion.Euler(90f, 0, 0);

            switch (touchPhase)
            {
                case TouchPhase.Began:
                    PolaroidManager.Instance.SpawnPolaroid(hitPose.position, hitPose.rotation);
                    break;
                case TouchPhase.Moved:
                    PolaroidManager.Instance.MovePolaroid(hitPose.position);
                    break;
                case TouchPhase.Ended:
                    PolaroidManager.Instance.DeselectPolaroid();
                    break;
                default:
                    break;
            }
        }
    }
}
