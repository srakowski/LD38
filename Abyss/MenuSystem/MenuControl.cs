using Abyss.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Abyss.MenuSystem
{
    public class MenuControl
    {
        private Stack<Menu> _menuStack = new Stack<Menu>();

        public Menu Menu => _menuStack.Peek();

        public MenuControl(Texture2D fontTexture, Menu topLevel)
        {
            FontTexture = fontTexture;
            PushMenu(topLevel);
            TitleLabel = new TextSprite(fontTexture, "Abyss");
            OptionLabels = new TextSprite[Config.MaxMenuOptions];
            for (int i = 0; i < OptionLabels.Length; i++)
                OptionLabels[i] = new TextSprite(fontTexture, String.Empty);
        }

        public MenuControl PushMenu(Menu menu)
        {
            _menuStack.Push(menu);
            return this;
        }

        public MenuControl PopMenu()
        {
            _menuStack.Pop();
            return this;
        }

        private Vector2 Position { get; } = Vector2.Zero;

        private Texture2D FontTexture { get; }

        private TextSprite TitleLabel { get; }

        private TextSprite[] OptionLabels { get; }

        public void Render(SpriteBatch sb)
        {
            Sync();
            sb.Begin(samplerState: SamplerState.PointClamp);
            TitleLabel.Draw(sb, Position);
            for (int i = 0; i < OptionLabels.Length; i++)
            {
                var drawAt = Position + new Vector2(0, ((i + 1) * OptionLabels[i].CharDim.Y * 1.5f));
                OptionLabels[i].Draw(sb, drawAt);
            }
            sb.End();
        }

        private void Sync()
        {
            TitleLabel.Text = Menu.Title;
            for (int i = 0; i < OptionLabels.Length; i++)
                OptionLabels[i].Enabled = false;

            int j = 0;
            foreach (var option in Menu.Options)
            {
                OptionLabels[j].Text = $"&{j + 1}: {option.Text}";
                OptionLabels[j].Enabled = true;
                OptionLabels[j].Color = option.Color;
                j++;
            }
        }
    }
}
