using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    [SerializeField]
    GameObject ship;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ship);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
