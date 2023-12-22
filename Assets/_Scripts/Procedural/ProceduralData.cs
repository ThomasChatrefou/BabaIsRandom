using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Node
{
    public static int ASCII_CODE_ALPHABET_START = 65;
    public static int NULL_NODE_ID = -1;
    public static Node NULL_NODE = new(NULL_NODE_ID);

    public int Id { get; private set; }
    public char AsciiName { get; private set; }
    public List<int> Children { get; private set; } = new List<int>();
    public List<int> Keys { get; private set; } = new List<int>();

    public Node(int id)
    {
        Id = id;
        AsciiName = (char)(id + ASCII_CODE_ALPHABET_START);
    }
}

public class Graph
{
    public bool IsValid { get { return Nodes.Count > 0; } }
    public Dictionary<int, Node> Nodes { get; private set; } = new();

    public Graph(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            Nodes.Add(node.Id, node);
            _isIdAvailable.Add(false);
        }
    }

    public Node GetNodeFromAscii(char code)
    {
        return GetNodeFromId(GetNodeIdFromAscii(code));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetNodeIdFromAscii(char code)
    {
        return code - Node.ASCII_CODE_ALPHABET_START;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Node GetNodeFromId(int nodeId)
    {
        return Nodes.ContainsKey(nodeId) ? Nodes[nodeId] : Node.NULL_NODE;
    }

    public Node CreateNode()
    {
        int newId = 0;
        while (newId < _isIdAvailable.Count && !_isIdAvailable[newId])
        {
            ++newId;
        }
        if (newId < _isIdAvailable.Count)
        {
            _isIdAvailable[newId] = false;
        }
        else
        {
            _isIdAvailable.Add(false);
        }
        Nodes.Add(newId, new Node(newId));
        return Nodes[newId];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(Node node)
    {
        return Nodes.ContainsKey(node.Id);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(int nodeId)
    {
        return Nodes.ContainsKey(nodeId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(Node node)
    {
        Nodes.Remove(node.Id);
        _isIdAvailable[node.Id] = true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsRootNode(int nodeId)
    {
        return nodeId == 0;
    }

    public Node FindParentNode(Node target)
    {
        for (int i = 0; i < _isIdAvailable.Count; i++)
        {
            if (_isIdAvailable[i]) continue;
            if (Nodes[i].Children.Contains(target.Id))
            {
                return Nodes[i];
            }
        }
        return Node.NULL_NODE;
    }

    public Node FindKeyLocation(Node target)
    {
        for (int i = 0; i < _isIdAvailable.Count; i++)
        {
            if (_isIdAvailable[i]) continue;
            if (Nodes[i].Keys.Contains(target.Id))
            {
                return Nodes[i];
            }
        }
        return Node.NULL_NODE;
    }

    #region Private

    private List<bool> _isIdAvailable = new();

    #endregion Private
}