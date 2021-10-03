using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertControlsPlayerEffect : PlayerEffectBase
{
	private const float DURATION = 5f;
	private PlayerEffectFactory _playerEffectFactory;
	private PlayerController _player;

	public InvertControlsPlayerEffect(PlayerEffectFactory playerEffectFactory, PlayerController player)
	{
		_playerEffectFactory = playerEffectFactory;
		_player = player;
	}

	public override void Do()
	{
		_player.InvertControls = true;
		_playerEffectFactory.StartCoroutine(DelayedStop());
	}

	public override void Stop()
	{
		_player.InvertControls = false;
	}

	private IEnumerator DelayedStop()
	{
		yield return new WaitForSeconds(DURATION);

		Stop();
	}
}
