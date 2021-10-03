using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private int ExplosionRadius = 8;

    [SerializeField] private int ExplosionForce = 500;

    [SerializeField] private int UpwardsModifier = 200;

    public static ExplosionManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public static void ExploseAt(Vector3 explosionPosition)
    {
        Instance.CheckIfPlayerAffected(explosionPosition);
        Instance.ApplyForceToRigidBodyArround(explosionPosition);
    }

    private void ApplyForceToRigidBodyArround(Vector3 explosionPosition)
    {
        var rigidbodies = FindObjectsOfType<Rigidbody>();
        foreach (var rb in rigidbodies)
        {
            rb.AddExplosionForce(ExplosionForce, explosionPosition, ExplosionRadius, UpwardsModifier, ForceMode.Force);
        }
    }

    private void CheckIfPlayerAffected(Vector3 explosionPosition)
    {
        if (PlayerIsAffected(explosionPosition))
        {
            _playerController.Explose();
        }
    }

    private bool PlayerIsAffected(Vector3 explosionPosition)
    {
        var playerPos = _playerController.transform.position;
        var distance = (explosionPosition - playerPos).magnitude;
        if (distance < ExplosionRadius)
        {
            return true;
        }

        return false;
    }
}
