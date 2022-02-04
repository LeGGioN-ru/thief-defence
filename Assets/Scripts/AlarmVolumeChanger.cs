using UnityEngine;

public class AlarmVolumeChanger : MonoBehaviour
{
    [SerializeField] private DoorAlarmTrigger _doorAlarmTrigger;
    [SerializeField] private AudioSource _alarmSound;
    [SerializeField] private float _volumePercentChanging;

    private void OnValidate()
    {
        const int MaxChangingPercent = 1;

        if (_volumePercentChanging > MaxChangingPercent)
        {
            _volumePercentChanging = MaxChangingPercent;
        }
    }

    private void Update()
    {
        Change();

        Debug.Log(_alarmSound.volume);
    }

    private void Change()
    {
        const int MaxVolume = 1;
        const int MinVolume = 0;

        if (_doorAlarmTrigger.IsThiefInHouse == false)
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, MinVolume, _volumePercentChanging * Time.deltaTime);
        }
        else
        {
            _alarmSound.volume = Mathf.MoveTowards(_alarmSound.volume, MaxVolume, _volumePercentChanging * Time.deltaTime);
        }
    }
}
