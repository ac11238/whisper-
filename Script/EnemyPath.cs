using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public MazeSolver pathfinder;
    public Transform startPoint;
    private Transform player;
    private controller PlayerStat;

    private List<Vector3Int> path;
    private int pathIndex;
    public float moveSpeed = 3f;
    public bool active = false; 

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("No GameObject tagged 'Player' found in the scene.");
        }

        PlayerStat = player.GetComponent<controller>();
        if (PlayerStat == null)
        {
            Debug.LogError("No 'controller' script found on the Player GameObject.");
        }

        path = new List<Vector3Int>();
    }

    void Update()
    {
        if (player == null || PlayerStat == null)
        {
            return; 
        }

        if (PlayerStat.currentAwakeness >= 100)
        {
            active = true;
        }

        if (active)
        {
            Vector3Int startCell = pathfinder.groundTilemap.WorldToCell(startPoint.position);
            Vector3Int endCell = pathfinder.groundTilemap.WorldToCell(player.position);

            Debug.Log($"Start Cell: {startCell}, End Cell: {endCell}");
            Debug.Log($"Ground Tilemap Bounds: {pathfinder.groundTilemap.cellBounds}");

            path = pathfinder.FindPath(startCell, endCell);

            if (path != null && path.Count > 0)
            {
                MoveAlongPath();
            }
        }
    }

    void MoveAlongPath()
    {
        if (pathIndex < path.Count)
        {
            Vector3 targetPosition = pathfinder.groundTilemap.CellToWorld(path[pathIndex]) + new Vector3(0.5f, 0.5f, 0);
            startPoint.position = Vector3.MoveTowards(startPoint.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(startPoint.position, targetPosition) < 0.1f)
            {
                pathIndex++;
            }
        }
        else
        {
            pathIndex = 0;
            path.Clear();
        }
    }
}
