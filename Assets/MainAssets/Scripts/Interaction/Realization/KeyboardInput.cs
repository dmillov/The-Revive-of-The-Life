using UnityEngine;
// Клас для введення з клавіатури (ПК)
public class KeyboardInput : IInteractionInput
{
    private readonly KeyCode _key;

    public KeyboardInput(KeyCode key)
    {
        _key = key;
    }

    public bool IsInputReceived()
    {
        return Input.GetKeyDown(_key);
    }

    public string GetInputAsString() => _key.ToString();
}
