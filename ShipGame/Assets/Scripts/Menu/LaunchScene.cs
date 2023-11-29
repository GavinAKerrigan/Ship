using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public void OnButtonPress() 
    {
        if (sceneName == "Quit") Application.Quit();
        SceneManager.LoadScene(sceneName);
    }
}
