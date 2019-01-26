using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    Transform camTransform;

    private Vector3 offset;

    void Start()
    {
        camTransform = transform;
        offset = camTransform.position - target.transform.position;
    }

    void LateUpdate()
    {
       
        //transform.LookAt(target.transform);
        camTransform.position = Vector3.Lerp(camTransform.position, target.transform.position + offset, 0.05f);
        transform.position = camTransform.position;

    }

    public void SetTarget(GameObject t)
    {
        target = t;
    }
}
