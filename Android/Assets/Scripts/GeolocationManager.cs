using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSONV;        //Need this for JSON Deserialization

public class GeolocationManager : MonoBehaviour
{
	//Get a Google API Key from https://developers.google.com/maps/documentation/geocoding/get-api-key
	public string GoogleAPIKey;

	public string latitude;
	public string longitude;
	public string countryLocation;

	IEnumerator Start()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;

		// Start service before querying location
		Input.location.Start();

		// Wait until service initializes
		int maxWait = 20;

		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			yield break;
		}

		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			Debug.Log("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieve
			longitude = Input.location.lastData.longitude.ToString();
			latitude = Input.location.lastData.latitude.ToString();
		}

		//Stop retrieving location
		Input.location.Stop();

		//Sends the coordinates to Google Maps API to request information
		using (WWW www = new WWW("https://maps.googleapis.com/maps/api/geocode/json?address=Str+Domnisori+16+bl+7g,+CA&key=AIzaSyCGkvleE_6f336ZU0jQcNNQSSeSDmB5bTE"))
		{
			yield return www;

			//if request was successfully
			if (www.error == null)
			{
				//Deserialize the JSON file
				var location = Json.Deserialize(www.text) as Dictionary<string, object>;
				var locationList = location["results"] as List<object>;
				var locationList2 = locationList[0] as Dictionary<string, object>;

				//Extract the substring information at the end of the locationList2 string
				countryLocation = locationList2["formatted_address"].ToString().Substring(locationList2["formatted_address"].ToString().LastIndexOf(",") + 2);

				//This will return the country information
				Debug.LogAssertion(countryLocation);
			}
		};
	}
}