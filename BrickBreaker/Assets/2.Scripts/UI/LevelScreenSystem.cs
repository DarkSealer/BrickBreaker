using UnityEngine;

public class LevelScreenSystem : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelPrefab = null;

    private void Start()
    {
        foreach (var levelName in GameManager.instance.LevelsName)
        {
            GameObject levelObj = Instantiate(_levelPrefab, this.transform);
            levelObj.GetComponent<LevelButton>().SetTheLevel(levelName);
        }
    }


}
