using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pong.Objects.Menu;

namespace Pong
{
    class SettingsManager
    {
        // Objects
        private Label _title;
        private Text _difficultyLabel;
        private Button _difficultyIncrement;
        private Text _difficulty;
        private Button _difficultyDecrement;
        private Button _back;

        public SettingsManager()
        {
            // Title Position
            string text = "Settings";
            int x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.TitleFont.MeasureString(text).X / 2));
            int y = (int)((Settings.WINDOW_HEIGHT / 4) - (FontManager.TitleFont.MeasureString(text).Y / 2));
            _title = new Label(text, new Vector2(x, y));

            text = "AI Difficulty";
            x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(text).X / 2));
            y += Settings.TITLE_OFFSET;
            _difficultyLabel = new Text(text, new Vector2(x, y));

            text = "Medium";
            x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(text).X / 2));
            y += Settings.LABEL_OFFSET;
            _difficulty = new Text(text, new Vector2(x, y));

            // Button Position
            text = "<";
            x = (int)(_difficulty.GetPosition().X - FontManager.MenuFont.MeasureString(text).X) - Settings.TEXT_OFFSET;
            _difficultyDecrement = new Button(text, new Point(x, y));

            text = ">";
            x = (int)(_difficulty.GetPosition().X + _difficulty.GetWidth()) + Settings.TEXT_OFFSET;
            _difficultyIncrement = new Button(text, new Point(x, y));

            text = "Back";
            x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(text).X / 2));
            y += Settings.BUTTON_OFFSET;
            _back = new Button(text, new Point(x, y));
        }

        public Settings.GameStates? Update()
        {
            if (_difficultyDecrement.Update())
            {
                // Setup
                string newDifficulty = _difficulty.GetDisplayText();

                // New Text
                switch (_difficulty.GetDisplayText())
                {
                    case "Medium":
                        newDifficulty = "Easy";
                        Settings.AI_Difficulty = Settings.AI_PREDICTION_EASY;
                        break;
                    case "Hard":
                        newDifficulty = "Medium";
                        Settings.AI_Difficulty = Settings.AI_PREDICTION_MEDIUM;
                        break;
                    case "Pure Evil":
                        newDifficulty = "Hard";
                        Settings.AI_Difficulty = Settings.AI_PREDICTION_HARD;
                        break;
                }

                // Update Text
                int x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(newDifficulty).X / 2));
                int y = (int)_difficulty.GetPosition().Y;

                _difficulty = new Text(newDifficulty, new Vector2(x, y));
            }

            if (_difficultyIncrement.Update())
            {
                // Setup
                string newDifficulty = _difficulty.GetDisplayText();
                
                // New Text
                switch (_difficulty.GetDisplayText())
                {
                    case "Easy":
                        newDifficulty = "Medium";
                        Settings.AI_Difficulty = Settings.AI_PREDICTION_MEDIUM;
                        break;
                    case "Medium":
                        newDifficulty = "Hard";
                        Settings.AI_Difficulty = Settings.AI_PREDICTION_HARD;
                        break;
                    case "Hard":
                        newDifficulty = "Pure Evil";
                        Settings.AI_Difficulty = Settings.AI_PREDICTION_EVIL;
                        break;
                }

                // Update Text
                int x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(newDifficulty).X / 2));
                int y = (int)_difficulty.GetPosition().Y;

                _difficulty = new Text(newDifficulty, new Vector2(x, y));
            }

            if (_back.Update())
            {
                return Settings.GameStates.Menu;
            }

            return Settings.GameStates.Settings;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _title.Draw(spriteBatch);
            _difficultyLabel.Draw(spriteBatch);
            _difficultyDecrement.Draw(spriteBatch);
            _difficulty.Draw(spriteBatch);
            _difficultyIncrement.Draw(spriteBatch);
            _back.Draw(spriteBatch);
        }
    }
}