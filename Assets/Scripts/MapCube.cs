using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo; //保存当前cube身上的炮台


    [HideInInspector]
    public TurretData turretData;

    public int turretLevel = 0;
    public int turretMaxLevel = 1;

    public GameObject buildEffect;

    private new Renderer renderer;

/*    [HideInInspector]
    public int level = 0;

    public int maxLevel = 1;*/

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretLevel = 0;
        this.turretData = turretData;
        turretGo = GameObject.Instantiate(this.turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    public void UpgradeTurret()
    {
        if (this.turretLevel >= this.turretMaxLevel) return;
        Destroy(turretGo);
        this.turretLevel += 1;
        turretGo = GameObject.Instantiate(this.turretData.turretUpgradePrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    public  void DestroyTurret()
    {
        Destroy(turretGo);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
        turretGo = null;
        turretData = null;
    }

    private void OnMouseEnter()
    {
        if (turretGo == null && EventSystem.current.IsPointerOverGameObject() == false)
        {
            renderer.material.color = Color.green;
        }
    }
    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

}
