using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public GameObject explosionPrefab; // 爆炸效果的预制体

    public float explosionRadius = 2f; // 爆炸半径
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Luggage"))
        {
            Debug.Log("Luggage 12312313");
            Explode();
        }
        
    }

        // 当炸弹被触发时调用
    public void Explode()
    {
        // 在炸弹位置生成爆炸效果
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // 获取所有在爆炸半径范围内的碰撞器
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // 循环处理每个碰撞器
        foreach (Collider col in colliders)
        {
            // 如果碰撞器有刚体组件
            Rigidbody rb = col.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 根据距离计算爆炸力
                Vector3 direction = rb.transform.position - transform.position;
                float distance = direction.magnitude;
                float force = 1f - (distance / explosionRadius);
                if (force > 0f)
                {
                    // 应用爆炸力
                    rb.AddForce(direction.normalized * force * 2f, ForceMode.Impulse);
                }
            }
        }
        Destroy(this.gameObject);
    }

}
