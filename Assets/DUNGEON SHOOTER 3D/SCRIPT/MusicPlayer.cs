using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> musicClips;  // Musiqalar ro'yxati
    private AudioSource audioSource;    // AudioSource komponenti
    private int currentTrackIndex = 0;  // Hozirgi trek indeksi
    public float fadeDuration = 1.0f;   // Ovoz pasayish vaqti
    private bool isPaused = false;      // Musiqa to'xtatilganligini belgilash

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
       // StartMusic();
        
    }

    void Update()
    {
        if (!audioSource.isPlaying && !isPaused)
        {
            PlayNextTrack();
        }
    }

    private void OnEnable()
    {
        // ActionExample dan OnActionTriggered ga obuna bo'lish
        UI_ARENA_Counter.MusicManagerStart += StartMusic;
        UI_ARENA_Counter.MusicManagerPause += FadeOutAndPause;
        UI_ARENA_Counter.MusicManagerResume += Resume;
    }

    private void OnDisable()
    {
        // ActionExample dan OnActionTriggered dan obunani olib tashlash
        UI_ARENA_Counter.MusicManagerStart -= StartMusic;
        UI_ARENA_Counter.MusicManagerPause -= FadeOutAndPause;
        UI_ARENA_Counter.MusicManagerResume -= Resume;
    }
    
    void StartMusic()
    {
        
        if (musicClips.Count > 0)
        {
            PlayCurrentTrack();
        }
    }
    void PlayCurrentTrack()
    {
        if (musicClips.Count == 0) return;

        audioSource.clip = musicClips[currentTrackIndex];
        audioSource.Play();
    }

    void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicClips.Count;
        PlayCurrentTrack();
    }

    public void FadeOutAndPause()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Pause();
        isPaused = true;
    }

    public void Resume()
    {
        if (isPaused)
        {
            audioSource.Play();
            StartCoroutine(FadeInCoroutine());
            isPaused = false;
        }
    }

    private IEnumerator FadeInCoroutine()
    {
        float startVolume = 0.2f;
        audioSource.volume = startVolume;

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = 1.0f;
    }
}
