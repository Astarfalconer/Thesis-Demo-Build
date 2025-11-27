using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    public float moveSpeed = 1.0f;      // How fast the text floats upward
    public float fadeSpeed = 1.0f;      // How fast it fades away
    public float lifetime = 1.0f;       // How long before it begins destroying

    private TextMeshProUGUI tmp;
    private Color originalColor;
    private float timer = 0f;

    void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        originalColor = tmp.color;
    }

    void Update()
    {
        // Move upward
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Fade out after some time
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            originalColor.a -= fadeSpeed * Time.deltaTime;
            tmp.color = originalColor;

            if (originalColor.a <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    // Called after you instantiate the prefab:
    public void SetText(string value)
    {
        tmp.text = value;
    }
}

