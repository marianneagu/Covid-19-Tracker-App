using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class Activities : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        text.text = PlayerPrefs.GetInt(this.gameObject.name, 0).ToString();
    }
    public void Increment()
    {
        PlayerPrefs.SetInt(this.gameObject.name,PlayerPrefs.GetInt(this.gameObject.name,0) + 1);
        //transform.parent.GetComponent<Activities>().text.text = PlayerPrefs.GetInt(transform.parent.name, 0).ToString();
        text.text = PlayerPrefs.GetInt(this.gameObject.name, 0).ToString();
        PlayerPrefs.SetInt("Total", PlayerPrefs.GetInt("Total", 0) + 1);
    }
}
