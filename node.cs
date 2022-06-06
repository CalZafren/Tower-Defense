using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class node : MonoBehaviour
{
public Color hoverColor;
private Color startColor;
public Color lackingMoneyColor;
private Renderer rend;

[HideInInspector]
public GameObject turret;
[HideInInspector]
public Turret _turret;
[HideInInspector]
public TurretBlueprint turretBlueprint;
[HideInInspector]
public bool isUpgraded = false;
public Vector3 positionOffset;
BuildManager buildManager;




    void Start(){
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    //Highlights the node and raises it 1 unit
    void OnMouseOver(){
        //Checks to see if mouse is hovering over button
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        //Checks if there is a current turret we have selected
        if(!buildManager.CanBuild()){
            return;
        }

        //Checks if there is already a turret there
        if(turret != null){
            rend.material.color = startColor;
            return;
        //Checks if player has enough money to build turret
        }else if(buildManager.HasMoney()){
            rend.material.color = hoverColor;
        }else{
            rend.material.color = lackingMoneyColor;
        }
    }

    //Returns the node to its original color and height
    void OnMouseExit(){
        rend.material.color = startColor;
    }


    public Vector3 GetBuildPosition(){
        return transform.position + positionOffset;
    }

    void OnMouseDown(){
        //Checks to see if mouse is hovering over button
        if(EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        
        //Checks if a turret has already been built
        if(turret != null){
            buildManager.SelectNode(this);
            return;
        }

        //Checks if there is a turret we have selected
        if(!buildManager.CanBuild()){
            return;
        }

        //Builds Turret
        BuildTurret(buildManager.GetTurretToBuild());
    }

    void BuildTurret(TurretBlueprint blueprint){
        if(PlayerStats.Money < blueprint.cost){
           Debug.Log("Not Enough Money");
           return;
       }
       //Subtracts the turret cost from money
       PlayerStats.Money -= blueprint.cost;
       //Builds the turret
       GameObject tempTurret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
       turretBlueprint = blueprint;
       //Spawns turret build effect then destroys it
       GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
       Destroy(effect, 5f);
       //Sets that node full
       turret = tempTurret;

        //Accesses Turret Script and stores it in _turret
       if(turret != null){
            _turret = turret.GetComponent<Turret>();   
        }
    }

    public void UpgradeTurret(){
        if(PlayerStats.Money < turretBlueprint.upgradeCost){
           Debug.Log("Not Enough Money");
           return;
       }
       //Subtracts the turret upgrade cost from money
       PlayerStats.Money -= turretBlueprint.upgradeCost;
       //Gets rid of old turret
       Destroy(turret);
       //Builds the upgraded turret
       GameObject tempTurret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
       turret = tempTurret;
       //Spawns turret build effect then destroys it
       GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
       Destroy(effect, 5f);
       isUpgraded = true;

       if(turret != null){
            _turret = turret.GetComponent<Turret>();   
        }
    }

    public void SellTurret(){
        //Adds the sell value to the player's money
        PlayerStats.Money += turretBlueprint.GetSellAmount();
        Destroy(turret);
        turretBlueprint = null;
        isUpgraded = false;

        //Spawns destroy effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

    }

    public void RepairTurret(int cost){
        if(PlayerStats.Money < turretBlueprint.repairCost){
           Debug.Log("Not Enough Money");
           return;
       }
            //Subtracts repair value from players money
            PlayerStats.Money -= cost;
            //Handles actual fixing of turret
            _turret.FixTurret();
        }
}
