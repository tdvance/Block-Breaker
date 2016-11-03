using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundFXManager : MonoBehaviour {

    public List<AudioClip> clips = new List<AudioClip>();
    public float volume = 0.7f;

    private AudioSource audioSource;

    // Use this for initialization
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;
    }

    public void play(int index) {
        audioSource.PlayOneShot(clips[index]);
    }

    #region Singleton
    private static SoundFXManager _instance;

    public static SoundFXManager instance {
        get {
            if (_instance == null) {//in case not awake yet
                _instance = FindObjectOfType<SoundFXManager>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (_instance != null && _instance != this) {
            Debug.LogError("Duplicate singleton " + this.gameObject + " created; destroying it now");
            Destroy(this.gameObject);
        }

        if (_instance != this) {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    #endregion
}
