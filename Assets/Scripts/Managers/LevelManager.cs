using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	[SerializeField] private GameObject[] levels;
	private int levelIndex;

	private void Awake()
	{
		LoadData();

		SpawnLevel();

		GameManager.onGameStateChanged += GameStateChangedCallback;
	}

	private void OnDestroy()
	{
		GameManager.onGameStateChanged -= GameStateChangedCallback;
	}

	private void GameStateChangedCallback(GameState gameState)
	{
		switch (gameState)
		{
			case GameState.LevelComplete:
				levelIndex++;
				SaveData();
				break;
		}
	}

	private void SpawnLevel()
	{
		levelIndex = levelIndex % levels.Length;

		GameObject levelInstance = Instantiate(levels[levelIndex], transform);

		StartCoroutine(EnableLevelCoroutine());

		IEnumerator EnableLevelCoroutine()
		{
			yield return new WaitForSeconds(Time.deltaTime);
			levelInstance.SetActive(true);
		}
	}

	private void LoadData()
	{
		levelIndex = PlayerPrefs.GetInt("Level");
	}

	private void SaveData()
	{
		PlayerPrefs.SetInt("Level", levelIndex);
	}
}