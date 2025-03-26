using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	private enum State { Alive, Dead }
	private State state;

	[Header("Elements")]
	private CharacterRagdoll characterRagdoll;
	private CharacterIK characterIK;
	private PlayerMovement playerMovement;
	private EnemyShooter enemyShooter;

	private void Start()
	{
		characterRagdoll = GetComponent<CharacterRagdoll>();
		characterIK = GetComponent<CharacterIK>();
		enemyShooter = GetComponent<EnemyShooter>();

		playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
		characterIK.ConfigureIK(playerMovement.GetEnemyTarget());
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

		enemyShooter.CancelInvoke("Shoot"); // Atış iptal ediliyor

		characterRagdoll.Ragdollify();
	}

	public void ShootAtPlayer()
	{
		enemyShooter.TryShooting();
	}

	public bool IsDead()
	{
		return state == State.Dead;
    }
}