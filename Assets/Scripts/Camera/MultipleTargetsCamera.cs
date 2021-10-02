using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetsCamera : MonoBehaviour
{
	[SerializeField] private List<Transform> _targets;
	[SerializeField] private Vector3 _offset;
	[SerializeField] private float _closeFromTargetDistance = 1f;
	[SerializeField] private float _smoothTime = 0.5f;

	[SerializeField] private float _minZoom = 40f;
	[SerializeField] private float _maxZoom = 10f;
	[SerializeField] private float _zoomLimiter = 50f;
	[SerializeField] private Camera _camera;

	private Vector3 _currentVelocity;
	private float _baseZoom;

	private void Start()
	{
		_baseZoom = _camera.fieldOfView;
	}

	private void LateUpdate()
	{
		if (_targets.Count == 0)
			return;

		Move();
		Zoom();
	}

	private void Move()
	{
		Vector3 centerPoint = GetCenterPoint();
		Vector3 newPosition = centerPoint + _offset;
		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _currentVelocity, _smoothTime);
	}

	private void Zoom()
	{

		Vector3 centerPoint = GetCenterPoint();
		if (IsCloseFromTarget(centerPoint))
		{
			float newZoom = Mathf.Lerp(_maxZoom, _minZoom, GetGreatestDistance() / _zoomLimiter);
			_camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, newZoom, Time.deltaTime);
		}
		else
		{
			_camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _baseZoom, Time.deltaTime);
		}
	}

	private Vector3 GetCenterPoint()
	{
		if (_targets.Count == 1)
		{
			return _targets[0].position;
		}

		var bounds = new Bounds(_targets[0].position, Vector3.zero);
		for (int i = 0; i < _targets.Count; i++)
		{
			bounds.Encapsulate(_targets[i].position);
		}

		return bounds.center;
	}

	private float GetGreatestDistance()
	{
		var bounds = new Bounds(_targets[0].position, Vector3.zero);
		for (int i = 0; i < _targets.Count; i++)
		{
			bounds.Encapsulate(_targets[i].position);
		}

		return Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
	}

	private bool IsCloseFromTarget(Vector3 centerPoint)
	{
		float distance = Vector3.Distance(_targets[0].position, centerPoint);
		return distance <= _closeFromTargetDistance;
	}
}
