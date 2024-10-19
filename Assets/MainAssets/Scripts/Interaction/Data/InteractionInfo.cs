using System;
// Клас для зберігання інформації про меню
[Serializable]
public class InteractionInfo
{
    public InteractionType InteractionType { get; }
    public string NameText { get; }
    public IInteractionInput InteractionInput { get; }
    public Action OnInteraction { get; }

    public InteractionInfo(InteractionType interactionType, string nameText, IInteractionInput interactionInput, Action onInteraction)
    {
        InteractionType = interactionType;
        NameText = nameText;
        InteractionInput = interactionInput;
        OnInteraction = onInteraction;
    }
}
