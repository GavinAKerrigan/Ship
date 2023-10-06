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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.tag == "Victory")
        {
            Debug.Log("Victory");
            spriteRenderer.color = Color.blue;
            //SceneManager.LoadScene(sceneName: "Victory");
        }
        else
        {
            Debug.Log("Other");
            spriteRenderer.color = Color.red;
            //SceneManager.LoadScene(sceneName: "Level");
        }
    }
}
