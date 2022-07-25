using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public enum GameState
    {
        InMenu,
        GameStarted,
        GameWon,
        GameOver,
        GameWait,
    }

    #region Levels
    [Header("Levels")]
    [SerializeField]
    private string[] _levelsName = new string[2];
    #endregion

    [Header("Game Settings")]
    [SerializeField]
    private int _playerLives = 3;
    [SerializeField]
    private GameObject[] _powerUps = new GameObject[1];
    [SerializeField]
    private int _chancesForBonusLife = 10;

    private GameState _theGameState = GameState.InMenu;
    private int _playerCurrentLives = 0;
    private int _currentScore = 0;
    private UnityAction _scoreAction;
    private UnityAction _livesAction;
    private int _currentSceneId = 0;
    private int _nrOfScenes = 0;

    public GameState TheGameState { get => _theGameState; }
    public int CurrentScore { get => _currentScore; }
    public UnityAction ScoreAction { get => _scoreAction; set => _scoreAction = value; }
    public UnityAction LivesAction { get => _livesAction; set => _livesAction = value; }
    public int PlayerCurrentLives { get => _playerCurrentLives; }
    public GameObject[] PowerUps { get => _powerUps; }
    public int ChancesForBonusLife { get => _chancesForBonusLife; }
    public string[] LevelsName { get => _levelsName; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }

        _nrOfScenes = SceneManager.sceneCountInBuildSettings;
    }

    public void ChangeGameState(GameState state)
    {
        _theGameState = state;
    }

    public void HasLives()
    {
        _playerCurrentLives -= 1;
        if (_playerCurrentLives <= 0)
        {
            _theGameState = GameState.GameOver;
            return;
        }

        _theGameState = GameState.GameWait;
    }

    public void UpdateScore(int brickScore)
    {
        _currentScore += brickScore;
        _scoreAction.Invoke();
    }

    public void IncreaseHealthByOne()
    {
        _playerCurrentLives += 1;
        _livesAction.Invoke();
    }

    #region Scene Management
    public void LoadScene(string sceneName, bool reset = false)
    {
        if (reset)
        {
            ResetGame();
        }

        SceneManager.LoadScene(sceneName);
        _currentSceneId = SceneManager.GetSceneByName(sceneName).buildIndex;
    }

    public void LoadScene(int sceneId, bool reset = false)
    {
        if (reset)
        {
            ResetGame();
        }

        SceneManager.LoadScene(sceneId);
        _currentSceneId = sceneId;
    }

    public void ResetGame()
    {
        _playerCurrentLives = _playerLives;
        _currentScore = 0;
    }
    #endregion
}
