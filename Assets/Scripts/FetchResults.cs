using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FetchResults : MonoBehaviour
{
    private static readonly string DefaultIcon = "Old";
    private bool _isQueryRunning;
    private Button _button;
    private OpenWeatherAPI _api;
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();

    public GameObject loadingMessage;
    public TMP_InputField cityInputField;
    public DayCard[] dayCards;
    public Sprite[] spriteIcons;
    public CanvasGroup panel;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _api = GetComponent<OpenWeatherAPI>();

        // Creates the Dictionary that maps the name of the sprite to its image.
        foreach (Sprite icon in spriteIcons)
        {
            _sprites[icon.name] = icon;
        }

        _button.onClick.AddListener(delegate
        {
            if (!string.IsNullOrEmpty(cityInputField.text.Trim()) && !_isQueryRunning)
            {
                StartCoroutine(FetchData(cityInputField.text));
            }
        });
    }

    private IEnumerator FetchData(string query)
    {
        _isQueryRunning = true;
        panel.alpha = 0; // Hides panel.
        loadingMessage.SetActive(true);

        yield return _api.GetForecast(query);

        loadingMessage.SetActive(false);
        _isQueryRunning = false;

        if (_api.Response != null)
        {
            FillDays(_api.Response);
            panel.alpha = 1; // Shows panel.
        }
    }

    private void FillDays(ResponseContainer response)
    {
        panel.alpha = 1;
        for (int index = 0; index < dayCards.Length; index++)
        {
            var icon = response.list[index].weather[0].icon;

            if (!_sprites.ContainsKey(icon))
                icon = DefaultIcon;

            Sprite sprite = _sprites[icon];
            DayCardModel day = new DayCardModel(response.list[index], sprite);
            DayCard dayCard = dayCards[index];
            dayCard.SetModel(day);
            
        }
    }
}