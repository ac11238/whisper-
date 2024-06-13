using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MazeSolver : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap wallTilemap;

    private Dictionary<Vector3Int, Node> nodes;

    private void Awake()
    {
        InitializeNodes();
    }

    private void InitializeNodes()
    {
        nodes = new Dictionary<Vector3Int, Node>();

        foreach (Vector3Int pos in groundTilemap.cellBounds.allPositionsWithin)
        {
            if (groundTilemap.HasTile(pos))
            {
                bool isWalkable = !wallTilemap.HasTile(pos);
                nodes[pos] = new Node(pos, isWalkable);
            }
        }
    }

    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int end)
    {
        if (!nodes.ContainsKey(start) || !nodes.ContainsKey(end))
        {
            Debug.LogWarning("Start or end position is not within the grid bounds.");
            return null;
        }

        Node startNode = nodes[start];
        Node endNode = nodes[end];

        List<Node> openList = new List<Node> { startNode };
        HashSet<Node> closedList = new HashSet<Node>();

        while (openList.Count > 0)
        {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++)
            {
                if (openList[i].FCost < currentNode.FCost || openList[i].FCost == currentNode.FCost && openList[i].hCost < currentNode.hCost)
                {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (currentNode == endNode)
            {
                return RetracePath(startNode, endNode);
            }

            foreach (Node neighbor in GetNeighbors(currentNode))
            {
                if (!neighbor.isWalkable || closedList.Contains(neighbor))
                {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    private List<Vector3Int> RetracePath(Node startNode, Node endNode)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }

    private List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector3Int direction in new Vector3Int[] { Vector3Int.up, Vector3Int.down, Vector3Int.left, Vector3Int.right })
        {
            Vector3Int neighborPos = node.position + direction;
            if (nodes.ContainsKey(neighborPos))
            {
                neighbors.Add(nodes[neighborPos]);
            }
        }

        return neighbors;
    }

    private int GetDistance(Node a, Node b)
    {
        int dstX = Mathf.Abs(a.position.x - b.position.x);
        int dstY = Mathf.Abs(a.position.y - b.position.y);
        return dstX + dstY;
    }

    private class Node
    {
        public Vector3Int position;
        public bool isWalkable;
        public int gCost;
        public int hCost;
        public Node parent;

        public Node(Vector3Int position, bool isWalkable)
        {
            this.position = position;
            this.isWalkable = isWalkable;
        }

        public int FCost
        {
            get { return gCost + hCost; }
        }
    }
}
