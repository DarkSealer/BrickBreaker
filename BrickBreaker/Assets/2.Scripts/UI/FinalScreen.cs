using TMPro;
using UnityEngine;

public class FinalScreen : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _titleText = null;

    public void LoadMainMenu()
    {
        GameManager.instance.LoadScene("1.MainMenu");
    }

    public void UpdateTitle(string title)
    {
        _titleText.text = title;
    }
}
