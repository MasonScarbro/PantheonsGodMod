using static GodsAndPantheons.Traits;
using UnityEngine;
using System.Collections.Generic;
using System;
namespace GodsAndPantheons
{
    public class StalagmitePath : BaseEffect
    {
        List<WorldTile> tiles;
        int Current = 0;
        float Speed;
        float TimeLeft;
        string Kingdom;
        public void Init(WorldTile Start, WorldTile End, float Speed, string kingdom = null)
        {
            spawnOnTile(Start);
            Kingdom = kingdom;
            Current = 0;
            sprRenderer.color = Start.Type.color;
            this.Speed = Speed;
            TimeLeft = Speed;
            World.world.pathfindingParam.ocean = true;
            World.world.pathfindingParam.block = true;
            World.world.pathfindingParam.ground = true;
            tiles = new List<WorldTile>();
            if (!World.world.calcPath(Start, End, tiles))
            {
                kill();
                return;
            }
            MusicBox.playSound("event:/SFX/NATURE/EarthQuake", Start);
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            if (Current < tiles.Count)
            {
                TimeLeft -= pElapsed;
                if (TimeLeft <= 0)
                {
                    TimeLeft = Speed;
                    CreateStalagmite();
                    Current += 1;
                }
            }
            else if (GetComponent<SpriteAnimation>().isLastFrame())
            {
                kill();
            }
        }
        public void CreateStalagmite()
        {
            EffectsLibrary.spawnAtTile("Stalagmite", tiles[Current], 0.1f).GetComponent<SpriteRenderer>().color = tiles[Current].Type.color;
            World.world.applyForce(tiles[Current], 2, 0.1f, false, true, 70, Kingdom != null ? new string[] { Kingdom } : null);
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            prepare(pTile, 0.05f);
            spriteAnimation.resetAnim(0);
        }
    }
    public class PulledRock : BaseEffect
    {
        bool Launched;
        Actor Actor;
        public void Init(WorldTile Tile, Actor pActor)
        {
            Actor = pActor;
            sprRenderer.color = Tile.Type.color;
            Launched = false;
            spawnOnTile(Tile);
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            if(Launched)
            {
                if(spriteAnimation.currentFrameIndex == 9)
                {
                    kill();
                }
                return;
            }
            if(spriteAnimation.currentFrameIndex == 7)
            {
                if (Toolbox.randomChance(0.2f))
                {
                    Launch();
                    return;
                }
                spriteAnimation.playType = AnimPlayType.Backward;
            }
            if(spriteAnimation.currentFrameIndex == 5)
            {
                spriteAnimation.playType = AnimPlayType.Forward;
            }
        }

