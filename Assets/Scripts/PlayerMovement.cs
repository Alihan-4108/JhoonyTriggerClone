using System;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{
	enum State { Idle, Run, Warzone }

	[Header("Elements")]
	private PlayerAnimator playerAnimator;
	private PlayerIK playerIK;

	[Header("Settings")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float slowMoScale;
	private State state;
	private Warzone currentWarzone;

	[Header("Actions")]
	public static Action onEnteredWarzone;
	public static Action onExitedWarzone;

	[Header("Spline Settings")]
	private float warzoneTimer;

	private void Awake()
	{
		playerAnimator = GetComponent<PlayerAnimator>();
		playerIK = GetComponent<PlayerIK>();
	}

	private void Start()
	{
		state = State.Idle;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
			StartRunning();

		ManageState();
	}

	private void ManageState()
	{
		switch (state)
		{
			case State.Idle:
				break;
			case State.Run:
				Move();
				break;
			case State.Warzone:
				ManageWarzoneState();
				break;
			default:
				break;
		}
	}

	private void StartRunning()
	{
		state = State.Run;
		playerAnimator.PlayRunAnimation();
	}

	private void Move()
	{
		transform.position += Vector3.right * moveSpeed * Time.deltaTime;
	}

	public void EnteredWarzoneCallback(Warzone warzone)
	{
		if (currentWarzone != null)
			return;

		state = State.Warzone;
		currentWarzone = warzone;

		currentWarzone.StartAnimatingIKTarget();

		warzoneTimer = 0;

		playerAnimator.Play(warzone.GetAnimationToPlay(), warzone.GetAnimatorSpeed());

		Time.timeScale = slowMoScale;

		playerIK.ConfigureIK(currentWarzone.GetIKTarget());

		onEnteredWarzone?.Invoke();
	}

	private void ManageWarzoneState()
	{
		warzoneTimer += Time.deltaTime;

		float splinePercent = warzoneTimer / currentWarzone.GetDuration();
		transform.position = currentWarzone.GetPlayerSpline().EvaluatePosition(splinePercent);

		if (splinePercent >= 1)
			ExitWarzone();
	}

	private void ExitWarzone()
	{
		currentWarzone = null;

		state = State.Run;
		playerAnimator.Play("Run", 1);

		Time.timeScale = 1;

		playerIK.DisableIK();

		onExitedWarzone?.Invoke();
	}
}