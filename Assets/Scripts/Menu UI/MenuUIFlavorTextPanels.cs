using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIFlavorTextPanels : MonoBehaviour
{
    public GameObject[] flavorPanels = new GameObject[4];

    public void SelectPanel(int panel) {
        for (int i = 0; i < flavorPanels.Length; i++) {
            flavorPanels[i].SetActive(false);
        }
        flavorPanels[panel].SetActive(true);
    }
}
