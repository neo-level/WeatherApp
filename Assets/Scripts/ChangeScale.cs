using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Change scale from celsius to fahrenheit and vice versa.
/// </summary>
public class ChangeScale : MonoBehaviour
{
    private bool _showMetric = true;

    public Sprite celsius;
    public Sprite fahrenheit;

    public Image image;
    public DayCard[] days;
    
    private void Awake()
    {
        // Attach a listener to the click event of the button this script is 
        // attached. When clicked, the button will negate the show metric flag
        // and inform all the DayCard instances to show the correct temperature
        // scale.
        var button = GetComponent<Button>();
        button.onClick.AddListener(delegate
        {
            _showMetric = !_showMetric;
            foreach (var day in days)
            {
                day.ShowMetric(_showMetric);
            }

            image.sprite = _showMetric ? celsius : fahrenheit;
        });
    }
}