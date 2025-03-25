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

	[Header("Actions")]
	public static Action onShot;

	private void Awake()
	{
		PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;
		PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
		PlayerMovement.onDied += DiedCallback;
	}

	private void OnDestroy()
	{
		PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;
		PlayerMovement.onExitedWarzone -= ExitedWarzoneCallback;
		PlayerMovement.onDied -= DiedCallback;
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
		if (Input.GetMouseButtonDown(0) && UIBulletsContainer.instance.CanShot())
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

		onShot?.Invoke();
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

	private void DiedCallback()
	{
		canShot = false;
		SetShootingLineVisibility(false);
	}
}