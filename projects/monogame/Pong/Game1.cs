using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        // Window Objects
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // Game Objects
        Settings.GameStates? gameState;
        MenuManager menu;
        SettingsManager settings;
        GameManager game;

        public Game1()
        {
            // Window Graphics Setup
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = Settings.WINDOW_WIDTH,
                PreferredBackBufferHeight = Settings.WINDOW_HEIGHT
            };
            graphics.ApplyChanges();
            
            // Content Setup
            Content.RootDirectory = "Content";

            // Game Defaults
            IsMouseVisible = true;
            gameState = Settings.GameStates.Menu;
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

            // Load Fonts
            FontManager.Initalise(Content);

            // Load Menu
            menu = new MenuManager();
            settings = new SettingsManager();
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

            // Update Accordingly
            switch (gameState)
            {
                case Settings.GameStates.Menu:
                    // Get Update
                    gameState = menu.Update();

                    // Check State
                    if (gameState != Settings.GameStates.Menu && gameState != Settings.GameStates.Settings)
                    {
                        switch (gameState)
                        {
                            case Settings.GameStates.Singleplayer:
                                game = new GameManager(GraphicsDevice, false);
                                break;

                            case Settings.GameStates.Multiplayer:
                                game = new GameManager(GraphicsDevice, true);
                                break;
                        }
                        IsMouseVisible = false; // Hide Mouse
                    }
                    break;

                case Settings.GameStates.Settings:
                    // Get Update
                    gameState = settings.Update();
                    break;

                case Settings.GameStates.Singleplayer:
                case Settings.GameStates.Multiplayer:
                    // Get Update
                    gameState = game.Update(gameTime);

                    // Check State
                    if (gameState == Settings.GameStates.Menu || gameState == Settings.GameStates.Settings)
                    {
                        IsMouseVisible = true;
                    }
                    break;

                case null:
                    Exit();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Settings.WINDOW_COLOUR);

            // Start Frame
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            // Draw Objects
            switch (gameState)
            {
                case Settings.GameStates.Menu:
                    menu.Draw(spriteBatch);
                    break;

                case Settings.GameStates.Settings:
                    settings.Draw(spriteBatch);
                    break;

                case Settings.GameStates.Singleplayer:
                case Settings.GameStates.Multiplayer:
                    game.Draw(spriteBatch);
                    break;
            }

            // Finish Frame
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}