using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabManager : MonoBehaviour
{
    public int sceneNr;

    public GameObject objectOne;
    void Start()
    {
        if (objectOne != null)
        {
            var grabInteractable = objectOne.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
                grabInteractable.selectEntered.AddListener(OnPlayerGrab);
        }
    }

    void OnPlayerGrab(SelectEnterEventArgs args) => SceneManager.LoadScene(sceneNr);
}
