using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Objects.Game;

namespace Pong
{
    class GameManager
    {
        private const string VICTORY_CONDITION_TEXT_BASE = "Score VICTORY_CONDITION points to win";

        // Params
        private bool _gameOver;
        private bool _checkForTwoPlayerInputs;
        private Settings.GameStates currentGameState;

        // Objects
        private Player _player1;
        private Player _player2;
        private AI _ai;
        private Ball _ball;
        private Rectangle _arena;
        private Rectangle _arenaBorder;
        private Texture2D _arenaTexture;
        private Texture2D _arenaBorderTexture;

        // Scores
        private int _player1Score = 0;
        private int _player2Score = 0;
        private Vector2 _player1ScorePosition;
        private Vector2 _player2ScorePosition;

        // Text
        private string _victoryConditionText;
        private Vector2 _victoryConditionPosition;
        private StringBuilder _endGameText;
        private Vector2 _endGamePosition;

        public GameManager(GraphicsDevice graphics, bool Multiplayer)
        {
            // Params
            _checkForTwoPlayerInputs = Multiplayer;
            currentGameState = _checkForTwoPlayerInputs ? Settings.GameStates.Multiplayer : Settings.GameStates.Singleplayer;
            _gameOver = false;
            _endGameText = new StringBuilder();

            // Setup
            SetupVictoryText();
            UpdateScorePositions();
            SetupArena(new Texture2D(graphics, 1, 1));

            // Start Positions
            Vector2 ballStartPosition = new Vector2(_arena.Center.X - (Settings.BALL_SIZE / 2), _arena.Center.Y - (Settings.BALL_SIZE / 2));
            Vector2 player1StartPosition = new Vector2(_arena.X + Settings.PADDLE_OFFSET, _arena.Center.Y - (Settings.PADDLE_SIZE / 2));
            Vector2 player2StartPosition = new Vector2(_arena.X + _arena.Width - (Settings.PADDLE_SIZE / 4) - Settings.PADDLE_OFFSET, _arena.Center.Y - (Settings.PADDLE_SIZE / 2));

            // Load Objects
            _ball = new Ball(new Texture2D(graphics, 1, 1), ballStartPosition);
            _player1 = new Player(new Texture2D(graphics, 1, 1), player1StartPosition);

            if (Multiplayer)
                _player2 = new Player(new Texture2D(graphics, 1, 1), player2StartPosition);
            else
                _ai = new AI(new Texture2D(graphics, 1, 1), player2StartPosition);
        }

        private void SetupVictoryText()
        {
            _victoryConditionText = VICTORY_CONDITION_TEXT_BASE.Replace("VICTORY_CONDITION", Settings.VICTORY_CONDITION.ToString());
            Vector2 victoryConditionTextSize = FontManager.GameFont.MeasureString(_victoryConditionText);
            _victoryConditionPosition = new Vector2((Settings.WINDOW_WIDTH / 2) - (victoryConditionTextSize.X / 2), victoryConditionTextSize.Y);
        }

        private void SetupArena(Texture2D texture)
        {
            // Calculate
            int offset = (int)(_victoryConditionPosition.Y + (int)FontManager.GameFont.MeasureString(_victoryConditionText).Y) + Settings.SCORE_OFFSET;

            // Rectange Setup
            _arena = new Rectangle(1, 0 + offset, Settings.WINDOW_WIDTH - 2, Settings.WINDOW_HEIGHT - (offset + 1));
            _arenaBorder = new Rectangle(_arena.X - 1, _arena.Y - 1, _arena.Width + 2, _arena.Height + 2);

            // Texture
            _arenaTexture = texture;
            _arenaTexture.SetData(new[] { Settings.BACKGROUND_COLOUR });

            _arenaBorderTexture = texture;
            _arenaBorderTexture.SetData(new[] { Settings.BACKGROUND_BORDER_COLOUR });
        }

        private void UpdateScorePositions()
        {
            Vector2 player1ScoreSize = FontManager.GameFont.MeasureString(_player1Score.ToString());
            _player1ScorePosition = new Vector2(Settings.SCORE_OFFSET, player1ScoreSize.Y);

            Vector2 player2ScoreSize = FontManager.GameFont.MeasureString(_player2Score.ToString());
            _player2ScorePosition = new Vector2(Settings.WINDOW_WIDTH - player2ScoreSize.X - Settings.SCORE_OFFSET, player2ScoreSize.Y);
        }

