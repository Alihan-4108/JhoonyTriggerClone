using System.Collections.Generic;
using UnityEngine;

public class PlayerEnemyTrigger : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private LineRenderer shootingLine;

	[Header("Settings")]
	[SerializeField] private LayerMask enemiesMask;
	private List<Enemy> currentEnemies = new List<Enemy>();
	private bool canCheckForShootingEnemies;

	private void Awake()
	{
		PlayerMovement.onEnteredWarzone += EnteredWarzoneCallback;
		PlayerMovement.onExitedWarzone += ExitedWarzoneCallback;
	}

	private void OnDestroy()
	{
		PlayerMovement.onEnteredWarzone -= EnteredWarzoneCallback;
		PlayerMovement.onEnteredWarzone -= ExitedWarzoneCallback;
	}

	private void Update()
	{
		if (canCheckForShootingEnemies)
			CheckForShootingEnemies();
	}

	private void EnteredWarzoneCallback()
	{
		canCheckForShootingEnemies = true;
	}

	private void ExitedWarzoneCallback()
	{
		canCheckForShootingEnemies = false;
	}

	private void CheckForShootingEnemies()
	{
		Vector3 rayOrigin = shootingLine.transform.TransformPoint(shootingLine.GetPosition(0));
		Vector3 worldSpaceSecondPoint = shootingLine.transform.TransformPoint(shootingLine.GetPosition(1));

		Vector3 rayDirection = (worldSpaceSecondPoint - rayOrigin).normalized;
		float maxDistance = Vector3.Distance(rayOrigin, worldSpaceSecondPoint);

		RaycastHit[] hits = Physics.RaycastAll(rayOrigin, rayDirection, maxDistance, enemiesMask);

		for (int i = 0; i < hits.Length; i++)
		{
			Enemy currentEnemy = hits[i].collider.GetComponent<Enemy>();

			if (!currentEnemies.Contains(currentEnemy))
			{
				currentEnemies.Add(currentEnemy);
			}
		}

		List<Enemy> enemiesToRemove = new List<Enemy>();

		foreach (Enemy enemy in currentEnemies)
		{
			bool enemyFound = false;

			for (int i = 0; i < hits.Length; i++)
			{
				if (hits[i].collider.GetComponent<Enemy>() == enemy)
				{
					enemyFound = true;
					break;
				}
			}

			if(!enemyFound)
			{
				enemy.ShootAtPlayer();
				enemiesToRemove.Add(enemy);
			}
		}

		foreach (Enemy enemy in enemiesToRemove)
			currentEnemies.Remove(enemy);
	}
}