using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private Bullet bulletPrefab;
	[SerializeField] private GameObject shootingLine;
	[SerializeField] private Transform bulletSpawnPosition;
	[SerializeField] private Transform bulletsParent;

	[Header("Settings")]
	[SerializeField] private float bulletSpeed;
	private bool canShot;

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

	private void Update()
	{
		if (canShot)
		{
			ManageShooting();
		}

	}

	private void ManageShooting()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Shot();
		}
	}

	private void Shot()
	{
		Vector3 direction = bulletSpawnPosition.right;
		direction.z = 0f;

		Bullet bulletInstance = Instantiate(bulletPrefab, bulletSpawnPosition.position, Quaternion.identity, bulletsParent);

		bulletInstance.Configure(direction * bulletSpeed);
	}

	private void EnteredWarzoneCallback()
	{
		canShot = true;
		SetShootingLineVisibility(true);
	}

	private void ExitedWarzoneCallback()
	{
		canShot = false;
		SetShootingLineVisibility(false);
	}

	private void SetShootingLineVisibility(bool visibility)
	{
		shootingLine.SetActive(visibility);
	}
}