﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
	{
		if(SceneManager.GetActiveScene().name.Contains("Level"))
		    FindObjectOfType<GameSession>().StopTimers();
		var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex + 1);
    }

	public void LoadStartScene()
	{
		SceneManager.LoadScene(0);
		FindObjectOfType<GameSession>().ResetGame();
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
