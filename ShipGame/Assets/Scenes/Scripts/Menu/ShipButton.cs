using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipButton : MonoBehaviour
{
    [SerializeField] GameObject ship;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonPress()
    {
        DontDestroyOnLoad(GameObject.Find("Selected").GetComponent<Selected>().ship);
        GameObject.Find("Selected").GetComponent<Selected>().ship = ship;
        Debug.Log("Ding");
    }
}