        private void Launch()
        {
            spriteAnimation.playType = AnimPlayType.Forward;
            Launched = true;
            if(Actor != null)
            {
                World.world.getObjectsInChunks(tile, 8, MapObjectType.Actor);
                List<Actor> enemies = GetEnemiesOfActor(World.world.temp_map_objects, Actor);
                if(enemies.Count > 0)
                {
                    ShootCustomProjectile(Actor, enemies.GetRandom(), "EarthShardProjectile", 1, tile.pos);
                }
            }
        }

        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 0.1f);
            spriteAnimation.resetAnim(0);
        }
    }
    public class TerraformPath : BaseEffect
    {
        List<WorldTile> tiles;
        int Current = 0;
        bool BuildUp;
        float Speed;
        float TimeLeft;
        bool shockwave;
        string Kingdom;
        int thickness;
        bool Reverse;
        bool Reversed = false;
        public void Init(WorldTile Start, WorldTile End, bool BuildUp, float Speed, int thickness, bool shockwave, string kingdom = null, bool Reverse = false)
        {
            spawnOnTile(Start);
            Kingdom = kingdom;
            Current = 0;
            this.Reverse = Reverse;
            this.BuildUp = BuildUp;
            this.thickness = thickness;
            this.shockwave = shockwave;
            sprRenderer.color = Start.Type.color;
            this.Speed = Speed;
            TimeLeft = Speed;
            World.world.pathfindingParam.ocean = true;
            World.world.pathfindingParam.block = true;
            World.world.pathfindingParam.ground = true;
            tiles = new List<WorldTile>();
            if(!World.world.calcPath(Start, End, tiles))
            {
                kill();
                return;
            }
            MusicBox.playSound("event:/SFX/NATURE/EarthQuake", Start);
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            if (Reversed ? Current > -1 : Current < tiles.Count)
            {
                TimeLeft -= pElapsed;
                if (TimeLeft <= 0)
                {
                    TimeLeft = Speed;
                    Build();
                    Current += Reversed ? -1 : 1;
                }
            }
            else if (GetComponent<SpriteAnimation>().isLastFrame())
            {
                if(Reverse && !Reversed)
                {
                    if (shockwave)
                    {
                        WorldTile tile = tiles[tiles.Count-1];
                        World.world.applyForce(tile, 4, 0.4f, true, false, 50, Kingdom != null ? new string[] { Kingdom } : null);
                        EffectsLibrary.spawnExplosionWave(tile.posV3, 10f);
                    }
                    Current--;
                    Reversed = true;
                    BuildUp = !BuildUp;
                    Build();
                    return;
                }
                kill();
            }
        }
        public void Build()
        {
            WorldTile Current = tiles[this.Current];
            TileType target = BuildUp ? TileLibrary.mountains : TileLibrary.soil_low;
            TerraformTile(Current, BuildUp, target);
            for (int i = -thickness; i <= thickness; i++)
            {
                for (int j = -thickness; j <= thickness; j++)
                {
                    WorldTile tile = World.world.GetTile(i + Current.x, j + Current.y);
                    if (tile != null)
                    {
                        TerraformTile(tile, BuildUp, target);
                    }
                }
            }
        }
        public static void TerraformTile(WorldTile pTile, bool Up, TileType TargetType)
        {
            if(pTile.Type == TargetType)
            {
                return;
            }
            MapAction.terraformMain(pTile, Up ? pTile.Type.increaseTo : pTile.Type.decreaseTo, AssetManager.terraform.get("flash"));
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            prepare(pTile, 0.07f);
            spriteAnimation.resetAnim(0);
        }
    }
    public class MoonOrbit : BaseEffect
    {
        Actor Actor;
        string Kingdom;
        const float TargetRadius = 5;
        const float Cooldown = 0.5f;
        float Radius;
        float TimeCooldown = Cooldown;
        float TimeLeft;
        float Angle;
        public void Init(Actor pActor, WorldTile pTile, float Time, float pAngleOffset = 0)
        {
            TimeLeft = Time;
            Actor = pActor;
            if(Actor.kingdom != null)
            {
                Kingdom = Actor.kingdom.id;
            }
            spawnOnTile(pTile);
            Angle = pAngleOffset;
        }
        public override void create()
        {
            base.create();
            Angle = 0;
            Radius = 0;
            TimeCooldown = Cooldown;
            Kingdom = null;
            Actor = null;
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            Angle += pElapsed * 2;
            TimeLeft -= pElapsed;
            if(Actor == null)
            {
                MapAction.damageWorld(tile, 5, AssetManager.terraform.get("moonFalling"));
                deactivate();
                return;
            }
            if(TimeLeft <= 0 )
            {
                BaseSimObject Target = Actor.findEnemyObjectTarget();
                if (Target != null) {
                    ShootCustomProjectile(Actor, Target, "moonFallSlow", 1);
                }
                else
                {
                    MapAction.damageWorld(tile, 5, AssetManager.terraform.get("moonFalling"), Actor);
                }
                deactivate();
                return;
            }
            if(Radius < TargetRadius)
            {
                Radius += pElapsed;
                if(Radius > TargetRadius)
                {
                    Radius = TargetRadius;
                }
            }
            Orbit();
            TimeCooldown -= pElapsed;
            if (TimeCooldown <= 0)
            {
                World.world.applyForce(tile, 2, 0.2f, true, true, 20, Kingdom != null ? new string[] { Kingdom } : null, Actor);
                TimeCooldown = Cooldown;
            }
        }
        public void Orbit()
        {
            float x = (Radius * Mathf.Cos(Angle)) + Actor.currentPosition.x;
            float y = (Radius * Mathf.Sin(Angle)) + Actor.currentPosition.y;
            transform.position = new Vector2(x, y);
            tile = World.world.GetTile((int)transform.position.x, (int)transform.position.y);
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 0.1f);
            spriteAnimation.resetAnim(0);
        }
    }
    public class BlackHoleFlash : CustomExplosionFlash
    {
        BaseSimObject byWho;
        public void Init(BaseSimObject actor, WorldTile tile)
        {
            GetComponent<SpriteAnimation>().returnToPool = false;
            byWho = actor;
            this.tile = tile;
            start(tile.pos, 0.01f, .1f, 0);
        }
        const float TimeCoolDown = 0.5f;
        float TimeLeft = TimeCoolDown;
        public override void update(float pElapsed)
        {
            if(byWho == null)
            {
                kill();
                return;
            }
            if(scale >= 0.5)
            {
                MapAction.damageWorld(tile, 4, AssetManager.terraform.get("BlackHole"), byWho);
                kill();
                return;
            }
            TimeLeft -= pElapsed;
            if(TimeLeft <= 0)
            {
                TimeLeft = TimeCoolDown;
                World.world.getObjectsInChunks(tile, (int)scale*10, MapObjectType.Actor);
                foreach (Actor a in World.world.temp_map_objects)
                {
                    if(a.kingdom.id != byWho.kingdom.id)
                    {
                        PushActor(a, tile.pos, 0.5f, 0.4f, true);
                        a.getHit(30, true, AttackType.Eaten, byWho, false);
                    }
                }
                SpawnCustomWave(tile.pos, 0.1f, -0.08f, 0.8f);
                MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", tile);
            }
            base.update(pElapsed);
        }
    }
    public class CustomExplosionFlash : BaseEffect
    {
        public float speed;
        public float AlphaSpeed;
        public void start(Vector2 pVector, float pRadius, float pSpeed = 1f, float pAlphaSpeed = -1f)
        {
            speed = pSpeed;
            AlphaSpeed = pAlphaSpeed;
            transform.position = new Vector3(pVector.x, pVector.y);
            setScale(pRadius);
            setAlpha(AlphaSpeed < 0.00000001f ? 1f : 0);
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            if (AlphaSpeed != 0)
            {
                setAlpha(alpha + (pElapsed * AlphaSpeed));
            }
            setScale(scale + pElapsed * speed);
            if(AlphaSpeed > 0)
            {
                if (alpha >= 1)
                {
                    kill();
                }
                return;
            }
            if (alpha <= 0)
            {
                kill();
            }
        }
    }
    public class Storm : BaseEffect
    {
        public float Time;
        public StormAction? StormAction;
        public float TimeCooldown;
        public float timeleft;
        public float Speed;

        public WorldTile? TileToGo;
        public bool UsingLaser = false;
        public void Init(float Time, float TimeCooldown, StormAction? StormAction)
        {
            this.Time = Time;
            this.StormAction = StormAction;
            this.TimeCooldown = TimeCooldown;
            timeleft = TimeCooldown;
            StormAnimation = GetComponent<SpriteAnimation>();
            Speed = Toolbox.randomFloat(4f, 10f);
        }
        public SpriteAnimation StormAnimation;
        public override void update(float Elapsed)
        {
            base.update(Elapsed);
            World.world.startShake(0.03f, 0.01f, 0.3f, false, true);
            Time -= Elapsed;
            if (Time <= 0)
            {
                return;
            }
            if (StormAnimation.currentFrameIndex > 6)
            {
                StormAnimation.playType = AnimPlayType.Backward;
            }
            if (StormAnimation.currentFrameIndex < 3)
            {
                StormAnimation.playType = AnimPlayType.Forward;
            }
            timeleft -= Elapsed;
            //fire god stuff
            if (UsingLaser)
            {
                pb.drawHeatray(tile, null);
                pb.heatrayFX(tile, "heatray");
                pb.flashPixel(tile, "");
            }
            if (TileToGo != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, TileToGo.posV, Speed * Elapsed);
                tile = World.world.GetTile((int)transform.position.x, (int)transform.position.y);
            }

            if (timeleft <= 0)
            {
                timeleft = TimeCooldown;
                StormAction?.Invoke(this);
            }
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 1);
            spriteAnimation.resetAnim(0);
        }
    }
    public delegate void StormAction(Storm S);
}
