using UnityEngine;

public class World
{
    public static World Instance { get; private set; }

    public static World Create()
    {
        return Instance ??= new World();
    }

    public GameGrid Grid { get; private set; }

    public World()
    {
        Grid = new GameGrid();
    }
}

public class WorldEntity
{
    public World World { get { return World.Instance ?? World.Create(); } }
}

public class WorldBehaviour : MonoBehaviour
{
    public World World { get { return World.Instance ?? World.Create(); } }
}

public class WorldConfig : ScriptableObject
{
    public World World { get { return World.Instance ?? World.Create(); } }
}