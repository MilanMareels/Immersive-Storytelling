using UnityEngine;

public class FloorScript : MonoBehaviour
{
    private Animator _Animator;

    void Start()
    {
        _Animator = gameObject.GetComponent<Animator>();
        FindFirstObjectByType<DirectorScript>().breakFloorDelegate += StartBreak;
    }

    void StartBreak()
    {
        _Animator.Play("floor_animation");
    }
}
