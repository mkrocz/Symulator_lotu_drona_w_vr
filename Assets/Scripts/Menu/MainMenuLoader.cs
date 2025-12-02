using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLoader: MonoBehaviour
{

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
    }

    void OnEnable()
    {
        if (button != null)
            button.onClick.AddListener(OnButtonClicked);
    }

    void OnDisable()
    {
        button.onClick.RemoveListener(OnButtonClicked);
    }

    public void OnButtonClicked()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

}