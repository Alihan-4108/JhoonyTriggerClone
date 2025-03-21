using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private GameObject shootingLine;

	private void Awake()
	{
		PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;
		PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
	}

	private void OnDestroy()
	{
		PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;
		PlayerMovement.onExitedWarzone -= ExitedWarzoneCallback;
	}

	private void Start()
	{
		SetShootingLineVisibility(false);
	}

	private void EnteredWarzoneCallback()
	{
		SetShootingLineVisibility(true);
	}


	private void ExitedWarzoneCallback()
	{
		SetShootingLineVisibility(false);
	}

	private void SetShootingLineVisibility(bool visibility)
	{
		shootingLine.SetActive(visibility);
	}
}