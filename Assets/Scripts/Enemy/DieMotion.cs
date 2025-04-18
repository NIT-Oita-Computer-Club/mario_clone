using UnityEngine;

class DieMotion : MonoBehaviour
{
    Vector2 initialVelocity;

    float gravity = -20f;
    float elapsed = 0f;
    Vector3 startPosition;

    public void Init(int direction)
    {
        initialVelocity = new Vector2(direction * 3f, 4f);
    }

    void OnEnable()
    {
        startPosition = transform.position;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
    }

    void Update()
    {
        elapsed += Time.deltaTime;

        float x = initialVelocity.x * elapsed;
        float y = initialVelocity.y * elapsed + 0.5f * gravity * elapsed * elapsed;

        transform.position = startPosition + new Vector3(x, y, 0f);
    }
}