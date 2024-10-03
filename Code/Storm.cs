using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static GodsAndPantheons.Traits;
namespace GodsAndPantheons
{
    public class Storm : MonoBehaviour
    {
        public float Time;
        public StormAction? StormAction;
        public float TimeCooldown;
        public float timeleft;
        public WorldTile pTile;

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
        }
        public SpriteAnimation StormAnimation;
        public void UpdateStorm(float Elapsed)
        {
            Time -= Elapsed;
            if(Time <= 0)
            {
                StormAnimation.returnToPool = true;
                return;
            }
            if(StormAnimation.currentFrameIndex > 7)
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
                transform.position = Vector3.Lerp(transform.position, TileToGo.posV, 2.5f * Elapsed);
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
