using UnityEngine;
using TMPro;

public class ItemDescriptionPanel : MonoBehaviour
{
    public TextMeshProUGUI descriptionText;
    public Vector3 offset;

    private void Start()
    {
        Hide();
    }

    public void Show(string description, Vector3 position)
    {
        descriptionText.text = description;
        transform.position = position + offset;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
