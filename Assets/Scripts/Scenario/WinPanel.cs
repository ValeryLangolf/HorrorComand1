using TMPro;
using UnityEngine;

public class WinPanel : Entity
{
    private const string WinText = "Прими наши поздравления!!!\n" +
        "Тебе удалось пройти демку за:\n\n" +
        "{0}\n\n" +
        "Жми \"Enter\", если хочешь побродить по лесу или \"Esc\" для выхода";

    [SerializeField] private TextMeshProUGUI _textWin;
    [SerializeField] private Stopwatch _stopwatch;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            HideObject();
    }

    public override void ShowObject()
    {
        base.ShowObject();
        _textWin.text = string.Format(WinText, _stopwatch.GetTimeGame());
    }
}