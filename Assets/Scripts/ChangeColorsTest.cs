using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeColorsTest : MonoBehaviour
{
    private Image _image;

    [SerializeField] private float _colorSpeed = 0.2f;

    private void Start()
    {
        _image = GetComponent<Image>();
        if (_image != null)
        {
            StartCoroutine(RainbowCycle());
        }
        else
        {
            Debug.LogError("Image not found on: " + gameObject.name);
        }
    }

    private IEnumerator RainbowCycle()
    {
        float col = 0f;

        while (true)
        {
            col += Time.deltaTime * _colorSpeed;
            if (col > 1f) col = 0f;

            _image.color = Color.HSVToRGB(col, 1f, 1f);
            yield return null;
        }
    }
}