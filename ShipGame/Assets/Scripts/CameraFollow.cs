using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] bool followPlayer;
    [SerializeField] Vector3 offset;
    public GameObject target;
    Vector3 cameraPosition;

    // Update is called once per frame
    void Update()
    {
        if (!followPlayer) return;
        else if (target == null) return;
        Vector3 cameraPosition = offset + target.transform.position;
        cameraPosition.z = -10;
        transform.position = cameraPosition;
    }
}
