using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collision : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Victory")
        {
            Debug.Log("Victory");
            spriteRenderer.color = Color.blue;
            //SceneManager.LoadScene(sceneName: "Victory");
        }
        else if (collision.gameObject.tag == "Respawn")
        {
            Debug.Log("respawn");
            //Ultimately does nothing
        }
        else
        {
            Debug.Log("Other");
            spriteRenderer.color = Color.red;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}
