using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; // singlton instance of the buildmanager
    private TurretBlueprint turretToBuild;
    private Node selectedNode;
   
    public NodeUI nodeUI;
 
    public GameObject turret1Prefab;
    public GameObject turret2Prefab;
    public GameObject turret3Prefab;

   
    public bool CanBuild { get { return turretToBuild != null; } }

    void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More than 1 buildmanager!");
        }
        instance = this;
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

   
    public void SelectTurretToBuild(TurretBlueprint turret)
    {

        turretToBuild = turret;

        DeselectNode();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

  
}
