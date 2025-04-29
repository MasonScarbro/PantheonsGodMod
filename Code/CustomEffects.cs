using static GodsAndPantheons.Traits;
using UnityEngine;
using System.Collections.Generic;
namespace GodsAndPantheons.CustomEffects
{
    public class BloodMoon : BaseEffect
    {
        public static bool BloodMoonPresent = false;
        Actor ByWho;
        Kingdom kingdom;
        float TimeLeft;
        public void Init(Actor who, WorldTile Tile, float Time)
        {
            BloodMoonPresent = true;
            ByWho = who;
            kingdom = ByWho.kingdom;
            spawnOnTile(Tile);
            if(Time == -1) {
                Time = Randy.randomFloat(15, 25);
            }
            TimeLeft = Time;
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            TimeLeft -= pElapsed;
            if(ByWho == null && kingdom == null)
            {
                TimeLeft = 0;
            }
            if(TimeLeft <= 0)
            {
                sprite_animation.playType = AnimPlayType.Backward;
                return;
            }
            if (sprite_animation.currentFrameIndex == 9)
            {
                sprite_animation.playType = AnimPlayType.Backward;
                CorruptEnviornment();
            }
            else if(sprite_animation.currentFrameIndex == 5)
            {
                sprite_animation.playType = AnimPlayType.Forward;
            }
        }
        public override void kill()
        {
            base.kill();
            BloodMoonPresent = false;
        }
        public void CorruptEnviornment()
        {
            foreach (BaseSimObject enemy in GetEnemiesOfKingdom(Finder.getUnitsFromChunk(tile, 4, 56), kingdom??ByWho.kingdom))
            {
                if (Randy.randomChance(0.5f) && !enemy.a.hasTrait("madness"))
                {
                    DropsLibrary.action_madness(tile);
                    DropsLibrary.action_madness(Toolbox.getRandomTileWithinDistance(tile, 8));
                    continue;
                }
                if (Randy.randomChance(0.2f))
                {
                    MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionForce", enemy.current_tile, true);
                    World.world.applyForceOnTile(enemy.current_tile, 6, 3f, true, 0, null, ByWho, null);
                    EffectsLibrary.spawnExplosionWave(enemy.current_position, 6f);
                    continue;
                }
                if (Randy.randomChance(0.1f))
                {
                    MapBox.spawnLightningBig(enemy.current_tile, 0.2f, ByWho);
                    continue;
                }
                if (enemy.a.IsGod())
                {
                    UnleashMoonFall(ByWho, enemy, enemy.current_tile);
                    continue;
                }
                Actor actor = enemy.a.Morph(SA.demon, false);
                if(actor != null)
                {
                    TurnActorIntoSummonedOne(actor, ByWho, 130);
                }
            }
        }

        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 0.5f);
            sprite_animation.resetAnim(0);
        }
    }
    public class LightGodsOrb : BaseEffect
    {
        public BaseSimObject ByWho;
        public BaseSimObject Target;
        public float TimeLeft;
        public WorldTile TargetTile;
        public float Speed;
        public float Timer;
        public float TrailCooldown = 0.1f;
        public void Init(BaseSimObject pByWho, WorldTile pTile, float pTimeLeft = -1)
        {
            if(pTimeLeft == -1)
            {
                pTimeLeft = Randy.randomInt(5, 10);
            }
            TimeLeft = pTimeLeft;
            Speed = Randy.randomFloat(9, 18);
            TargetTile = null;
            ByWho = pByWho;
            Timer = 0.5f;
            Target = null;
            pTile ??= pByWho.current_tile;
            spawnOnTile(pTile);
        }
        private void updateTrailEffect(float pElapsed)
        {
            if (TrailCooldown > 0f)
            {
                TrailCooldown -= pElapsed;
                return;
            }
            EffectsLibrary.spawnAt("fx_plasma_trail", transform.position, 0.1f);
            TrailCooldown = 0.1f;
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            updateTrailEffect(pElapsed);
            if (TimeLeft <= 0)
            {
                setScale(scale *= 0.8f);
                if(scale <= 0.005f)
                {
                    kill();
                }
                return;
            }
            TimeLeft -= pElapsed;
            tile = World.world.GetTileSimple((int)transform.position.x, (int)transform.position.y);
            if (tile == null || ByWho == null)
            {
                TimeLeft = 0;
                return;
            }
            Target ??= GetTarget();
            if(Target == null || !Target.isAlive())
            {
                if (SwitchTile())
                {
                    TargetTile = Toolbox.getRandomTileWithinDistance(tile, 5);
                }
            }
            else if(SwitchTile())
            {
                TargetTile = Target.current_tile;
            }
            if (TargetTile != null)
            {
                Vector3 tDirection = (TargetTile.posV3 - transform.position).normalized;
                transform.position += tDirection * Speed * pElapsed;
            }
            Timer -= pElapsed;
            if(Timer <= 0)
            {
                Timer = 0.5f;
                MapAction.damageWorld(tile, 3, AssetManager.terraform.get("PassiveDamage"), ByWho);
            }
        }
        public Actor GetTarget()
        {
            List<Actor> enemies = GetEnemiesOfActor(Finder.getUnitsFromChunk(tile, 1, 8), ByWho);
            if (enemies.Count > 0)
            {
                return enemies.GetRandom();
            }
            return null;
        }
        public bool SwitchTile()
        {
            return tile == TargetTile || TargetTile == null;
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 0.02f);
            sprite_animation.resetAnim(0);
        }
    }
    public class FireTornado : TornadoEffect
    {
        float FireTimer = 0.8f;
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            if (World.world.isPaused())
            {
                return;
            }
            if (isKilled())
            {
                return;
            }
            FireTimer -= pElapsed;
            if (FireTimer < 0)
            {
                FireTimer = 0.8f;
                for (int i = 0; i < Randy.randomInt(3, 7); i++)
                {
                    World.world.drop_manager.spawnParabolicDrop(current_tile, "fire", 0, 0.15f, 113, 1, 80, 0.7f);
                }
            }
        }
    }
    public class ChaosLaser : BaseEffect
    {
        public Actor pSelf;
        public int currentframe;
        public float timetochange;
        const float Timer = 0.07f;
        public SpriteRenderer LaserSprite;
        public Transform LaserPoint;
        public Transform Laser;
        public static List<Sprite> LaserSprites;
        public override void Awake() {}
        public void UpdateTransform()
        {
            transform.localScale = pSelf.current_scale;
            transform.position = pSelf.current_position;
            transform.rotation = Quaternion.Euler(pSelf.current_rotation);
            float angle = Mathf.Atan2(pSelf.attack_target.current_position.y - Laser.position.y, pSelf.attack_target.current_position.x - Laser.position.x) * Mathf.Rad2Deg;
            Laser.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        public void Init(Actor actor)
        {
            currentframe = 0;
            timetochange = Timer;
            LaserSprite.sprite = LaserSprites[currentframe];
            pSelf = actor;
            UpdateTransform();
        }
        public override void update(float pElapsed)
        {
            if (pSelf == null || !pSelf.hasStatus("Lassering"))
            {
                kill();
                return;
            }
            if (pSelf.has_attack_target && pSelf.attack_target != null)
            {
                updatelaser();
            }
            else if (!pSelf.checkEnemyTargets())
            {
                pSelf.finishStatusEffect("Lassering");
                kill();
            }
        }

        public void updatelaser()
        {
            if (World.world.isPaused())
            {
                return;
            }
            UpdateTransform();
            updatelasersprite(Time.deltaTime);
            float x = LaserPoint.transform.position.x;
            float y = LaserPoint.transform.position.y;
            MusicBox.inst.playDrawingSound("event:/SFX/UNIQUE/Crabzilla/CrabzillaLazer", x, y);
            if (currentframe > 6 && currentframe < 10)
            {
                DamageWorld(x, y);
            }
        }
        public void DamageWorld(float x, float y)
        {
            WorldTile tile = World.world.GetTile(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
            if (tile != null)
            {
                ///ARMOR PENETRATING
                BaseSimObject Enemy = pSelf.attack_target;
                if(Enemy.isActor())
                {
                    Enemy.a.getHit(2 * pSelf.stats["damage"], true, AttackType.Fire, pSelf, false);
                    Enemy.a.calculateForce(x, y, Enemy.a.current_position.x, Enemy.a.current_position.y, 6, 1);
                }
                else
                {
                    Enemy.b.getHit(2 * pSelf.stats["damage"], true, AttackType.Fire, pSelf, false);
                }
                MapAction.damageWorld(tile, 4, AssetManager.terraform.get("LesserCrabLaser"), pSelf);
            }
        }
        public void updatelasersprite(float pElapsed)
        {
            timetochange -= pElapsed;
            LaserSprite.enabled = true;
            if (timetochange <= 0f)
            {
                currentframe++;
                if (currentframe >= 10)
                {
                    currentframe = 6;
                }
                timetochange = Timer;
            }
            if (LaserSprite.sprite.name != LaserSprites[currentframe].name)
            {
                LaserSprite.sprite = LaserSprites[currentframe];
            }
        }
    }
    public class StalagmitePath : BaseEffect
    {
        List<WorldTile> tiles;
        int Current = 0;
        float Speed;
        float TimeLeft;
        BaseSimObject ByWho;
        public void Init(WorldTile Start, WorldTile End, float Speed, BaseSimObject ByWho)
        {
            spawnOnTile(Start);
            Current = 0;
            this.ByWho = ByWho;
            sprite_renderer.color = Start.Type.color;
            this.Speed = Speed;
            TimeLeft = Speed;
            World.world.pathfinding_param.ocean = false;
            World.world.pathfinding_param.block = true;
            World.world.pathfinding_param.ground = true;
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
            EffectsLibrary.spawnAtTile("Stalagmite", tiles[Current], 0.1f);
            World.world.applyForceOnTile(tiles[Current], 2, 0.1f, true, 70, null, ByWho);
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            prepare(pTile, 0.05f);
            sprite_animation.resetAnim(0);
        }
    }
    public class PulledRock : BaseEffect
    {
        bool Launched;
        Actor Actor;
        public void Init(WorldTile Tile, Actor pActor)
        {
            Actor = pActor;
            sprite_renderer.color = Tile.Type.color;
            Launched = false;
            spawnOnTile(Tile);
        }
        public override void update(float pElapsed)
        {
            base.update(pElapsed);
            if(Launched)
            {
                if(sprite_animation.currentFrameIndex == 9)
                {
                    kill();
                }
                return;
            }
            if(sprite_animation.currentFrameIndex == 7)
            {
                if (Randy.randomChance(0.2f))
                {
                    Launch();
                    return;
                }
                sprite_animation.playType = AnimPlayType.Backward;
            }
            if(sprite_animation.currentFrameIndex == 5)
            {
                sprite_animation.playType = AnimPlayType.Forward;
            }
        }

        private void Launch()
        {
            sprite_animation.playType = AnimPlayType.Forward;
            Launched = true;
            if(Actor != null)
            {
                List<Actor> enemies = GetEnemiesOfActor(Finder.getUnitsFromChunk(tile, 1, 8), Actor);
                if(enemies.Count > 0)
                {
                    ShootProjectileSafe(Actor, enemies.GetRandom(), "EarthShardProjectile", 1, 1, tile.pos);
                }
            }
        }

        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 0.1f);
            sprite_animation.resetAnim(0);
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
        int thickness;
        BaseSimObject ByWho;
        bool Reverse;
        bool Reversed = false;
        public void Init(WorldTile Start, WorldTile End, bool BuildUp, float Speed, int thickness, bool shockwave, BaseSimObject ByWho = null, bool Reverse = false)
        {
            spawnOnTile(Start);
            this.ByWho = ByWho;
            Current = 0;
            this.Reverse = Reverse;
            Reversed = false;
            this.BuildUp = BuildUp;
            this.thickness = thickness;
            this.shockwave = shockwave;
            sprite_renderer.color = Start.Type.color;
            this.Speed = Speed;
            TimeLeft = Speed;
            World.world.pathfinding_param.ocean = true;
            World.world.pathfinding_param.block = true;
            World.world.pathfinding_param.ground = true;
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
                        World.world.applyForceOnTile(tile, 4, 0.4f, true, 50, null, ByWho);
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
        public void TerraformTile(WorldTile pTile, bool Up, TileType TargetType)
        {
            if(pTile.Type == TargetType)
            {
                return;
            }
            MapAction.terraformMain(pTile, Up ? pTile.Type.increase_to : pTile.Type.decrease_to, AssetManager.terraform.get("flash"));
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 0.07f);
            sprite_animation.resetAnim(0);
        }
    }
    public class MoonOrbit : BaseEffect
    {
        Actor Actor;
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
            spawnOnTile(pTile);
            Angle = pAngleOffset;
        }
        public override void create()
        {
            base.create();
            Angle = 0;
            Radius = 0;
            TimeCooldown = Cooldown;
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
                BaseSimObject Target = null;
                if(Actor.kingdom != null)
                {
                    Target = Actor.findEnemyObjectTarget();
                }
                if (Target != null) {
                    ShootProjectileSafe(Actor, Target, "moonFallSlow", 1, 0.25f, tile.pos, "starShowerProjectile");
                }
                else
                {
                    MapAction.damageWorld(tile, 5, AssetManager.terraform.get("MoonFallSlow"), Actor);
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
                World.world.applyForceOnTile(tile, 2, 0.2f, true, 20, null, Actor);
                TimeCooldown = Cooldown;
            }
        }
        public void Orbit()
        {
            float x = (Radius * Mathf.Cos(Angle)) + Actor.current_position.x;
            float y = (Radius * Mathf.Sin(Angle)) + Actor.current_position.y;
            transform.position = new Vector2(x, y);
            tile = World.world.GetTile((int)transform.position.x, (int)transform.position.y);
        }
        public override void spawnOnTile(WorldTile pTile)
        {
            tile = pTile;
            prepare(pTile, 0.1f);
            sprite_animation.resetAnim(0);
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
                byWho.addStatusEffect("invincible");
                MapAction.damageWorld(tile, 4, AssetManager.terraform.get("BlackHole"), byWho);
                byWho.finishStatusEffect("invincible");
                kill();
                return;
            }
            TimeLeft -= pElapsed;
            if(TimeLeft <= 0)
            {
                TimeLeft = TimeCoolDown;
                foreach (Actor a in Finder.getUnitsFromChunk(tile, 1, 5))
                {
                    if(a.kingdom?.id != byWho.kingdom?.id)
                    {
                        PushActor(a, tile.pos, 0.5f, 0.4f);
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
        public BaseSimObject ByWho;
        public WorldTile? TileToGo;
        public bool UsingLaser = false;
        public void Init(float Time, float TimeCooldown, BaseSimObject ByWho, StormAction? StormAction)
        {
            this.Time = Time;
            this.ByWho = ByWho;
            this.StormAction = StormAction;
            this.TimeCooldown = TimeCooldown;
            timeleft = TimeCooldown;
            Speed = Randy.randomFloat(4f, 10f);
        }
        public override void update(float Elapsed)
        {
            base.update(Elapsed);
            World.world.startShake(0.03f, 0.01f, 0.3f, false, true);
            Time -= Elapsed;
            if (Time <= 0)
            {
                return;
            }
            if (sprite_animation.currentFrameIndex > 6)
            {
                sprite_animation.playType = AnimPlayType.Backward;
            }
            if (sprite_animation.currentFrameIndex < 3)
            {
                sprite_animation.playType = AnimPlayType.Forward;
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
            sprite_animation.resetAnim(0);
        }
    }
    public delegate void StormAction(Storm S);
}