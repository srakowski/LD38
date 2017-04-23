using Abyss.Infrastructure;
using Abyss.MenuSystem;
using Abyss.SidebarSystem;
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
        private Starfield _sf;
        private GameState _gs;
        private SidebarControl _sbc;

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
            var cam = new Camera(GraphicsDevice);
            _sf = new Starfield(Content.Load<Texture2D>("sprites/star"), cam);
            _gs = new GameState(cam);
            _gs.LoadContent(Content);
            _sbc = new SidebarControl();
            _sbc.LoadContent(Content);
            _menuControl = new MenuControl(
                Content.Load<Texture2D>("sprites/font"),
                Menus.Starting.MainMenu(_gs));
        }

        protected override void Update(GameTime gameTime)
        {
            Delta.Value = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            _input.Update();
            _menuControl.HandleInput(_input);
            _gs.HandleInput(_input);
            Coroutines.Update(gameTime);
            _sf.Update(gameTime);
            _menuControl.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(GameColors.Background);
            _sf.Render(_sb);
            _gs.Render(_sb);
            _sbc.Render(_sb);
            _menuControl.Render(_sb);
        }
    }
}
