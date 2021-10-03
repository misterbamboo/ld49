using Assets.Chemicals;
using Assets.Scripts.Player;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public event Action onDied;
    [SerializeField] private float _movementSpeed = 7f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private float _maxTimeToLive = 20f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlayerPickup _playerPickup;
    [SerializeField] private PlayerInstrument _playerInstrument;
    [SerializeField] private PlayerFlower _playerFlower;
    [SerializeField] private PlayerConsumeChemical _playerConsumeChemical;

    private Vector3 _moveDirection;

    private Transform _cameraObject;
    private float _timeToLive;

    private bool _isExplosionMode = false;
    private Vector3 lastCheckExplosionMouvementPosition;

    public void Explose()
    {
        if (_isExplosionMode)
        {
            return;
        }

        _isExplosionMode = true;
        _rigidbody.constraints = RigidbodyConstraints.None;
        StartCoroutine(CheckExplosionMouvement());
    }

    private IEnumerator CheckExplosionMouvement()
    {
        lastCheckExplosionMouvementPosition = transform.position;
        bool stillMoving = true;
        while (stillMoving)
        {
            yield return new WaitForSeconds(0.5f);
            var previousPosition = lastCheckExplosionMouvementPosition;
            lastCheckExplosionMouvementPosition = transform.position;

            var distanceSinceLastCheck = (lastCheckExplosionMouvementPosition - previousPosition).magnitude;
            if (distanceSinceLastCheck < 0.2)
            {
                stillMoving = false;
            }
        }

        StartCoroutine(GetUpFromExplosion());
    }

    private IEnumerator GetUpFromExplosion()
    {
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        var targetForward = -transform.up;
        var targetRotation = Quaternion.LookRotation(targetForward);

        bool continuerGetUp = true;
        while (continuerGetUp)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            var angle = Quaternion.Angle(transform.rotation , targetRotation);
            print(angle);
            if (angle < 10)
            {
                transform.rotation = targetRotation;
                continuerGetUp = false;
            }

            yield return 0;
        }

        _isExplosionMode = false;
    }

    private void Awake()
    {
        _cameraObject = Camera.main.transform;
        _inputManager.OnPickupInput += OnPickupInputHandler;
        _inputManager.OnUseInput += OnUseInputHandler;
        _inputManager.OnConsume += OnConsumeInputHandler;

        _timeToLive = _maxTimeToLive;
    }

    private void Update()
    {
        if (!_isExplosionMode)
        {
            HandleMovement();
            HandleRotation();
        }

        if (!IsDead)
        {
            CheckLife();
        }
    }

    private void CheckLife()
    {
        ReduceTimeToLive();
        if (IsDead)
        {
            DieEvent();
        }
    }

    private void DieEvent()
    {
        onDied?.Invoke();
    }

    private void ReduceTimeToLive()
    {
        _timeToLive -= Time.deltaTime;
    }

    private bool IsDead => _timeToLive <= 0;

    private void HandleMovement()
    {
        _moveDirection = _cameraObject.forward * _inputManager.VerticalInput;
        _moveDirection = _moveDirection + _cameraObject.right * _inputManager.HorizontalInput;
        _moveDirection.Normalize();
        _moveDirection = _moveDirection * _movementSpeed;

        _moveDirection.y = _rigidbody.velocity.y;
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
        _playerFlower.ToggleInteraction();
        _playerPickup.TogglePickup();
    }

    private void OnUseInputHandler()
    {
        _playerFlower.ToggleInteraction();
        _playerInstrument.ToggleUse();
    }

    private void OnConsumeInputHandler()
    {
        _playerConsumeChemical.ToogleConsume();
    }

    public void GiveEffect(PlayerEffects playerEffects)
    {
        Debug.Log("Player consume : " + playerEffects);
    }
}
