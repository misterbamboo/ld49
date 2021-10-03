using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class MonoBehaviourExtention
{
    public static T FindClosest<T>(this List<T> elements, Vector3 position) where T : MonoBehaviour
    {
        T closest = null;
        float minDist = Mathf.Infinity;

        var elementsToRemoved = new List<T>();

        foreach (T element in elements)
        {
            if (element == null)
            {
                elementsToRemoved.Add(element);
                continue;
            }
            float dist = Vector3.Distance(element.transform.position, position);
            if (dist < minDist)
            {
                closest = element;
                minDist = dist;
            }
        }

        foreach (T elementToRemoved in elementsToRemoved)
        {
            elements.Remove(elementToRemoved);
        }

        return closest;
    }
}