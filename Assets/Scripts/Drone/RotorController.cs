using UnityEngine;

public class RotorController : MonoBehaviour
{
    public Animator[] rotors;
    public Rigidbody drone;
    void Start()
    {
        
    }

    void Update()
    {
        bool running = drone.position.y >= 50.1f;
        UpdateRotors(running);
    }

    private void UpdateRotors(bool state)
    {
        foreach (var rotor in rotors)
        {
            rotor.enabled = state;
        }
    }
}
