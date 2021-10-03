using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwiceTheSizePlayerEffect : PlayerEffectBase
{
	private const float DURATION = 5f;
	private PlayerEffectFactory _playerEffectFactory;
	private PlayerController _player;

	public TwiceTheSizePlayerEffect(PlayerEffectFactory playerEffectFactory, PlayerController player)
	{
		_playerEffectFactory = playerEffectFactory;
		_player = player;
	}

	public override void Do()
	{
		_player.ScaleUp();
		_playerEffectFactory.StartCoroutine(DelayedStop());
	}

	public override void Stop()
	{
		_player.ScaleDown();
	}

	private IEnumerator DelayedStop()
	{
		yield return new WaitForSeconds(DURATION);

		Stop();
	}
}
