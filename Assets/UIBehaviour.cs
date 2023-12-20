using System.Collections;
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
    public GameObject TabNodes;
    /*generator*/
    public ProceduralHandler ProHand;
    public TMP_InputField ProHandSeed;
    public ProceduralDebugTranslator translatorInstance;

    private void ShowOutputLogs(List<string> outputLogs,List<Node> nodes)
  {
        Debug.Log("Evenement logs déclenché");
        foreach (string logs in outputLogs)
        {
            
            GameObject textLog = Instantiate(templateTextLogs, templateRootLog.transform);
            textLog.GetComponent<TextMeshProUGUI>().text = logs;
            
            //ajouter bouton au gameobject -> backup graph -> peut etre meme dans le template 
            //bouton verifie si tu appuies sur ctrl pour choisir plusieurs nodes 
        }
        
            // compter le nombre d'enfant 
            List<int> ColPerRow = new List<int>();
            ColPerRow.Add(1);
        ColPerRow.Add(nodes[0].Children.Count);
        
  }
    private void ShowPaths(List<string> paths)
    {
        Debug.Log("Evenement path déclenché");
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
        ProHand._useCustomSeed = !ProHand._useCustomSeed;
    }
    private void OnEnable()
    {
        ProHandSeed.text = ProHand._seed.ToString();

        /*abonnement aux event*/
        //translatorInstance = new ProceduralDebugTranslator();
        translatorInstance.OnGraphTranslated += ShowOutputLogs;
        translatorInstance.OnSolutionTranslated += ShowPaths;
    }
    public void ChangeSeed()
    {
        if (int.TryParse(ProHandSeed.text, out int seed))
        {
            ProHand._seed = seed;
        }
        
    }
}
