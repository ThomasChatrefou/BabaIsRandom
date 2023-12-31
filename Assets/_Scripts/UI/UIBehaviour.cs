using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    /*Ui*/
    public GameObject TemplateTextLogs;
    public GameObject TemplatePaths; // This is never used
    public GameObject TemplateRootLog;
    public GameObject TemplateRootPath;
    public GameObject TemplateTreeButton;
    public GameObject TabNodes;

    /*generator*/
    public ProceduralHandler ProHand;
    public TMP_InputField ProHandSeed;
    public ProceduralDebugTranslator TranslatorInstance;
    public TMP_InputField ProHandNodeCount;

    public void UseRandomSeed()
    {
        ProHand.UseCustomSeed = !ProHand.UseCustomSeed;
    }

    public void ChangeSeed()
    {
        if (int.TryParse(ProHandSeed.text, out int seed))
        {
            ProHand.Seed = seed;
        }
    }

    public void GetSeed()
    {
        ProHandSeed.text = ProHand.Seed.ToString();
    }

    // The tree building should be separated in a different method
    private void ShowOutputLogs(List<string> outputLogs, Graph graph)
    {
        foreach (Transform enfant in TemplateRootLog.transform)
        {
            // Destruction de l'enfant
            Destroy(enfant.gameObject);
        }
        foreach (Transform enfant in TabNodes.transform)
        {
            // Destruction de l'enfant
            Destroy(enfant.gameObject);
        }
        Debug.Log("Evenement logs d�clench�");
        foreach (string logs in outputLogs)
        {

            GameObject textLog = Instantiate(TemplateTextLogs, TemplateRootLog.transform);
            textLog.GetComponent<TextMeshProUGUI>().text = logs;

            //ajouter bouton au gameobject -> backup graph -> peut etre meme dans le template 
            //bouton verifie si tu appuies sur ctrl pour choisir plusieurs nodes 
        }
        List<List<Node>> treeLevels = new List<List<Node>>();
        GridLayoutGroup grid = TabNodes.GetComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 1;
        PrintTree(graph, treeLevels);
        Debug.Log(treeLevels.Count);
        for (int level = 0; level < treeLevels.Count; level++)
        {
            // Cr�ez un GameObject pour chaque niveau
            GameObject levelObject = new GameObject("Level" + level);
            levelObject.transform.SetParent(TabNodes.transform);
            GridLayoutGroup levelGrid = levelObject.AddComponent<GridLayoutGroup>();
            levelGrid.cellSize = grid.cellSize;

            // D�finissez la propri�t� constraintCount pour ce niveau
            levelGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            levelGrid.constraintCount = treeLevels[level].Count;
            foreach (Node node in treeLevels[level])
            {
                GameObject TreeButton = Instantiate(TemplateTreeButton, levelObject.transform);
                TreeButton.GetComponentInChildren<TextMeshProUGUI>().text = node.AsciiName.ToString();
            }
        }
        // test 
        for (int level = 0; level < treeLevels.Count; level++)
        {
            string row = "";
            foreach (Node node in treeLevels[level])
            {
                row += node.AsciiName + " ";
            }
            Debug.Log(row);
        }
    }

    // This can be refacto with ShowOutputLogs
    private void ShowPaths(List<string> paths)
    {
        foreach (Transform enfant in TemplateRootPath.transform)
        {
            // Destruction de l'enfant
            Destroy(enfant.gameObject);
        }
        //Debug.Log("Evenement path d�clench�");
        foreach (string path in paths)
        {
            GameObject textPath = Instantiate(TemplateTextLogs, TemplateRootPath.transform);
            textPath.GetComponent<TextMeshProUGUI>().text = path;
            //ajouter bouton au gameobject -> backup graph -> peut etre meme dans le template 
            //bouton verifie si tu appuies sur ctrl pour choisir plusieurs nodes 
        }
    }

    private void OnEnable()
    {
        ProHandSeed.text = ProHand.Seed.ToString();
        ProceduralGenerator.Input Input = ProHand.GetInput();
        Debug.Log(Input.NodeCountRange);
        ProHandNodeCount.text = Input.NodeCountRange.x.ToString();
        
        
        /*abonnement aux event*/
        //translatorInstance = new ProceduralDebugTranslator();
        TranslatorInstance.OnGraphTranslated += ShowOutputLogs;
        TranslatorInstance.OnSolutionTranslated += ShowPaths;
    }
    public void ChangeSeed()
    {
        if (int.TryParse(ProHandSeed.text, out int seed))
        {
            ProHand.Seed = seed;
        }
    }
    public void ChangeInput()
    {
        if (int.TryParse(ProHandNodeCount.text, out int count))
        {
            ProHand.ProGenInput.NodeCountRange.x = count;
            ProHand.ProGenInput.NodeCountRange.y = count;
        }
    }
    public void getSeed()
    {
        ProHandSeed.text = ProHand.Seed.ToString();
    }

    private void OnDisable()
    {
        TranslatorInstance.OnGraphTranslated -= ShowOutputLogs;
        TranslatorInstance.OnSolutionTranslated -= ShowPaths;
    }

    private List<List<Node>> PrintTree(Graph graph, List<List<Node>> treeLevels)
    {
        // Initialiser la premi�re ligne avec le n�ud racine (A)
        treeLevels.Add(new List<Node> { graph.GetNodeFromId(0) });

        // Construire les niveaux suivants
        for (int level = 0; level < treeLevels.Count; level++)
        {
            List<Node> currentLevel = treeLevels[level];
            List<Node> nextLevel = new List<Node>();

            foreach (Node node in currentLevel)
            {
                foreach (int childId in node.Children)
                {
                    Node childNode = graph.GetNodeFromId(childId);
                    nextLevel.Add(childNode);
                }
            }

            if (nextLevel.Count > 0)
            {
                treeLevels.Add(nextLevel);
            }
        }
        /*for (int level = 0; level < treeLevels.Count; level++)
        {
            string row = "";
            foreach (Node node in treeLevels[level])
            {
                row += node.AsciiName + " ";
            }
            Debug.Log(row);
        }*/
        return treeLevels;

        // Afficher le tableau
    }
}
