using System.Collections;
using UnityEngine;

public class Player_VFX : Entity_VFX
{
    [Header("Image Echo VFX")]
    [Range(.01f, 2f)]
    [SerializeField] private float imageEchoInterval = .05f;
    [SerializeField] private GameObject imageEchoPrefab;
    private Coroutine imageEchoCo;

    public void DoImageEchoEffect(float duration)
    {
        if (imageEchoCo != null)
            StopCoroutine(imageEchoCo);

        imageEchoCo = StartCoroutine(ImageEchoEffectCo(duration));

    }

    private IEnumerator ImageEchoEffectCo(float duration)
    {
        float timeTracker = 0;

        while (timeTracker < duration)
        {
            CreateImageEcho();

            yield return new WaitForSeconds(imageEchoInterval);
            timeTracker = timeTracker + imageEchoInterval;

        }
    }

    // 引数で指定したGameObjectを、指定の位置に複製 (増やすベースになるimageEchoPrefabへは、Unity側で割り当てよう)
    // (今回の場合) VFX_ImageEchoの子要素(Animator)のSprite Renderer.spriteに、sr.spriteを割り当てる
    // sr.spriteは、Entity_VFX側で定義されているもの。
    // これも同じくGetComponentInChildrenで取得しているので、Entity_VFX(もしくはPlayer_VFX)の割り当てられたGameObjectから、sprite rendererを探して格納している
    // 今回、PlayerのGameObjectに、Player_VFXの割当てがある。その中でスプライトがあるのは、Animatorだけ（Playerのドット絵) つまりこれが取得される
    // ちなみに、 Debug.Log($"[CreateImageEcho] 現在のSprite名: {sr.sprite?.name ?? "null"}"); でデバッグもできる

    private void CreateImageEcho()
    {
        GameObject imageEcho = Instantiate(imageEchoPrefab, transform.position, transform.rotation);
        imageEcho.GetComponentInChildren<SpriteRenderer>().sprite = sr.sprite;
        // ちなみに、↑の書き方は、imageEchoの子要素から、最初に見つかったSpriteRendererを変える処理
        // imageEcho.transform.Find("Animator")?.GetComponent<SpriteRenderer>().sprite = sr.sprite; とすると、 AnimatorのSRを変えるという意味になる
    }


}
