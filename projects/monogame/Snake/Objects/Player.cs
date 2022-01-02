using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Snake.Objects
{
    public class Player
    {
        // Drawing Params
        private Texture2D _texture;
        private Rectangle _boundingbox;
        private Rectangle _drawingbox;
        private int _X = 0;
        private int _Y = 0;

        // Game Params
        private readonly List<Body> _bodies = new List<Body>();
        private int _delay = Settings.DELAY;
        private int _currentDelay = 0;
        private Settings.DIRECTION _direction = Settings.START_DIRECTION;
        private bool _changeDirection = true;
        private bool _newBodyCheck = false;

        public Player(Texture2D texture, Vector2 StartPosition)
        {
            // Texture
            _texture = texture;
            _texture.SetData(new[] { Settings.SNAKE_COLOUR });

            // Position
            _X = (int)StartPosition.X;
            _Y = (int)StartPosition.Y;

            // Add Body Pieces
            for (int i = 1; i <= Settings.STARTING_BODY_COUNT; i++)
            {
                // Get Position
                Vector2 position = StartPosition;

                // Reverse Direction
                switch (_direction)
                {
                    case Settings.DIRECTION.North:
                        position = new Vector2(position.X, position.Y + i);
                        break;
                    case Settings.DIRECTION.East:
                        position = new Vector2(position.X - i, position.Y);
                        break;
                    case Settings.DIRECTION.South:
                        position = new Vector2(position.X, position.Y - i);
                        break;
                    case Settings.DIRECTION.West:
                        position = new Vector2(position.X + i, position.Y);
                        break;
                }

                // Add to Body
                _bodies.Add(new Body(_texture, position)); 
            }

            // Update Drawbox
            UpdateBoundingBox();
        }

        public int GetBodyCount() => _bodies.Count;

        private void UpdateBoundingBox()
        {
            _boundingbox = Global_Functions.GetPosition(_X, _Y);
            _drawingbox = new Rectangle(_boundingbox.X, _boundingbox.Y, _boundingbox.Width - Settings.SNAKE_DRAWING_OFFSET, _boundingbox.Height - Settings.SNAKE_DRAWING_OFFSET);
        }

        public bool Update(GameTime gameTime)
        {
            bool result = false;

            // Check Input
            if (_changeDirection)
            {
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.W) || ks.IsKeyDown(Keys.Up))
                {
                    if (_direction != Settings.DIRECTION.South)
                    {
                        _direction = Settings.DIRECTION.North;
                        _changeDirection = false;
                    }
                }
                if (ks.IsKeyDown(Keys.D) || ks.IsKeyDown(Keys.Right))
                {
                    if (_direction != Settings.DIRECTION.West)
                    {
                        _direction = Settings.DIRECTION.East;
                        _changeDirection = false;
                    }
                }
                if (ks.IsKeyDown(Keys.S) || ks.IsKeyDown(Keys.Down))
                {
                    if (_direction != Settings.DIRECTION.North)
                    {
                        _direction = Settings.DIRECTION.South;
                        _changeDirection = false;
                    }
                }
                if (ks.IsKeyDown(Keys.A) || ks.IsKeyDown(Keys.Left))
                {
                    if (_direction != Settings.DIRECTION.East)
                    {
                        _direction = Settings.DIRECTION.West;
                        _changeDirection = false;
                    }
                }
            }
            // Update Position
            _currentDelay += (int)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_currentDelay >= _delay)
            {
                // Update Bodies
                for (int i = (_bodies.Count - 1); i >= 0; i--)
                {
                    if (i == 0)
                        _bodies[i].Update(new Vector2(_X, _Y));
                    else
                        _bodies[i].Update(_bodies[i - 1].GetPosition());
                }

                // Update Position
                switch (_direction)
                {
                    case Settings.DIRECTION.North:
                        _Y--;
                        break;
                    case Settings.DIRECTION.East:
                        _X++;
                        break;
                    case Settings.DIRECTION.South:
                        _Y++;
                        break;
                    case Settings.DIRECTION.West:
                        _X--;
                        break;
                }

                // Update Drawbox
                UpdateBoundingBox();

                // Apply Changes (Should match anyway)
                _X = _boundingbox.X / Settings.CELL_SIZE;
                _Y = _boundingbox.Y / Settings.CELL_SIZE;

                // Update Params
                _currentDelay = 0;
                _newBodyCheck = false;
                _changeDirection = true;

                // Check if Losing Condition has been met
                result = CheckLoseCondition();
            }
            return result;
        }

        public bool CheckIfColliding(Rectangle rectangle)
        {
            if (Global_Functions.CheckCollision(_boundingbox, rectangle))
                return true;

            for (int i = 0; i < _bodies.Count; i++)
            {
                if (Global_Functions.CheckCollision(_bodies[i].GetBoundingBox(), rectangle))
                    return true;
            }

            return false;
        }

        public void CollidedWithApple()
        {
            // Add New Body
            _bodies.Add(new Body(_texture, new Vector2(_X, _Y)));

            // Update Delay
            if (_delay >= Settings.DELAY_MIN)
                _delay -= Settings.DELAY_DECREASE;

            // Signify new body
            _newBodyCheck = true;
        }

        public bool CheckLoseCondition()
        {
            for (int i = 0; i < _bodies.Count; i++)
            {
                if ((i != (_bodies.Count - 1) && _newBodyCheck) || !_newBodyCheck)
                {
                    if (Global_Functions.CheckCollision(_boundingbox, _bodies[i].GetBoundingBox()))
                        return true;
                }
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw Head
            spriteBatch.Draw(_texture, _drawingbox, Settings.SNAKE_COLOUR);

            // Draw Body 
            for (int i = 0; i < _bodies.Count; i++)
            {
                _bodies[i].Draw(spriteBatch); 
            }
        }
    }
}