using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] Scene scene;
    public void OnButtonPress() { SceneManager.LoadScene(scene.name); }
}
