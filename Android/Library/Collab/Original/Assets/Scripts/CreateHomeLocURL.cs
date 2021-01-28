using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class CreateHomeLocURL : MonoBehaviour
{
    public Text city, street, streetNumber,maximOutgoings, name, lastName;
    public string apiKey, myURL;
    public GameObject profileFailedBox;
    public Menus menus;
    public Google_Geocoding geocoding;
    public Database_Master db;

    private void Start()
    {
        profileFailedBox.SetActive(false);
        if (PlayerPrefs.HasKey("city"))
            city.text = PlayerPrefs.GetString("city");
        if (PlayerPrefs.HasKey("street"))
            street.text = PlayerPrefs.GetString("street");
        if (PlayerPrefs.HasKey("streetNr"))
            streetNumber.text = PlayerPrefs.GetString("streetNr");
        if (PlayerPrefs.HasKey("name"))
            name.text = PlayerPrefs.GetString("name");
        if (PlayerPrefs.HasKey("lastName"))
            lastName.text = PlayerPrefs.GetString("lastName");
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

            PlayerPrefs.SetString("city", city.text);
            PlayerPrefs.SetString("street", street.text);
            PlayerPrefs.SetString("streetNr", streetNumber.text);

            db.PutNameInDB(name.text, lastName.text);
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
