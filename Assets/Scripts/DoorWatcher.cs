using UnityEngine;
using UnityEngine.Events;

public class DoorWatcher : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Thief thief))
        {
            _event?.Invoke();
        }
    }
}
