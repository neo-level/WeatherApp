using System.Collections;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;


public class OpenWeatherAPI : MonoBehaviour
{
    private static readonly string APIBaseUrl = "https://api.openweathermap.org/data/2.5/forecast/weather?q={0}&cnt=5&appid={1}";

    [Tooltip("The API key that allows access to the OpenWeatherMap API")]
    public string apiKey;
    
    public ResponseContainer Response { get; private set; }

    public IEnumerator GetForecast(string city)
    {
        Response = null;

        string UrlEncodedCity = HttpUtility.UrlEncode(city);
        string url = string.Format(APIBaseUrl, UrlEncodedCity, apiKey);

        var webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string json = webRequest.downloadHandler.text;
            Response = JsonUtility.FromJson<ResponseContainer>(json);
        }
        else
        {
            Debug.Log(webRequest.error);
        }
    }
}
