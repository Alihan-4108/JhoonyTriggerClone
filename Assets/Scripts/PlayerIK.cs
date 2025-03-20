using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerIK : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private RigBuilder rigBuilder;


	public void ConfigureIK()
	{
		rigBuilder.enabled = true;
	}

	public void DisableIK()
	{
		rigBuilder.enabled = false;
	}
}