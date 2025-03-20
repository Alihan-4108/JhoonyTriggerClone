using UnityEngine;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{
	enum State { Idle, Run, Warzone }

	[Header("Elements")]
	private PlayerAnimator playerAnimator;

	[Header("Settings")]
	[SerializeField] private float moveSpeed;
	[SerializeField] private float slowMoScale;
	private State state;
	private Warzone currentWarzone;

	[Header("Spline Settings")]
	private float warzoneTimer;

	private void Awake()
	{
		playerAnimator = GetComponent<PlayerAnimator>();
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

		playerAnimator.Play(warzone.GetAnimationToPlay(), warzone.GetAnimatorSpeed());

		Time.timeScale = slowMoScale;

		warzoneTimer = 0;

		print("Entered Warzone");
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
	}
}