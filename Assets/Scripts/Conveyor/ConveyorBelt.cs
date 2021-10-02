using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
	[SerializeField] private Transform _endpoint;
	[SerializeField] private float _speed = 3f;

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "PickableObject" && other.TryGetComponent<PickableObject>(out PickableObject pickableObject) && !pickableObject.IsPickedUp)
		{
			other.transform.position = Vector3.MoveTowards(other.transform.position, _endpoint.position, _speed * Time.deltaTime);
		}
	}
}
