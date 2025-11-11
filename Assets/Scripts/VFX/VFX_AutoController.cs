using System.Collections;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{

    private SpriteRenderer sr;

    [SerializeField] private bool autoDestroy = true;
    [SerializeField] private float destroyDelay = 1;
    [Space]
    [SerializeField] private bool randomOffset = true;
    [SerializeField] private bool randomRotation = true;

    [Header("Fade effect")]
    [SerializeField] private bool canFade;
    [SerializeField] private float fadeSpeed = 1;


    [Header("Random Rotation")]
    [SerializeField] private float minRotation = 0;
    [SerializeField] private float maxRotation = 360;

    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -.3f;
    [SerializeField] private float xMaxOffset = .3f;
    [Space]
    [SerializeField] private float yMinOffset = -.3f;
    [SerializeField] private float yMaxOffset = .3f;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        if (canFade)
            StartCoroutine(FadeCo());


        ApplyRandomOffset();
        ApplyRandomRotation();

        if (autoDestroy == true)
            Destroy(gameObject, destroyDelay);
    }

    private IEnumerator FadeCo()
    {
        Color targetColor = Color.white;

        // 徐々にダッシュエフェクトが消えていくように、透明度をいじる
        while(targetColor.a > 0)
        {
            targetColor.a = targetColor.a - (fadeSpeed * Time.deltaTime); // Time.deltaTimeとすることで、フレーム依存とならない消え方になる
            sr.color = targetColor;
            yield return null;
        }

        sr.color = targetColor;
    }


    // エフェクトが出るたび、多少x,y軸がずれる
    private void ApplyRandomOffset()
    {
        if (randomOffset == false)
            return;

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset, yMaxOffset);

        transform.position = transform.position + new Vector3(xOffset, yOffset);
    }

    // エフェクトが出るたび、少し回転されて表示される（適度なばらつき間の演出)
    private void ApplyRandomRotation()
    {
        if (randomRotation == false)
            return;

        float zRotation = Random.Range(minRotation, maxRotation);
        transform.Rotate(0,0, zRotation);
    }


}
