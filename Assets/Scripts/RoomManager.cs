using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public static RoomManager Instance;

    public Vector2 roomSize = new Vector2(16, 10);
    private Dictionary<Vector2Int, GameObject> placedRooms = new();

    private void Start()
    {
        RegisterRoom(Vector2Int.zero, GameObject.Find("BaseRoom"));
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public bool IsRoomAt(Vector2Int gridPos) => placedRooms.ContainsKey(gridPos);

    public void RegisterRoom(Vector2Int gridPos, GameObject room)
    {
        if (!IsRoomAt(gridPos))
            placedRooms.Add(gridPos, room);
    }

    public GameObject GetRoom(Vector2Int gridPos)
    {
        return IsRoomAt(gridPos) ? placedRooms[gridPos] : null;
    }

    public Vector3 GridToWorldPosition(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * roomSize.x, gridPos.y * roomSize.y, 0f);
    }

    public Vector2Int WorldToGridPosition(Vector3 worldPos)
    {
        int x = Mathf.RoundToInt(worldPos.x / roomSize.x);
        int y = Mathf.RoundToInt(worldPos.y / roomSize.y);
        return new Vector2Int(x, y);
    }
}
