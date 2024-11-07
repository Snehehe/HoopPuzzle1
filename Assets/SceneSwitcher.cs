using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwitcher : MonoBehaviour
{
    // Specify the name of the scene you want to load
    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            Debug.Log("Player has entered the trigger zone");  // Debug message

            SceneManager.LoadScene(nextSceneName);
        }
    }
}
