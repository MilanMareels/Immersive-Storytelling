using UnityEngine;

public class FloorScript : MonoBehaviour
{
    private Animator _Animator;

    void Start()
    {
        _Animator = gameObject.GetComponent<Animator>();
    }

    public void StartBreak()
    {
        _Animator.Play("floor_animation");
    }

    public void ReverseBreak()
    {
        _Animator.Play("floor_animation_reverse");
    }
}
