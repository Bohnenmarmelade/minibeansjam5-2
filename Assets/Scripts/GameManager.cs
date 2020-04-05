using System;
using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour {

    [SerializeField] private bool debug = false;
    [SerializeField] private String debugScene;
    void OnEnable()
    {
        EventManager.StartListening(Events.GAME_OVER, onGameOver);
        EventManager.StartListening(Events.START_GAME, onStartGame);
        EventManager.StartListening(Events.SHOW_TITLE, onShowTitleScreen);

        if (debug && debugScene != null) {
            SceneManager.LoadScene(debugScene);
        }
        else {
            SceneManager.LoadScene("TitleScene");
        }

    }

    void OnDisable()
    {
        EventManager.StopListening(Events.GAME_OVER, onGameOver);
    }
    
    public void onShowTitleScreen(string _)
    {
        EventManager.StopListening(Events.SHOW_TITLE, onShowTitleScreen);
        SceneManager.LoadScene("TitleScene");
    }

    private void onStartGame(string eventPayload){
        SceneManager.LoadScene("MainScene");
    }
    private void onGameOver(string gameOverPayload) {
        Debug.Log("GameOver Dude!!! " +  gameOverPayload);
        SceneManager.LoadScene("GameOverScene");
        //SceneManager.LoadScene("MainScene");
    }
}

