using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Snake.Objects;

namespace Snake
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // Const Objects
        private const string GAMEOVER_TEXT = "Game Over! Press [R] to Restart";

        // Window Objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont _scoreFont;
        SpriteFont _gameOverFont;

        // Display Objects
        Texture2D backgroundTexture;
        Rectangle whiteBorder;
        Rectangle background;

        // Game Objects
        Player snake;
        Apple apple;
        bool GameOver = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Settings.GRID_WIDTH,
                PreferredBackBufferHeight = Settings.GRID_HEIGHT
            };
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() => base.Initialize();

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Initialise Objects
            _scoreFont = Content.Load<SpriteFont>("Score");
            _gameOverFont = Content.Load<SpriteFont>("GameOver");

            // Populate Display Objects
            backgroundTexture = new Texture2D(GraphicsDevice, 1, 1);
            backgroundTexture.SetData(new[] { Settings.BORDER_COLOUR });

            whiteBorder = new Rectangle(new Point(0, 0), Global_Functions.GetActualGridSize());
            background = new Rectangle(whiteBorder.X + 1, whiteBorder.Y + 1, whiteBorder.Width - 2, whiteBorder.Height - 2);

            // Populate Game Objects
            snake = new Player(new Texture2D(GraphicsDevice, 1, 1), Global_Functions.GetStartPosition());
            apple = new Apple(new Texture2D(GraphicsDevice, 1, 1), snake);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() { }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!GameOver)
            {
                // Player Logic (also checks Loss Condition)
                GameOver = snake.Update(gameTime);

                // If Loss Condition not met
                if (!GameOver)
                {
                    // Apple Logic (also checks Win Condition)
                    GameOver = apple.Update(snake);
                }
            }
            else
            {
                // Reset the Game
                KeyboardState ks = Keyboard.GetState();
                if (ks.IsKeyDown(Keys.R))
                {
                    snake = new Player(new Texture2D(GraphicsDevice, 1, 1), Global_Functions.GetStartPosition());
                    apple = new Apple(new Texture2D(GraphicsDevice, 1, 1), snake);
                    GameOver = false;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Settings.GRID_COLOUR);

            // Initalise
            int score = snake.GetBodyCount() - Settings.STARTING_BODY_COUNT;

            // Start Frame
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            // Draw Background
            spriteBatch.Draw(backgroundTexture, whiteBorder, Settings.BORDER_COLOUR);
            spriteBatch.Draw(backgroundTexture, background, Settings.GRID_COLOUR);

            // Draw Objects
            snake.Draw(spriteBatch);
            apple.Draw(spriteBatch);

            // Draw Text
            spriteBatch.DrawString(_scoreFont, score.ToString(), new Vector2(Settings.GRID_WIDTH - 35, 10), Settings.FONT_COLOUR);

            // Game Over Text
            if (GameOver)
            {
                int textX = (int)((Settings.GRID_WIDTH / 2) - (_gameOverFont.MeasureString(GAMEOVER_TEXT).X / 2)); // Divide by 2 so in center of Screen
                int textY = Settings.GRID_HEIGHT / 2;
                spriteBatch.DrawString(_gameOverFont, GAMEOVER_TEXT, new Vector2(textX, textY), Settings.FONT_COLOUR);
            }

            // Finish Frame
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}