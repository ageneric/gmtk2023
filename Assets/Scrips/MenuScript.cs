using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    public GameObject tutorial;

    private void Start()
    {
        menu.SetActive(true);
        tutorial.SetActive(false);
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
}
