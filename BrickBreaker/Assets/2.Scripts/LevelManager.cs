using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText = null;
    [SerializeField]
    private TMP_Text _livesText = null;
    [SerializeField]
    private Button _launchBallBtn = null;
    [SerializeField]
    private int _nrOfBrickInScene = 0;
    [SerializeField]
    private FinalScreen _finalScreen = null;

    //public List<GameObject> BricksInScene { get => _bricksInScene; set => _bricksInScene = value; }

    public void OnEnable()
    {
        GameManager.instance.ScoreAction += UpdateScore;
        GameManager.instance.LivesAction += UpdateLives;
        _livesText.text = "Lives: " + GameManager.instance.PlayerCurrentLives;
    }

    private void Start()
    {
        if (_finalScreen == null)
        {
            Debug.LogError("You forgot to set the _finalScreen var");
        }

        var bricks = GameObject.FindObjectsOfType<Brick>();
        //_nrOfBrickInScene = bricks.Length;

        for (int i = 0; i < bricks.Length; i++)
        {   
            // avoid the undistractable bricks
            if (bricks[i].CanBeDestroyed)
            {
                _nrOfBrickInScene += 1;
            }
        }
    }

    private void UpdateScore()
    {
        _scoreText.text = "Score: " + GameManager.instance.CurrentScore;
        BrickDestroyed();
    }

    private void UpdateLives()
    {
        _livesText.text = "Lives: " + GameManager.instance.PlayerCurrentLives;
    }

    internal void CheckPlayerLives()
    {
        GameManager.instance.HasLives();

        // check the lives
        if (GameManager.instance.TheGameState == GameManager.GameState.GameOver)
        {
            _livesText.text = "Lives: 0";
            Debug.Log("Game Over");
            // TODO: display the GameOver panel
            _finalScreen.gameObject.SetActive(true);
            _finalScreen.UpdateTitle("GAME OVER");
        }
        else if (GameManager.instance.TheGameState == GameManager.GameState.GameWait)
        {
            //Debug.Log("continue playing");
            _livesText.text = "Lives: " + GameManager.instance.PlayerCurrentLives;
            _launchBallBtn.gameObject.SetActive(true);
        }
    }

    public void BrickDestroyed() 
    {
        _nrOfBrickInScene -= 1;

        if (_nrOfBrickInScene <= 0)
        {
            // display Win Screen
            Debug.Log("You Won!");

            _finalScreen.gameObject.SetActive(true);
            _finalScreen.UpdateTitle("YOU WON!");
            GameManager.instance.ChangeGameState(GameManager.GameState.GameWon);
        }
    }


    private void OnDestroy()
    {
        //GameManager.instance.ScoreAction -= UpdateScore;
    }

    /*public void LoadNextLevel()
    {
        GameManager.instance.ScoreAction -= UpdateScore;
        GameManager.instance.ChangeGameState(GameManager.GameState.GameWait);

    }*/

    public void LoadMainMenu()
    {
        GameManager.instance.ScoreAction -= UpdateScore;
        GameManager.instance.ChangeGameState(GameManager.GameState.InMenu);
        GameManager.instance.LoadScene(0);
    }
}
