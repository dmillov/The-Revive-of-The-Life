using UnityEngine.UI;
// Клас для введення на мобільних пристроях через кнопку
public class MobileMenuInput : IInteractionInput
{
    private bool _isInputReceived;

    public void Initialize(Button mobileButton)
    {
        mobileButton.onClick.AddListener(OnMobileButtonClick);
    }

    // Викликається при натисканні мобільної кнопки
    private void OnMobileButtonClick()
    {
        _isInputReceived = true;
    }

    public bool IsInputReceived()
    {
        if (_isInputReceived)
        {
            _isInputReceived = false; // Скидаємо прапорець після отримання вводу
            return true;
        }
        return false;
    }

    public string GetInputAsString() => "Tap";
}
