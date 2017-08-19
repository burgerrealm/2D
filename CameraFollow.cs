using UnityEngine;

using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offsetPos;

    void Start()
    {
        offsetPos = transform.position - target.position;
    }
    void Update()   // or LateUpdate or FixedUpdate if camera follows smt which uses physisc!! IMP
    {
        transform.position = target.position + offsetPos;
    }
}