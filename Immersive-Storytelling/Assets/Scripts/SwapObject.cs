using UnityEngine;

public class SwapObject : MonoBehaviour
{
    public GameObject cubeVersion;
    public GameObject treeVersion;
    public float switchInterval = 2f;
    public float transitionTime = 1f;
    public float startScale = 0.2f; // nieuwe variabele!

    private bool isTree = false;
    private bool isTransitioning = false;

    private void Start()
    {
        InvokeRepeating("StartSwap", switchInterval, switchInterval);
    }

    void StartSwap()
    {
        if (!isTransitioning)
            StartCoroutine(SmoothSwap());
    }

    System.Collections.IEnumerator SmoothSwap()
    {
        isTransitioning = true;

        GameObject toHide = isTree ? treeVersion : cubeVersion;
        GameObject toShow = isTree ? cubeVersion : treeVersion;

        // Nieuwe startScale voor groeiende object
        Vector3 startScaleShow = new Vector3(startScale, startScale, startScale);
        Vector3 endScaleShow = Vector3.one;

        // Verbergen-animatie
        Vector3 startScaleHide = Vector3.one;
        Vector3 endScaleHide = startScaleShow;

        // Voorbereiden: show-object actief en klein maar zichtbaar
        toShow.SetActive(true);
        toShow.transform.localScale = startScaleShow;

        float t = 0;

        while (t < transitionTime)
        {
            t += Time.deltaTime;
            float progress = t / transitionTime;

            // Verkleinen van het oude object
            toHide.transform.localScale = Vector3.Lerp(startScaleHide, endScaleHide, progress);

            // Groeien van het nieuwe object
            toShow.transform.localScale = Vector3.Lerp(startScaleShow, endScaleShow, progress);

            yield return null;
        }

        // Verbergen
        toHide.SetActive(false);

        // Reset voor volgende ronde
        toHide.transform.localScale = Vector3.one;

        isTree = !isTree;
        isTransitioning = false;
    }
}
