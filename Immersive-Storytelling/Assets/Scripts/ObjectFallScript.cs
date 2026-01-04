using UnityEngine;

public class ObjectFallScript : MonoBehaviour
{
    private Animator _Animator;

    void Start()
    {
        _Animator = gameObject.GetComponent<Animator>();
    }

    public void StartFall()
    {
        _Animator.Play("objects_fall");
    }

    public void ReverseFall()
    {
        _Animator.Play("objects_fall_reverse");
    }
}
