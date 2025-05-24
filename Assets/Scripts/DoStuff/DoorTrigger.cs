using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GameObject roomPrefab;
    public Vector2Int directionOffset;
    public string entranceDirection;  
    public bool leadsToBossRoom = false; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Vector2Int currentGridPos = RoomManager.Instance.WorldToGridPosition(transform.root.position);
        Vector2Int newGridPos = currentGridPos + directionOffset;

        GameObject room;

        if (!RoomManager.Instance.IsRoomAt(newGridPos))
        {
            Vector3 spawnPos = RoomManager.Instance.GridToWorldPosition(newGridPos);
            room = Instantiate(roomPrefab, spawnPos, Quaternion.identity);
            RoomManager.Instance.RegisterRoom(newGridPos, room);
        }
        else
        {
            room = RoomManager.Instance.GetRoom(newGridPos);
        }

        Transform spawn = room.transform.Find("SpawnPoints/Spawn_" + entranceDirection);
        if (spawn != null)
        {
            other.transform.position = spawn.position;
        }
        else
        {
            Debug.LogWarning("Spawn point not found: " + entranceDirection);
        }


        if (leadsToBossRoom)
        {
            GameManager.Instance.PlayBossMusic();
        }
        else
        {
            GameManager.Instance.PlayNormalMusic();
        }

    }
}
