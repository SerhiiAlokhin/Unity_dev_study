using UnityEngine;
using System.Collections;

public class ChangeMaterialColor : MonoBehaviour
{
    [SerializeField] private Material _material;
    [SerializeField] private float _colorSpeed = 0.2f;

    private void Start()
    {
        if (_material != null)
        {
            StartCoroutine(RainbowCycle());
        }
        else
        {
            Debug.LogError("Помилка матеріалу !");
        }
    }

    private IEnumerator RainbowCycle()
    {
        float col = 0f;

        while (true)
        {
            col += Time.deltaTime * _colorSpeed;
            if (col > 1f) col = 0f;

            _material.color = Color.HSVToRGB(col, 1f, 1f);
            yield return null;
        }
    }
}