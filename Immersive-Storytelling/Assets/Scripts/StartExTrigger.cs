using UnityEngine;

public class StartExTrigger : MonoBehaviour
{
    public DirectorScript directorScript;
    
    void Start()
    {
        directorScript = GameObject.FindAnyObjectByType<DirectorScript>();

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
           directorScript.NextState();
    }
}
