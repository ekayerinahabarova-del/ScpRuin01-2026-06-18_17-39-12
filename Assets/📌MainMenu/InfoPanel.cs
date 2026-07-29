using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public string discordLink = "https://discord.gg/NFx9gJE3qQ";
    public string websiteLink = "https://sites.google.com/view/plainstudio/";

    public Button discordButton;
    public Button websiteButton;
    public Button closeButton;

    void Start()
    {
        discordButton.onClick.AddListener(() => Application.OpenURL(discordLink));
        websiteButton.onClick.AddListener(() => Application.OpenURL(websiteLink));
        closeButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}