using UnityEngine;
using UnityEngine.UI;
//Made using ChatGPT and human corrections
public class SFXVolumeController : MonoBehaviour
{
    private GameObject sfxGameObject; // Assign the "SFXs" GameObject in the Inspector
    private Slider volumeSlider;
    private AudioSource[] sfxAudioSources;

    private void Start()
    {
        sfxGameObject = GameObject.Find("SFXs");
        volumeSlider = GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });

        // Get all AudioSource components from children of the "SFXs" GameObject
        sfxAudioSources = sfxGameObject.GetComponentsInChildren<AudioSource>();

        // Set the initial volume based on the slider value
        UpdateVolume();
    }

    public void ValueChangeCheck()
    {
        // Called when the slider value changes
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        // Set the volume of all AudioSource components based on the slider value
        float volume = volumeSlider.value;

        foreach (AudioSource audioSource in sfxAudioSources)
        {
            if (audioSource != null)
            {
                audioSource.volume = volume;
            }
        }
    }
}
