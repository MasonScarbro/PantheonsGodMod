using static GodsAndPantheons.Traits;
using UnityEngine;
namespace GodsAndPantheons
{
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
