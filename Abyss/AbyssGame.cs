using Abyss.MenuSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss
{
    public class AbyssGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private InputState _input;
        private MenuControl _menuControl;
        private SpriteBatch _sb;

        public AbyssGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Config.ViewportWidth;
            _graphics.PreferredBackBufferHeight = Config.ViewportHeight;
            Content.RootDirectory = "Content";
            _input = new InputState();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            _sb = new SpriteBatch(GraphicsDevice);
            _menuControl = new MenuControl(
                Content.Load<Texture2D>("sprites/font"),
                Menus.Starting.Main(new GameState()));
        }

        protected override void Update(GameTime gameTime)
        {
            _input.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameColors.Background);
            _menuControl.Render(_sb);
        }
    }
}
