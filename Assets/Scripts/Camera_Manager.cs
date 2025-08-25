using UnityEngine;
using UnityEngine.UI;

public class Camera_Manager : MonoBehaviour
{
    [Header("Cámaras")]
    public GameObject mainCamera;
    public GameObject topCamera;

    [Header("Botón de cámara")]
    public Button cameraButton;

    [Header("Sprites de botón")]
    public Sprite cameraNormalSprite;
    public Sprite cameraPressedSprite;

    private bool isTopCameraActive = false;

    void Start()
    {
        cameraButton.onClick.AddListener(OnCameraButtonPressed);
        SwitchToMainCamera();
    }

    void OnCameraButtonPressed()
    {
        if (isTopCameraActive)
            SwitchToMainCamera();
        else
            SwitchToTopCamera();
    }

    void SwitchToMainCamera()
    {
        mainCamera.SetActive(true);
        topCamera.SetActive(false);
        SetCameraButtonState(false);
        isTopCameraActive = false;
    }

    void SwitchToTopCamera()
    {
        mainCamera.SetActive(false);
        topCamera.SetActive(true);
        SetCameraButtonState(true);
        isTopCameraActive = true;
    }

    void SetCameraButtonState(bool isPressed)
    {
        Image img = cameraButton.GetComponent<Image>();
        img.sprite = isPressed ? cameraPressedSprite : cameraNormalSprite;
        cameraButton.interactable = true;
    }
}
