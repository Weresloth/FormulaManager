using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {

	
	void Start () {
		
	}
	

    public void QuitGame()
    {

        Application.Quit();

    }

    public void OpenTutorial()
    {

        SceneManager.LoadScene("tutorialAbout", LoadSceneMode.Single);


    }


    public void OpenTeamSelect()
    {

        SceneManager.LoadScene("teamSelect", LoadSceneMode.Single);

    }

	
    public void OpenMainMenu()
    {

        SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);

    }

    public void GotoRace()
    {

        SceneManager.LoadScene("race3", LoadSceneMode.Single);

    }
}
