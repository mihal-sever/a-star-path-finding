# A* Path Finding
This particular implementation of A* is designed for a 2-dimensional regular grid, and it allows to move in 4 directions (left, right, up and down).

Warning: Grid size by default is 50*50, and it is performing quite badly at the very moment.

## User can perform the following actions:
- Add and remove obstacles
- Edit start and goal points
- Request path finding
- Clear either path or the whole map
- Save designed map which will be opened first at the next session

## Further development plan:
- Add A* implementation for moving in 8 directions (includes diagonals)
- Animate algorithm's work
- Achieve better performance (in both rendering and path finding)
- Expose map size to user and adjust camera position dynamically
- Consider its visual appearance
- Refactor MapView class to be more readable
