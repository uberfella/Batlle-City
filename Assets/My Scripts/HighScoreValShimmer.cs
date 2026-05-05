using UnityEngine;
using TMPro;
using System.Collections;

public class HighScoreValShimmer : MonoBehaviour
{
    public float duration = 9f;
    public float flickerSpeed = 15f;   // higher = faster flicker

    private TMP_Text tmpText;
    private Material mat;

    private Color bright = new Color32(255, 255, 255, 255);
    private Color dim = new Color32(128, 128, 128, 255);

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        mat = tmpText.fontSharedMaterial; // shared is fine in your case

        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            // Sin-based smooth oscillation
            float t = Mathf.Abs(Mathf.Sin(Time.time * flickerSpeed));

            Color current = Color.Lerp(dim, bright, t);
            mat.SetColor("_FaceColor", current);

            yield return null;
        }

        // Reset to bright when done
        mat.SetColor("_FaceColor", bright);
    }
}
