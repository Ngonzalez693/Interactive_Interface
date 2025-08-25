using UnityEngine;
using UnityEngine.UI;

public class Views_Manager : MonoBehaviour
{
    [Header("Botones de vista")]
    public Button halfWallsButton;
    public Button smallWallsButton;
    public Button noWallsButton;

    [Header("Sprites de botones")]
    public Sprite halfWallsNormalSprite;
    public Sprite halfWallsPressedSprite;
    public Sprite smallWallsNormalSprite;
    public Sprite smallWallsPressedSprite;
    public Sprite noWallsNormalSprite;
    public Sprite noWallsPressedSprite;

    [Header("Objetos de la Habitación")]
    public GameObject smallWall1;
    public GameObject smallWall2;
    public GameObject smallWall3;
    public GameObject smallWall4;
    public GameObject wall1;
    public GameObject wall2;

    void Start()
    {
        halfWallsButton.onClick.AddListener(() => SetActiveButton(halfWallsButton));
        smallWallsButton.onClick.AddListener(() => SetActiveButton(smallWallsButton));
        noWallsButton.onClick.AddListener(() => SetActiveButton(noWallsButton));
        
        SetActiveButton(halfWallsButton); // Estado inicial
        
    }

    void SetActiveButton(Button activeButton)
    {
        SetButtonState(halfWallsButton, activeButton == halfWallsButton, halfWallsNormalSprite, halfWallsPressedSprite);
        SetButtonState(smallWallsButton, activeButton == smallWallsButton, smallWallsNormalSprite, smallWallsPressedSprite);
        SetButtonState(noWallsButton, activeButton == noWallsButton, noWallsNormalSprite, noWallsPressedSprite);

        UpdateRoomObjects(activeButton);
    }

    void SetButtonState(Button button, bool isPressed, Sprite normalSprite, Sprite pressedSprite)
    {
        Image img = button.GetComponent<Image>();
        img.sprite = isPressed ? pressedSprite : normalSprite;
        button.interactable = !isPressed;
    }

    void UpdateRoomObjects(Button activeButton)
    {
        if (activeButton == halfWallsButton)
        {
            smallWall1.SetActive(true);
            smallWall2.SetActive(true);
            smallWall3.SetActive(true);
            smallWall4.SetActive(true);
            wall1.SetActive(true);
            wall2.SetActive(true);
        }
        else if (activeButton == smallWallsButton)
        {
            smallWall1.SetActive(true);
            smallWall2.SetActive(true);
            smallWall3.SetActive(true);
            smallWall4.SetActive(true);
            wall1.SetActive(false);
            wall2.SetActive(false);
        }
        else if (activeButton == noWallsButton)
        {
            smallWall1.SetActive(false);
            smallWall2.SetActive(false);
            smallWall3.SetActive(false);
            smallWall4.SetActive(false);
            wall1.SetActive(false);
            wall2.SetActive(false);
        }
    }
}
