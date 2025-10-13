using UnityEngine;

public class TestScript : MonoBehaviour
{
    public SoundEffect soundEffect;
    void Start()
    {
        soundEffect.PlaySoundEffect(6, 5.0f);
    }
}
