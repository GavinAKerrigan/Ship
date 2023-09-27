using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

    void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< Updated upstream
        if (collision.gameObject.tag == "Victory")
=======
        Debug.Log("?");
        Debug.Log("Collision");
        if (GetComponent<Collider>().tag == "Victory")
>>>>>>> Stashed changes
        {
            //SceneManager.LoadScene(sceneName: "Victory");
        }
        else
        {
            //SceneManager.LoadScene(sceneName: "Level");
        }
    }
}
