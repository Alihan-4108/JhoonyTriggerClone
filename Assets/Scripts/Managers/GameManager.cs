using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState { Menu, Game, LevelComplete, GameOver }

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	[Header("Settings")]
	private GameState gameState;

	[Header("Actions")]
	public static Action<GameState> onGameStateChanged;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
			Destroy(gameObject);
	}

	private void Start()
	{
		SetGameState(GameState.Menu);
	}

	public void SetGameState(GameState gameState)
	{
		this.gameState = gameState;
		onGameStateChanged?.Invoke(gameState);
	}

	public void Retry()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}