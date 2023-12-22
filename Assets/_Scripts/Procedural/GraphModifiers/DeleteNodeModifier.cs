using UnityEngine;

public class DeleteNodeModifier : IProceduralGraphModifier
{
    public bool Check(Graph graph, NodeSelector select)
    {
        return graph.IsValid
            && select.IsValid
            && graph.Contains(select.LastSelectedNodeId)
            && !graph.IsRootNode(select.LastSelectedNodeId);
    }

    public void Execute(ref Graph graph, NodeSelector select)
    {
        Node toDelete = graph.GetNodeFromId(select.LastSelectedNodeId);
        Node parent = graph.FindParentNode(toDelete);
        Node keyLocation = graph.FindKeyLocation(toDelete);
        parent.Children.Remove(toDelete.Id);
        parent.Children.AddRange(toDelete.Children);
        parent.Keys.AddRange(toDelete.Keys);
        keyLocation.Keys.Remove(toDelete.Id);
        graph.Remove(toDelete);
        Debug.Log($"[DeleteNodeModifier] Deleted node {toDelete.AsciiName}, placing its keys to the parent {parent.AsciiName}");
    }
}
