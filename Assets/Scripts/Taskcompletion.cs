using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Taskcompletion : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string sceneToLoad;

    // Called when the mouse button is pressed down over the collider
    private void OnMouseDown()
    {
        // Load the specified scene
        Debug.Log("Clicked on the object");
        SceneManager.LoadScene(sceneToLoad);
    }
}
