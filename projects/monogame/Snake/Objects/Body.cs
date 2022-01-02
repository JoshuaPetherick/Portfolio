using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Objects
{
    public class Body
    {
        // Drawing Params
        private Texture2D _texture;
        private Rectangle _boundingbox;
        private Rectangle _drawingbox;
        private int _X = 0;
        private int _Y = 0;

        public Body(Texture2D texture, Vector2 StartPosition)
        {
            // Texture
            _texture = texture;
            _texture.SetData(new[] { Settings.SNAKE_COLOUR });

            // Position
            _X = (int)StartPosition.X;
            _Y = (int)StartPosition.Y;

            // Update Drawbox
            UpdateBoundingBox();
        }

        public Vector2 GetPosition() => new Vector2(_X, _Y);

        public Rectangle GetBoundingBox() => _boundingbox;

        private void UpdateBoundingBox()
        {
            _boundingbox = Global_Functions.GetPosition(_X, _Y);
            _drawingbox = new Rectangle(_boundingbox.X, _boundingbox.Y, _boundingbox.Width - Settings.SNAKE_DRAWING_OFFSET, _boundingbox.Height - Settings.SNAKE_DRAWING_OFFSET);
        }

        public void Update(Vector2 Position)
        {
            // Update Position
            _X = (int)Position.X;
            _Y = (int)Position.Y;

            UpdateBoundingBox();
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _drawingbox, Settings.SNAKE_COLOUR);
    }
}
