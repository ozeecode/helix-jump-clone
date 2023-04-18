using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState GameState;

    [SerializeField] CanvasGroup gameoverCanvas;
    [SerializeField] CanvasGroup winCanvas;
    [SerializeField] Ball ball;
    [SerializeField] Platform platform;
    [SerializeField] CameraFollow cameraFollow;

    private void Awake()
    {
        Instance = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Restart();
    }
    public void GameOver()
    {
        GameState = GameState.Fail;
        gameoverCanvas.alpha = 1;
        gameoverCanvas.blocksRaycasts = true;
        gameoverCanvas.interactable = true;
    }

    public void LevelPass()
    {
        GameState = GameState.Win;
        winCanvas.alpha = 1;
        winCanvas.blocksRaycasts = true;
        winCanvas.interactable = true;
    }

    public void Restart()
    {
        GameState = GameState.Play;
        winCanvas.alpha = 0;
        winCanvas.blocksRaycasts = false;
        winCanvas.interactable = false;

        gameoverCanvas.alpha = 0;
        gameoverCanvas.blocksRaycasts = false;
        gameoverCanvas.interactable = false;
        platform.Init();
        ball.Restart();
        cameraFollow.CurrentY = 0;
    }
}

public enum GameState
{
    Play,
    Fail,
    Win
}