using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
    private const string WinText = "����� ���� ������������!!!\n" +
        "���� ������� ������ ����� ��:\n\n" +
        "{0}\n\n" +
        "��� \"Enter\", ���� ������ ��������� �� ���� ��� \"Esc\" ��� ������";

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