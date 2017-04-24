using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Abyss.Infrastructure
{
    public class TextSprite
    {
        public Vector2 CharDim { get; }
        private Rectangle[] SourceRects { get; }
        public Texture2D FontTexture { get; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public bool Enabled { get; set; } = true;

        public TextSprite(Texture2D texture, string text)
        {
            Color = GameColors.Foreground;
            Text = text;
            SourceRects = new Rectangle[256];
            FontTexture = texture;
            var charDim = new Vector2(texture.Width / 16, texture.Height / 16);
            int c = 0;
            for (int y = 0; y < 16; y++)
                for (int x = 0; x < 16; x++)
                {
                    SourceRects[c] = new Rectangle((new Vector2(x, y) * charDim).ToPoint(), charDim.ToPoint());
                    c++;
                }
            CharDim = charDim;
        }

        public void Draw(SpriteBatch sb, Vector2 pos)
        {
            if (!Enabled) return;

            for (int x = 0; x < Text.Length; x++)
                sb.Draw(
                    FontTexture,
                    pos + new Vector2(CharDim.X * x, 0),
                    SourceRects[Text[x]],
                    Color);

        }
    }
}
