using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Objects.Menu
{
    class Label
    {
        // Drawing Params
        private string _displayText;
        private Vector2 _textPosition;

        public Label(string Text, Vector2 Location)
        {
            // Set Params
            _displayText = Text;
            _textPosition = Location;
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.DrawString(FontManager.TitleFont, _displayText, _textPosition, Settings.MENU_ITEMS_COLOUR);
    }
}