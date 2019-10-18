using System;
using System.Collections.Generic;

public interface IMapModel
{
    event Action<List<Point>> OnPathChanged;
    void FindPath();
}
