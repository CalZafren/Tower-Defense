using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    //Set this buildmanager into a variable to be referenced anywhere
    public static BuildManager instance;
    private TurretBlueprint turretToBuild;
    private node selectedNode;
    public GameObject buildEffect;
    public GameObject sellEffect;
    public NodeUI nodeUI;
    public GameObject upgradeButton;

    void Awake(){
        instance = this;
    }

    void Start(){
      
    }
   public bool CanBuild(){
       return turretToBuild != null;
    }

    public bool HasMoney(){
        return PlayerStats.Money >= turretToBuild.cost;
    }

   public void SelectTurretToBuild(TurretBlueprint turret){
       turretToBuild = turret;
       DeselectNode();
   }

   public TurretBlueprint GetTurretToBuild(){
       return turretToBuild;
   }

   public void SelectNode(node node){
       if(selectedNode == node){
           DeselectNode();
           return;
       }
       selectedNode = node;
       turretToBuild = null;
       nodeUI.SetTarget(node);
   }

   public void DeselectNode(){
       selectedNode = null;
       nodeUI.Hide();
   } 
}
