using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowerPlayerEffect : PlayerEffectBase
{
	private const float DURATION = 5f;
	private PlayerEffectFactory _playerEffectFactory;
	private PlayerController _player;
	private float _startingSpeed;

	public SlowerPlayerEffect(PlayerEffectFactory playerEffectFactory, PlayerController player)
	{
		_playerEffectFactory = playerEffectFactory;
		_player = player;
		_startingSpeed = player.MovementSpeed;
	}

	public override void Do()
	{
		_player.MovementSpeed /= 1.8f;
		_playerEffectFactory.StartCoroutine(DelayedStop());
	}

	public override void Stop()
	{
		_player.MovementSpeed = _startingSpeed;
	}

	private IEnumerator DelayedStop()
	{
		yield return new WaitForSeconds(DURATION);

		Stop();
	}
}
