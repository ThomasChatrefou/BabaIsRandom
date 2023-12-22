using UnityEngine;

public class AddNewBranchModifier : IProceduralGraphModifier
{
    public bool Check(Graph graph, NodeSelector select)
    {
        return graph.IsValid
            && select.IsValid
            && graph.Contains(select.LastSelectedNodeId);
    }

    public void Execute(ref Graph graph, NodeSelector select)
    {
        Node parent = graph.GetNodeFromId(select.LastSelectedNodeId);
        Node newNode = graph.CreateNode();
        parent.Children.Add(newNode.Id);
        parent.Keys.Add(newNode.Id);
        Debug.Log($"[AddNewBranchModifier] Added node {newNode.AsciiName} in a new branch from parent {parent.AsciiName}");
    }
}
