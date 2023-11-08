using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Selected : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject defaultShip;
    public GameObject ship;


    // Start is called before the first frame update
    void Awake()
    {
        if(ship == null) { ship = defaultShip; }
        GameObject instShip = Instantiate(ship, this.gameObject.transform.position, this.gameObject.transform.rotation);
        if (mainCamera) { mainCamera.GetComponent<CameraFollow>().target = instShip; }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
