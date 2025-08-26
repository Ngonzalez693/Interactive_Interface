using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public enum FurnitureType { Silla = 0, Sofa = 1, Mesa = 2 }

[System.Serializable]
public class FurnitureData
{
    public string name;
    public Sprite image;
    public int numTiles;
    public GameObject prefab;
    public FurnitureType type;
    public Vector3 rotation = Vector3.zero;
}

public class FurniturePanelUI : MonoBehaviour
{
    [Header("Datos de muebles")]
    public FurnitureData[] muebles;

    [Header("Referencias UI")]
    public Image furnitureImage;
    public TMP_Text tileCountText;
    public TMP_Text furnitureNameText;
    public Button placeButton;
    public TMP_Text rotationText;

    private FurnitureData actualFurniture;
    private int baldosasSeleccionadas = 1;

    void Start()
    {
        placeButton.onClick.AddListener(OnPlaceButtonClicked);
    }
    public void ShowFurnitureByTypeInt(int typeInt)
    {
        FurnitureType type = (FurnitureType)typeInt;
        ShowFurnitureByType(type);
    }

    public void ShowFurnitureByType(FurnitureType type)
    {
        var FilteredFurniture = muebles.Where(m => m.type == type).ToArray();
        if (FilteredFurniture.Length > 0)
        {
            ShowFurniture(FilteredFurniture[0]);
        }
        else
        {
            Debug.LogWarning("No hay muebles del tipo: " + type);
        }
    }

    public void ShowFurniture(FurnitureData data)
    {
        actualFurniture = data;
        furnitureImage.sprite = data.image;
        tileCountText.text = $"{data.numTiles}";
        furnitureNameText.text = data.name;
        ValidateSelection();

        Tile_Manager.Instance.SetFurnitureSelected(data);
    }

    public void SetBaldosasSeleccionadas(int count)
    {
        baldosasSeleccionadas = count;
        ValidateSelection();
    }

    public void ValidateSelection()
    {
        int seleccionados = Tile_Manager.Instance.GetSelectedTiles().Count;
        placeButton.interactable = (actualFurniture != null && seleccionados == actualFurniture.numTiles);

        // Mostrar rotación actual
        if (rotationText != null)
        {
            rotationText.text = $"Rotación: {Tile_Manager.Instance.currentRotationY}° (Presiona R)";
        }
    }

    private void OnPlaceButtonClicked()
    {
        Tile_Manager.Instance.PlaceFurniture();
    }

    public void RefreshRotationText()
    {
        if (rotationText != null)
        {
            rotationText.text = $"Rotación: {Tile_Manager.Instance.currentRotationY}° (Presiona R)";
        }
    }

}
