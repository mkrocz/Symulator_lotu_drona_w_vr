using UnityEngine;

public class RotorController : MonoBehaviour
{
    public Animator[] rotors;
    public Rigidbody drone;
    void Start()
    {
        UpdateRotors(true);
    }


    private void UpdateRotors(bool state)
    {
        foreach (var rotor in rotors)
        {
            rotor.enabled = state;
        }
    }
}
