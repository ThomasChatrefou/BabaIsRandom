using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    /*Ui*/
    public GameObject templateTextLogs;
    public GameObject templatePaths;
    public GameObject templateRootLog;
    public GameObject templateRootPath;
    public GameObject templateTreeButton;
    public GameObject TabNodes;

    /*generator*/
    public ProceduralHandler ProHand;
    public TMP_InputField ProHandSeed;
    public TMP_InputField ProHandNodeCount;
    public ProceduralDebugTranslator translatorInstance;



    private void ShowOutputLogs(List<string> outputLogs, List<Node> nodes)
    {
        foreach (Transform enfant in templateRootLog.transform)
        {
            // Destruction de l'enfant
            Destroy(enfant.gameObject);
        }
        foreach (Transform enfant in TabNodes.transform)
        {
            // Destruction de l'enfant
            Destroy(enfant.gameObject);
        }
        Debug.Log("Evenement logs déclenché");
        foreach (string logs in outputLogs)
        {

            GameObject textLog = Instantiate(templateTextLogs, templateRootLog.transform);
            textLog.GetComponent<TextMeshProUGUI>().text = logs;

            //ajouter bouton au gameobject -> backup graph -> peut etre meme dans le template 
            //bouton verifie si tu appuies sur ctrl pour choisir plusieurs nodes 
        }
        List<List<Node>> treeLevels = new List<List<Node>>();
        GridLayoutGroup grid = TabNodes.GetComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = 1;
        PrintTree(nodes, treeLevels);
        Debug.Log(treeLevels.Count);
        for (int level = 0; level < treeLevels.Count; level++)
        {
            // Créez un GameObject pour chaque niveau
            GameObject levelObject = new GameObject("Level" + level);
            levelObject.transform.SetParent(TabNodes.transform);
            GridLayoutGroup levelGrid = levelObject.AddComponent<GridLayoutGroup>();
            levelGrid.cellSize = grid.cellSize;

            // Définissez la propriété constraintCount pour ce niveau
            levelGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            levelGrid.constraintCount = treeLevels[level].Count;
            foreach (Node node in treeLevels[level])
            {


                GameObject TreeButton = Instantiate(templateTreeButton, levelObject.transform);
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
        //

    }
    private void ShowPaths(List<string> paths)
    {
        foreach (Transform enfant in templateRootPath.transform)
        {
            // Destruction de l'enfant
            Destroy(enfant.gameObject);
        }
        //Debug.Log("Evenement path déclenché");
        foreach (string path in paths)
        {

            GameObject textPath = Instantiate(templateTextLogs, templateRootPath.transform);
            textPath.GetComponent<TextMeshProUGUI>().text = path;
            //ajouter bouton au gameobject -> backup graph -> peut etre meme dans le template 
            //bouton verifie si tu appuies sur ctrl pour choisir plusieurs nodes 
        }
    }
    public void UseRandomSeed()
    {
        ProHand.UseCustomSeed = !ProHand.UseCustomSeed;
    }
    private void OnEnable()
    {
        ProHandSeed.text = ProHand.Seed.ToString();
        ProceduralGenerator.Input Input = ProHand.GetInput();
        Debug.Log(Input.NodeCountRange);
        ProHandNodeCount.text = Input.NodeCountRange.x.ToString();
        
        
        /*abonnement aux event*/
        //translatorInstance = new ProceduralDebugTranslator();
        translatorInstance.OnGraphTranslated += ShowOutputLogs;
        translatorInstance.OnSolutionTranslated += ShowPaths;
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

    List<List<Node>> PrintTree(List<Node> nodes, List<List<Node>> treeLevels)
    {

        // Initialiser la première ligne avec le nœud racine (A)
        treeLevels.Add(new List<Node> { nodes[0] });

        // Construire les niveaux suivants
        for (int level = 0; level < treeLevels.Count; level++)
        {
            List<Node> currentLevel = treeLevels[level];
            List<Node> nextLevel = new List<Node>();

            foreach (Node node in currentLevel)
            {
                foreach (int childId in node.Children)
                {
                    Node childNode = nodes.Find(n => n.Id == childId);
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
