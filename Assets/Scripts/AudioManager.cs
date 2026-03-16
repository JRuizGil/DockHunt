using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [Header("Fuentes de audio")]
    public AudioSource musicSource;     // Música de fondo
    public AudioSource audioSource;     // SFX (clicks)

    [Header("Sonidos")]
    public List<AudioClip> buttonSounds;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider sfxSlider;

    IEnumerator Start()
    {
        yield return null; // Esperar al resto de Start()

        // Crear audio source de música si falta
        if (musicSource == null)
        {
            GameObject m = new GameObject("MusicSource");
            m.transform.SetParent(transform);
            musicSource = m.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

        // Crear audio source para SFX si falta
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Cargar volumen guardado
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol   = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSource.volume = musicVol;
        audioSource.volume = sfxVol;

        if (musicSlider != null)
        {
            musicSlider.value = musicVol;
            musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxVol;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }

        // Añadir sonido a todos los botones
        Button[] allButtons = FindObjectsOfType<Button>();
        foreach (Button btn in allButtons)
        {
            btn.onClick.AddListener(() => PlayRandomButtonSound());
        }
    }

    // Reproducir sonido de botón
    void PlayRandomButtonSound()
    {
        if (buttonSounds == null || buttonSounds.Count == 0) return;

        int randomIndex = Random.Range(0, buttonSounds.Count);
        audioSource.PlayOneShot(buttonSounds[randomIndex]);
    }

    // Ajustar volumen de música
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    // Ajustar volumen de efectos
    public void SetSFXVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }
}
