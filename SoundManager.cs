using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip _slowHearthBeat;
    [SerializeField] private AudioClip _fastHearthBeat;
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private AudioClip _tavernGreetings;
    [SerializeField] private AudioClip _torchAlmostDead;
    [SerializeField] private AudioClip _torchDead;
    [SerializeField] private AudioClip _cratePickup;
    [SerializeField] private AudioClip _successfullGameOver;
    [SerializeField] private AudioClip _indiceTorche;
    [SerializeField] private AudioClip _indiceForge;
    [SerializeField] private AudioClip _soundTrackLabyrinth;
    [SerializeField] private AudioClip[] _insults;
    [SerializeField] private AudioClip[] _success;
    [SerializeField] private AudioClip[] _gameOver;
    [SerializeField] private AudioClip[] _replies;
    [SerializeField] private AudioClip[] _talkingToHimself;
    [SerializeField] private AudioClip[] _findItem;

    [SerializeField] private AudioSource _sourceFX;
    [SerializeField] private AudioSource _sourceLabyrinth;
    [SerializeField] private AudioSource _sourceVoices;
    [SerializeField] private AudioSource _sourceInteractibles;

    public void StartSlowHearthBeat()
    {
        if (_sourceFX.isPlaying)
        {
            _sourceFX.Stop();
        }

        _sourceFX.clip = _slowHearthBeat;
        _sourceFX.Play();

        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        _sourceVoices.clip = _torchAlmostDead;
        _sourceVoices.Play();
    }

    public void StartFastHearthBeat()
    {
        if (_sourceFX.isPlaying)
        {
            _sourceFX.Stop();
        }

        _sourceFX.clip = _fastHearthBeat;
        _sourceFX.Play();

        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        _sourceVoices.clip = _torchDead;
        _sourceVoices.Play();
    }

    public void PlayRandomGameOverSound()
    {
        if (_sourceFX.isPlaying)
        {
            _sourceFX.Stop();
        }

        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        _sourceVoices.clip = _gameOver[Random.Range(0,_gameOver.Length)];
        _sourceVoices.Play();
    }

    public void GameStarted()
    {
        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        

        _sourceVoices.clip = _tavernGreetings;
        _sourceVoices.Play();
    }

    public void PlayRandomInsult()
    {
        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        _sourceVoices.clip = _insults[Random.Range(0, _insults.Length)];
        StartCoroutine(InsultReply(_sourceVoices.clip.length + 0.5f));
        _sourceVoices.Play();
    }

    public void PlayRandomSuccess(int itemsCollected)
    {
        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        if (itemsCollected != 4)
        {
            _sourceVoices.clip = _success[Random.Range(0, _success.Length)];
        }
        else
        {
            _sourceVoices.clip = _successfullGameOver;
        }

        _sourceVoices.Play();
    }

    public void StopFXSource()
    {
        if (_sourceFX.isPlaying)
        {
            _sourceFX.Stop();
        }
    }

    public void PlayCratePickupSound()
    {
        if (_sourceInteractibles.isPlaying)
        {
            _sourceInteractibles.Stop();
        }

        _sourceInteractibles.clip = _cratePickup;
        _sourceInteractibles.Play();
    }
    
    public void PlayRandomTalkingToHimself()
    {
        if (!_sourceVoices.isPlaying)
        {
            _sourceVoices.clip = _talkingToHimself[Random.Range(0, _talkingToHimself.Length)];
            _sourceVoices.Play();
        }
    }

    public void PlayFoundItem(int noItem)
    {
        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        _sourceVoices.clip = _findItem[noItem - 1];
        _sourceVoices.Play();
    }

    public void PlayHintTorch()
    {
        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        _sourceVoices.clip = _indiceTorche;
        _sourceVoices.Play();
    }

    public void PlayHintForge()
    {
        if (_sourceVoices.isPlaying)
        {
            _sourceVoices.Stop();
        }

        _sourceVoices.clip = _indiceForge;
        _sourceVoices.Play();
    }

    public void PlaySoundTrack()
    {
        _sourceLabyrinth.clip = _soundTrackLabyrinth;
        _sourceLabyrinth.Play();
    }

    public void PauseSoundTrack()
    {
        _sourceLabyrinth.Pause();
    }

    IEnumerator InsultReply(float wait)
    {
        yield return new WaitForSeconds(wait);

        _sourceVoices.clip = _replies[Random.Range(0, _replies.Length)];
        _sourceVoices.Play();
    }
}
