using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[Header("Settings")]
	private Vector3 velocity;
	[SerializeField] private float detectionRadius;

	private LayerMask enemiesMask;

	private void Awake()
	{
		enemiesMask = LayerMask.GetMask("Enemy");
	}

	private void Start()
	{
		Destroy(gameObject, 3f);
	}

	private void Update()
	{
		Move();

		CheckForEnemies();
	}

	private void Move()
	{
		transform.position += velocity * Time.deltaTime;
	}

	public void Configure(Vector3 velocity)
	{
		this.velocity = velocity;
	}

	private void CheckForEnemies()
	{
		Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRadius, enemiesMask);

		foreach (Collider enemyCollider in detectedObjects)
		{
			Destroy(enemyCollider.gameObject);
		}
	}
}