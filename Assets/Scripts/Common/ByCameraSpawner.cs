using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�N���[�����ɓ������ہC���g�̈ʒu�ɓ����GameObject���X�|�[��������D
/// </summary>
public class ByCameraSpawner : MonoBehaviour
{
    bool shouldSpawn = true;
    [SerializeField] GameObject spawnObj;
    GameObject instance = null;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, Camera.main.transform.position) <= 15)
        {
            if (shouldSpawn)
            {
                instance = Instantiate(spawnObj, transform.position, Quaternion.identity);
                shouldSpawn = false;
            }
        }
        else if(!shouldSpawn && instance == null)
        {
            shouldSpawn = true;
        }
    }
}
