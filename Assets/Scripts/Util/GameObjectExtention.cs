using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetLayer(this GameObject gameObject, int layerNum, bool setChildren = true)
    {
        gameObject.layer = layerNum;
        if (!setChildren) return;
        foreach (Transform tfm in gameObject.transform)
        {
            SetLayer(tfm.gameObject, layerNum, setChildren);
        }
    }
}