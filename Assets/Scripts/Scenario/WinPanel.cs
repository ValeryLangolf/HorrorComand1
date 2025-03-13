using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    private const string WinText = "Прими наши поздравления!!!\n" +
        "Тебе удалось пройти демку за:\n\n" +
        "{0}\n\n" +
        "Жми \"Enter\", если хочешь побродить по лесу или \"Esc\" для выхода";

    [SerializeField] private TextMeshProUGUI _textWin;
    [SerializeField] private Stopwatch _stopwatch;

    public void Enable()
    {
        _textWin.text = string.Format(WinText, _stopwatch.GetTimeGame());
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            gameObject.SetActive(false);
    }
}