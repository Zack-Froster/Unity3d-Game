using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public int endHP = 3;
    private void Update()
    {
        if(endHP == 0)
        {
            if(endHP >= 0)
            {
                endHP -= 1;
            }
            GameManager.Instance.GameOver();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            endHP -= 1;
        }
    }
}
