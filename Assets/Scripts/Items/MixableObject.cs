using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixableObject : MonoBehaviour
{
	private List<InstrumentDropZone> _instrumentDropZones = new List<InstrumentDropZone>();

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == "InstrumentDropZone" && other.TryGetComponent<InstrumentDropZone>(out InstrumentDropZone instrumentDropZone))
		{
			_instrumentDropZones.Add(instrumentDropZone);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "InstrumentDropZone" && other.TryGetComponent<InstrumentDropZone>(out InstrumentDropZone instrumentDropZone))
		{
			_instrumentDropZones.Remove(instrumentDropZone);
		}
	}

	public void Mix(PickableObject pickableObject)
	{
		InstrumentDropZone closestInstrumentDropZone = FindClosestInstrumentDropZone();
		if (closestInstrumentDropZone != null)
		{
			closestInstrumentDropZone.DropElement(pickableObject);
		}
	}

	private InstrumentDropZone FindClosestInstrumentDropZone()
	{
		InstrumentDropZone closestInstrumentDropZone = null;
		float minDist = Mathf.Infinity;

		foreach (InstrumentDropZone instrumentDropZone in _instrumentDropZones)
		{
			float dist = Vector3.Distance(instrumentDropZone.transform.position, transform.position);
			if (dist < minDist)
			{
				closestInstrumentDropZone = instrumentDropZone;
				minDist = dist;
			}
		}

		return closestInstrumentDropZone;
	}
}
