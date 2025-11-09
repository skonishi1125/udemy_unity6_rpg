using System;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class UI_TreeConnectDetails
{
    public UI_TreeConnectHandler childNode;
    public NodeDirectionType direction;
    [Range(100f, 350f)] public float length;
    [Range(-50f, 50f)] public float rotation;
}

public class UI_TreeConnectHandler : MonoBehaviour
{
    private RectTransform rect => GetComponent<RectTransform>();
    [SerializeField] private UI_TreeConnectDetails[] connectionDetails;
    [SerializeField] private UI_TreeConnection[] connections;

    private Image connectionImage;
    private Color originalColor; // アンロック前の、灰色

    private void Awake()
    {
        if (connectionImage != null)
            originalColor = connectionImage.color;
    }

    private void OnValidate()
    {
        if (connectionDetails.Length <= 0)
            return;

        if (connectionDetails.Length != connections.Length)
        {
            Debug.Log("Amount of details should be same as amount of connections. - " + gameObject.name);
            return;
        }

        UpdateConnection();
    }

    public void UpdateConnection()
    {
        for (int i = 0; i < connectionDetails.Length; i++)
        {
            var detail = connectionDetails[i];
            var connection = connections[i];
            Vector2 targetPosition = connection.GetConnectionPoint(rect);
            Image connectionImage = connection.GetConnectionImage();

            connection.DirectConnection(detail.direction, detail.length, detail.rotation);

            if (detail.childNode == null)
                continue;

            detail.childNode.SetPosition(targetPosition);
            detail.childNode.SetConnectionImage(connectionImage);

            // オブジェクトを子階層の最後にする。foreachの中で行われることで、自然とソートされる
            //detail.childNode.transform.SetAsLastSibling(); 不具合があるのでコメントアウト



        }
    }

    public void UpdateAllConnections()
    {
        UpdateConnection();

        foreach(var node in connectionDetails)
        {
            if (node.childNode == null) continue;
            node.childNode?.UpdateAllConnections();
        }

    }


    public void UnlockConnectionImage(bool unlocked)
    {
        if (connectionImage == null)
            return;

        connectionImage.color = unlocked ? Color.white : originalColor;

    }

    public void SetConnectionImage(Image image) => connectionImage = image;

    public void SetPosition(Vector2 position) => rect.anchoredPosition = position;



}
