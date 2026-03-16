using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UISound : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        Button[] buttons = GetComponentsInChildren<Button>(true);

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayRandomSound);
        }
    }

    void PlayRandomSound()
    {
        if (clips.Count == 0) return;

        int index = Random.Range(0, clips.Count);
        audioSource.PlayOneShot(clips[index]);
    }
}