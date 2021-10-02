using UnityEngine;

public class PickableObject : MonoBehaviour
{
	[SerializeField] private Rigidbody _rigibbody;
	private bool _isPickedUp;

	public void Pickup(Transform newParent)
	{
		if (!_isPickedUp)
		{
			_isPickedUp = true;

			_rigibbody.isKinematic = true;
			_rigibbody.useGravity = false;

			transform.position = newParent.position;
			transform.SetParent(newParent);
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
		}
	}
}