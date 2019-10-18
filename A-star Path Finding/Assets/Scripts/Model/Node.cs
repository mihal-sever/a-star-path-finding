public class Node
{
    Point position;
    public int h;
    public int g; //if node is an obstacle, then g = max value
    public Node parent;

    public Node(Point position, int g)
    {
        this.position = position;
        this.g = g;
    }
}
