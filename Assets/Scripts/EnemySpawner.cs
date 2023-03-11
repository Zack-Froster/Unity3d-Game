using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    public Wave waveEnemys;
    public int waves;

    public Transform START;

    public float waveRate = 0.3f;
    public static int EnemyAliveCount = 0;
    private Coroutine coroutine;

    private int enemyType = 0;
    private int initEnemyNums = 5;

    public int addedEnemysPerWaves = 2;

    public float[] waitTime = { 0.1f, 0.15f, 0.2f, 0.25f };


    private Enemy enemy1;
    private Enemy enemy2;
    private Enemy enemy3;
    private Enemy enemy4;

    private float initEnemy1HP;
    private float initEnemy2HP;
    private float initEnemy3HP;
    private float initEnemy4HP;

    private void Start()
    {
        coroutine = StartCoroutine(SpawnEnemy());

    }
    public void Stop()
    {
        StopCoroutine(coroutine);
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(5.5f);
        for (int i = 0; i < waves; i++)
        {

            for (int j = 0; j < initEnemyNums + addedEnemysPerWaves * i; j++)
            {
                //随机生成怪的种类
                enemyType = Random.Range(0, 4);
                //生成怪物
                switch (enemyType)
                {
                    case 0:
                        {
                            enemy1 = waveEnemys.enemy1Prefab.GetComponent<Enemy>();
                            initEnemy1HP = enemy1.HP;
                            enemy1.HP = initEnemy1HP + initEnemy1HP * 0.2f * (i % 5);
                            GameObject.Instantiate(enemy1, START.position, Quaternion.identity);
                            enemy1.HP = initEnemy1HP;
                            EnemyAliveCount++;
                            break;
                        }
                    case 1:
                        {
                            enemy2 = waveEnemys.enemy2Prefab.GetComponent<Enemy>();
                            initEnemy2HP = enemy2.HP;
                            enemy2.HP = initEnemy2HP + initEnemy2HP * 0.2f * (i % 5);
                            GameObject.Instantiate(enemy2, START.position, Quaternion.identity);
                            enemy2.HP = initEnemy2HP;
                            EnemyAliveCount++;
                            break;
                        }
                    case 2:
                        {
                            enemy3 = waveEnemys.enemy3Prefab.GetComponent<Enemy>();
                            initEnemy3HP = enemy3.HP;
                            enemy3.HP = initEnemy3HP + initEnemy3HP * 0.2f * (i % 5);
                            GameObject.Instantiate(enemy3, START.position, Quaternion.identity);
                            enemy3.HP = initEnemy3HP;
                            EnemyAliveCount++;
                            break;
                        }
                    case 3:
                        {
                            enemy4 = waveEnemys.enemy4Prefab.GetComponent<Enemy>();
                            initEnemy4HP = enemy4.HP;
                            enemy4.HP = initEnemy4HP + initEnemy4HP * 0.2f * (i % 5);
                            GameObject.Instantiate(enemy4, START.position, Quaternion.identity);
                            enemy4.HP = initEnemy4HP;
                            EnemyAliveCount++;
                            break;
                        }
                    default: break;

                }

                if (j != initEnemyNums + addedEnemysPerWaves - 1)
                    yield return new WaitForSeconds(waitTime[0]);
            }
            while (EnemyAliveCount > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
        while (EnemyAliveCount > 0)
        {
            yield return 0;
        }
        GameManager.Instance.Victory();
    }

}
