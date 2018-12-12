using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {

	public void LoadStartScene()
	{
		SceneManager.LoadScene(0);
	}
	
	public void LoadGameScene()
	{
		SceneManager.LoadScene("Game");
	}

	public void LoadGameOverScene()
	{
		SceneManager.LoadScene("Game Over");
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
