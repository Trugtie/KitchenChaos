using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSrc;
    private void Awake()
    {
        this.audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool isPlaySound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (isPlaySound)
        {
            audioSrc.Play();
        }
        else
        {
            audioSrc.Pause();
        }
    }
}
