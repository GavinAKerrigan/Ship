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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Victory")
        {
            //SceneManager.LoadScene(sceneName: "Victory");
        } else if (collision.gameObject.tag == "Respawn")
        {
            //Do nothing
        }
        else
        {
            //SceneManager.LoadScene(sceneName: "Level");
            Reset();
        }
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
