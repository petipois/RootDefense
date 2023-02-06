using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource asource;
    public AudioClip[] clipsToPlay;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        asource = GetComponent<AudioSource>();
    }
    public void PlaySound(int soundClip)
    {
        asource.PlayOneShot(clipsToPlay[soundClip]);
    }
   
}
