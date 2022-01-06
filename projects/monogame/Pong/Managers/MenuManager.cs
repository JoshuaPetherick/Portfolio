using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Pong.Objects.Menu;

namespace Pong
{
    /// <summary>
    /// Handles the menu and its objects
    /// </summary>
    class MenuManager
    {
        // Objects
        private Label _title;
        private Button _singleplayer;
        private Button _multiplayer;
        private Button _settings;
        private Button _exit;

        public MenuManager()
        {
            // Title Position
            string text = "Pong";
            int x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.TitleFont.MeasureString(text).X / 2));
            int y = (int)((Settings.WINDOW_HEIGHT / 4) - (FontManager.TitleFont.MeasureString(text).Y / 2));
            _title = new Label(text, new Vector2(x, y));

            // Button Position
            text = "Singleplayer";
            x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(text).X / 2));
            y += Settings.TITLE_OFFSET;
            _singleplayer = new Button(text, new Point(x, y));

            text = "Multiplayer";
            x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(text).X / 2));
            y += Settings.BUTTON_OFFSET;
            _multiplayer = new Button(text, new Point(x, y));

            text = "Settings";
            x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(text).X / 2));
            y += Settings.BUTTON_OFFSET;
            _settings = new Button(text, new Point(x, y));

            text = "Exit";
            x = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.MenuFont.MeasureString(text).X / 2));
            y += Settings.BUTTON_OFFSET;
            _exit = new Button(text, new Point(x, y));
        }

        public Settings.GameStates? Update()
        {
            if (_singleplayer.Update())
            {
                return Settings.GameStates.Singleplayer;
            }

            if (_multiplayer.Update())
            {
                return Settings.GameStates.Multiplayer;
            }

            if (_settings.Update())
            {
                return Settings.GameStates.Settings;
            }

            if (_exit.Update())
            {
                return null;
            }

            return Settings.GameStates.Menu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _title.Draw(spriteBatch);
            _singleplayer.Draw(spriteBatch);
            _multiplayer.Draw(spriteBatch);
            _settings.Draw(spriteBatch);
            _exit.Draw(spriteBatch);
        }
    }
}