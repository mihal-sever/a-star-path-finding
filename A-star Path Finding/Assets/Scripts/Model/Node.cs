public class Node
{
    public Point position;
    /// <summary>
    ///  Heuristic distance from node to goal.
    /// </summary>
    public int h;
    /// <summary>
    /// Distance from start to node.
    /// </summary>
    public int g;
    /// <summary>
    /// Estimated distance from start to goal.
    /// </summary>
    public int F => h + g;
    public Node parent;

    public Node(Point position, int g, int h, Node parent)
    {
        this.position = position;
        this.g = g;
        this.h = h;
        this.parent = parent;
    }
}
