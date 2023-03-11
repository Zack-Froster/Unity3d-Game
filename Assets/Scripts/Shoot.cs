using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{



    private List<GameObject> enemys = new List<GameObject>();

    public float attackRateTime = 1; //几秒攻击一次
    private float timer;

    public GameObject bulletPrefab;
    public Transform firePositonOne;
    public Transform firePositonTwo;
    public Transform firePositonThree;
    public Transform head;



    public AudioSource MissleSE;
    public AudioSource StandardSE;



    private void Start()
    {
        timer = attackRateTime;
    }


    private void Update()
    {

        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        timer += Time.deltaTime;
        if (enemys.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            Attack();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }



    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            if (gameObject.name == "MissileTurret(Clone)" || gameObject.name == "MissileTurretUpgrade(Clone)")
            {
                MissileAttack(bulletPrefab, firePositonOne.position, firePositonOne.rotation, enemys[0].transform);
            }
            else if (gameObject.name == "StandardTurret(Clone)" || gameObject.name == "StandardTurretUpgrade(Clone)")
            {
                StandardAttack(bulletPrefab, firePositonOne.position, firePositonTwo.position, firePositonThree.position, firePositonOne.rotation, enemys[0].transform);
            }
        }

        else
        {
            timer = attackRateTime;
        }
    }

    void MissileAttack(GameObject bulletPrefab, Vector3 position, Quaternion rotation, Transform transform)
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, position, rotation);
        bullet.GetComponent<Bullet>().SetTarget(transform);
        MissleSE.Play();
    }
    void StandardAttack(GameObject bulletPrefab, Vector3 positionOne, Vector3 positionTwo, Vector3 positionThree, Quaternion rotation, Transform transform)
    {
        StartCoroutine(BulletCreate(bulletPrefab, positionOne, positionTwo, positionThree, rotation, transform));
    }
    private IEnumerator BulletCreate(GameObject bulletPrefab, Vector3 positionOne, Vector3 positionTwo, Vector3 positionThree, Quaternion rotation, Transform transform)
    {
        //第一颗子弹
        GameObject bulletOne = GameObject.Instantiate(bulletPrefab, positionOne, rotation);
        bulletOne.GetComponent<Bullet>().SetTarget(transform);
        StandardSE.Play();
        yield return new WaitForSeconds(0.1f);

        //第二颗子弹
        GameObject bulletTwo = GameObject.Instantiate(bulletPrefab, positionTwo, rotation);
        bulletTwo.GetComponent<Bullet>().SetTarget(transform);
        StandardSE.Play();
        yield return new WaitForSeconds(0.1f);

        //第三颗子弹
        GameObject bulletThree = GameObject.Instantiate(bulletPrefab, positionThree, rotation);
        bulletThree.GetComponent<Bullet>().SetTarget(transform);
        StandardSE.Play();
    }

    void UpdateEnemys()
    {
        enemys.RemoveAll(item => item == null);
    }
}
