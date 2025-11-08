using Unity.VisualScripting;
using UnityEngine;

public class UI_TreeConnection : MonoBehaviour
{
    [SerializeField] private RectTransform rotationPoint;
    [SerializeField] private RectTransform connectionLength;
    [SerializeField] private RectTransform childNodeConnectionPoint;

    public void DirectConnection(NodeDirectionType direction, float length)
    {
        bool shoudBeActive = direction != NodeDirectionType.None;

        float finalLength = shoudBeActive ? length : 0;
        float angle = GetDirectionAngle(direction);

        rotationPoint.localRotation = Quaternion.Euler(0,0,angle);
        connectionLength.sizeDelta = new Vector2(finalLength, connectionLength.sizeDelta.y);


    }

    public Vector2 GetConnectionPoint(RectTransform rect)
    {
        // childNodeConnectionPointの位置を、別のUI要素(rect.parent)のローカル座標として表現する
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (
                rect.parent as RectTransform, // 基準となるRectTransform 親要素のUI
                childNodeConnectionPoint.position, // スクリーンの座標(ワールド座標) 
                null, // カメラ
                out var localPosition // 出力(ローカル座標)
            );

        return localPosition;
    }


    private float GetDirectionAngle(NodeDirectionType type)
    {
        switch(type)
        {
            case NodeDirectionType.UpLeft: return 135f;
            case NodeDirectionType.Up: return 90f;
            case NodeDirectionType.UpRight: return 45f;
            case NodeDirectionType.Left: return 180f;
            case NodeDirectionType.Right: return 0f;
            case NodeDirectionType.DownLeft: return -135f;
            case NodeDirectionType.Down: return -90f;
            case NodeDirectionType.DownRight: return -45f;
            default: return 0f;
        }

    }
}

public enum NodeDirectionType
{
    None,
    UpLeft,
    Up,
    UpRight,
    Left,
    Right,
    DownLeft,
    Down,
    DownRight
}
