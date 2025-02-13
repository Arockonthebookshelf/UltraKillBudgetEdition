using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    [SerializeField] GameObject interactionProgressBar;
    [SerializeField] GameObject interactionButtonImage;
    [SerializeField] GameObject interactionTextGO;
    Slider interactionSlider;
    Image interactionButton;
    TMP_Text interactionText;

    void Awake()
    {
        interactionSlider = interactionProgressBar.GetComponent<Slider>();
        interactionButton = interactionButtonImage.GetComponent<Image>();
        interactionText = interactionTextGO.GetComponent<TMP_Text>();
    }

    public void ToggleDisplay(bool value)
    {
        interactionProgressBar.SetActive(value);
        interactionButtonImage.SetActive(value);
        interactionTextGO.SetActive(value);
    }

    public void UpdateInteractionPrompt(bool showButton,float progressbar, string text)
    {
        interactionButton.enabled = showButton;
        interactionSlider.value = progressbar;
        interactionText.SetText(text);
    }

    public void ResetInteractionPrompt()
    {
        interactionButton.enabled = true;
        interactionSlider.value = 0;
        interactionText.SetText("");
    }
}
