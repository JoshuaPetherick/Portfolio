using Microsoft.Xna.Framework;

namespace Pong
{
    public static class Settings
    {
        public const int WINDOW_WIDTH = 800;
        public const int WINDOW_HEIGHT = 600;
        public enum GameStates
        {
            Menu,
            Singleplayer,
            Multiplayer,
            Settings
        };
        public const int VICTORY_CONDITION = 5;
        public const int AI_PREDICTION_EASY = 10;
        public const int AI_PREDICTION_MEDIUM = 25;
        public const int AI_PREDICTION_HARD = 50;
        public const int AI_PREDICTION_EVIL = 100;
        public const int BALL_SIZE = 10;
        public const int BALL_START_SPEED = 150;
        public const int BALL_SPEED_INCREMENT = 20;
        public const int BALL_DISABLE_FRAME_COUNT = 10;
        public const int PADDLE_SIZE = 50;
        public const int PADDLE_SPEED = 200;
        public const int PADDLE_OFFSET = 15;
        public const int TITLE_OFFSET = 90;
        public const int TEXT_OFFSET = 45;
        public const int LABEL_OFFSET = 40;
        public const int BUTTON_OFFSET = 50;
        public const int SCORE_OFFSET = 10;
        public static Color WINDOW_COLOUR = Color.Black;
        public static Color BACKGROUND_COLOUR = Color.Black;
        public static Color BACKGROUND_BORDER_COLOUR = Color.Blue;
        public static Color MENU_ITEMS_COLOUR = Color.White;
        public static Color MENU_ITEMS_HOVER_COLOUR = Color.LightGray;
        public static Color MENU_ITEMS_CLICKED_COLOUR = Color.Gray;
        public static Color SCORE_COLOUR = Color.White;
        public static Color VICTORY_COLOUR = Color.White;
        public static Color PADDLE_COLOUR = Color.White;
        public static Color AI_COLOUR = Color.White;
        public static Color BALL_COLOUR = Color.White;

        // Customisable Settings
        public static int AI_Difficulty = AI_PREDICTION_MEDIUM;
    }
}