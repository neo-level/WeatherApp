using System;

[Serializable]
public class ResponseItem
{
    public long daytime;
    public ResponseTemperature temperature;
    public WeatherItem[] weather;
    public long sunrise;
    public long sunset;
}
