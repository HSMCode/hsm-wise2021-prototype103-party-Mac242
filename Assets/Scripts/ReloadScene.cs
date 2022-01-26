using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            ReloadingScene();
        }
    }


    public void ReloadingScene()
    {
        // reload the scene and unpause the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("Reloaded Scene!");
    }







}
