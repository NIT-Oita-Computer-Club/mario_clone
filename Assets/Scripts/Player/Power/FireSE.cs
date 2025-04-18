using UnityEngine;

class FireSE : MonoBehaviour
{
    Fire fire;
    [SerializeField] AudioClip fireEmitSE;
    [SerializeField] AudioClip fireSparkSE;

    void Start()
    {
        fire = GetComponent<Fire>();
        fire.OnSpark += OnSpark;
        Locator<SEManager>.I.Play(fireEmitSE);
    }

    void Oestroy()
    {
        fire.OnSpark -= OnSpark;
    }

    void OnSpark()
    {
        Locator<SEManager>.I.Play(fireSparkSE);
    }
}