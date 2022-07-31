using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

//manage the audio mixer
public class DataSavingManager : MonoBehaviour
{
    public const string BACKGROUND_VALUE = "BackgroundMusic";
    public const string SFX_VALUE = "SFXMusic";

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider backgroundSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TextMeshProUGUI backgroundValueText;
    [SerializeField] TextMeshProUGUI sfxValueText;

    float bgValue;
    float sfxValue;

    private void Awake() 
    {
        //Callback executed when the value of the slider is changed.
        backgroundSlider.onValueChanged.AddListener(SetBackgroundMusic);
        sfxSlider.onValueChanged.AddListener(Set_SFX_Music);
    }
    private void Start() 
    {
        //Get the slider values after saving
        backgroundSlider.value = PlayerPrefs.GetFloat(MusicPlayer.BACKGROUND_VALUE);
        sfxSlider.value = PlayerPrefs.GetFloat(MusicPlayer.SFX_VALUE); 
    }

    //When we stop playing on editor, this method will be active
    //this method will take the last values (SetFloat) before stopping playing on editor
    private void OnDisable() 
    {
        Debug.Log("Disabled");
        PlayerPrefs.SetFloat(MusicPlayer.BACKGROUND_VALUE, backgroundSlider.value);
        PlayerPrefs.SetFloat(MusicPlayer.SFX_VALUE, sfxSlider.value);
    }

    public void SetBackgroundMusic (float bgValue)
    {
        this.bgValue = bgValue;
        backgroundValueText.text = this.bgValue.ToString("0.0");
        audioMixer.SetFloat(BACKGROUND_VALUE, Mathf.Log10(bgValue) *20); //we times the value by 20 so that it can reach -80dB when lowest
                                                                        //the highest is so on
    }

    public void Set_SFX_Music (float sfxValue)
    {      
        this.sfxValue = sfxValue;
        sfxValueText.text = this.sfxValue.ToString("0.0");
        audioMixer.SetFloat(SFX_VALUE, Mathf.Log10(sfxValue) * 20); //we times the value by 20 so that it can reach -80dB when lowest
                                                                        //the highest is so on
    }
}
