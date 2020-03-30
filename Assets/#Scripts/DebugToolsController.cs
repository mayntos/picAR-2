using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugToolsController : MonoBehaviour
{
    [SerializeField]
    GameObject runtimeHierarchyRef;

    [SerializeField]
    GameObject runtimeInspectorRef;

    public void ToggleToolDisplay()
    {
        runtimeHierarchyRef.SetActive(!runtimeHierarchyRef.activeSelf);
        runtimeInspectorRef.SetActive(!runtimeInspectorRef.activeSelf);
    }
}
