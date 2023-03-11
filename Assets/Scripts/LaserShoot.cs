using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShoot : MonoBehaviour
{



    private List<GameObject> enemys = new List<GameObject>();


    public Transform startPositon;

    public Transform head;

    public float damageRate = 70;

    public LineRenderer laserRenderer;

    public GameObject laserEffect;

    [HideInInspector]
    public Enemy target;

    public AudioSource LaserSE;

    private void Start()
    {

    }


    private void Update()
    {

        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(targetPosition);
        }
        if (enemys.Count > 0)
        {

            LaserAttack();
            if (LaserSE.isPlaying == false)
            {
                LaserSE.Play();
            }
        }
        else
        {
            LaserSE.Stop();
            laserEffect.SetActive(false);
            laserRenderer.enabled = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().RecoverSpeed();
            enemys.Remove(other.gameObject);
        }
    }

    void LaserAttack()
    {
        if (laserRenderer.enabled == false)
        {
            laserRenderer.enabled = true;
            laserEffect.SetActive(true);
        }
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            laserRenderer.SetPositions(new Vector3[] { startPositon.position, enemys[0].transform.position });
            target = enemys[0].GetComponent<Enemy>();
            target.TakeDamage(damageRate * Time.deltaTime);
            target.Moderate();
            laserEffect.transform.position = enemys[0].transform.position;
            Vector3 pos = transform.position;
            pos.y = enemys[0].transform.position.y;
            laserEffect.transform.LookAt(pos);
        }
    }

    void UpdateEnemys()
    {
        enemys.RemoveAll(item => item == null);
    }
}
