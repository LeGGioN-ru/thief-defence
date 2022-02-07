using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _sound;
    [SerializeField] private float _volumePercentChanging;

    private WaitForFixedUpdate _fixedUpdateDelay = new WaitForFixedUpdate();
    private bool _isThiefInHouse = false;

    public void DetectThief()
    {
        _isThiefInHouse = !_isThiefInHouse;
        StartSound();
    }

    private void StartSound()
    {
        if (_sound.isPlaying == false)
        {
            _sound.Play();
        }

        StartCoroutine(ChangeVolume());
    }

    private void OnValidate()
    {
        const int MaxChangingPercent = 1;

        if (_volumePercentChanging > MaxChangingPercent)
        {
            _volumePercentChanging = MaxChangingPercent;
        }
    }

    private IEnumerator ChangeVolume()
    {
        const int MaxVolume = 1;
        const int MinVolume = 0;

        while (_sound.isPlaying)
        {
            if (_isThiefInHouse == false)
            {
                _sound.volume = Mathf.MoveTowards(_sound.volume, MinVolume, _volumePercentChanging * Time.deltaTime);

                if (_sound.volume == MinVolume)
                {
                    _sound.Stop();
                }
            }
            else
            {
                _sound.volume = Mathf.MoveTowards(_sound.volume, MaxVolume, _volumePercentChanging * Time.deltaTime);
            }

            yield return _fixedUpdateDelay;

            Debug.Log("Статус музыки (вкл/выкл) - " + _sound.isPlaying + " громкость музыки - " + _sound.volume);
        }
    }
}
