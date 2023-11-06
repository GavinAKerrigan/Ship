using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selected : MonoBehaviour
{
    //[SerializeField] GameObject mainCamera;
    [SerializeField] public GameObject ship;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject.Find("Starting").GetComponent<Selected>().ship = this.gameObject;
        Instantiate(ship, this.gameObject.transform.position, this.gameObject.transform.rotation);
        //mainCamera.GetComponent<CameraFollow>().target = GameObject.Find(ship.name);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
