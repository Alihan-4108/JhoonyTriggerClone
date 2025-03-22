using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private enum State { Alive, Dead }
	private State state;

	[Header("Elements")]
	private CharacterRagdoll characterRagdoll;

	private void Start()
	{
		characterRagdoll = GetComponent<CharacterRagdoll>();
	}

	public void TakeDamage()
	{
		if (state == State.Dead)
			return;

		Die();
	}

	private void Die()
	{
		state = State.Dead;

		characterRagdoll.Ragdollify();
	}
}