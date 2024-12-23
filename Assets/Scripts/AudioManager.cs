using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Static instance for global access

    [Header("Sound Clips")]
    public AudioClip[] soundClips; // Array of sound effect clips

    [Header("Music Clips")]
    public AudioClip[] musicClips; // Array of background music clips

    public AudioSource soundSource; // For sound effects
    public AudioSource musicSource; // For background music

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager
        }
    }

    void Start()
    {
        // Initialize AudioSources
        soundSource = gameObject.GetComponent<AudioSource>();
        musicSource = gameObject.GetComponent<AudioSource>();

        musicSource.loop = true; // Ensure music loops
    }

    /// <summary>
    /// Play a sound effect by index.
    /// </summary>
    public void PlaySound(int index, float volume = 1.0f)
    {
        if (soundSource == null)
        {
            Debug.LogWarning("No AudioSource available for sound effects.");
            return;
        }

        if (index >= 0 && index < soundClips.Length)
        {
            soundSource.PlayOneShot(soundClips[index], volume);
        }
        else
        {
            Debug.LogWarning("Invalid sound index: " + index);
        }
    }

    /// <summary>
    /// Play background music by index.
    /// </summary>
    public void PlayMusic(int index, float volume = 1.0f)
    {
        if (musicSource == null)
        {
            Debug.LogWarning("No AudioSource available for music.");
            return;
        }

        if (index >= 0 && index < musicClips.Length)
        {
            musicSource.clip = musicClips[index];
            musicSource.volume = volume;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music index: " + index);
        }
    }

    /// <summary>
    /// Stop the currently playing background music.
    /// </summary>
    public void StopMusic()
    {
        if (musicSource == null)
        {
            Debug.LogWarning("No AudioSource available to stop music.");
            return;
        }

        musicSource.Stop();
    }

    /// <summary>
    /// Adjust music volume.
    /// </summary>
    public void SetMusicVolume(float volume)
    {
        if (musicSource == null)
        {
            Debug.LogWarning("No AudioSource available to set music volume.");
            return;
        }

        musicSource.volume = Mathf.Clamp01(volume);
    }

    /// <summary>
    /// Check if music is playing.
    /// </summary>
    public bool IsMusicPlaying()
    {
        if (musicSource == null)
        {
            Debug.LogWarning("No AudioSource available to check if music is playing.");
            return false;
        }

        return musicSource.isPlaying;
    }
}
