using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    public AudioClip clickSound; // The sound clip to play
    private AudioSource audioSource; // The AudioSource to play the sound
    public AudioClip hoverSound; // The sound clip to play

    void Start () {

        // Find or assign the AudioSource component
        audioSource = FindFirstObjectByType<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		SceneManager.LoadScene ("level1");
        PlayClickSound();

    }

	public void Quit(){
        PlayClickSound();

        Application.Quit ();
	}
    public void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
    public void PlayHoverSound()
    {
        if (audioSource != null && hoverSound != null)
        {
            audioSource.PlayOneShot(hoverSound);
        }
    }
}
