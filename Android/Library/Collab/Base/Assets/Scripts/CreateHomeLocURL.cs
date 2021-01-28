using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class CreateHomeLocURL : MonoBehaviour
{
    public Text city, street, streetNumber,maximOutgoings;
    public string apiKey, myURL;
    public GameObject profileFailedBox;
    public Menus menus;
    public Google_Geocoding geocoding;
    private void Start()
    {
        profileFailedBox.SetActive(false);
    }
    public void OnSaveButton()
    {
        if(!String.IsNullOrEmpty(city.text) && !String.IsNullOrEmpty(street.text) && !String.IsNullOrEmpty(streetNumber.text) && !String.IsNullOrEmpty(maximOutgoings.text))
        {
            myURL = "https://maps.googleapis.com/maps/api/geocode/json?address=" + city.text + "+" + street.text + "+" + streetNumber.text + "&key=" + apiKey;
            PlayerPrefs.SetString("home_url", myURL);
            Debug.Log(myURL);
            menus.MainMenu();
            geocoding.StartRequest();
            PlayerPrefs.SetInt("Maxim", int.Parse(maximOutgoings.ToString()));
        }
        else
        {
            profileFailedBox.SetActive(true);
        }
    }

    public void CloseProfileFailedBox()
    {
        profileFailedBox.SetActive(false);
    }
}
