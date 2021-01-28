using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instantiate_Locations : MonoBehaviour
{
    public Text prefab;
    public GameObject content;

    public void Start()
    {
        InstantiateVisitedLoc();
    }

    public void InstantiateVisitedLoc()
    {
        Text spawnedGameObject = Instantiate(prefab);
        spawnedGameObject.rectTransform.SetParent(content.transform);
        spawnedGameObject.transform.localScale = new Vector3(1, 1, 1);

        spawnedGameObject.text = "tradf";

        
    }
}
