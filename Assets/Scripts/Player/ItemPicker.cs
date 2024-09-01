using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class ItemPicker : MonoBehaviour
{
    [SerializeField] PlayerStateManager stateManager;
    public event UnityAction<Item> OnTakeItem = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag(Constants.Tags.Mushroom)) 
        {
            if(stateManager.CurrentPowerUpState.Growth == PlayerGrowth.Small)
                stateManager.ChangeState(stateManager.CurrentPowerUpState.Copy(growth: PlayerGrowth.Large));
            OnTakeItem.Invoke(Item.Mushroom);
            Destroy(collision.gameObject);
        }
        else if(collision.transform.CompareTag(Constants.Tags.FireFlower))
        {
            if (stateManager.CurrentPowerUpState.Growth != PlayerGrowth.Fire)
                stateManager.ChangeState(stateManager.CurrentPowerUpState.Copy(growth: PlayerGrowth.Fire));
            OnTakeItem.Invoke(Item.FireFlower);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag(Constants.Tags.Star))
        {
            stateManager.ChangeState(stateManager.CurrentPowerUpState.Copy(isStar: true));
            OnTakeItem.Invoke(Item.Star);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.CompareTag(Constants.Tags.OneUpMushroom))
        {

        }
    }
}
