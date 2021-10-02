using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
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
}
