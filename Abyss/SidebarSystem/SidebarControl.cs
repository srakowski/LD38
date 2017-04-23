using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Abyss.SidebarSystem
{
    public class SidebarControl
    {
        
        private Texture2D SidebarTexture { get; set; }
        private Texture2D FontTexture { get; set; }
        private Vector2 Position { get; } = new Vector2(Config.ViewportWidth - 320, 0);
        private Color Color { get; } = new Color(Color.Black, 180);

        public void LoadContent(ContentManager content)
        {
            SidebarTexture = content.Load<Texture2D>("sprites/sidebar");
            FontTexture = content.Load<Texture2D>("sprites/font");
        }

        public void Render(SpriteBatch sb)
        {
            sb.Begin(blendState: BlendState.NonPremultiplied);

            sb.Draw(SidebarTexture,
                Position,
                Color);


            sb.End();
        }
    }
}
