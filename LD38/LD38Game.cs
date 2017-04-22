using Coldsteel;
using LD38.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace LD38
{
    public class LD38Game : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public LD38Game()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
            Components.Add(new ColdsteelComponent(this,
                Gameplay.Scene, Controls.Get));
        }
    }
}
