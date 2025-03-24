using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
	[Header("Settings")]
	private Vector3 velocity;
	[SerializeField] private float detectionRadius;

	private LayerMask playerMask;

	private void Start()
	{
		playerMask = LayerMask.GetMask("Player");
	}

	private void Update()
	{
		Move();
		CheckForPlayer();
	}

	private void Move()
	{
		transform.position += velocity * Time.deltaTime;
	}

	public void Configure(Vector3 velocity)
	{
		this.velocity = velocity;
	}

	private void CheckForPlayer()
	{
		Collider[] detectedPlayer = Physics.OverlapSphere(transform.position, detectionRadius, playerMask);

		foreach (Collider playerCollider in detectedPlayer)
		{
			playerCollider.GetComponent<PlayerMovement>().TakeDamage();
		}
	}
}