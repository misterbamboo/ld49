using System;
using Assets.Chemicals;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{
	[SerializeField] private PlayerController _playerController;

	public void ApplyEffect(PlayerEffects effect)
	{
		PlayerEffectBase playerEffect = PlayerEffectFactory.Instance.GetEffect(effect, _playerController);
		playerEffect?.Do();
	}
}
