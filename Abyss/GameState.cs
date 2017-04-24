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
        public Int64 Credits { get; set; }

        public Stats Stats { get; set; }

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

        public GameState(Camera camera)
        {
            Camera = camera;
            Current = this;
        }

        private Coroutine SimClock { get; set; }

        public IEnumerator Step()
        {
            while (true)
            {
                foreach (var colony in PlayerColonies)
                    colony.Step();

                //var taxRev = PlayerColonies.Sum(s => s.TaxRevenue);
                //Credits += taxRev;
                ////var operatingCost = PlayerColonies.Sum(s => s.OperatingCost);
                //Credits -= operatingCost;

                //var pop = this.PlayerColonies.Sum(c => c.Population);
                //this.Stats = new Stats(
                //    (int)Credits,
                //    taxRev,
                //    operatingCost,
                //    pop,
                //    this.PlayerColonies.Count(),
                //    this.PlayerShips.Count());

                yield return WaitYieldInstruction.Create(1000);
            }
        }

        public static GameState Current { get; private set; }

        public GameState Initialize(Faction faction)
        {
            SimClock?.Stop();

            Credits = 0;

            PlayerFaction = faction;
            _playerShips.Clear();
            var starterShip = Ship.Starter(faction, this);
            _playerShips.Add(starterShip);

            _playerColonies.Clear();

            for (int i = 0; i < _sectors.Length; i++)
                _sectors[i] = new Sector(i);

            var startingSector = _sectors[Config.R.Next(0, _sectors.Length)];
            starterShip.JumpToSector(startingSector);

            SimClock = new Coroutine(Step());
            Coroutines.Start(SimClock);

            Current = this;
            return this;
        }

        internal void AddPlayerColony(Colony colony)
        {
            _playerColonies.Add(colony);
            Controllable = colony;
        }

        private bool _acceptingInput = true;

        public void HandleInput(InputState input)
        {
            if (Controllable == null)
                return;

            if (input.WasAnyOfTheseKeysPressed(Keys.Up, Keys.W)) { Controllable.ActionUp(); SkipInputForABit(); }
            else if (input.WasAnyOfTheseKeysPressed(Keys.Down, Keys.S)) { Controllable.ActionDown(); SkipInputForABit(); }
            else if (input.WasAnyOfTheseKeysPressed(Keys.Left, Keys.A)) { Controllable.ActionLeft(); SkipInputForABit(); }
            else if (input.WasAnyOfTheseKeysPressed(Keys.Right, Keys.D)) { Controllable.ActionRight(); SkipInputForABit(); }
            //if (_acceptingInput)
            //{
            //    if (input.AreAnyOfTheseKeysDown(Keys.Up, Keys.W)) { Controllable.ActionUp(); SkipInputForABit(); }
            //    else if (input.AreAnyOfTheseKeysDown(Keys.Down, Keys.S)) { Controllable.ActionDown(); SkipInputForABit(); }
            //    else if (input.AreAnyOfTheseKeysDown(Keys.Left, Keys.A)) { Controllable.ActionLeft(); SkipInputForABit(); }
            //    else if (input.AreAnyOfTheseKeysDown(Keys.Right, Keys.D)) { Controllable.ActionRight(); SkipInputForABit(); }
            //}

            if (Controllable == null)
                return;

            if (Vector2.Distance(Camera.Position, Controllable.RenderPosition) > Config.CameraFollowThreshold)
                Camera.MoveToLocation(Controllable.RenderPosition);
        }

        private void SkipInputForABit()
        {
            _acceptingInput = false;
            IEnumerator SkipInput()
            {
                yield return WaitYieldInstruction.Create(Config.InputRepeatDelay);
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
        private Texture2D[] PlanetTextures = new Texture2D[Config.PlanetTextureCount];
        private Texture2D CellTexture;
        private Texture2D ColonyTexture;

        private TextSprite CreditsLabel { get; set; }

        public GameState LoadContent(ContentManager content)
        {
            ShipTexturesByType[ShipType.SmallTrading] = content.Load<Texture2D>("sprites/smalltrade");
            CellTexture = content.Load<Texture2D>("sprites/cell");
            ColonyTexture = content.Load<Texture2D>("sprites/colony");
            Texture2D font = content.Load<Texture2D>("sprites/font");
            CreditsLabel = new TextSprite(font, "$0");
            for (int i = 0; i < Config.PlanetTextureCount; i++)
                PlanetTextures[i] = content.Load<Texture2D>($"sprites/planet{i + 1}");
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
                        var cell = LoadedSector.Cells[x, y];
                        sb.Draw(CellTexture,
                            Config.CellSize * new Vector2(x, y),
                            null,
                            new Color(GameColors.Foreground, 10),
                            0f,
                            CellTexture.CenterOrigin(),
                            1f,
                            SpriteEffects.None,
                            0f);

                        switch (cell.Occupant)
                        {
                            case Planet p:
                                var t = PlanetTextures[p.TextureIdx];
                                sb.Draw(t,
                                    Config.CellSize * new Vector2(x, y),
                                    null,
                                    new Color(Color.White, Controllable == p ? 255 : 100),
                                    0f,
                                    t.CenterOrigin(),
                                    1f,
                                    SpriteEffects.None,
                                    0f);
                                break;
                            case Colony c:
                                var u = PlanetTextures[c.Planet.TextureIdx];
                                sb.Draw(u,
                                    Config.CellSize * new Vector2(x, y),
                                    null,
                                    new Color(Color.White, Controllable == c ? 255 : 100),
                                    0f,
                                    u.CenterOrigin(),
                                    1f,
                                    SpriteEffects.None,
                                    0f);

                                CreditsLabel.Text = $"${c.Credits}";
                                CreditsLabel.Draw(sb, (Config.CellSize * new Vector2(x, y)) + u.CenterOrigin());
                                break;

                            case Store s:
                                sb.Draw(ColonyTexture,
                                    Config.CellSize * new Vector2(x, y),
                                    null,
                                    new Color(Color.White, Controllable == s ? 255 : 100),
                                    0f,
                                    ColonyTexture.CenterOrigin(),
                                    1f,
                                    SpriteEffects.None,
                                    0f);


                                break;
                            default:
                                break;
                        }
                    }
            }

            //foreach (var colony in LoadedSector?.ColoniesThisSector ?? Enumerable.Empty<Colony>())
            //{
            //    sb.Draw(ColonyTexture,
            //        colony.RenderPosition,
            //        null,
            //        new Color(Color.White, Controllable == colony ? 255 : 100),
            //        0f,//ship.Rotation,
            //        ColonyTexture.CenterOrigin(),
            //        1f,
            //        SpriteEffects.None,
            //        0f);
            //}

            foreach (var ship in LoadedSector?.ShipsInSector ?? Enumerable.Empty<Ship>())
            {
                var texture = ShipTexturesByType[ship.Type];
                sb.Draw(texture,
                    ship.RenderPosition,
                    null,
                    new Color(Color.White, Controllable == ship ? 255 : 100),
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
