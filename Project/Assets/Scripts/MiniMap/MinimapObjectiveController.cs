using UnityEngine;
using UnityEngine.UI;

public class MinimapObjectiveController : MonoBehaviour
{
    public RectTransform minimapBounds;
    public Transform player;
    public Transform questLocation;
    public float disableDistance = 30.0f;
    public Image minimapIcon;

    void Update()
    {
        if (questLocation != null)
        {
            Vector3 direction = (questLocation.position - player.position).normalized;
            Vector2 direction2D = new Vector2(direction.x, direction.z).normalized;
            Vector2 intersection = PointOnMinimapBounds(direction2D);
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.anchoredPosition = intersection;

            float distance = Vector3.Distance(player.position, questLocation.position);
            minimapIcon.enabled = distance > disableDistance;
        }
    }

    Vector2 PointOnMinimapBounds(Vector2 direction)
    {
        float radius = Mathf.Min(minimapBounds.rect.width, minimapBounds.rect.height) * 0.5f;
        float angle = Mathf.Atan2(direction.y, direction.x);
        float x = radius * Mathf.Cos(angle);
        float y = radius * Mathf.Sin(angle);
        return new Vector2(x, y);
    }
}
