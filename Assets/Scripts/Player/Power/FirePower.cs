using UnityEngine;

class FirePower: MonoBehaviour
{
    [SerializeField] InputReader input;
    static GameObject coinPfb;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(input.RetrieveDashInput(thisFrame: true))
        {

        }
    }
}