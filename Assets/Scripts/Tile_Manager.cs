using UnityEngine;
using System.Collections.Generic;

public class Tile_Manager : MonoBehaviour
{
    public static Tile_Manager Instance;
    private List<Tile> selectedTiles = new List<Tile>();
    public GameObject furniturePanel;
    private FurnitureData furnitureSelected;
    public Vector3 furnitureOffset = Vector3.zero;

    [HideInInspector]
    public float currentRotationY = 0f;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Detectar tecla R para rotar
        if (Input.GetKeyDown(KeyCode.R) && furnitureSelected != null && selectedTiles.Count > 0)
        {
            RotateFurniture();
        }
    }

    public void SelectTile(Tile tile)
    {
        if (!selectedTiles.Contains(tile) && !tile.isOccupied)
        {
            selectedTiles.Add(tile);
            tile.SelectTile();
            UpdateFurniturePanel();
        }
    }

    public void DeselectTile(Tile tile)
    {
        if (selectedTiles.Contains(tile))
        {
            tile.DeselectTile();
            selectedTiles.Remove(tile);
            UpdateFurniturePanel();
        }
    }

    public void DeselectAllTiles()
    {
        foreach (var tile in selectedTiles)
            tile.DeselectTile();
        selectedTiles.Clear();
        currentRotationY = 0f; // Reset rotación al deseleccionar
        UpdateFurniturePanel();
    }

    public List<Tile> GetSelectedTiles()
    {
        return selectedTiles;
    }

    public void SetFurnitureSelected(FurnitureData mueble)
    {
        furnitureSelected = mueble;
        currentRotationY = 0f; // Reset rotación al cambiar mueble
        UpdateFurniturePanel();
    }

    // Rotar el mueble seleccionado
    private void RotateFurniture()
    {
        if (furnitureSelected.numTiles == 1)
        {
            // Muebles de 1 tile: rotar de 90 en 90 grados
            currentRotationY += 90f;
        }
        else
        {
            // Muebles de 2+ tiles: rotar 180 grados
            currentRotationY += 180f;
        }

        // Mantener rotación entre 0-360
        if (currentRotationY >= 360f)
            currentRotationY -= 360f;

        Debug.Log($"Rotación actual: {currentRotationY}°");

        if (furniturePanel != null)
        {
            var panelUI = furniturePanel.GetComponent<FurniturePanelUI>();
            if (panelUI != null)
            {
                panelUI.RefreshRotationText();
            }
        }
    }

    // Detectar orientación automática de los tiles seleccionados
    private bool AreHorizontallyAligned()
    {
        if (selectedTiles.Count < 2) return false;

        // Verificar si todos los tiles están en la misma fila (mismo Y)
        float firstY = selectedTiles[0].transform.position.z; // En Unity, Z es como Y en 2D para grids
        foreach (var tile in selectedTiles)
        {
            if (Mathf.Abs(tile.transform.position.z - firstY) > 0.1f)
                return false;
        }
        return true;
    }

    private bool AreVerticallyAligned()
    {
        if (selectedTiles.Count < 2) return false;

        // Verificar si todos los tiles están en la misma columna (mismo X)
        float firstX = selectedTiles[0].transform.position.x;
        foreach (var tile in selectedTiles)
        {
            if (Mathf.Abs(tile.transform.position.x - firstX) > 0.1f)
                return false;
        }
        return true;
    }

    public void PlaceFurniture()
    {
        if (furnitureSelected == null) return;

        int count = selectedTiles.Count;
        if (count != furnitureSelected.numTiles)
        {
            Debug.LogWarning("Debes seleccionar exactamente " + furnitureSelected.numTiles + " baldosas.");
            return;
        }

        // Calcular posición promedio
        Vector3 sum = Vector3.zero;
        foreach (var tile in selectedTiles)
            sum += tile.transform.position;
        Vector3 center = sum / count;

        // Calcular rotación final
        float finalRotation = currentRotationY;

        // Para muebles de 2+ tiles, detectar orientación automática
        if (furnitureSelected.numTiles > 1)
        {
            if (AreHorizontallyAligned())
            {
                // Tiles horizontales: mueble debe estar horizontal (0° o 180°)
                finalRotation += 0f; // No ajustar extra
            }
            else if (AreVerticallyAligned())
            {
                // Tiles verticales: mueble debe estar vertical (90° o 270°)
                finalRotation += 90f;
            }
        }

        // Combinar rotación del prefab + rotación manual + orientación automática
        Vector3 totalRotation = furnitureSelected.rotation + new Vector3(0, finalRotation, 0);
        Quaternion furnitureRotation = Quaternion.Euler(totalRotation);

        GameObject instance = Instantiate(furnitureSelected.prefab, center + furnitureOffset, furnitureRotation);

        // Marcar tiles como ocupados
        foreach (var tile in selectedTiles)
        {
            tile.isOccupied = true;
            tile.currentFurniture = instance;
        }

        DeselectAllTiles();
    }

    public void UpdateFurniturePanel()
    {
        int count = selectedTiles.Count;
        if (count == 0)
        {
            furniturePanel.SetActive(false);
        }
        else
        {
            furniturePanel.SetActive(true);
        }
    }
    
}
