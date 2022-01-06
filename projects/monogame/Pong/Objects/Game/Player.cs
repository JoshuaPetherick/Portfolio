using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Objects.Game
{
    class Player
    {
        // Drawing Params
        private Texture2D _texture;
        private Vector2 _resetPosition;
        private Rectangle _boundingbox;
        private int _X = 0;
        private float _Y = 0;

        public Player(Texture2D texture, Vector2 StartPosition)
        {
            // Texture
            _texture = texture;
            _texture.SetData(new[] { Settings.PADDLE_COLOUR });

            // Position
            _resetPosition = StartPosition;
            Reset();
        }

        private void UpdateBoundingBox() => _boundingbox = new Rectangle(_X, (int)_Y, Settings.PADDLE_SIZE / 4, Settings.PADDLE_SIZE);

        public void Update(GameTime gameTime, Rectangle arena, bool player2 = false)
        {
            KeyboardState ks = Keyboard.GetState();

            if (player2)
            {
                // Arrow Keys
                if (ks.IsKeyDown(Keys.Up))
                {
                    _Y -= Settings.PADDLE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (ks.IsKeyDown(Keys.Down))
                {
                    _Y += Settings.PADDLE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
            else
            {
                if (ks.IsKeyDown(Keys.W))
                {
                    _Y -= Settings.PADDLE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                if (ks.IsKeyDown(Keys.S))
                {
                    _Y += Settings.PADDLE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            // Check Y Axis
            if (_Y < arena.Y)
                _Y = arena.Y;
            else if (_Y + Settings.PADDLE_SIZE > arena.Y + arena.Height)
                _Y = (arena.Y + arena.Height) - Settings.PADDLE_SIZE;

            // Update Box
            UpdateBoundingBox();
        }

        public Rectangle GetBoundingBox() => _boundingbox;

        public void Reset()
        {
            _X = (int)_resetPosition.X;
            _Y = (int)_resetPosition.Y;
            UpdateBoundingBox();
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _boundingbox, Settings.PADDLE_COLOUR);
    }
}