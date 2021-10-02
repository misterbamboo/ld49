using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _movementSpeed = 7f;
	[SerializeField] private float _rotationSpeed = 15f;
	[SerializeField] private Rigidbody _rigidbody;
	[SerializeField] private InputManager _inputManager;
	[SerializeField] private PlayerPickup _playerPickup;

	private Vector3 _moveDirection;
	private Transform _cameraObject;

	private void Awake()
	{
		_cameraObject = Camera.main.transform;
		_inputManager.OnPickupInput += OnPickupInputHandler;
	}

	private void Update()
	{
		HandleMovement();
		HandleRotation();
	}

	private void HandleMovement()
	{
		_moveDirection = _cameraObject.forward * _inputManager.VerticalInput;
		_moveDirection = _moveDirection + _cameraObject.right * _inputManager.HorizontalInput;
		_moveDirection.Normalize();
		_moveDirection.y = 0;
		_moveDirection = _moveDirection * _movementSpeed;

		_rigidbody.velocity = _moveDirection;
	}

	private void HandleRotation()
	{
		Vector3 targetDirection = Vector3.zero;

		targetDirection = _cameraObject.forward * _inputManager.VerticalInput;
		targetDirection = targetDirection + _cameraObject.right * _inputManager.HorizontalInput;
		targetDirection.Normalize();
		targetDirection.y = 0;

		if (targetDirection == Vector3.zero)
			targetDirection = transform.forward;

		Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
		Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

		transform.rotation = playerRotation;
	}

	private void OnPickupInputHandler()
	{
		_playerPickup.TogglePickup();
	}
}