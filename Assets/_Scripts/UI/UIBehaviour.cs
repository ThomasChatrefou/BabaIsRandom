using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    /*Ui*/
    public GameObject TemplateTextLogs;
    public GameObject TemplateRootLog;
    public GameObject TemplateRootPath;
    public GameObject TemplateTreeLevel;
    public GameObject TemplateTreeButton;
    public GameObject TemplateTreeEmptySpace;
    public GameObject TabNodes;

    /*generator*/
    public ProceduralHandler ProHand;
    public TMP_InputField ProHandSeed;
    public ProceduralDebugTranslator TranslatorInstance;
    public TMP_InputField ProHandNodeCount;

    public void ChangeInput()
    {
        if (int.TryParse(ProHandNodeCount.text, out int count))
        {
            ProHand.SetNodesCount_Clamped(ref count);
            ProHandNodeCount.text = count.ToString();
        }
        else
        {
            ProHand.ResetGeneratorInput();
        }
    }

    public void ToggleCustomSeed()
    {
        ProHand.UseCustomSeed = !ProHand.UseCustomSeed;
        ProHandSeed.interactable = ProHand.UseCustomSeed;
    }

    public void ChangeSeed()
    {
        if (int.TryParse(ProHandSeed.text, out int seed))
        {
            ProHand.SetSeed_Clamped(ref seed);
            ProHandSeed.text = seed.ToString();
        }
    }

    public void UpdateSeedField()
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
        //Debug.Log("Evenement logs d�clench�");
        foreach (string logs in outputLogs)
        {

            GameObject textLog = Instantiate(TemplateTextLogs, TemplateRootLog.transform);
            textLog.GetComponent<TextMeshProUGUI>().text = logs;

            //ajouter bouton au gameobject -> backup graph -> peut etre meme dans le template 
            //bouton verifie si tu appuies sur ctrl pour choisir plusieurs nodes 
        }
        List<List<Node>> treeLevels = new();
        BuildTree(graph, treeLevels);
        //Debug.Log(treeLevels.Count);

        ComputeTreeEmptySpaces(out List<List<Vector2Int>> emptySpacesByNode, treeLevels, graph);

        for (int levelIdx = 0; levelIdx < treeLevels.Count; ++levelIdx)
        {
            GameObject treeLevelGO = Instantiate(TemplateTreeLevel, TabNodes.transform);

            List<Node> currentLevel = treeLevels[levelIdx];
            List<Vector2Int> currentlevelSpaces = emptySpacesByNode[levelIdx];
            for (int nodeIdx = 0; nodeIdx < currentLevel.Count; ++nodeIdx)
            {
                Node currentNode = currentLevel[nodeIdx];
                Vector2Int currentNodeSpace = currentlevelSpaces[nodeIdx];

                FillWithEmptySpace(currentNodeSpace.x, treeLevelGO.transform);

                GameObject treeButton = Instantiate(TemplateTreeButton, treeLevelGO.transform);
                treeButton.GetComponentInChildren<TextMeshProUGUI>().text = currentNode.AsciiName.ToString();
                if (treeButton.TryGetComponent<NodeSelectorButton>(out var nodeSelectorButton))
                {
                    nodeSelectorButton.NodeId = currentNode.Id;
                }

                FillWithEmptySpace(currentNodeSpace.y, treeLevelGO.transform);
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
            //Debug.Log(row);
        }

        if (!ProHandNodeCount.IsInteractable()) ProHandNodeCount.text = outputLogs.Count.ToString();
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
        ProHandSeed.interactable = ProHand.UseCustomSeed;

        ProceduralGenerator.Input Input = ProHand.ProGenInput;
        
        /*abonnement aux event*/
        //translatorInstance = new ProceduralDebugTranslator();
        TranslatorInstance.OnGraphTranslated += ShowOutputLogs;
        TranslatorInstance.OnSolutionTranslated += ShowPaths;
    }

    private void OnDisable()
    {
        TranslatorInstance.OnGraphTranslated -= ShowOutputLogs;
        TranslatorInstance.OnSolutionTranslated -= ShowPaths;
    }

    private List<List<Node>> BuildTree(Graph graph, List<List<Node>> treeLevels)
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

    private void ComputeTreeEmptySpaces(out List<List<Vector2Int>> result, List<List<Node>> treeLevels, Graph graph)
    {
        // emptySpacesByNode[i,j].x is the space needed before the node and y is the space needed after
        result = new(treeLevels.Count);
        foreach (List<Node> level in treeLevels)
        {
            List<Vector2Int> currentlevelSpaces = new(level.Count);
            foreach (Node node in level)
            {
                currentlevelSpaces.Add(Vector2Int.zero);
            }
            result.Add(currentlevelSpaces);
        }

        // Compute space needed BEFORE each node : we need to parse the tree from top to bottom
        for (int levelIdx = 1; levelIdx < treeLevels.Count; ++levelIdx)
        {
            List<Node> currentLevel = treeLevels[levelIdx];
            List<Node> parentLevel = treeLevels[levelIdx - 1];
            List<Vector2Int> currentlevelSpaces = result[levelIdx];
            List<Vector2Int> parentlevelSpaces = result[levelIdx - 1];
            int lastParentIdx = -1;

            for (int nodeIdx = 0; nodeIdx < currentLevel.Count; ++nodeIdx)
            {
                Node currentNode = currentLevel[nodeIdx];
                Node currentParent = graph.FindParentNode(currentNode);
                int myParentIdx = parentLevel.FindIndex(x => x == currentParent);

                if (myParentIdx > lastParentIdx)
                {
                    int parentSpace = parentlevelSpaces[myParentIdx].x;
                    if (nodeIdx == 0)
                    {
                        for (int previousParentIdx = 0; previousParentIdx < myParentIdx; ++previousParentIdx)
                        {
                            parentSpace += parentlevelSpaces[previousParentIdx].x;
                        }
                    }
                    int spaceToAdd = myParentIdx - lastParentIdx - 1;
                    int spaceBeforeCurrentNode = parentSpace + spaceToAdd;
                    currentlevelSpaces[nodeIdx] = new Vector2Int(spaceBeforeCurrentNode, currentlevelSpaces[nodeIdx].y);
                    lastParentIdx = myParentIdx;
                }
            }
        }

        // Compute space needed AFTER each node : we need to parse the tree from bottom to top
        for (int levelIdx = treeLevels.Count - 2; levelIdx >= 0; --levelIdx)
        {
            List<Node> currentLevel = treeLevels[levelIdx];
            List<Node> childrenLevel = treeLevels[levelIdx + 1];
            List<Vector2Int> currentlevelSpaces = result[levelIdx];
            List<Vector2Int> childrenlevelSpaces = result[levelIdx + 1];

            for (int nodeIdx = 0; nodeIdx < currentLevel.Count; ++nodeIdx)
            {
                Node currentNode = currentLevel[nodeIdx];
                int spaceAfterNode = Mathf.Max(0, currentNode.Children.Count - 1);

                foreach (int child in currentNode.Children)
                {
                    int childIdx = childrenLevel.FindIndex((x) => x == graph.GetNodeFromId(child));
                    spaceAfterNode += childrenlevelSpaces[childIdx].y;
                }
                currentlevelSpaces[nodeIdx] = new Vector2Int(currentlevelSpaces[nodeIdx].x, spaceAfterNode);
            }
        }
    }

    private void FillWithEmptySpace(int spaceCount, Transform parent)
    {
        for (int i = 0; i < spaceCount; ++i)
        {
            Instantiate(TemplateTreeEmptySpace, parent);
        }
    }
}
