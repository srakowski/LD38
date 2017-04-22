using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LD38.Rendering
{
    public class TextSprite
    {
        private Vector2 CharDim { get; }
        private Rectangle[] SourceRects { get; }
        public Texture2D FontTexture { get; }
        public Transform Transform { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }

        public TextSprite(Texture2D texture, string text, Vector2 position)
        {
            Color = GameColors.Foreground;
            Transform = new Transform(position, 0f, 1f);
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

        public void Draw(SpriteBatch sb)
        {
            for (int x = 0; x < Text.Length; x++)
                sb.Draw(
                    FontTexture,
                    Transform.Position + new Vector2(CharDim.X * x, 0),
                    SourceRects[Text[x]],
                    Color);

        }
    }
}
