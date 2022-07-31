using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Remember to set the minimum of each slider to 0.0001 
//so that it can be equal to the minimum of the Audio Mixer (-80.dB) when we use Mathf.Log10*20 it
public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    public const string BACKGROUND_VALUE = "Background Music";
    public const string SFX_VALUE = "SFX Music";

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource sfxMusic;
    [SerializeField] List<AudioClip> sfxClips = new List<AudioClip>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBackgroundMusic();
        Play_SFX_Music();
    }
    public void PlayBackgroundMusic ()
    {
        backgroundMusic.Play();
    }

    public void Play_SFX_Music ()
    {
        AudioClip sfxClip = sfxClips[Random.Range(0, sfxClips.Count - 1)];
        sfxMusic.PlayOneShot(sfxClip);
    }


    //use this method for the value changed of the background slider on inspector section
    public void LoadBackgroundMusic ()
    {
        float bgValue = GetBackgroundMusic();
        audioMixer.SetFloat(DataSavingManager.BACKGROUND_VALUE, Mathf.Log10(bgValue) * 20);
    }

    //use this method for the value changed of the SFX slider on inspector section
    public void LoadSFXMusic ()
    {
        float sfxValue = Get_SFX_Music();
        audioMixer.SetFloat(DataSavingManager.SFX_VALUE, Mathf.Log10(sfxValue) * 20);
    }

    public static float GetBackgroundMusic ()
    {
        return PlayerPrefs.GetFloat(BACKGROUND_VALUE);
    }

    public static float Get_SFX_Music()
    {
        return PlayerPrefs.GetFloat(SFX_VALUE);
    }
}
