using System.Collections;
using UnityEditor;
using UnityEngine;

public class AudioManager1 : MonoBehaviour {
    
    private AudioSource _audioSource;
    private Coroutine PlayAllSounds;
    public static AudioManager1 Instance 
    { get; private set; }
    void Awake() { 
        // Add an AudioSource component to the GameObject
        _audioSource = gameObject.AddComponent<AudioSource>();
        
        if (Instance == null)
        {
            Instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    
    // PlaySFX is expected a two digit string from 01 to 50
    public void PlaySFX(string name) {
        // Stop All Sounds, if playing
        if (PlayAllSounds != null)
        {
            StopCoroutine(PlayAllSounds);
        }
        
        // Need assign s an audioClip from Assets/Casual Game Sounds U6/CasualGameSounds/
        AudioClip s = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Casual Game Sounds U6/CasualGameSounds/DM-CGS-" + name + ".wav");
        _audioSource.PlayOneShot(s);
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
            AudioClip s = AssetDatabase.LoadAssetAtPath<AudioClip>("Assets/Casual Game Sounds U6/CasualGameSounds/DM-CGS-" + name + ".wav");
            _audioSource.PlayOneShot(s);
            var soundLength = s.length;
            yield return new WaitForSeconds(soundLength);
            i++;
        }
    }
}