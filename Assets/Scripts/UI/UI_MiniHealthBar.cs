using UnityEngine;

public class UI_MiniHealthBar : MonoBehaviour
{

    private Entity entity;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();

    }

    private void OnEnable()
    {
        entity.OnFliped += HandleFlip;
    }

    private void OnDisable()
    {
        entity.OnFliped -= HandleFlip;
    }

    private void HandleFlip()
    {
        // 敵が反転したときヘルスバーも反転してしまうので、それを防ぐ
        transform.rotation = Quaternion.identity;
    }
}
