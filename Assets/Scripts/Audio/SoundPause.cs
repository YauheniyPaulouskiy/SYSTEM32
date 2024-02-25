using GameManager.PauseGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPause : MonoBehaviour, IPausedHandler
{
    private AudioSource _audioSource;

    #region[Initialization]
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PauseGame.instance.AddList(this);
    }

    private void OnDisable()
    {
        PauseGame.instance.RemoveList(this);
    }
    #endregion

    private void SoundPaused(bool isPaused)
    {
        if (isPaused)
        {
            ChangeAudioVolume(0.1f);
        }

        else
        {
            ChangeAudioVolume(0.2f);
        }
    }

    private void ChangeAudioVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    public void IsPaused(bool isPaused)
    {
        SoundPaused(isPaused);
    }
}
