using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, 0.05f);
    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }
}
