using UnityEngine;

public class AddInBetweenModifier : IProceduralGraphModifier
{
    public bool Check(Graph graph, NodeSelector select)
    {
        Node last = graph.GetNodeFromId(select.LastSelectedNodeId);
        Node previous = graph.GetNodeFromId(select.LastPreSelectedNodeId);
        bool multiSelectionHasDirectLink = last.Children.Contains(previous.Id) || previous.Children.Contains(last.Id);
        return graph.IsValid
            && select.HasMultiSelection
            && graph.Contains(select.LastSelectedNodeId)
            && graph.Contains(select.LastPreSelectedNodeId)
            && multiSelectionHasDirectLink;
    }

    public void Execute(ref Graph graph, NodeSelector select)
    {
        Node last = graph.GetNodeFromId(select.LastSelectedNodeId);
        Node previous = graph.GetNodeFromId(select.LastPreSelectedNodeId);
        Node parent;
        Node child;
        if (last.Children.Contains(previous.Id))
        {
            parent = last;
            child = previous;
        }
        else
        {
            parent = previous;
            child = last;
        }

        if (parent.Children.Contains(child.Id)) parent.Children.Remove(child.Id);
        Node newNode = graph.CreateNode();
        parent.Children.Add(newNode.Id);
        parent.Keys.Add(newNode.Id);
        newNode.Children.Add(child.Id);
        Debug.Log($"[AddInBetweenModifier] Added node {newNode.AsciiName} in between parent node {parent.AsciiName} and child node {child.AsciiName}");
    }
}
