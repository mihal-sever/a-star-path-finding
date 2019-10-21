using System;
using System.Linq;
using System.Collections.Generic;

public class AStarPathFinder : IPathFinder
{
    Point gridSize;
    

    public List<Point> FindPath(int[,] grid, Point start, Point goal)
    {
        gridSize = new Point(grid.GetLength(0), grid.GetLength(1));

        List<Node> opened = new List<Node>();
        List<Node> closed = new List<Node>();

        Node startNode = new Node(start, 0, GetManhattanDistance(start, goal), null);

        opened.Add(startNode);

        while (opened.Count > 0)
        {
            Node current = opened.First(c=>c.F == opened.Min(p => p.F));
            if (current.position == goal)
                return GetPathFromNode(current);

            opened.Remove(current);
            closed.Add(current);

            List<Point> neighbouringCells = GetNeighbouringCells(current.position);

            foreach (Point p in neighbouringCells)
            {
                if (!IsInBounds(p) || IsObstacle(grid[p.x, p.y]) || IsVisited(closed, p))
                    continue;

                int lengthFromStartToCurrent = current.g + grid[p.x, p.y];

                Node openedNode = opened.FirstOrDefault(n => n.position == p);
                if (openedNode == null)
                {
                    Node node = new Node(p, lengthFromStartToCurrent, GetManhattanDistance(p, goal), current);
                    opened.Add(node);
                }
                else if (openedNode.g > lengthFromStartToCurrent)
                {
                    openedNode.g = lengthFromStartToCurrent;
                    openedNode.parent = current;
                }
            }
        }
        return null;
    }
    
    private bool IsInBounds(Point point)
    {
        return point.x >= 0 && point.x < gridSize.x &&
            point.y >= 0 && point.y < gridSize.y;
    }

    private bool IsObstacle(int cellValue)
    {
        return cellValue == int.MaxValue;
    }

    private bool IsVisited(List<Node> closed, Point point)
    {
        return closed.Count(node => node.position == point) > 0;
    }

    private int GetManhattanDistance(Point p, Point goal)
    {
        return Math.Abs(p.x - goal.x) + Math.Abs(p.y - goal.y);
    }

    private List<Point> GetNeighbouringCells(Point cell)
    {
        List<Point> neighbours = new List<Point>();

        neighbours.Add(new Point(cell.x - 1, cell.y));
        neighbours.Add(new Point(cell.x + 1, cell.y));
        neighbours.Add(new Point(cell.x, cell.y - 1));
        neighbours.Add(new Point(cell.x, cell.y + 1));

        return neighbours;
    }

    private List<Point> GetPathFromNode(Node node)
    {
        var path = new List<Point>();
        var currentNode = node;
        while (currentNode != null)
        {
            path.Add(currentNode.position);
            currentNode = currentNode.parent;
        }
        path.Reverse();
        return path;
    }
}
