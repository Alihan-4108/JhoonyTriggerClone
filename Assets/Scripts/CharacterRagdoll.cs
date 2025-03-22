using UnityEngine;

public class CharacterRagdoll : MonoBehaviour
{
	[Header("Elements")]
	[SerializeField] private Animator animator;
	[SerializeField] private Collider mainCollider;
	[SerializeField] private Rigidbody[] rigidbodies;

	[Header("Elements")]
	[SerializeField] private float ragdollForce;

	private void Start()
	{
		for (int i = 0; i < rigidbodies.Length; i++)
		{
			rigidbodies[i].isKinematic = true;
		}
	}

	public void Ragdollify()
	{
		for (int i = 0; i < rigidbodies.Length; i++)
		{
			Rigidbody rig = rigidbodies[i];

			rig.isKinematic = false;

			rig.AddForce((Vector3.up + Random.insideUnitSphere) * ragdollForce);
		}

		animator.enabled = false;
		mainCollider.enabled = false;
	}
}