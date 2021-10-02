using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
	public event Action OnPickupInput;
	public event Action OnUseInput;
	public float HorizontalInput => _horizontalInput;
	private float _horizontalInput = 0f;
	public float VerticalInput => _verticalInput;
	private float _verticalInput = 0f;

	[SerializeField] private PlayerInputs _playerInputs;

	private Vector2 _movementInput;

	private void OnEnable()
	{
		if (_playerInputs == null)
		{
			_playerInputs = new PlayerInputs();

			_playerInputs.Player.Movement.performed += OnMovementInputHandler;
			_playerInputs.Player.Pickup.performed += OnPickupInputHandler;
			_playerInputs.Player.Use.performed += OnUseInputHandler;
		}

		_playerInputs.Enable();
	}

	private void OnDisable()
	{
		_playerInputs.Disable();
	}

	public void OnMovementInputHandler(CallbackContext ctx)
	{
		_movementInput = ctx.ReadValue<Vector2>();

		_horizontalInput = _movementInput.x;
		_verticalInput = _movementInput.y;
	}

	public void OnPickupInputHandler(CallbackContext context)
	{
		OnPickupInput?.Invoke();
	}

	public void OnUseInputHandler(CallbackContext context)
	{
		OnUseInput?.Invoke();
	}
}
