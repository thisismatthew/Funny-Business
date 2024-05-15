using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioSource audioSource; // Drag your Audio Source here in the Unity Editor

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Get the Button component attached to this GameObject
        Button button = GetComponent<Button>();
        // Add a listener to call the PlaySound function when the button is clicked
        button.onClick.AddListener(PlaySound);
    }

    void PlaySound()
    {
        // Check if the AudioSource is not null and the audio clip is not playing
        if (audioSource != null && !audioSource.isPlaying)
        {
            // Play the audio clip
            audioSource.Play();
        }
    }
}
