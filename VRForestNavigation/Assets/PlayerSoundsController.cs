using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] footStepSounds;


    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void PlayRandomFootStepSound()
    {
        audioSource.PlayOneShot(footStepSounds[Random.Range(0, footStepSounds.Length)]);
    }
}
