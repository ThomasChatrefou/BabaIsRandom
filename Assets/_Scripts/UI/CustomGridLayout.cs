using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class CustomGridLayout : MonoBehaviour
{
    public GridLayoutGroup GridLayoutGroup;
    public GameObject ItemPrefab;
    public List<string> ItemList;

    // Should we keep this ?
    /*
    void Start()
    {
        PopulateGridLayout();
    }

    void PopulateGridLayout()
    {
        
        // Nombre de colonnes pour chaque ligne
        int[] columnsPerRow = { 2, 4, 2 };

        // Instancier et positionner les �l�ments dans le GridLayoutGroup
        int currentIndex = 0;

        for (int row = 0; row < columnsPerRow.Length; row++)
        {
            // D�finir le nombre de colonnes pour cette ligne
            gridLayoutGroup.constraintCount = columnsPerRow[row];

            for (int col = 0; col < columnsPerRow[row]; col++)
            {
                // V�rifier si nous avons atteint la fin de la liste des �l�ments
                if (currentIndex >= itemList.Count)
                {
                    return;
                }

                // Instancier l'�l�ment
                GameObject item = Instantiate(itemPrefab, transform);

                // D�finir le texte de l'�l�ment (utilisez votre propre logique pour d�finir le texte)
                item.GetComponentInChildren<Text>().text = itemList[currentIndex];

                // D�finir la position de l'�l�ment
                RectTransform itemRect = item.GetComponent<RectTransform>();
                itemRect.anchoredPosition = new Vector2(col * gridLayoutGroup.cellSize.x, -row * gridLayoutGroup.cellSize.y);

                currentIndex++;
            }
        }
    }*/
}
