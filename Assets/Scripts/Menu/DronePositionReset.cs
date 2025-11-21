using UnityEngine;
using UnityEngine.UI;

public class DronePositionReset : MonoBehaviour
{
    public GameObject drone;
    public Vector3 startPosition = new Vector3(2f, 51f, 0f);
    public Vector3 startRotation = new Vector3(0f, 0f, 0f);

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
        drone.transform.position = startPosition;
        drone.transform.rotation = Quaternion.Euler(startRotation);
    }

}