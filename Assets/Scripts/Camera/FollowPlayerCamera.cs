using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float threshold;
    [SerializeField] float xMin;
    [SerializeField] float xMax;

    void LateUpdate()
    {
        var diff = transform.position.x - target.position.x;
        if (Mathf.Abs(diff) > threshold)
        {
            transform.position = 
                new Vector3(Mathf.Clamp(target.position.x + Mathf.Sign(diff) * threshold,xMin,xMax), 
                transform.position.y, transform.position.z);
        }
    }
}
