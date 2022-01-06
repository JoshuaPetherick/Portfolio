using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Objects.Game
{
    class Ball
    {
        // Drawing Params
        private Texture2D _texture;
        private Vector2 _resetPosition;
        private Rectangle _boundingbox;
        private float _X = 0;
        private float _Y = 0;

        // Game Params
        public enum Direction
        {
            NorthEast,
            SouthEast,
            SouthWest,
            NorthWest
        };
        private Direction _currentDirection;
        private int _speed;
        private bool _tempDisableCollsions;
        private int _tempDisableFrameCount;

        public Ball(Texture2D texture, Vector2 StartPosition)
        {
            // Setup
            _currentDirection = Direction.NorthWest;
            _resetPosition = StartPosition;
            _tempDisableCollsions = false;
            _tempDisableFrameCount = 0;

            // Texture
            _texture = texture;
            _texture.SetData(new[] { Settings.BALL_COLOUR });

            // Position
            Reset();
        }

        private void UpdateBoundingBox() => _boundingbox = new Rectangle((int)_X, (int)_Y, Settings.BALL_SIZE, Settings.BALL_SIZE);

        public Vector2 GetPosition() => new Vector2((int)_X, (int)_Y);

        public Direction GetDirection() => _currentDirection;

        public int GetSpeed() => _speed;

        public void Update(GameTime gameTime, Rectangle arena)
        {
            // Move Ball
            switch(_currentDirection)
            {
                case Direction.NorthEast:
                    _Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.SouthEast:
                    _Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _X += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.SouthWest:
                    _Y += _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
                case Direction.NorthWest:
                    _Y -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _X -= _speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    break;
            }

            // Update Bounding Box
            UpdateBoundingBox();

            // Check if colliding with surface (bounce accordingly)
            if (_Y <= arena.Y)
            {
                if (_currentDirection == Direction.NorthEast)
                    _currentDirection = Direction.SouthEast;
                else if (_currentDirection == Direction.NorthWest)
                    _currentDirection = Direction.SouthWest;
            }
            else if ((_Y + Settings.BALL_SIZE) >= (arena.Y + arena.Height))
            {
                if (_currentDirection == Direction.SouthEast)
                    _currentDirection = Direction.NorthEast;
                else if (_currentDirection == Direction.SouthWest)
                    _currentDirection = Direction.NorthWest;
            }
        }

        public void CheckCollision(Player player)
        {
            if (!_tempDisableCollsions)
            {
                if (Global_Functions.CheckCollision(_boundingbox, player.GetBoundingBox()))
                {
                    // Adjust Properties
                    _speed += Settings.BALL_SPEED_INCREMENT;
                    InvertDirection();

                    // Temp Disable Collision
                    _tempDisableCollsions = true;
                    _tempDisableFrameCount = 0;
                }
            }
            else
            {
                _tempDisableFrameCount++;
                if (_tempDisableFrameCount >= Settings.BALL_DISABLE_FRAME_COUNT)
                {
                    _tempDisableCollsions = false;
                }
            }
        }

        public void CheckCollision(AI ai)
        {
            if (!_tempDisableCollsions)
            {
                if (Global_Functions.CheckCollision(_boundingbox, ai.GetBoundingBox()))
                {
                    // Adjust Properties
                    _speed += Settings.BALL_SPEED_INCREMENT;
                    InvertDirection();

                    // Temp Disable Collision
                    _tempDisableCollsions = true;
                    _tempDisableFrameCount = 0;
                }
            }
            else
            {
                _tempDisableFrameCount++;
                if (_tempDisableFrameCount >= Settings.BALL_DISABLE_FRAME_COUNT)
                {
                    _tempDisableCollsions = false;
                }
            }
        }

        private void InvertDirection()
        {
            switch (_currentDirection)
            {
                case Direction.NorthEast:
                    _currentDirection = Direction.NorthWest;
                    break;
                case Direction.SouthEast:
                    _currentDirection = Direction.SouthWest;
                    break;
                case Direction.SouthWest:
                    _currentDirection = Direction.SouthEast;
                    break;
                case Direction.NorthWest:
                    _currentDirection = Direction.NorthEast;
                    break;
            }
        }

        public void Reset()
        {
            _X = _resetPosition.X;
            _Y = _resetPosition.Y;
            _speed = Settings.BALL_START_SPEED;
            UpdateBoundingBox();
            InvertDirection();
        }

        public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(_texture, _boundingbox, Settings.BALL_COLOUR);
    }
}
