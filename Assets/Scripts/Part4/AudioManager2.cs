using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager2 : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _ambientGroup, _sfxGroup;
    [SerializeField] private AudioClip[] _ambientTracks;
    private AudioSource _currentTrack, _nextTrack;
    private AudioSource[] _sfxTrack = new AudioSource[5];
    private Coroutine PlayAllSounds, AmbientLoop;

    public static AudioManager2 Instance { get; private set; }

    void Awake()
    {
        // Add an Ambient AudioSource component to the GameObject
        _currentTrack = gameObject.AddComponent<AudioSource>();
        _currentTrack.outputAudioMixerGroup = _ambientGroup;

        // Add an AmbientAudioSource component to the GameObject
        _nextTrack = gameObject.AddComponent<AudioSource>();
        _nextTrack.outputAudioMixerGroup = _ambientGroup;

        for (int i = 0; i < _sfxTrack.Length; i++)
        {
            // Add an SFX AudioSource component to the GameObject
            _sfxTrack[i] = gameObject.AddComponent<AudioSource>();
            _sfxTrack[i].outputAudioMixerGroup = _sfxGroup;
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        AmbientLoop = StartCoroutine(ContinuousMusicPlayback());
    }
    
    #region Play_Ambient
    
    IEnumerator ContinuousMusicPlayback()
    {
        while (true) // This creates an infinite loop
        {
            // Shuffle the array
            _ambientTracks = _ambientTracks.OrderBy(x => Random.value).ToArray();

            // Loop through each track in the shuffled array
            foreach (AudioClip clip in _ambientTracks)
            {
                Debug.Log("Playing: " + clip.name + ", " + clip.length + " seconds long");
                yield return StartCoroutine(FadeTracks(clip, 2f)); // 2f is the fade duration
            
                // Optional: Wait for the track to finish before starting the next fade
                yield return new WaitForSeconds(clip.length - 2f); // Subtract fade duration to start fade before clip ends
            }
        }
    }
    
    IEnumerator FadeTracks(AudioClip newClip, float duration) {
        float timer = 0;
        _nextTrack.clip = newClip;
        _nextTrack.Play();
        while (timer < duration) {
            timer += Time.deltaTime;
            _currentTrack.volume = Mathf.Lerp(1, 0, timer/duration);
            _nextTrack.volume = Mathf.Lerp(0, 1, timer/duration);
            yield return null;
        }
        _currentTrack.Stop();
        (_currentTrack, _nextTrack) = (_nextTrack, _currentTrack); // Swap references
    }  
    #endregion

    #region Play_SFX
    // PlaySFX is expected a two digit string from 01 to 50
    public void PlaySFX(string name)
    {
        // Stop All Sounds, if playing
        if (PlayAllSounds != null)
        {
            StopCoroutine(PlayAllSounds);
        }

        // Need assign s an audioClip from Assets/Casual Game Sounds U6/CasualGameSounds/
        AudioClip s =
            AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Casual Game Sounds U6/CasualGameSounds/DM-CGS-" + name +
                                                     ".wav");
        var availableSFXTrack = FindAvailableSFXTrack();
        if (availableSFXTrack != null)
        {
            availableSFXTrack.PlayOneShot(s);
        }
    }

    public void PlayAllSFX()
    {
        PlayAllSounds = StartCoroutine(PlayAll());
    }

    private IEnumerator PlayAll()
    {
        int i = 1;
        while (i <= 50)
        {
            string name = i.ToString("00");
            AudioClip s =
                AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Casual Game Sounds U6/CasualGameSounds/DM-CGS-" +
                                                         name + ".wav");
            var availableSFXTrack = FindAvailableSFXTrack();
            if (availableSFXTrack != null)
            {
                availableSFXTrack.PlayOneShot(s);
            }
            var soundLength = s.length;
            yield return new WaitForSeconds(soundLength);
            i++;
        }
    }

    private AudioSource FindAvailableSFXTrack()
    {
        for (int i = 0; i < _sfxTrack.Length; i++)
        {
            if (!_sfxTrack[i].isPlaying)
            {
                return _sfxTrack[i];
            }
        }
        return null;
    }
    #endregion

    private void OnDestroy()
    {
        if (AmbientLoop != null)
        {
            StopCoroutine(AmbientLoop);
        }
        
        if (PlayAllSounds != null)
        {
            StopCoroutine(PlayAllSounds);
        }
    }
}