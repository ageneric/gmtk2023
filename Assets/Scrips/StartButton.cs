using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
	public void NewGameButton() {
        SceneManager.LoadScene("SampleScene");
    }
}