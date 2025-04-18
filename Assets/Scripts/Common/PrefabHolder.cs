using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabHolder : MonoBehaviour
{
    [SerializeField] GameObject coinPfb;
    public GameObject CoinPfb => coinPfb;
    [SerializeField] GameObject mushroomPfb;
    public GameObject MushroomPfb => mushroomPfb;
    [SerializeField] GameObject starPfb;
    public GameObject StarPfb => starPfb;
    [SerializeField] GameObject oneUpMushroomPfb;
    public GameObject OneUpMushroomPfb => oneUpMushroomPfb;
    [SerializeField] GameObject fireflowerPfb;
    public GameObject FireflowerPfb => fireflowerPfb;

    void Awake()
    {
        Locator<PrefabHolder>.Bind(this);
    }
}
