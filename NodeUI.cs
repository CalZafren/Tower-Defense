using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{

    public GameObject ui;
    public GameObject repairButton;
    public GameObject sellButton;
    public GameObject upgradeButton;
    private node target;
    BuildManager buildManager;
    public Text upgradeCost;
    public Text sellCost;
    public Text repairCost;
    private bool pauseUpgradeButton = false;
    [HideInInspector]
    public float tempCost;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            //In this case pause upgrade button is serving as pause sell button since it has same functionality
            if(target.isUpgraded == true && pauseUpgradeButton == true){
                buildManager.upgradeButton.SetActive(false);
                sellButton.SetActive(false);
            }else if(target.isUpgraded == true){
                buildManager.upgradeButton.SetActive(false);
            }else if(pauseUpgradeButton == false){
                buildManager.upgradeButton.SetActive(true); 
            }
        }
    }

    public void SetTarget(node _target){
        //Handles functionality if turret is broken
        if(_target._turret.isBroken == true){
            pauseUpgradeButton = true;
            target = _target;
            transform.position = target.GetBuildPosition();
            ui.SetActive(true);
            sellButton.SetActive(false);
            buildManager.upgradeButton.SetActive(false);
        }else{
            pauseUpgradeButton = false;
            target = _target;
            ui.SetActive(true);
            sellButton.SetActive(true);
            buildManager.upgradeButton.SetActive(true);
            transform.position = target.GetBuildPosition();
            upgradeCost.text = "-$" + target.turretBlueprint.upgradeCost;
            sellCost.text = "+$" + target.turretBlueprint.GetSellAmount();
        }
        //Updates value for repair
        tempCost = 1 - (target._turret.currentShots/target._turret.maxShots);
        int tempCost2 = (int)(tempCost * target.turretBlueprint.repairCost);
        if(_target.isUpgraded){
            tempCost2 *= 2;
        }
        repairCost.text = "-$" + tempCost2; 
    }

    public void Hide(){
        ui.SetActive(false);
    }

    public void Upgrade(){
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell(){
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Repair(){
        int cost = (int)(tempCost * target.turretBlueprint.repairCost);
        int valueCheck = PlayerStats.Money - cost;

        if((valueCheck) < 0){
            Debug.Log("Not enough Money");
            return;
        }else{
            target.RepairTurret(cost);
        }

        BuildManager.instance.DeselectNode();
    }
}
