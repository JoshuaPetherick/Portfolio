using Microsoft.Xna.Framework;

namespace Snake
{
    /// <summary>
    /// Contains all the games properties
    /// </summary>
    public static class Settings
    {
        public const int GRID_WIDTH = 800;
        public const int GRID_HEIGHT = 600;
        public const int CELL_SIZE = 25;
        public const int STARTING_BODY_COUNT = 3;
        public const int DELAY = 200; // ms
        public const int DELAY_MIN = 50; // ms
        public const int DELAY_DECREASE = 5; // ms
        public enum DIRECTION
        {
            North = 1,
            East = 2,
            South = 3,
            West = 4
        };
        public const DIRECTION START_DIRECTION = DIRECTION.East;
        public static Color GRID_COLOUR = Color.Black;
        public static Color BORDER_COLOUR = Color.White;
        public static Color FONT_COLOUR = Color.Cyan;
        public static Color SNAKE_COLOUR = Color.White;
        public static Color APPLE_COLOUR = Color.Red;
        public const int DRAWING_OFFSET = 1;
    }
}