        public Settings.GameStates Update(GameTime gameTime)
        {
            // Check Game isn't paused
            if (!_gameOver)
            {
                // Standard Update Checks
                _ball.Update(gameTime, _arena);
                _player1.Update(gameTime, _arena);

                // Check if Multiplayer
                if (_checkForTwoPlayerInputs)
                {
                    _player2.Update(gameTime, _arena, _checkForTwoPlayerInputs);
                }
                else
                {
                    _ai.Update(gameTime, _arena, _ball);
                }

                // Check Collisions
                _ball.CheckCollision(_player1);
                if (_checkForTwoPlayerInputs)
                {
                    _ball.CheckCollision(_player2);
                }
                else
                {
                    _ball.CheckCollision(_ai);
                }

                // Check if Scored
                if (_ball.GetPosition().X < _arena.X)
                {
                    _ball.Reset();
                    _player2Score++;
                    UpdateScorePositions();
                }
                else if ((_ball.GetPosition().X + Settings.BALL_SIZE) > (_arena.X + _arena.Width))
                {
                    _ball.Reset();
                    _player1Score++;
                    UpdateScorePositions();
                }

                // Check Victory Condition
                if (_player1Score >= Settings.VICTORY_CONDITION || _player2Score >= Settings.VICTORY_CONDITION)
                {
                    // Clear Existing
                    _endGameText.Clear();

                    // Append Relevant Text
                    if (_player1Score >= Settings.VICTORY_CONDITION)
                    {
                        if (_checkForTwoPlayerInputs)
                        {
                            _endGameText.AppendLine("Congratulations Player 1!");
                        }
                        else
                        {
                            _endGameText.AppendLine("Congratulations!");
                        }
                    }
                    else if (_player2Score >= Settings.VICTORY_CONDITION)
                    {
                        if (_checkForTwoPlayerInputs)
                        {
                            _endGameText.AppendLine("Congratulations Player 2!");
                        }
                        else
                        {
                            _endGameText.AppendLine("Game Over!");
                        }
                    }
                    _endGameText.AppendLine("Press [R] to restart.");
                    _endGameText.AppendLine("Press [M] to return to the Menu.");

                    // Calculate Text Position
                    int endGameTextX = (int)((Settings.WINDOW_WIDTH / 2) - (FontManager.GameFont.MeasureString(_endGameText).X / 2));
                    int endGameTextY = (int)((Settings.WINDOW_HEIGHT / 2) - (FontManager.GameFont.MeasureString(_endGameText).Y / 2));
                    _endGamePosition = new Vector2(endGameTextX, endGameTextY);

                    // Stop Game
                    _gameOver = true;
                }
            }
            else
            {
                KeyboardState ks = Keyboard.GetState();

                if (ks.IsKeyDown(Keys.M))
                    return Settings.GameStates.Menu;
                else if (ks.IsKeyDown(Keys.R))
                    Reset();
            }

            return currentGameState;
        }

        private void Reset()
        {
            // Scores
            _player1Score = 0;
            _player2Score = 0;
            UpdateScorePositions();

            // Objects
            _ball.Reset();
            _player1.Reset();

            if (_checkForTwoPlayerInputs)
                _player2.Reset();
            else
                _ai.Reset();

            // Restart
            _endGameText.Clear();
            _gameOver = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Arena
            spriteBatch.Draw(_arenaBorderTexture, _arenaBorder, Settings.BACKGROUND_BORDER_COLOUR);
            spriteBatch.Draw(_arenaTexture, _arena, Settings.BACKGROUND_COLOUR);

            _ball.Draw(spriteBatch);
            _player1.Draw(spriteBatch);

            if (_checkForTwoPlayerInputs)
            {
                _player2.Draw(spriteBatch);
            }
            else
            {
                _ai.Draw(spriteBatch);
            }

            // Score
            spriteBatch.DrawString(FontManager.GameFont, _player1Score.ToString(), _player1ScorePosition, Settings.SCORE_COLOUR);
            spriteBatch.DrawString(FontManager.GameFont, _player2Score.ToString(), _player2ScorePosition, Settings.SCORE_COLOUR);
            spriteBatch.DrawString(FontManager.GameFont, _victoryConditionText, _victoryConditionPosition, Settings.SCORE_COLOUR);

            // End Game Text
            if (_gameOver)
            {
                spriteBatch.DrawString(FontManager.GameFont, _endGameText, _endGamePosition, Settings.VICTORY_COLOUR);
            }
        }
    }
}
