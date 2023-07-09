using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject tutorial;
    public GameObject credits;

    private void Start()
    {
        menu.SetActive(true);
        tutorial.SetActive(false);
        credits.SetActive(false);
    }
    public void showTutorial()
    {
        menu.SetActive(false);
        tutorial.SetActive(true);
    }

    public void hideTutorial()
    {
        menu.SetActive(true);
        tutorial.SetActive(false);
    }

    public void showCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
    }

    public void hideCredits()
    {
        menu.SetActive(true);
        credits.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
