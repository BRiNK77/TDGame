using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    private Node target;
    public Text upgradeC;
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {

        target = _target;

        transform.position = target.spawnSpot.position;
        if (!target.isUpgraded)
        {
         upgradeC.text = target.currentTurret.upCost.ToString();
			upgradeButton.interactable = true;
		} else
		{
			upgradeC.text = "DONE";
			upgradeButton.interactable = false;
		}
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
}
