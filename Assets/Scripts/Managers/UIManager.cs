using UnityEngine;

public class UIManager : MonoBehaviour
{
	[Header("Panels")]
	[SerializeField] private GameObject menuPanel;
	[SerializeField] private GameObject gamePanel;
	[SerializeField] private GameObject levelCompletePanel;
	[SerializeField] private GameObject gameOverPanel;

	private void Awake()
	{
		GameManager.onGameStateChanged += GameStatedChangedCallback;
	}

	private void OnDestroy()
	{
		GameManager.onGameStateChanged -= GameStatedChangedCallback;
	}

	private void Start()
	{

	}

	private void GameStatedChangedCallback(GameState gameState)
	{
		switch (gameState)
		{
			case GameState.Menu:
				menuPanel.SetActive(true);
				gamePanel.SetActive(false);
				levelCompletePanel.SetActive(false);
				gameOverPanel.SetActive(false);
				break;
			case GameState.Game:
				menuPanel.SetActive(false);
				gamePanel.SetActive(true);
				break;
			case GameState.LevelComplete:
				break;
			case GameState.GameOver:
				gamePanel.SetActive(false);
				gameOverPanel.SetActive(true);
				break;
			default:
				break;
		}
	}

	public void PlayButtonCallback()
	{
		GameManager.instance.SetGameState(GameState.Game);
	}

	public void RetryButtonCallback()
	{
		GameManager.instance.Retry();
	}
}