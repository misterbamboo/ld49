using Assets.Chemicals;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerFlower : MonoBehaviour
    {
        [SerializeField] private PlayerPickup _playerPickup;
        private Flower _flowerInRange = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Flower.Tag && other.TryGetComponent(out Flower flower))
            {
                _flowerInRange = flower;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == Flower.Tag && other.TryGetComponent(out Flower flower))
            {
                _flowerInRange = null;
            }
        }

        public void ToggleInteraction()
        {
            var pickedUpObject = _playerPickup.PickedUpObject;
            if (_flowerInRange == null || pickedUpObject == null)
            {
                return;
            }

            var reaction = pickedUpObject.GetReaction();
            if (reaction != null)
            {
                _flowerInRange.GiveEffect(reaction.GetFlowerEffect());

                // Remove PickableObject component
                Destroy(pickedUpObject);
                StartCoroutine(GiveFlowerAnimation(pickedUpObject.gameObject, _flowerInRange.transform.position));
            }
        }

        private IEnumerator GiveFlowerAnimation(GameObject gameObject, Vector3 flowerPosition)
        {
            // Throw curve
            // https://www.desmos.com/calculator/qsqtxxqasl
            var transform = gameObject.transform;
            var initialPos = transform.position;

            float timeUntilFinished = 1;
            while (timeUntilFinished > 0)
            {
                timeUntilFinished -= Time.deltaTime;
                var t = 1 - timeUntilFinished;

                var newPos = Vector3.Lerp(initialPos, flowerPosition, t);
                newPos.y = ThrowCurve(t);
                transform.position = newPos;

                yield return 0;
            }

            var chemicalBeakerGlass = gameObject.GetComponent<ChemicalBeakerGlass>();
            if (chemicalBeakerGlass != null)
            {
                chemicalBeakerGlass.Fill
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private float ThrowCurve(float t)
        {
            var beforeSquare = (1.4 * t - 0.4);
            var parabol = Math.Pow(beforeSquare, 2) * -1;
            var parabolWithMaxHeight = parabol + 1;
            return (float)parabolWithMaxHeight;
        }
    }
}
