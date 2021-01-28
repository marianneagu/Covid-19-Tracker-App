using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using Unity.Notifications.Android;

public class Location_Check : MonoBehaviour
{
    public Text gpsOut;
    public bool isUpdating;
    public float currentLatitude=0f, currentLongitude=0f;
    public bool outOfHomeArea;

    public void Start()
    {
        outOfHomeArea = false;
        CreateNotifications();
        SendNotification("TEST: Don't forget", "TEST: Tap to chose the activity you are doing now.");
    }

    private void Update()
    {
        if (!isUpdating)
        {
            StartCoroutine(GetLocation());
            isUpdating = !isUpdating;
        }
        //Debug.Log("Home lat: " + PlayerPrefs.GetFloat("home_lat"));
        if (PlayerPrefs.HasKey("home_lat") && currentLatitude != 0)
        {
            //Afara din zona casei
            if (PlayerPrefs.GetFloat("home_lat") + 0.0002f < currentLatitude || PlayerPrefs.GetFloat("home_lat") - 0.0002f > currentLatitude ||
                PlayerPrefs.GetFloat("home_lng") + 0.0002f < currentLongitude || PlayerPrefs.GetFloat("home_lng") - 0.0002f > currentLongitude)
                outOfHomeArea = true;
            //Debug.Log("Home lat: "+PlayerPrefs.GetFloat("home_lat"));
        }
        else
        {
            outOfHomeArea = false;
        }

        if(outOfHomeArea)
        {
            //notificare
            SendNotification("Don't forget", "Tap to chose the activity you are doing now.");

        }

    }

    IEnumerator GetLocation()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
            Permission.RequestUserPermission(Permission.CoarseLocation);
        }
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield return new WaitForSeconds(10);

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 10;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            //gpsOut.text = "Timed out";
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            //gpsOut.text = "Unable to determine device location";
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            //gpsOut.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + 100f + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;

            currentLatitude = Input.location.lastData.latitude;
            currentLongitude = Input.location.lastData.longitude;

            //gpsOut.text = "Latitude: " + currentLatitude.ToString() + "\n" + "Longitude: " + currentLongitude.ToString();
            


            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        isUpdating = !isUpdating;
        Input.location.Stop();
    }

    //NOTIFICATIONS

    public void CreateNotifications()
    {
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

    }

    public void SendNotification(string title, string text)
    {
        //Debug.Log("not");
        var notification = new AndroidNotification();
        notification.IntentData = "{\"title\": \"Notification 1\", \"data\": \"200\"}";
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = System.DateTime.Now.AddMinutes(1);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");
    }
    
}