using Unity.VisualScripting;
using UnityEngine;

class FirePower : MonoBehaviour
{
    [SerializeField] InputReader input;
    [SerializeField] PlayerAnimator animator;
    [SerializeField] GameObject firePfb;

    [SerializeField] Vector3 offset;

    private void Update()
    {
        if (input.RetrieveDashInput(thisFrame: true))
        {
            var obj = Instantiate(firePfb, transform.position +
            new Vector3(offset.x * (animator.FaceToRight ? 1 : -1), offset.y, offset.z), Quaternion.identity);
            obj.GetComponent<Fire>().SetDirection(animator.FaceToRight);
        }
    }
}