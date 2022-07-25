using TMPro;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    [SerializeField]
    private string _levelName = "";
    [SerializeField]
    private TMP_Text _levelText = null;

    public void SetTheLevel(string levelName) 
    {
        _levelName = levelName;
        _levelText.text = levelName;
    }

    public void LoadLevel()
    {
        GameManager.instance.LoadScene(_levelName, true);
        GameManager.instance.ChangeGameState(GameManager.GameState.GameWait);
    }
}
