using Immersal.XR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBehaviour : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = GameObject.FindObjectOfType<XRSpace>().transform;
    }
}
