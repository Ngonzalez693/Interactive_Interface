using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public enum FurnitureType { Silla = 0, Sofa = 1, Mesa = 2 }

[System.Serializable]
public class FurnitureData
{
    public string nombre;
    public Sprite imagen;
    public int numBaldosas;
    public GameObject prefab;
    public FurnitureType tipo;
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

    private FurnitureData muebleActual;
    private int baldosasSeleccionadas = 1;

    public void MostrarMueblesPorTipoInt(int tipoInt)
    {
        Debug.Log("Botón de categoría presionado, tipo: " + tipoInt);
        FurnitureType tipo = (FurnitureType)tipoInt;
        MostrarMueblesPorTipo(tipo);
    }

    public void MostrarMueblesPorTipo(FurnitureType tipo)
    {
        var mueblesFiltrados = muebles.Where(m => m.tipo == tipo).ToArray();
        if (mueblesFiltrados.Length > 0)
        {
            MostrarMueble(mueblesFiltrados[0]);
        }
        else
        {
            Debug.LogWarning("No hay muebles del tipo: " + tipo);
        }
    }

    public void MostrarMueble(FurnitureData data)
    {
        muebleActual = data;
        furnitureImage.sprite = data.imagen;
        tileCountText.text = $"{data.numBaldosas}";
        furnitureNameText.text = data.nombre;
        ValidarSeleccion();
    }

    public void SetBaldosasSeleccionadas(int count)
    {
        baldosasSeleccionadas = count;
        ValidarSeleccion();
    }

    private void ValidarSeleccion()
    {
        int seleccionados = Tile_Manager.Instance.GetSelectedTiles().Count;
        placeButton.interactable = (muebleActual != null && seleccionados == muebleActual.numBaldosas);
    }

    public void ColocarMueble()
    {
        // Aquí va la lógica de instanciar el prefab del mueble en las baldosas seleccionadas
    }
}
