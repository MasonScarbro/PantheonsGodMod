using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static GodsAndPantheons.Traits;
namespace GodsAndPantheons
{
    public class Storm : EffectModifier
    {
        public float Time;
        public StormAction? StormAction;
        public float TimeCooldown;
        public float timeleft;
        public WorldTile pTile;
        public float Speed;

        //fire god stuff
        public WorldTile? TileToGo;
        public bool UsingLaser = false;
        public void Init(float Time, float TimeCooldown, StormAction? StormAction)
        {
            this.Time = Time;
            this.StormAction = StormAction;
            this.TimeCooldown = TimeCooldown;
            timeleft = TimeCooldown;
            pTile = GetComponent<AntimatterBombEffect>().tile;
            StormAnimation = GetComponent<SpriteAnimation>();
            Speed = Toolbox.randomFloat(4f, 10f);
            GetComponent<AntimatterBombEffect>().used = true;
        }
        public SpriteAnimation StormAnimation;
        public override void update(float Elapsed)
        {
            Time -= Elapsed;
            if(Time <= 0)
            {
                return;
            }
            if(StormAnimation.currentFrameIndex > 5)
            {
                StormAnimation.setFrameIndex(0);
            }
            timeleft -= Elapsed;
            //fire god stuff
            if (UsingLaser)
            {
                pb.drawHeatray(pTile, null);
                pb.heatrayFX(pTile, "heatray");
                pb.flashPixel(pTile, "");
            }
            if (TileToGo != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, TileToGo.posV, Speed * Elapsed);
                pTile = World.world.GetTile((int)transform.position.x, (int)transform.position.y);
            }

            if (timeleft <= 0)
            {
                timeleft = TimeCooldown;
                StormAction?.Invoke(this);
            }
        }
    }
    public delegate void StormAction(Storm S);
}
