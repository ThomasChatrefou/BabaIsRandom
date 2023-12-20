using System.Collections.Generic;

public class Node
{
    public int Id { get; private set; }
    public char AsciiName { get; private set; }
    public List<int> Children { get; private set; } = new List<int>();
    public List<int> Keys { get; private set; } = new List<int>();

    public Node(int id)
    {
        Id = id;
        AsciiName = (char)(id + 65);
    }

    public void AddChildren(int childId)
    {
        Children.Add(childId);
    }

    public void AddKey(int keyId)
    {
        Keys.Add(keyId);
    }
}