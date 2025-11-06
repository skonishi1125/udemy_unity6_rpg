using System.Collections;
using UnityEngine;

[System.Serializable]
public class Buff
{
    public StatType type;
    public float value;
}

public class Object_Buff : MonoBehaviour
{
    private SpriteRenderer sr;
    private Entity_Stats statsToModify;

    [Header("Buff details")]
    [SerializeField] private Buff[] buffs;
    [SerializeField] private string buffName;
    [SerializeField] private float buffValue = 5;
    [SerializeField] private float buffDuration = 4;
    [SerializeField] private bool canBeUsed = true;

    [Header("Floaty movement")]
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float floatRange = .1f;
    private Vector3 startPosition;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        startPosition = transform.position;
    }

    private void Update()
    {
        // sinから、時間とともに、-1 ~ +1 の間を滑らかに上下する数値が得られる (緩やかな上下が実現できる)
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatRange;
        transform.position = startPosition + new Vector3(0, yOffset);
    }

    // Unity側で、Colliderの設定をisTriggerのチェックを入れて、この関数を走らせることができる
    // コルーチンで起動する (時間が経ったらバフを取り除くため）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canBeUsed == false)
            return;

        statsToModify = collision.GetComponent<Entity_Stats>();

        StartCoroutine(BuffCo(buffDuration));
    }

    private IEnumerator BuffCo(float duration)
    {
        canBeUsed = false;
        sr.color = Color.clear; // 透明にする

        ApplyBuff(true);

        yield return new WaitForSeconds(duration);

        ApplyBuff(false);
        Destroy(gameObject);
    }

    private void ApplyBuff(bool apply)
    {
        foreach (var buff in buffs)
        {
            if (apply)
                statsToModify.GetStatBytype(buff.type).AddModifier(buff.value, buffName);
            else
                statsToModify.GetStatBytype(buff.type).RemoveModifier(buffName);

        }
    }


}
