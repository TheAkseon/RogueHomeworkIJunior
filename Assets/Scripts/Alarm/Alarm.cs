using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();

    public event UnityAction OnReached
    {
        add => _reached.AddListener(value);
        remove => _reached.RemoveListener(value);
    }

    public bool IsReached { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            IsReached = true;
            _reached?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            IsReached = false;
            _reached?.Invoke();
        }
    }
}
