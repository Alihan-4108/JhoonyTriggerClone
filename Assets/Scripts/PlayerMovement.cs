using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	enum State { Idle, Run }

	[Header("Elements")]
	private PlayerAnimator playerAnimator;

	[Header("Settings")]
	[SerializeField] private float moveSpeed;
	private State state;

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
}