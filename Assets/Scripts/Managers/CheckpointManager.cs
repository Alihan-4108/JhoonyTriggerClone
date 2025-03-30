using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    [Header("Settings")]
    private Vector3 lastCheckpointPosition;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);

        Checkpoint.onInteracted += CheckpointInteractedCallback;
        GameManager.onGameStateChanged += GameStatedChangedCallback;
    }

    private void OnDestroy()
    {
        Checkpoint.onInteracted -= CheckpointInteractedCallback;
        GameManager.onGameStateChanged -= GameStatedChangedCallback;
    }

    private void GameStatedChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.LevelComplete:
                lastCheckpointPosition = Vector3.zero;
                break;
        }
    }

    private void CheckpointInteractedCallback(Checkpoint checkpoint)
    {
        lastCheckpointPosition = checkpoint.GetPosition();
    }

    public Vector3 GetLastCheckpointPosition()
    {
        return lastCheckpointPosition;
    }
}