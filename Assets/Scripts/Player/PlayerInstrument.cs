using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstrument : MonoBehaviour
{
	private List<InstrumentBase> _usableInstruments = new List<InstrumentBase>();
	private InstrumentBase _usingInstrument = null;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Instrument" && other.TryGetComponent<InstrumentBase>(out InstrumentBase instrument))
		{
			_usableInstruments.Add(instrument);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Instrument" && other.TryGetComponent<InstrumentBase>(out InstrumentBase instrument))
		{
			_usableInstruments.Remove(instrument);
			if (instrument == _usingInstrument)
			{
				StopUsingInstrument();
			}
		}
	}

	public void ToggleUse()
	{
		if (_usingInstrument != null)
		{
			StopUsingInstrument();
			return;
		}

		InstrumentBase closestUsableInstrument = FindClosestUsableObject();
		if (closestUsableInstrument != null)
		{
			closestUsableInstrument.Use();
			_usingInstrument = closestUsableInstrument;
		}
	}

	private InstrumentBase FindClosestUsableObject()
	{
		InstrumentBase closestUsableInstrument = null;
		float minDist = Mathf.Infinity;

		List<InstrumentBase> instrumentsToRemoved = new List<InstrumentBase>();

		foreach (InstrumentBase pickableObject in _usableInstruments)
		{
			if (pickableObject == null)
			{
				instrumentsToRemoved.Add(pickableObject);
				continue;
			}
			float dist = Vector3.Distance(pickableObject.transform.position, transform.position);
			if (dist < minDist)
			{
				closestUsableInstrument = pickableObject;
				minDist = dist;
			}
		}

		foreach (InstrumentBase usableInstrument in instrumentsToRemoved)
		{
			_usableInstruments.Remove(usableInstrument);
		}

		return closestUsableInstrument;
	}

	private void StopUsingInstrument()
	{
		_usingInstrument.StopUsing();
		_usingInstrument = null;
	}
}
