using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] bool followPlayer;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        if (!followPlayer) return;
        Vector3 cameraPosition = offset + target.transform.position;
        cameraPosition.z = -10;
        transform.position = cameraPosition;
    }
}
