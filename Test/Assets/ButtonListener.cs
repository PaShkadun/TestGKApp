using Cysharp.Threading.Tasks;
using System;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListener : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _displayedText;

    private HttpService _httpService;
    private Button _button;

    void Start()
    {
        _httpService = new HttpService();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _button.interactable = false;
        OnClickAsync();
    }

    private async UniTask OnClickAsync()
    {
        try
        {
            _displayedText.text = "Sending request...";

            var datas = await _httpService.GetData();
            var text = new StringBuilder();

            foreach (var data in datas)
            {
                text.Append("UserId: " + data.UserId);
                text.Append("\tId: " + data.Id);
                text.Append("\tTitle: " + data.Title + "\n");
            }

            _displayedText.text = text.ToString();
        }
        catch (UnityWebRequestException e)
        {
            Debug.LogError(e.Message);
            _displayedText.text = "UnityWebRequestException. See logs";
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            _displayedText.text = "Unknown exception. See logs";
        }
        finally
        {
            _button.interactable = true;
        }
    }
}
