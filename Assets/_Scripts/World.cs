using UnityEngine;

public class World
{
    public static World Instance { get; private set; }

    public static World Create()
    {
        return Instance ??= new World();
    }

    public Grid Grid { get; private set; }
    public PlayerController Player { get; private set; }

    public World()
    {
        Grid = new Grid();
    }

    public void Register(PlayerController player)
    {
        Player = player;
    }

    public bool CheckPlayer()
    {
        bool isRegistered = Player != null;
        if (!isRegistered) Debug.LogWarning("[World] No Player Controller is registered");
        return isRegistered;
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