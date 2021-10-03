using System;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public const string Tag = "PickableObject";

    public event Action OnPickup;
    public event Action OnDrop;

    [SerializeField] private Rigidbody _rigibbody;
    [SerializeField] private SnappableObject _snappableObject;
    [SerializeField] private MixableObject _mixableObject;
    public bool IsPickedUp => _isPickedUp;
    private bool _isPickedUp;

    private bool _availableForPickup = true;
    public bool IsAvailableForPickup => _availableForPickup;

    public void SetAvailableForPickup(bool availableForPickup)
    {
        _availableForPickup = availableForPickup;
    }

    public void Pickup(Transform newParent)
    {
        if (!_isPickedUp)
        {
            _isPickedUp = true;

            _rigibbody.isKinematic = true;
            _rigibbody.useGravity = false;

            transform.position = newParent.position;
            transform.SetParent(newParent);

            if (_snappableObject != null)
            {
                _snappableObject.Unsnap();
            }

            OnPickup?.Invoke();
        }
    }

    public void Drop()
    {
        if (_isPickedUp)
        {
            _isPickedUp = false;

            _rigibbody.isKinematic = false;
            _rigibbody.useGravity = true;

            transform.parent = null;
            if (_mixableObject != null)
            {
                _mixableObject.Mix(this);
            }
            if (_snappableObject != null)
            {
                _snappableObject.Snap();
            }

            OnDrop?.Invoke();
        }
    }
}
