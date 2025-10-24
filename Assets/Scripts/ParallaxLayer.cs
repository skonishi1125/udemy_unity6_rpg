using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float parallaxMultiplier; // 視差倍率(カメラの何倍の速度で背景レイヤーを移動させるかの倍率)

    public void Move(float distanceToMove)
    {
        //background.position = background.position + new Vector3(distanceToMove * parallaxMultiplier, 0);
        background.position += Vector3.right * (distanceToMove * parallaxMultiplier); // right: xだけ弄る場合の書き方 (x, 0,0)
    }
}
