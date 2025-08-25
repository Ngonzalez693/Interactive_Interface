using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public GameObject FurniturePanel;
    public bool isOccupied = false;
    public GameObject currentFurniture = null;
    private Renderer rend;
    private Color originalColor;
    public Color selectedColor = Color.green;

    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseDown()
    {
        Debug.Log("Tile clicked: " + gameObject.name);

        if (!isOccupied)
        {
            if (Tile_Manager.Instance.GetSelectedTiles().Contains(this))
            {
                Tile_Manager.Instance.DeselectTile(this);
            }
            else
            {
                Tile_Manager.Instance.SelectTile(this);
            }
        }
    }


    public void SelectTile()
    {
        rend.material.color = selectedColor;
    }


    public void DeselectTile()
    {
        rend.material.color = originalColor;
    }
}