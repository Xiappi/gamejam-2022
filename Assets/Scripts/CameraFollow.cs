using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [Range(0,10)]
    public float smoothing = 5f;
    public Vector3 offset;
    //public Vector2 minPosition;
    //public Vector2 maxPosition;

    private void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        //targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        //targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
        Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, smoothing * Time.fixedDeltaTime);
        transform.position = smoothPos;
    }
}
