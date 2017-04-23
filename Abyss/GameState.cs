using Abyss.Infrastructure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace Abyss
{
    public class GameState
    {
        public Camera Camera { get; set; }

        public Faction PlayerFaction { get; private set; }

        private List<Ship> _playerShips = new List<Ship>();
        public IEnumerable<Ship> PlayerShips => _playerShips;

        private List<Colony> _playerColonies = new List<Colony>();
        public IEnumerable<Colony> PlayerColonies => _playerColonies;

        private Sector[] _sectors = new Sector[Config.NumSectors];
        public IEnumerable<Sector> Sectors => _sectors;

        public Sector LoadedSector { get; private set; }

        public IControllable Controllable { get; set; }

        public GameState(Camera camera) =>
            Camera = camera;

        public GameState Initialize(Faction faction)
        {
            PlayerFaction = faction;
            _playerShips.Clear();
            var starterShip = Ship.Starter(faction);
            _playerShips.Add(starterShip);

            _playerColonies.Clear();

            for (int i = 0; i < _sectors.Length; i++)
                _sectors[i] = new Sector(i);

            var startingSector = _sectors[Config.R.Next(0, _sectors.Length)];
            starterShip.JumpToSector(startingSector);

            return this;
        }

        internal void AddPlayerColony(Colony colony) =>
            _playerColonies.Add(colony);

        private bool _acceptingInput = true;

        public void HandleInput(InputState input)
        {
            if (!_acceptingInput) return;
            if (Controllable != null)
            {
                if (input.AreAnyOfTheseKeysDown(Keys.Up, Keys.W)) { Controllable.ActionUp(); SkipInputForABit(); }
                if (input.AreAnyOfTheseKeysDown(Keys.Down, Keys.S)) { Controllable.ActionDown(); SkipInputForABit(); }
                if (input.AreAnyOfTheseKeysDown(Keys.Left, Keys.A)) { Controllable.ActionLeft(); SkipInputForABit(); }
                if (input.AreAnyOfTheseKeysDown(Keys.Right, Keys.D)) { Controllable.ActionRight(); SkipInputForABit(); }
            }
            if (Controllable is Ship ship)
                if (Vector2.Distance(Camera.Position, ship.RenderPosition) > 100f)
                    Camera.MoveToLocation(ship.RenderPosition);
        }

        private void SkipInputForABit()
        {
            _acceptingInput = false;
            IEnumerator SkipInput()
            {
                yield return WaitYieldInstruction.Create(100);
                _acceptingInput = true;
            }
            Coroutines.Start(new Coroutine(SkipInput()));
        }


        public GameState JumpToSector(int sectorNumber)
        {
            if ((LoadedSector?.Number ?? -1) == sectorNumber)
                return this;

            LoadedSector = _sectors[MathHelper.Clamp(sectorNumber, 0, Config.NumSectors)];
            LoadedSector.IsExplored = true;

            return this;
        }

        public GameState Select(IControllable controllable)
        {
            this.Controllable = controllable;
            return this;
        }

        private Dictionary<ShipType, Texture2D> ShipTexturesByType = new Dictionary<ShipType, Texture2D>();
        private Texture2D CellTexture;
        private Texture2D ColonyTexture;

        public GameState LoadContent(ContentManager content)
        {
            ShipTexturesByType[ShipType.SmallTrading] = content.Load<Texture2D>("sprites/smalltrade");
            CellTexture = content.Load<Texture2D>("sprites/cell");
            ColonyTexture = content.Load<Texture2D>("sprites/colony");
            return this;
        }

        internal void Render(SpriteBatch sb)
        {
            sb.Begin(transformMatrix: Camera.TransformationMatrix, blendState: BlendState.NonPremultiplied);

            if (LoadedSector != null)
            {
                for (int x = 0; x < Config.SectorWidth; x++)
                    for (int y = 0; y < Config.SectorHeight; y++)
                    {
                        sb.Draw(CellTexture,
                            Config.CellSize * new Vector2(x, y),
                            null,
                            new Color(GameColors.Foreground, 10),
                            0f,
                            CellTexture.CenterOrigin(),
                            1f,
                            SpriteEffects.None,
                            0f);
                    }
            }

            foreach (var colony in LoadedSector?.ColoniesThisSector ?? Enumerable.Empty<Colony>())
            {
                sb.Draw(ColonyTexture,
                    colony.RenderPosition,
                    null,
                    Color.White,
                    0f,//ship.Rotation,
                    ColonyTexture.CenterOrigin(),
                    1f,
                    SpriteEffects.None,
                    0f);
            }

            foreach (var ship in LoadedSector?.ShipsInSector ?? Enumerable.Empty<Ship>())
            {
                var texture = ShipTexturesByType[ship.Type];
                sb.Draw(texture,
                    ship.RenderPosition,
                    null,
                    Color.White,
                    0f,//ship.Rotation,
                    texture.CenterOrigin(),
                    1f,
                    SpriteEffects.None,
                    0f);
            }

            sb.End();
        }
    }
}
