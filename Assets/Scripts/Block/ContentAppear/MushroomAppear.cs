using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ItemAppear))]
class MushroomAppear : MonoBehaviour
{
    [SerializeField] ReboundMovement movement;
    [SerializeField] GameObject wallDetect;
    ItemAppear itemAppear;

    private void Start()
    {
        itemAppear = GetComponent<ItemAppear>();
        itemAppear.OnAppeared += OnAppeared;
    }

    private void OnDestroy()
    {
        itemAppear.OnAppeared -= OnAppeared;
    }

    void OnAppeared()
    {
        movement.enabled = true;
        wallDetect.SetActive(true);
        Destroy(this);
    }
}