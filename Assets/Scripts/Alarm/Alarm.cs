using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSound;

    private float _maxAlarmVolume = 1.0f;
    private float _minAlarmVolume = 0.0f;
    private float _changeVolumeTime = 3.0f;
    private AlarmTrigger[] _alarmsTriggers;

    private void Awake()
    {
        _alarmsTriggers = gameObject.GetComponentsInChildren<AlarmTrigger>();

        foreach (var alarm in _alarmsTriggers)
        {
            alarm.Reached += OnAlarmReached;
            alarm.Reached += OnAlarmExit;
        }
    }

    private void OnDisable()
    {
        foreach (var alarm in _alarmsTriggers)
        {
            alarm.Reached -= OnAlarmReached;
            alarm.Reached -= OnAlarmExit;
        }
    }

    private void Start()
    {
        _alarmSound = GetComponent<AudioSource>();
    }

    private void OnAlarmReached()
    {
        foreach (var alarm in _alarmsTriggers)
        {
            if(alarm.IsReached == true)
            {
                StartCoroutine(ChangeVolume(_alarmSound.volume, _maxAlarmVolume, _changeVolumeTime));
            }
        }
    }

    private void OnAlarmExit()
    {
        foreach (var alarm in _alarmsTriggers)
        {
            if (alarm.IsReached == false)
            {
                StartCoroutine(ChangeVolume(_alarmSound.volume, _minAlarmVolume, _changeVolumeTime));
            }
        }
    }

    private IEnumerator ChangeVolume(float startVolume, float endVolume, float changeTime)
    {
        float currentTime = 0f;

        while (currentTime < changeTime)
        {
            currentTime += Time.deltaTime;
            _alarmSound.volume = Mathf.MoveTowards(startVolume, endVolume, currentTime / changeTime);
            yield return null;
        }
    }
}
