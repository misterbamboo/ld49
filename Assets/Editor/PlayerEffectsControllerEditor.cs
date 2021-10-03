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
		if (GUILayout.Button("Invert Controles"))
		{
			playerEffectsController.ApplyEffect(PlayerEffects.InvertedControls);
		}
	}
}
