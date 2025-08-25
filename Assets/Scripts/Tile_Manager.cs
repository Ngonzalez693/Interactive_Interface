using UnityEngine;
using System.Collections.Generic;

public class Tile_Manager : MonoBehaviour
{
    public static Tile_Manager Instance;
    private List<Tile> selectedTiles = new List<Tile>();
    public GameObject furniturePanel;

    void Awake()
    {
        Instance = this;
    }

    public void SelectTile(Tile tile)
    {
        if (!selectedTiles.Contains(tile) && !tile.isOccupied)
        {
            selectedTiles.Add(tile);
            tile.SelectTile();
            UpdateFurniturePanel(); // <-- actualiza panel aquí
        }
    }

    public void DeselectTile(Tile tile)
    {
        if (selectedTiles.Contains(tile))
        {
            tile.DeselectTile();
            selectedTiles.Remove(tile);
            UpdateFurniturePanel(); // <-- y aquí
        }
    }

    public void DeselectAllTiles()
    {
        foreach (var tile in selectedTiles)
        {
            tile.DeselectTile();
        }
        selectedTiles.Clear();
        UpdateFurniturePanel(); // <-- también aquí
    }

    public List<Tile> GetSelectedTiles()
    {
        return selectedTiles;
    }

    public void UpdateFurniturePanel()
    {
        int count = selectedTiles.Count;

        if (count == 0)
        {
            furniturePanel.SetActive(false);
        }
        else if (count == 1)
        {
            furniturePanel.SetActive(true);
            // Mostrar muebles del tile seleccionado, por ejemplo:
            FurniturePanelUI panelUI = furniturePanel.GetComponent<FurniturePanelUI>();
            if (panelUI != null)
            {
                // Asume que el tile seleccionado tiene info de tipo mueble (ajusta según tu caso)
                panelUI.MostrarMueblesPorTipoInt(0); // ejemplo: todos o el tipo deseado
            }
        }
        else
        {
            // Si quieres mostrar algo distinto cuando hay múltiples tiles seleccionados
            furniturePanel.SetActive(true);
            // O furniturePanel.SetActive(false); según cómo quieras manejarlo.
        }
    }
}
