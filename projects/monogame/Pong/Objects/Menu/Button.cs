using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Objects.Menu
{
    class Button
    {
        // Drawing Params
        private string _displayText;
        private Rectangle _boundingbox;
        private Vector2 _textPosition;
        private enum ButtonStates
        {
            Default,
            Hovered,
            Clicked
        };
        private ButtonStates state = ButtonStates.Default;

        public Button(string Text, Point Location)
        {
            // Set Params
            _displayText = Text;

            // Calculate Width & Height
            Vector2 measure = FontManager.MenuFont.MeasureString(Text);
            Point size = new Point((int)measure.X, (int)measure.Y);

            // Bounding Box
            UpdateBoundingBox(size, Location);
        }

        private void UpdateBoundingBox(Point Size, Point Location)
        {
            _boundingbox = new Rectangle(Location, Size);
            _textPosition = new Vector2(_boundingbox.X, _boundingbox.Y);
        }

        public bool Update()
        {
            // Get Mouse State
            MouseState mouse = Mouse.GetState();

            // Check Current State
            if (state == ButtonStates.Clicked)
            {
                if (mouse.LeftButton == ButtonState.Released)
                {
                    state = ButtonStates.Default;
                    return true;
                }
            }

            // New State
            if (Global_Functions.CheckCollision(_boundingbox, mouse.Position))
            {
                if (mouse.LeftButton == ButtonState.Pressed)
                    state = ButtonStates.Clicked;
                else
                    state = ButtonStates.Hovered;
            }
            else
                state = ButtonStates.Default;

            // Finished
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch(state)
            {
                case ButtonStates.Hovered:
                    spriteBatch.DrawString(FontManager.MenuFont, _displayText, _textPosition, Settings.MENU_ITEMS_HOVER_COLOUR);
                    break;
                case ButtonStates.Clicked:
                    spriteBatch.DrawString(FontManager.MenuFont, _displayText, _textPosition, Settings.MENU_ITEMS_CLICKED_COLOUR);
                    break;
                default:
                    spriteBatch.DrawString(FontManager.MenuFont, _displayText, _textPosition, Settings.MENU_ITEMS_COLOUR);
                    break;
            }
        }
    }
}