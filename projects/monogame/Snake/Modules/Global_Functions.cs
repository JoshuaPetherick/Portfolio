using System;

using Microsoft.Xna.Framework;

using Snake.Objects;

namespace Snake
{
    public static class Global_Functions
    {
        private static Random _randomGenerator = new Random();

        public static Rectangle GetPosition(int X, int Y) => new Rectangle(GetActualPosition_X(X), GetActualPosition_Y(Y), Settings.CELL_SIZE, Settings.CELL_SIZE);

        public static Point GetActualGridSize()
        {
            int gridCellsX = Settings.GRID_WIDTH / Settings.CELL_SIZE;
            int gridCellsY = Settings.GRID_HEIGHT / Settings.CELL_SIZE;

            return new Point(gridCellsX * Settings.CELL_SIZE, gridCellsY * Settings.CELL_SIZE);
        }

        private static int GetActualPosition_X(int X)
        {
            // Params
            int actualX = X;
            int gridCells = Settings.GRID_WIDTH / Settings.CELL_SIZE;
            // Checks
            if (actualX < 0)
                actualX = (gridCells - 1); // Offset so doesn't exist outside screen
            else if (actualX >= gridCells)
                actualX = 0;
            // Actual Position
            return Settings.CELL_SIZE * actualX;
        }

        private static int GetActualPosition_Y(int Y)
        {
            // Params
            int actualY = Y;
            int gridCells = Settings.GRID_HEIGHT / Settings.CELL_SIZE;
            // Checks
            if (actualY < 0)
                actualY = (gridCells - 1); // Offset so doesn't exist outside screen
            else if (actualY >= gridCells)
                actualY = 0;
            // Actual Position
            return Settings.CELL_SIZE * actualY;
        }

        public static Vector2 GetStartPosition()
        {
            int gridCellsX = Settings.GRID_WIDTH / Settings.CELL_SIZE;
            int gridCellsY = Settings.GRID_HEIGHT / Settings.CELL_SIZE;

            return new Vector2(gridCellsX / 2, gridCellsY / 2);
        }

        public static Vector2 GetRandomPosition()
        {
            int gridCellsX = Settings.GRID_WIDTH / Settings.CELL_SIZE;
            int gridCellsY = Settings.GRID_HEIGHT / Settings.CELL_SIZE;
            
            return new Vector2(_randomGenerator.Next(gridCellsX), _randomGenerator.Next(gridCellsY));
        }

        public static bool CheckCollision(Rectangle box1, Rectangle box2) => box1.Intersects(box2);

        public static bool CheckVictoryCondition(Player snake)
        {
            int gridCellsX = Settings.GRID_WIDTH / Settings.CELL_SIZE;
            int gridCellsY = Settings.GRID_HEIGHT / Settings.CELL_SIZE;
            int totalCells = gridCellsX * gridCellsY;

            int totalSnakeCoverage = snake.GetBodyCount() + 1; // + 1 = Snake Head

            if (totalSnakeCoverage >= totalCells)
                return true;

            return false;
        }
    }
}