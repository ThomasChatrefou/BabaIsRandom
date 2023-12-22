using UnityEngine;

public class AddInBetweenModifier : IProceduralGraphModifier
{
    public bool Check(Graph graph, NodeSelector select)
    {
        return graph.IsValid
            && select.HasMultiSelection
            && graph.Contains(select.LastSelectedNodeId)
            && graph.Contains(select.LastPreselectedNodeId);
    }

    public void Execute(ref Graph graph, NodeSelector select)
    {
        Node parent = graph.GetNodeFromId(select.LastSelectedNodeId);
        Node child = graph.GetNodeFromId(select.LastPreselectedNodeId);
        if (parent.Children.Contains(child.Id)) parent.Children.Remove(child.Id);
        Node newNode = graph.CreateNode();
        parent.Children.Add(newNode.Id);
        parent.Keys.Add(newNode.Id);
        newNode.Children.Add(child.Id);
        Debug.Log($"[AddInBetweenModifier] Added node {newNode.AsciiName} in between parent node {parent.AsciiName} and child node {child.AsciiName}");
    }
}
