using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Objects.Game
{
    class AI
    {
        // Drawing Params
        private Texture2D _texture;
        private Vector2 _resetPosition;
        private Rectangle _boundingbox;
        private int _X = 0;
        private float _Y = 0;

        public AI(Texture2D texture, Vector2 StartPosition)
        {
            // Texture
            _texture = texture;
            _texture.SetData(new[] { Settings.AI_COLOUR});

            // Position
            _resetPosition = StartPosition;
            Reset();
        }

        private void UpdateBoundingBox() => _boundingbox = new Rectangle(_X, (int)_Y, Settings.PADDLE_SIZE / 4, Settings.PADDLE_SIZE);

        public void Update(GameTime gameTime, Rectangle arena, Ball target)
        {
            // Move accordingly
            if (target.GetDirection() == Ball.Direction.NorthEast || target.GetDirection() == Ball.Direction.SouthEast)
            {
                // Get Predicted Move
                Vector2 prediction = PredictBallMove(gameTime, arena, target);

                // Check if need to move
                if (prediction.Y <= _Y + (Settings.PADDLE_SIZE / 4) || prediction.Y >= _Y + (Settings.PADDLE_SIZE - (Settings.PADDLE_SIZE / 4)))
                {
                    // Move Accordingly
                    if (prediction.Y > (_Y + (Settings.PADDLE_SIZE / 2)))
                        _Y += Settings.PADDLE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    else if (prediction.Y < (_Y + (Settings.PADDLE_SIZE / 2)))
                        _Y -= Settings.PADDLE_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            // Check Y Axis
            if (_Y < arena.Y + 1)
                _Y = arena.Y + 1;
            else if (_Y + Settings.PADDLE_SIZE > arena.Y + arena.Height)
                _Y = (arena.Y + arena.Height) - Settings.PADDLE_SIZE;

            // Update Bounding Box
            UpdateBoundingBox();
        }

        private Vector2 PredictBallMove(GameTime gameTime, Rectangle arena, Ball target)
        {
            float x = target.GetPosition().X;
            float y = target.GetPosition().Y;
            Ball.Direction direction = target.GetDirection();
            int speed = target.GetSpeed();

            for (int i = 1; i <= Settings.AI_Difficulty; i++)
            {
                // Move 
                switch(direction)
                {
                    case Ball.Direction.NorthEast:
                        y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        x += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Ball.Direction.SouthEast:
                        y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        x += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Ball.Direction.SouthWest:
                        y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        x -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                    case Ball.Direction.NorthWest:
                        y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        x -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        break;
                }

                // Check Collision
                if (y <= arena.Y)
                {
                    if (direction == Ball.Direction.NorthEast)
                        direction = Ball.Direction.SouthEast;
                    else if (direction == Ball.Direction.NorthWest)
                        direction = Ball.Direction.SouthWest;
                }
                else if ((y + Settings.BALL_SIZE) >= (arena.Y + arena.Height))
                {
                    if (direction == Ball.Direction.SouthEast)
                        direction = Ball.Direction.NorthEast;
                    else if (direction == Ball.Direction.SouthWest)
                        direction = Ball.Direction.NorthWest;
                }

                // Can't predict if past AI
                if (x >= _X)
                    break;
            }

            return new Vector2(x, y);
        }

        public Rectangle GetBoundingBox() => _boundingbox;

        public void Reset()
        {
            _X = (int)_resetPosition.X;
            _Y = (int)_resetPosition.Y;
            UpdateBoundingBox();
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _boundingbox, Settings.AI_COLOUR);
    }
}
