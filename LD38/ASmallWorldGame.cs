using LD38.Gameplay;
using LD38.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LD38.DesktopGL
{
    public class ASmallWorldGame : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;
        GameModel _model;

        public ASmallWorldGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Sizes.ScreenBounds.Width;
            _graphics.PreferredBackBufferHeight = Sizes.ScreenBounds.Height;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            Hud.Camera = new Camera(GraphicsDevice);
            _model = GameModel.Dev(new[] {
                Content.Load<Texture2D>("sprites/planet1"),
                Content.Load<Texture2D>("sprites/planet2")
                });
            Hud.MiniMap.Initialize(_model);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Global.FontTexture = Content.Load<Texture2D>("sprites/font");
            Renderer.Initialize(Content, GraphicsDevice);
            Hud.Init(Content);
        }

        protected override void UnloadContent() => Content.Unload();

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Global.Delta = delta;

            _model
                .HandleScroll(delta)
                .HandleClick(delta, Hud.Camera.ToWorldCoords);

            Coroutines.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameColors.Background);
            Renderer.Render(_model, _spriteBatch);
        }
    }
}
