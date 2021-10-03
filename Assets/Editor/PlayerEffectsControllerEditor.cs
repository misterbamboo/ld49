using System.Collections;
using System.Collections.Generic;
using Assets.Chemicals;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerEffectsController))]
public class PlayerEffectsControllerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		PlayerEffectsController playerEffectsController = (PlayerEffectsController)target;
		if (GUILayout.Button("Invert Controls"))
		{
			playerEffectsController.ApplyEffect(PlayerEffects.InvertedControls);
		}
		else if (GUILayout.Button("Twice the Size"))
		{
			playerEffectsController.ApplyEffect(PlayerEffects.TwiceTheSize);
		}
		else if (GUILayout.Button("Faster"))
		{
			playerEffectsController.ApplyEffect(PlayerEffects.FasterSpeed);
		}
	}
}
