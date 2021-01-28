using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConnectCsharpToMysql;
using MySql.Data.MySqlClient;
using System;
using TMPro;
using UnityEngine.UI;

public class ScannedQR : MonoBehaviour
{
    public static string public_id;
    public static bool startInstert = false;
    public Text outgoings;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if(startInstert)
        {
            if(PlayerPrefs.HasKey("UserId"))
            {
                DateTime dt = DateTime.Now;
                insertTable(PlayerPrefs.GetString("UserId"), public_id, dt.ToString("yyyy-MM-dd HH:mm:ss"));
            }
            startInstert = false;
        }
        outgoings.text = "Outings " + PlayerPrefs.GetInt("Total", 0).ToString() + "/" + PlayerPrefs.GetString("Maxim");
    }

    void insertTable(string user_id,string location_id,string createdOnData )
    {
        DBConnect dBConnect;
        dBConnect = new DBConnect();
        string query = "INSERT INTO scanned_qr (public_user_id,public_location_id,created_on) VALUES('" + user_id + "', '" + location_id + "','" +createdOnData+"')";
        if (dBConnect.OpenConnection() == true)
        {
            //create command and assign the query and connection from the constructor
            MySqlCommand cmd = new MySqlCommand(query, dBConnect.connection);

            //Execute command
            cmd.ExecuteNonQuery();

            //close connection
            dBConnect.CloseConnection();
        }

    }
}
