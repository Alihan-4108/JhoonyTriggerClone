using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerDetection : MonoBehaviour
{
	[Header("Elements")]
	private PlayerMovement playerMovement;

	[Header("Settings")]
	[SerializeField] private float detectionRadius = 5f;

	private void Awake()
	{
		playerMovement = GetComponent<PlayerMovement>();
	}

	private void Update()
	{
		DetectStuff();
	}

	private void DetectStuff()
	{
		Collider[] detectedObjects = Physics.OverlapSphere(transform.position, detectionRadius);

		for (int i = 0; i < detectedObjects.Length; i++)
		{
			Collider detectedObject = detectedObjects[i];

			if (detectedObject.CompareTag("WarzoneEnter"))
			{
				EnteredWarzoneCallback(detectedObject);
			}
		}
	}

	private void EnteredWarzoneCallback(Collider warzoneTriggerCollider)
	{
		Warzone warzone = warzoneTriggerCollider.GetComponentInParent<Warzone>();
		playerMovement.EnteredWarzoneCallback(warzone);

	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, detectionRadius);
	}
}