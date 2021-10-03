using Assets.Chemicals;
using UnityEngine;

public class PlayerEffectFactory : MonoBehaviour
{
	public static PlayerEffectFactory Instance => _instance;
	private static PlayerEffectFactory _instance;

	private void Awake()
	{
		_instance = this;
	}

	public PlayerEffectBase GetEffect(PlayerEffects effect, PlayerController player)
	{
		switch (effect)
		{
			case PlayerEffects.InvertedControls:
				return new InvertControlsPlayerEffect(this, player);
			case PlayerEffects.TwiceTheSize:
				break;
		}
		return null;
	}
}
