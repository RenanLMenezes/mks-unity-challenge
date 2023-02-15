using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SmoothCamera : MonoBehaviour
{
    [MinValue(0f), MaxValue(1f)] public float smoothSpeed = 0.125f;
    private Vector3 offset = new Vector3(0f,0f,-10f);
    private Vector3 velocity = Vector3.zero;

    private Transform target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothSpeed);
    }
}
