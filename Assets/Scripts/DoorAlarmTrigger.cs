using UnityEngine;
using UnityEngine.Events;

public class DoorAlarmTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _alarm;

    private bool _isThiefInHouse = false;

    public bool IsThiefInHouse => _isThiefInHouse;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief) == true)
        {
            _isThiefInHouse = IsThiefInHouse == true ? false : true;
            _alarm?.Invoke();
        }
    }
}
