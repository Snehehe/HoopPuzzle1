using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject youWinPanel; // Assign your "You Win" panel in the inspector

    // This method is called when another collider enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            ShowWinScreen();
        }
    }

    // Method to show the "You Win" screen
    private void ShowWinScreen()
    {
        if (youWinPanel != null)
        {
            youWinPanel.SetActive(true); // Activate the "You Win" panel
            Time.timeScale = 0f;         // Optional: Pause the game
        }
        else
        {
            Debug.LogError("You Win panel is not assigned in the inspector.");
        }
    }
}
