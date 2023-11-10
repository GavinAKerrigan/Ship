using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] bool followPlayer;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] float lerpSpeed = 0.03f;
    Vector3 cameraPosition;

    void Awake()
    {
        if (!followPlayer) return;
        Vector3 cameraPosition = offset + target.transform.position;
        cameraPosition.z = -10;
        transform.position = cameraPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!followPlayer) return;
        Vector3 cameraPosition = offset + target.transform.position;
        cameraPosition.z = -10;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, lerpSpeed);
    }
}
