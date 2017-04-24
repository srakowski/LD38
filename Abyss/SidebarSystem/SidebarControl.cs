using Abyss.Infrastructure;
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
        private TextSprite CreditsLabel { get; set; }

        public void LoadContent(ContentManager content)
        {
            SidebarTexture = content.Load<Texture2D>("sprites/sidebar");
            FontTexture = content.Load<Texture2D>("sprites/font");
            CreditsLabel = new TextSprite(FontTexture, "");
        }

        public void Render(SpriteBatch sb, GameState gs)
        {
            sb.Begin(blendState: BlendState.NonPremultiplied);

            sb.Draw(SidebarTexture,
                Position,
                Color);

            CreditsLabel.Text = $"${gs.Credits}";
            CreditsLabel.Draw(sb, new Vector2(Config.ViewportWidth - 310, Config.ViewportHeight - 16));

            sb.End();
        }
    }
}
