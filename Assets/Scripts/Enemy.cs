using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Transform[] positions;
    private int index = 0;
    public float speed = 10f;

    private float minSpeed = 0;
    private float maxSpeed = 0;

    public float HP = 500;
    public int value;
    private float totalHP;
    public GameObject explosionEffect;
    public Slider hpSlider;

    private Text moneyText;

    public AudioSource DieSE;
    public AudioSource MoneyAddSE;


    private void Start()
    {
        minSpeed = speed * 0.8f;
        maxSpeed = speed;
        positions = WayPoint.positions;
        totalHP = HP;
        moneyText = GameObject.Find("Money").GetComponent<Text>();;
    }

    private void Update()
    {
        Move();

    }

    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if(Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1)
        {
            ReachDestination();

        }

    }

    void ReachDestination()
    {
        if (DieSE.isPlaying == false)
        {
            DieSE.Play();
        }
        Destroy(this.gameObject, 0.3f);
    }


    private void OnDestroy()
    {
        EnemySpawner.EnemyAliveCount--;
    }

    public void TakeDamage(float damage)
    {
        if (HP <= 0) return;
        HP -= damage;

        hpSlider.value = (float)HP / totalHP;

        if(HP <= 0)
        {
            Die();
            
        }
    }

    public void Moderate()
    {
        if (speed <= minSpeed)
        {
            speed = minSpeed;
            return;
        }
        this.speed *= 0.8f;
        if(speed <= minSpeed)
        {
            speed = minSpeed;
        }
    }
    public void RecoverSpeed()
    {
        this.speed = maxSpeed;
    }
    private void Die()
    {
        DieSE.Play();
        GameObject effect = GameObject.Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(effect, 1.5f);
        ChangeMoney(value);

        Destroy(this.gameObject, 0.2f);
        
    }

    public void ChangeMoney(int change = 0)
    {
        MoneyAddSE.Play();
        BulidManager.money += change;
        moneyText.text = "" + BulidManager.money;

    }
}
