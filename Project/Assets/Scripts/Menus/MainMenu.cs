using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void PressSolo() {
        SceneManager.LoadScene("MapSelection");
    }

    public void PressMultiplayer() {
        SceneManager.LoadScene("MultiplayerSelection");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
