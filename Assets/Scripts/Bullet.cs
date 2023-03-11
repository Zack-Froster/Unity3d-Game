using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 50;

    public float speed = 20;

    private Transform target;

    public GameObject explosionEffectPrefab;


    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    void Update()
    {
        if(target == null)
        {
            BulletDestroy();
            return;
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {   /*
             * 1.µÐÈËµôÑª
             * 2.×Óµ¯±¬Õ¨*/
            other.GetComponent<Enemy>().TakeDamage(damage);
            Die();
        }
    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(effect, 1);
    }
    private void BulletDestroy()
    {
        transform.LookAt(transform.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Invoke("Die", 1);
    }
}