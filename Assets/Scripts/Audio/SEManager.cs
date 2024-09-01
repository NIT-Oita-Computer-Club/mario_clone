using UnityEngine;

public class SEManager: MonoBehaviour
{
    public AudioSource AudioSource { get; private set; }

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        Locator<SEManager>.Bind(this);
    }

    private void OnDestroy()
    {
        Locator<SEManager>.Unbind(this);
    }

    public void Play(AudioClip clip)
    {
        AudioSource.PlayOneShot(clip);
    }
}