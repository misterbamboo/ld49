using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerConsumeChemical : MonoBehaviour
    {
        [SerializeField] private PlayerPickup _playerPickup;

        [SerializeField] private PlayerController _playerController;

        public void ToogleConsume()
        {
            ExplosionManager.ExploseAt(transform.position);

            var pickedUpObject = _playerPickup.PickedUpObject;
            if (pickedUpObject == null)
            {
                return;
            }

            var reaction = pickedUpObject.GetReaction();
            if (reaction != null)
            {
                _playerController.GiveEffect(reaction.GetPlayerEffect());

                // Remove PickableObject component
                Destroy(pickedUpObject);
                StartCoroutine(EatAnimation(pickedUpObject.gameObject));
            }
        }

        private IEnumerator EatAnimation(GameObject gameObject)
        {
            var transform = gameObject.transform;

            float timeUntilFinished = 1;
            while (timeUntilFinished > 0)
            {
                timeUntilFinished -= Time.deltaTime;

                transform.localScale = Vector3.one * timeUntilFinished;

                yield return 0;
            }

            Destroy(gameObject);
        }
    }
}
