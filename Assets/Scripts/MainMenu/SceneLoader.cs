using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    private string selectedScene;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        selectedScene = "Tutorial";
        dropdown.onValueChanged.AddListener(OnSceneSelected);
    }

    private void OnSceneSelected(int index)
    {
        if (index == 0)         // Samouczek
        {
            selectedScene = "Tutorial";
        }
        else if (index == 1)    // Las
        {
            selectedScene = "Forest";
        }
        else if (index == 2)    // Miasto
        {
            selectedScene = "City";
        }
    }

    public void OnButtonClicked()
    {
        StartGame();
    }

    public void StartGame()
    {

        SceneManager.LoadScene(selectedScene);
    }


    void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

}
