using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Objects.Menu
{
    class Text
    {
        // Drawing Params
        private string _displayText;
        private Vector2 _textPosition;

        public Text(string Text, Vector2 Position)
        {
            // Set Params
            _displayText = Text;
            _textPosition = Position;
        }

        public string GetDisplayText() => _displayText;

        public Vector2 GetPosition() => _textPosition;

        public float GetWidth() => FontManager.MenuFont.MeasureString(_displayText).X;

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.DrawString(FontManager.MenuFont, _displayText, _textPosition, Settings.MENU_ITEMS_COLOUR);
    }
}