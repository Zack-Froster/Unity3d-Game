using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BulidManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missleTurretData;
    public TurretData standardTurretData;

    public Animator moneyAnimator;

    public GameObject upgradeUI;
    private Animator upgradeUIAnimator;

    public GameObject beginUI;
    private Animator beginAnimator;

    //表示被选择为要创建的炮台
    private TurretData selectedTurretData;

    //被点击的mapCube
    private MapCube mapCube;

    [HideInInspector]
    public static int money = 500;

    public int initMoney = 500;

    public Text moneyText;


    public Button upgradeButton;

    private AnimatorStateInfo info;

    public AudioSource BGMSE;
    public AudioSource BuildTurretSE;
    public AudioSource DestroyTurretSE;
    public AudioSource PressTurretUISE;
    public AudioSource CountDownSE;
    public AudioSource MoneyNotEnoughSE;

    private void Awake()
    {
        selectedTurretData = null;
    }

    private void Start()
    {
        MoneyInit();
        upgradeUIAnimator = upgradeUI.GetComponent<Animator>();

        beginUI.SetActive(true);
        beginAnimator = beginUI.GetComponent<Animator>();

        CountDownSE.Play();
        
    }

    private void Update()
    {


        info = beginAnimator.GetCurrentAnimatorStateInfo(0);
        // 判断开始动画是否播放完成
        if (info.normalizedTime >= 1.0f)
        {
            beginUI.SetActive(false);
            BGMSE.Play();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isColleder = Physics.Raycast(ray,out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isColleder)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();//得到点击的mapCube
                    if(selectedTurretData != null && mapCube.turretGo == null)
                    {
                        //进行开发炮台的建造
                        if(money >= selectedTurretData.price)
                        {
                            ChangeMoney(-selectedTurretData.price);
                            mapCube.BuildTurret(selectedTurretData);
                            BuildTurretSE.Play();

                        }
                        else
                        {
                            //提示钱不够
                            MoneyFliker();
                        }
                    }
                    else if(mapCube.turretGo != null)
                    {
                        //升级处理 
                        ShowUpgradeUI(mapCube.transform.position, mapCube.turretLevel < mapCube.turretMaxLevel);
                        this.mapCube = mapCube;
                    }
                    if(mapCube.turretGo == null && upgradeUI.activeInHierarchy == true)
                    {
                        StartCoroutine(HideUpgradeUI());
                    }

                }
                else {
                    if (upgradeUI.activeInHierarchy == true)
                    {
                        StartCoroutine(HideUpgradeUI());
                    }
                }


            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
            PressTurretUISE.Play();
        }
    }
    public void OnMissleSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missleTurretData;
            PressTurretUISE.Play();
        }
    }
    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
            PressTurretUISE.Play();
        }
    }

    public void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = "" + money;
    }


    private void ShowUpgradeUI(Vector3 pos, bool isInteractable = true)
    {

        StopCoroutine("HideUpgradeUI");

        //先禁用在初始化，防止动画未播放
        upgradeUI.SetActive(false);

        upgradeUI.SetActive(true);
        upgradeUI.transform.position = pos;
        upgradeButton.interactable = isInteractable;
    }

    IEnumerator HideUpgradeUI()
    {
        upgradeUIAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        upgradeUI.SetActive(false);
    }
    public void OnUpgradeButtonDown()
    {
        if (money >= mapCube.turretData.pirceUpgrade)
        {
            ChangeMoney(-mapCube.turretData.pirceUpgrade);
            mapCube.UpgradeTurret();
            BuildTurretSE.Play();
        }
        else
        {
            MoneyFliker();
        }
        StartCoroutine(HideUpgradeUI());
    }

    public void OnDemolishButtonDown()
    {
        mapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
        DestroyTurretSE.Play();
    }
    private void MoneyInit()
    {
        money = initMoney;
        moneyText.text = "" + money;
    }

    private void MoneyFliker()
    {
        moneyAnimator.SetTrigger("Flicker");
        MoneyNotEnoughSE.Play();
    }

}
