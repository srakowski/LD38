using Abyss.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Collections;

namespace Abyss.MenuSystem
{
    public class MenuControl
    {
        private Stack<Menu> _menuStack = new Stack<Menu>();

        public Menu Menu => _menuStack.Peek();

        public bool IsOpen { get; private set; } = true;

        public MenuControl(Texture2D fontTexture, Menu topLevel)
        {
            FontTexture = fontTexture;
            PushMenu(topLevel);
            TitleLabel = new TextSprite(fontTexture, "Abyss");
            InstructionLabel = new TextSprite(fontTexture, "Press [Escape] or [Enter] to display the menu");
            OptionLabels = new TextSprite[Config.MaxMenuOptions];
            for (int i = 0; i < OptionLabels.Length; i++)
                OptionLabels[i] = new TextSprite(fontTexture, String.Empty);

            DataSectionTitleLabel = new TextSprite(fontTexture, String.Empty);
            DataSectionLabels = new TextSprite[Config.NumDataSectionLines];
            for (int i = 0; i < DataSectionLabels.Length; i++)
                DataSectionLabels[i] = new TextSprite(fontTexture, String.Empty);
        }

        public MenuControl PushMenu(Menu menu)
        {
            _menuStack.Push(menu);
            return this;
        }

        internal void SwapMenus(Menu menu)
        {
            PopMenu();
            PushMenu(menu);
        }

        public MenuControl PopMenu()
        {
            _menuStack.Pop();
            return this;
        }

        private Vector2 Position { get; } = new Vector2(Config.ViewportWidth - 320 + 12, 12);

        private Texture2D FontTexture { get; }

        private TextSprite TitleLabel { get; }

        private TextSprite InstructionLabel { get; }

        private TextSprite DataSectionTitleLabel { get; }
        private TextSprite[] DataSectionLabels { get; }

        private TextSprite[] OptionLabels { get; }
        
        private bool skipInput = false;

        private TextSprite GameTitle { get; } 

        public void HandleInput(InputState input)
        {
            if (input.WasAnyOfTheseKeysPressed(Keys.Enter, Keys.Escape))
            {
                IsOpen = !IsOpen;
            }

            if (IsOpen)
            {
                var options = Menu.Options.ToArray();
                if (input.WasAnyOfTheseKeysPressed(Keys.D1, Keys.NumPad1) && options.Length > 0) { options[0].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D2, Keys.NumPad2) && options.Length > 1) { options[1].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D3, Keys.NumPad3) && options.Length > 2) { options[2].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D4, Keys.NumPad4) && options.Length > 3) { options[3].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D5, Keys.NumPad5) && options.Length > 4) { options[4].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D6, Keys.NumPad6) && options.Length > 5) { options[5].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D7, Keys.NumPad7) && options.Length > 6) { options[6].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D8, Keys.NumPad8) && options.Length > 7) { options[7].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D9, Keys.NumPad9) && options.Length > 8) { options[8].Invoke(this); }
                if (input.WasAnyOfTheseKeysPressed(Keys.D0, Keys.NumPad0) && options.Any(o => o.IsCancel)) { options.First(o => o.IsCancel).Invoke(this); }
            }
        }

        public MenuControl Update()
        {
            Menu?.Update?.Invoke(this);
            return this;
        }
         
        public void Render(SpriteBatch sb)
        {
            if (IsOpen) RenderOpenMenu(sb);
            else RenderInstruction(sb);
        }

        private void RenderOpenMenu(SpriteBatch sb)
        {
            Sync();
            sb.Begin(samplerState: SamplerState.PointClamp, transformMatrix: Matrix.Identity * Matrix.CreateScale(1));
            TitleLabel.Draw(sb, Position);
            int i = 0;
            foreach (var option in OptionLabels.Where(o => o.Enabled))
            {
                var drawAt = Position + new Vector2(0, ((i + 1) * option.CharDim.Y * 1.5f));
                option.Draw(sb, drawAt);
                i++;
            }

            i = Config.MaxMenuOptions;
            var newPos = Position + new Vector2(0, ((i + 1) * 8f * 1.5f) + 24);
            DataSectionTitleLabel.Draw(sb, newPos);
            i = 0;
            foreach (var option in DataSectionLabels.Where(o => o.Enabled))
            {
                var drawAt = newPos + new Vector2(0, ((i + 1) * option.CharDim.Y * 1.5f));
                option.Draw(sb, drawAt);
                i++;
            }

            sb.End();
        }

        private void RenderInstruction(SpriteBatch sb)
        {
            sb.Begin(samplerState: SamplerState.PointClamp);
            InstructionLabel.Draw(sb, Vector2.Zero);
            sb.End();
        }

        private void Sync()
        {
            TitleLabel.Text = Menu.Title;

            for (int i = 0; i < OptionLabels.Length; i++)
                OptionLabels[i].Enabled = false;

            for (int i = 0; i < DataSectionLabels.Length; i++)
                DataSectionLabels[i].Text = "";

            int j = 0;
            foreach (var option in Menu.Options.Where(o => !o.IsCancel))
            {
                if (!option.JustText)
                    OptionLabels[j].Text = $"{j + 1}:{option.Text}";
                else
                    OptionLabels[j].Text = $"<{option.Text}>";

                OptionLabels[j].Enabled = true;
                OptionLabels[j].Color = option.Color;
                j++;
            }

            var cancel = Menu.Options.FirstOrDefault(o => o.IsCancel);
            if (cancel != null)
            {
                OptionLabels[9].Text = $"0:{cancel.Text}";
                OptionLabels[9].Enabled = true;
                OptionLabels[9].Color = cancel.Color;
            }

            DataSectionTitleLabel.Text = Menu.DataSectionTitle ?? string.Empty;
            if (Menu.DataSectionText != null)
            {
                j = 0;
                foreach (var text in Menu.DataSectionText)
                {
                    DataSectionLabels[j].Text = text;
                    j++;
                }
            }
        }
    }
}
