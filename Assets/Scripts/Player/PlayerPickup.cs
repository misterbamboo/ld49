using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
	[SerializeField] private Transform _pickableObjectParent;
	private List<PickableObject> _pickableObjects = new List<PickableObject>();
	private PickableObject _pickedUpObject = null;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "PickableObject" && other.TryGetComponent<PickableObject>(out PickableObject pickableObject))
		{
			_pickableObjects.Add(pickableObject);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "PickableObject" && other.TryGetComponent<PickableObject>(out PickableObject pickableObject))
		{
			_pickableObjects.Remove(pickableObject);
		}
	}

	public void TogglePickup()
	{
		if (_pickedUpObject != null)
		{
			_pickedUpObject.Drop();
			_pickableObjects.Remove(_pickedUpObject);
			_pickedUpObject = null;
			return;
		}

		PickableObject closestPickableObject = FindClosestPickableObject();
		if (closestPickableObject != null)
		{
			closestPickableObject.Pickup(_pickableObjectParent);
			_pickedUpObject = closestPickableObject;
		}
	}

	private PickableObject FindClosestPickableObject()
	{
		PickableObject closestPickableObject = null;
		float minDist = Mathf.Infinity;

		List<PickableObject> objectsToRemoved = new List<PickableObject>();

		foreach (PickableObject pickableObject in _pickableObjects)
		{
			if (pickableObject == null)
			{
				objectsToRemoved.Add(pickableObject);
				continue;
			}
			float dist = Vector3.Distance(pickableObject.transform.position, transform.position);
			if (dist < minDist)
			{
				closestPickableObject = pickableObject;
				minDist = dist;
			}
		}

		foreach (PickableObject pickableObject in objectsToRemoved)
		{
			_pickableObjects.Remove(pickableObject);
		}

		return closestPickableObject;
	}
}
