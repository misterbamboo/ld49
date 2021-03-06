using System.Collections.Generic;
using UnityEngine;

public class SnappableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibbody;
    [SerializeField] private Vector3 _snapOffset;
    private List<SnappableSurface> _snappableSurfaces = new List<SnappableSurface>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == SnappableSurface.Tag && other.TryGetComponent(out SnappableSurface snappableSurface))
        {
            if (!_snappableSurfaces.Contains(snappableSurface))
                _snappableSurfaces.Add(snappableSurface);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == SnappableSurface.Tag && other.TryGetComponent(out SnappableSurface snappableSurface))
        {
            if (_snappableSurfaces.Contains(snappableSurface))
                _snappableSurfaces.Remove(snappableSurface);
        }
    }

    public void Snap()
    {
        SnappableSurface closestSnappableSurface = FindClosestSnappableSurface();
        if (closestSnappableSurface == null)
            return;

        _rigibbody.isKinematic = true;
        _rigibbody.useGravity = false;

        transform.rotation = Quaternion.identity;
        transform.position = closestSnappableSurface.transform.position + _snapOffset;

        if (TryGetComponent<CookingPot>(out CookingPot cookingPot))
        {
            cookingPot.Use();
        }
    }

    public void Unsnap()
    {
        if (TryGetComponent<CookingPot>(out CookingPot cookingPot))
        {
            cookingPot.StopUsing();
        }
    }

    private SnappableSurface FindClosestSnappableSurface()
    {
        SnappableSurface closestSnappableSurface = null;
        float minDist = Mathf.Infinity;

        foreach (SnappableSurface snappableSurface in _snappableSurfaces)
        {
            float dist = Vector3.Distance(snappableSurface.transform.position, transform.position);
            if (dist < minDist)
            {
                closestSnappableSurface = snappableSurface;
                minDist = dist;
            }
        }

        return closestSnappableSurface;
    }
}
