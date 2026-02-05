using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] bgms;
    [SerializeField] private Sprite[] icons;
    [SerializeField] private Image volumeIconImage;
    [SerializeField] private AudioSource mouseClick;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgms[0];
        audioSource.Play();
        volumeIconImage.sprite = icons[0];
    }

    public void EnterForest()   // Cancelled on 6/28
    {
        audioSource.Stop();
        audioSource.clip = bgms[1];
        audioSource.Play();
    }

    public void LeaveForest()   // Cancelled on 6/28
    {
        audioSource.Stop();
        audioSource.clip = bgms[0];
        audioSource.Play();
    }

    private void Mute()
    {
        Debug.Log("Mute!");
        audioSource.mute = true;
        mouseClick.mute = true;
        volumeIconImage.sprite = icons[1];
    }

    private void UnMute()
    {
        Debug.Log("Unmute!");
        audioSource.mute = false;
        mouseClick.mute = false;
        volumeIconImage.sprite = icons[0];
    }

    public void ChangeVolume()
    {
        if (audioSource.mute) 
        { UnMute(); }
        else 
        { Mute(); }
    }
}
