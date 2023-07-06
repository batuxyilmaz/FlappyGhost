using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [Header("Heart Beat Sounds\n0 => idle, 1 => slow, 2 => medium, 3 => fast")]
    public AudioClip[] _clips;
    [SerializeField] private AudioSource _audioSource;
    
    [Header("Jump Sounds")]
    public AudioClip[] _jumpClips;
    [SerializeField] private AudioSource _jumpAudioSource;

    internal void PlayHeartBeatAudio(int clipIndex)
    {
        _audioSource.clip = _clips[clipIndex];
        if (!_audioSource.isPlaying)
            _audioSource.Play();
    }

    internal void PlayJumpSound()
    {
        if (!_jumpAudioSource.isPlaying)
        {
            _jumpAudioSource.clip = _jumpClips[Random.Range(0, _jumpClips.Length)];
            _jumpAudioSource.Play();
        }
    }

    internal void StopAudio()
    {
        _audioSource.Stop();
    }
}