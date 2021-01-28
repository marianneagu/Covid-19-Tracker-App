using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
public class Menus : MonoBehaviour
{
    public GameObject mainMenu, registerMenu, loginMenu, profileMenu;
    public Animator animator;
    
    public void Start()
    {
        var notificationIntentData = AndroidNotificationCenter.GetLastNotificationIntent();

        if (notificationIntentData != null)
        {
            registerMenu.SetActive(false);
            mainMenu.SetActive(true);
            profileMenu.SetActive(false);
            loginMenu.SetActive(false);
            animator.Play("MenuMove");
        }

        else if (!PlayerPrefs.HasKey("register")) //niciodata inregistrat, va lua true atunci cand s-a inreg cu SUCCES
        {
            registerMenu.SetActive(true);
            mainMenu.SetActive(false);
            profileMenu.SetActive(false);
            loginMenu.SetActive(false);
        }
        else if(!PlayerPrefs.HasKey("login")) // nu este logat dar s-a inregistrat, va lua true atunci cand s-a logat cu SUCCES
        {
            registerMenu.SetActive(false);
            mainMenu.SetActive(false);
            profileMenu.SetActive(false);
            loginMenu.SetActive(true);
        }
        else
        {
            
            AutoLogare();
        }
    }

    public void AutoLogare()
    {
        if(!PlayerPrefs.HasKey("FirstTime"))
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            registerMenu.SetActive(false);
            mainMenu.SetActive(false);
            profileMenu.SetActive(true);
            loginMenu.SetActive(false);
        }
        else
        {
            registerMenu.SetActive(false);
            mainMenu.SetActive(true);
            profileMenu.SetActive(false);
            loginMenu.SetActive(false);
        }
    }

    public void MainMenu()
    {
        registerMenu.SetActive(false);
        mainMenu.SetActive(true);
        profileMenu.SetActive(false);
        loginMenu.SetActive(false);
    }
    public void Register()
    {
        registerMenu.SetActive(true);
        mainMenu.SetActive(false);
        profileMenu.SetActive(false);
        loginMenu.SetActive(false);
    }
    public void LoginMenu()
    {

        registerMenu.SetActive(false);
        mainMenu.SetActive(false);
        profileMenu.SetActive(false);
        loginMenu.SetActive(true);
    }
    public void ProfileMenu()
    {
        registerMenu.SetActive(false);
        mainMenu.SetActive(false);
        profileMenu.SetActive(true);
        loginMenu.SetActive(false);

    }

    public void Outgoings()
    {
        animator.Play("MenuMove");
    }
    public void OutgoingsBack()
    {
        animator.Play("MenuMoveReverse");
    }

    public void VisitedLoc()
    {
        animator.Play("MenuMove_Vis");
    }
    public void VisitedLocBack()
    {
        animator.Play("MenuMove_VisReverse");
    }


}
