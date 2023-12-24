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

        // Instancier et positionner les éléments dans le GridLayoutGroup
        int currentIndex = 0;

        for (int row = 0; row < columnsPerRow.Length; row++)
        {
            // Définir le nombre de colonnes pour cette ligne
            gridLayoutGroup.constraintCount = columnsPerRow[row];

            for (int col = 0; col < columnsPerRow[row]; col++)
            {
                // Vérifier si nous avons atteint la fin de la liste des éléments
                if (currentIndex >= itemList.Count)
                {
                    return;
                }

                // Instancier l'élément
                GameObject item = Instantiate(itemPrefab, transform);

                // Définir le texte de l'élément (utilisez votre propre logique pour définir le texte)
                item.GetComponentInChildren<Text>().text = itemList[currentIndex];

                // Définir la position de l'élément
                RectTransform itemRect = item.GetComponent<RectTransform>();
                itemRect.anchoredPosition = new Vector2(col * gridLayoutGroup.cellSize.x, -row * gridLayoutGroup.cellSize.y);

                currentIndex++;
            }
        }
    }*/
}
