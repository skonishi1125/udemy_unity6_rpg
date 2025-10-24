using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth;

    [SerializeField] private ParallaxLayer[] backgroundLayers;

    private void Awake()
    {
        mainCamera = Camera.main;
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        CalculateImageLength();
    }

    private void FixedUpdate()
    {
        float currentCameraPositionx = mainCamera.transform.position.x; // メインカメラの現在位置xの取得
        float distanceToMove = currentCameraPositionx - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionx;

        float cameraLeftEdge = currentCameraPositionx - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionx + cameraHalfWidth;

        foreach(ParallaxLayer layer in backgroundLayers)
        {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }
    }

    private void CalculateImageLength()
    {
        foreach (ParallaxLayer layer in backgroundLayers)
        {
            layer.CalculateImageWidth();
        }
    }
}
