using UnityEngine; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.StartListening(Events.GAME_OVER, onGameOver);
        EventManager.StartListening(Events.START_GAME, onStartGame);
        EventManager.StartListening(Events.SHOW_TITLE, onShowTitleScreen);
        SceneManager.LoadScene("MainScene");
    }

    void OnDisable()
    {
        EventManager.StopListening(Events.GAME_OVER, onGameOver);
    }
    
    public void onShowTitleScreen(string _)
    {
        EventManager.StopListening(Events.SHOW_TITLE, onShowTitleScreen);
        SceneManager.LoadScene("TitleScreen");
    }

    private void onStartGame(string eventPayload){
        SceneManager.LoadScene("GameOverScene");
        //SceneManager.LoadScene("MainScene");
    }
    private void onGameOver(string gameOverPayload) {
        Debug.Log("GameOver Dude!!! " +  gameOverPayload);
        SceneManager.LoadScene("GameOverScene");
        //SceneManager.LoadScene("MainScene");
    }
}

