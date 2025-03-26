using System;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{
    enum State { Idle, Run, Warzone, Dead }

    [Header("Elements")]
    private PlayerAnimator playerAnimator;
    private CharacterIK playerIK;
    private CharacterRagdoll characterRagdoll;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float slowMoScale;
    [SerializeField] private Transform enemyTarget;
    private State state;
    private Warzone currentWarzone;

    [Header("Actions")]
    public static Action onEnteredWarzone;
    public static Action onExitedWarzone;
    public static Action onDied;

    [Header("Spline Settings")]
    private float warzoneTimer;

    private void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        playerIK = GetComponent<CharacterIK>();
        characterRagdoll = GetComponent<CharacterRagdoll>();

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void Start()
    {
        state = State.Idle;
    }

    private void Update()
    {
        if (GameManager.instance.IsGameState())
            ManageState();
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                StartRunning();
                break;
        }
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
        Time.fixedDeltaTime = slowMoScale / 50;

        playerIK.ConfigureIK(currentWarzone.GetIKTarget());

        onEnteredWarzone?.Invoke();
    }

    private void ManageWarzoneState()
    {
        warzoneTimer += Time.deltaTime;

        float splinePercent = Mathf.Clamp01(warzoneTimer / currentWarzone.GetDuration());
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
        Time.fixedDeltaTime = 1f / 50;

        playerIK.DisableIK();

        onExitedWarzone?.Invoke();
    }

    public Transform GetEnemyTarget()
    {
        return enemyTarget;
    }

    public void TakeDamage()
    {
        state = State.Dead;

        characterRagdoll.Ragdollify();

        Time.timeScale = 1;
        Time.fixedDeltaTime = 1f / 50;

        onDied?.Invoke();

        GameManager.instance.SetGameState(GameState.GameOver);
    }

    public void HitFinishLine()
    {
        state = State.Idle;
        playerAnimator.PlayIdleAnimation();

        GameManager.instance.SetGameState(GameState.LevelComplete);
    }
}