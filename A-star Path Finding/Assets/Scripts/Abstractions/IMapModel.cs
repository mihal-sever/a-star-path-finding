﻿using System;
using System.Collections.Generic;

public interface IMapModel
{
    event Action<List<Point>> OnPathChanged;
    void FindPath();
    void SetupMapModel(int[,] grid, Point startPoint, Point goalPoint, IPathFinder pathFinder);
}
