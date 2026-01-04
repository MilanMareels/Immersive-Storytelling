using UnityEngine;

public class StartExTrigger : MonoBehaviour
{
    public DirectorScript directorScript;
    private bool _enabled = true;
    
    void Start()
    {
        directorScript = GameObject.FindAnyObjectByType<DirectorScript>();

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_enabled)
            return;
        if(other.tag == "Player")
           directorScript.NextState();
        _enabled = false;
    }

    public void ResetTrigger()
    {
        _enabled = true;
    }
}
