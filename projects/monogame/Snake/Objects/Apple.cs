using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Objects
{
    public class Apple
    {
        // Drawing Params
        private Texture2D _texture;
        public Rectangle _boundingbox;
        public Rectangle _drawingbox;
        private Vector2 _position;

        public Apple(Texture2D texture, Player snake)
        {
            // Texture
            _texture = texture;
            _texture.SetData(new[] { Settings.APPLE_COLOUR });

            // Start Position
            UpdatePosition();

            // Check Position
            while (snake.CheckIfColliding(_boundingbox))
            {
                UpdatePosition();
            }
        }

        private void UpdatePosition()
        {
            _position = Global_Functions.GetRandomPosition();
            UpdateBoundingBox();
        }

        private void UpdateBoundingBox()
        {
            _boundingbox = Global_Functions.GetPosition((int)_position.X, (int)_position.Y);
            _drawingbox = new Rectangle(_boundingbox.X + (Settings.APPLE_DRAWING_OFFSET / 2), _boundingbox.Y + (Settings.APPLE_DRAWING_OFFSET / 2), _boundingbox.Width - Settings.APPLE_DRAWING_OFFSET, _boundingbox.Height - Settings.APPLE_DRAWING_OFFSET);
        }

        public bool Update(Player snake)
        {
            bool result = false;
            // Check if Colliding
            if (snake.CheckIfColliding(_boundingbox))
            {
                // Add New Body
                snake.CollidedWithApple();

                // Check Win Condition
                result = Global_Functions.CheckVictoryCondition(snake);

                if (!result)
                {
                    // New Position
                    while (snake.CheckIfColliding(_boundingbox))
                    {
                        UpdatePosition();
                    }
                }
            }
            return result;
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _drawingbox, Settings.APPLE_COLOUR);
    }
}
