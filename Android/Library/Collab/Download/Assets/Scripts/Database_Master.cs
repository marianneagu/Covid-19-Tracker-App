using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ConnectCsharpToMysql;
using MySql.Data.MySqlClient;
using UnityEngine.UI;
using System;
using System.Linq;

public class Database_Master : MonoBehaviour
{
    // Start is called before the first frame update
    DBConnect dBConnect;
    public Text email_reg, password_reg;
    public Text email_login, password_login;
    public GameObject loginFailedBox, registerFailedBox;
    public Menus menus;
    void Start()
    {
        //dBConnect.Insert("test@eu", "test");
        dBConnect = new DBConnect();
        loginFailedBox.SetActive(false);
        registerFailedBox.SetActive(false);

    }
    public void Retrieve()
    {
        /*
        string query = "SELECT public_location_id ,created_on FROM scanned_qr WHERE public_user_id =" +  PlayerPrefs.GetString("UserId");
        List<string> Result = new List<string>(dBConnect.Select(query).ToArray<>);
        //string query2 = "SELECT created_on FROM scanned_qr WHERE public_user_id =" + PlayerPrefs.GetString("UserId");
        */

    }
    public void Register()
    {
        //if(email_reg.text != null && password_reg.text != null)
        if(!String.IsNullOrEmpty(email_reg.text) && !String.IsNullOrEmpty(password_reg.text))
        {
            
            Debug.Log(email_reg.text);
            Debug.Log(email_reg);
            string query = "INSERT INTO public_users (email, password) VALUES('" + email_reg.text + "', '" + password_reg.text + "')";

            //open connection
            if (dBConnect.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, dBConnect.connection);

                //Execute command
                cmd.ExecuteNonQuery();
                print(cmd.ExecuteNonQuery().ToString());
                menus.LoginMenu();
                PlayerPrefs.SetInt("register", 1);
                //close connection
                dBConnect.CloseConnection();
            }
            
        }
        else
        {
            registerFailedBox.SetActive(true);
        }
        

    }

    public void Login()
    {
        if (!String.IsNullOrEmpty(email_login.text) && !String.IsNullOrEmpty(password_login.text))
        {
            string query_email = "SELECT id FROM public_users WHERE email = '" + email_login.text + "'";
            string query_password = "SELECT id FROM public_users WHERE password = '" + password_login.text + "'";

            Debug.Log(query_email);
            //open connection
            if (dBConnect.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query_email, dBConnect.connection);
                MySqlCommand cmd_pass = new MySqlCommand(query_password, dBConnect.connection);
                //Execute command
                //cmd.ExecuteNonQuery();
                if (cmd.ExecuteScalar() != null && cmd_pass.ExecuteScalar() != null)
                { 
                    print(cmd.ExecuteScalar().ToString());

                    PlayerPrefs.SetString("UserId", cmd.ExecuteScalar().ToString());
                    PlayerPrefs.SetInt("login", 1);
                    menus.AutoLogare();
                }
                else
                {
                    //nu merge
                    print("EROARE LA AUTH");
                    loginFailedBox.SetActive(true);
                }


                //close connection
                dBConnect.CloseConnection();
            }
           

        }
        else loginFailedBox.SetActive(true);


    }
    public void PutNameInDB(string name, string lastName)
    {
        if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(lastName))
        {

            string query = "INSERT INTO public_users (first_name, last_name) VALUES('" + name + "', '" + lastName + "')";

            //open connection
            if (dBConnect.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, dBConnect.connection);

                //Execute command
                cmd.ExecuteNonQuery();
                print(cmd.ExecuteNonQuery().ToString());

                PlayerPrefs.SetString("name", name);
                PlayerPrefs.SetString("lastName", lastName);
                //close connection
                dBConnect.CloseConnection();
            }

        }
    }
    public void CloseRegError()
    {
        registerFailedBox.SetActive(false);
    }
    public void CloseLogError()
    {
        loginFailedBox.SetActive(false);
    }

}
