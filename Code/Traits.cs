/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/
using System;
using System.Threading;
using NCMS;
using UnityEngine;
using ReflectionUtility;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ai;

namespace GodsAndPantheons
{
    class Traits
    { 
        public static void init()
        {

            /* The Clans could be the way to control inheritence and assign the lesser god or demi god trait */

            
            ActorTrait chaosGod = new ActorTrait();
            chaosGod.id = "God Of Chaos";
            chaosGod.path_icon = "ui/icons/chaosGod";
            chaosGod.base_stats[S.damage] += 30f;
            chaosGod.base_stats[S.health] += 800;
            chaosGod.base_stats[S.attack_speed] += 5f;
            chaosGod.base_stats[S.attack_speed] += 10f;
            chaosGod.base_stats[S.critical_chance] += 0.05f;
            chaosGod.base_stats[S.range] += 8f;
            chaosGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            chaosGod.action_attack_target = new AttackAction(chaosGodAttack);
            chaosGod.action_death = new WorldAction(ActionLibrary.turnIntoDemon);
            chaosGod.action_death = (WorldAction)Delegate.Combine(chaosGod.action_death, new WorldAction(chaosGodsTrick));
            chaosGod.base_stats[S.scale] = 0.08f;
            AssetManager.traits.add(chaosGod);
            PlayerConfig.unlockTrait(chaosGod.id);
            addTraitToLocalizedLibrary(chaosGod.id, "Tis's The God Of Chaos!");

            
            ActorTrait sunGod = new ActorTrait();
            sunGod.id = "God Of light";
            sunGod.path_icon = "ui/icons/lightGod";
            sunGod.base_stats[S.damage] += 20f;
            sunGod.base_stats[S.health] += 500;
            sunGod.base_stats[S.attack_speed] += 80f;
            sunGod.base_stats[S.critical_chance] += 0.05f;
            sunGod.base_stats[S.speed] += 100f;
            sunGod.base_stats[S.dodge] += 30f;
            sunGod.base_stats[S.accuracy] += 10f;
            sunGod.base_stats[S.range] += 10f;
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addBurningEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(ActionLibrary.addSlowEffectOnTarget);
            sunGod.action_attack_target = new AttackAction(sunGodAttack);
            sunGod.action_death = (WorldAction)Delegate.Combine(sunGod.action_death, new WorldAction(sunGodsDeath));
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sunGodEraStatus));
            AssetManager.traits.add(sunGod);
            PlayerConfig.unlockTrait(sunGod.id);
            sunGod.action_special_effect = (WorldAction)Delegate.Combine(sunGod.action_special_effect, new WorldAction(sunGodAutoTrait));
            addTraitToLocalizedLibrary(sunGod.id, "Tis' The God Of light!");
            

            ActorTrait darkGod = new ActorTrait();
            darkGod.id = "God Of the Night";
            darkGod.path_icon = "ui/icons/godDark";
            darkGod.base_stats[S.damage] += 20f;
            darkGod.base_stats[S.health] += 550;
            darkGod.base_stats[S.attack_speed] += 3f;
            darkGod.base_stats[S.critical_chance] += 0.25f;
            darkGod.base_stats[S.scale] = 0.02f;
            darkGod.base_stats[S.dodge] += 3f;
            darkGod.base_stats[S.range] += 6f;
            darkGod.action_attack_target = new AttackAction(darkGodAttack);
            darkGod.action_death = (WorldAction)Delegate.Combine(darkGod.action_death, new WorldAction(darkGodsDeath));
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(darkGodEraStatus));
            AssetManager.traits.add(darkGod);
            PlayerConfig.unlockTrait(darkGod.id);
            darkGod.action_special_effect = (WorldAction)Delegate.Combine(darkGod.action_special_effect, new WorldAction(darkGodAutoTrait));
            addTraitToLocalizedLibrary(darkGod.id, "Tis' The God Of darkness!");

            ActorTrait knowledgeGod = new ActorTrait();
            knowledgeGod.id = "God Of Knowledge";
            knowledgeGod.path_icon = "ui/icons/knowledgeGod";
            knowledgeGod.base_stats[S.damage] += 20f;
            knowledgeGod.base_stats[S.health] += 600;
            knowledgeGod.base_stats[S.attack_speed] += 1f;
            knowledgeGod.base_stats[S.critical_chance] += 0.25f;
            knowledgeGod.base_stats[S.range] += 15f;
            knowledgeGod.base_stats[S.scale] = 0.04f;
            knowledgeGod.base_stats[S.intelligence] += 35f;
            knowledgeGod.base_stats[S.accuracy] += 10f;
            knowledgeGod.action_attack_target = new AttackAction(knowledgeGodAttack);
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgeGodEraStatus));
            AssetManager.traits.add(knowledgeGod);
            PlayerConfig.unlockTrait(knowledgeGod.id);
            knowledgeGod.action_special_effect = (WorldAction)Delegate.Combine(knowledgeGod.action_special_effect, new WorldAction(knowledgeGodAutoTrait));
            addTraitToLocalizedLibrary(knowledgeGod.id, "Tis' The God Of Knowledges!");

            ActorTrait starsGod = new ActorTrait();
            starsGod.id = "God Of the Stars";
            starsGod.path_icon = "ui/icons/starsGod";
            starsGod.base_stats[S.damage] += 25f;
            starsGod.base_stats[S.health] += 600;
            starsGod.base_stats[S.attack_speed] += 1f;
            starsGod.base_stats[S.critical_chance] += 0.05f;
            starsGod.base_stats[S.scale] = 0.02f;
            starsGod.base_stats[S.range] += 15f;
            starsGod.base_stats[S.intelligence] += 3f;
            starsGod.action_attack_target = new AttackAction(ActionLibrary.addFrozenEffectOnTarget);
            starsGod.action_attack_target = new AttackAction(starsGodAttack);
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(starsGodEraStatus));
            AssetManager.traits.add(starsGod);
            PlayerConfig.unlockTrait(starsGod.id);
            starsGod.action_special_effect = (WorldAction)Delegate.Combine(starsGod.action_special_effect, new WorldAction(starsGodAutoTrait));
            addTraitToLocalizedLibrary(starsGod.id, "Now Cometh the Age of stars, A Thousand Year Voyage under the wisdom of the moon");

            ActorTrait earthGod = new ActorTrait();
            earthGod.id = "God Of the Earth";
            earthGod.path_icon = "ui/icons/earthGod";
            earthGod.base_stats[S.damage] += 40f;
            earthGod.base_stats[S.health] += 1000;
            earthGod.base_stats[S.attack_speed] += 1f;
            earthGod.base_stats[S.armor] += 30f;
            earthGod.base_stats[S.scale] = 0.1f;
            earthGod.base_stats[S.range] += 10f;
            earthGod.base_stats[S.intelligence] += 3f;
            earthGod.action_attack_target = new AttackAction(earthGodAttack);
            AssetManager.traits.add(earthGod);
            PlayerConfig.unlockTrait(earthGod.id);
            earthGod.action_special_effect = (WorldAction)Delegate.Combine(earthGod.action_special_effect, new WorldAction(earthGodAutoTrait));
            addTraitToLocalizedLibrary(earthGod.id, "God of the Natural Enviornment, The titan of creation");

            ActorTrait subGod = new ActorTrait();
            subGod.id = "LesserGod";
            subGod.path_icon = "ui/icons/subGod";
            subGod.base_stats[S.damage] += 5f;
            subGod.base_stats[S.health] += 400;
            subGod.base_stats[S.attack_speed] += 1f;
            subGod.base_stats[S.scale] = 0.02f;
            subGod.base_stats[S.critical_chance] += 0.05f;
            AssetManager.traits.add(subGod);
            PlayerConfig.unlockTrait(subGod.id);
            addTraitToLocalizedLibrary(subGod.id, "These Are the gods that have smaller importance");

            ActorTrait warGod = new ActorTrait();
            warGod.id = "God Of War";
            warGod.path_icon = "ui/icons/warGod";
            warGod.base_stats[S.damage] += 100f;
            warGod.base_stats[S.health] += 700;
            warGod.base_stats[S.attack_speed] += 35f;
            warGod.base_stats[S.armor] += 50f;
            warGod.base_stats[S.knockback_reduction] += 0.5f;
            warGod.base_stats[S.scale] = 0.03f;
            warGod.base_stats[S.range] += 8f;
            warGod.base_stats[S.warfare] += 40f;
            warGod.action_attack_target = new AttackAction(warGodAttack);
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(warGodEraStatus));
            AssetManager.traits.add(warGod);
            PlayerConfig.unlockTrait(warGod.id);
            warGod.action_special_effect = (WorldAction)Delegate.Combine(warGod.action_special_effect, new WorldAction(warGodAutoTrait));
            addTraitToLocalizedLibrary(warGod.id, "God of Conflict, Bravery, Ambition, Many spheres of domain lie with him");



        }

        public static bool chaosGodsTrick(BaseSimObject pTarget, WorldTile pTile = null)
        {
        
            Actor pActor = (Actor)pTarget;
            pActor.removeTrait("chaosGod");
            World.world.eraManager.setEra(S.age_chaos, true);


            return true;

        }

        public static bool sunGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
        

            World.world.eraManager.setEra(S.age_dark, true);


            return true;

        }

        public static bool darkGodsDeath(BaseSimObject pTarget, WorldTile pTile = null)
        {
        
            World.world.eraManager.setEra(S.age_sun, true);


            return true;

        }

        public static bool chaosGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                if (Toolbox.randomChance(0.01f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("fireBallX", newPoint, newPoint2, 0.0f);
                    
                }
                if (Toolbox.randomChance(0.02f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("boneFire", newPoint, newPoint2, 0.0f);
                    

                }
                   
                    
                return true;
            }
            return false;
        }


        public static bool knowledgeGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;

                if (Toolbox.randomChance(0.2f))
                {
                    // randomly spawns a flash of fire or acid on the tile 
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "acid", 5f, -1f);
                    MapBox.instance.dropManager.spawn(pTile, "fire", 5f, -1f); // Drops fire from distance 5 with scale of one at current tile
                }
                if (Toolbox.randomChance(0.01f))
                {
                    ActionLibrary.castCurses(null, pTarget, null); // casts curses
                }
                if (Toolbox.randomChance(0.01f))
                {
                    ActionLibrary.addFrozenEffectOnTarget(null, pTarget, null); // freezezz the target
                }
                if (Toolbox.randomChance(0.05f))
                {
                    ActionLibrary.castShieldOnHimself(null, pSelf, null); // Casts a shield for himself !! hint: pSelf !!
                }
                if (Toolbox.randomChance(0.04f))
                {
                    ActionLibrary.teleportRandom(null, pTarget, null); // teleports the target
                }
                
                if (Toolbox.randomChance(0.02f))
                {
                    ActionLibrary.castLightning(null, pTarget, null); // Casts Lightning on the target
                }
                if (Toolbox.randomChance(0.005f))
                {
                    EffectsLibrary.spawn("fx_meteorite", pTarget.currentTile, "meteorite_disaster", null, 0f, -1f, -1f);    //spawn 1 meteorite
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(0.01f))
                {
                    EffectsLibrary.spawn("fx_fireball_explosion", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    MapAction.damageWorld(pSelf.currentTile, 2, AssetManager.terraform.get("grenade"), null);
                    pSelf.a.addStatusEffect("invincible", 1f);
                }
                if (Toolbox.randomChance(0.09f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("PagesOfKnowledge", newPoint, newPoint2, 0.0f);
                }
                if (Toolbox.randomChance(0.04f))
                {
                    ActionLibrary.teleportRandom(null, pTarget, null); // teleports the target
                }

                return true;
            }
            return false;
        }


        public static bool darkGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;

                
                if (Toolbox.randomChance(0.0001f))
                {
                    EffectsLibrary.spawn("fx_antimatter_effect", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(0.02f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("BlackHoleProjectile1", newPoint, newPoint2, 0.0f);
                    

                }
                if (Toolbox.randomChance(0.04f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("DarkDaggersProjectiles", newPoint, newPoint2, 0.0f);
                    

                }
                if (Toolbox.randomChance(0.01f))
                {
                    EffectsLibrary.spawnAtTile("fx_smokeFlash_dej", pTile, 0.1f);
                    MapAction.damageWorld(pTarget.currentTile, 5, AssetManager.terraform.get("lightning_power"), null);
                    MapAction.damageWorld(pTarget.currentTile, 8, AssetManager.terraform.get("smokeFlash"), null);
                    World.world.applyForce(pTarget.currentTile, 2, 0.4f, false, true, 20, null, pTarget, null);

                }
                
                    
                return true;
            }
            return false;
        }

        public static bool starsGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;

                
                if (Toolbox.randomChance(0.0005f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x + 35f, pSelf.currentPosition.y + 95f, (float)pos.x + 1f, (float)pos.y + 1f, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("moonFall", newPoint, newPoint2, 0.0f);
                    pSelf.a.addStatusEffect("invincible", 2f);
                }
                if (Toolbox.randomChance(1f))
                {
                    EffectsLibrary.spawnAtTile("fx_cometAzureDown_dej", pTarget.a.currentTile, 0.1f);
                    MapAction.applyTileDamage(pTarget.currentTile, 8, AssetManager.terraform.get("cometAzureDownDamage"));
                    MapAction.damageWorld(pTarget.currentTile.neighbours[2], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                    MapAction.damageWorld(pTarget.currentTile.neighbours[1], 8, AssetManager.terraform.get("cometAzureDownDamage"), null);
                    World.world.applyForce(pTarget.currentTile.neighbours[0], 4, 0.4f, false, true, 200, null, pTarget, null);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(0.005f))
                {
                    EffectsLibrary.spawnAtTile("fx_cometShower_dej", pTarget.a.currentTile, 0.09f);
                    MapAction.applyTileDamage(pTarget.currentTile, 2f, AssetManager.terraform.get("cometRain"));
                    pSelf.a.addStatusEffect("invincible", 5f);
                    // The Rain Damage effect 
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[0].neighbours[1].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[1].neighbours[2].neighbours[3].neighbours[0], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[1].neighbours[1].neighbours[1].neighbours[1], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[2].neighbours[2].neighbours[2], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 1f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[1], 2f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[2].neighbours[3], 2f, AssetManager.terraform.get("cometRain"));
                    MapAction.applyTileDamage(pTarget.currentTile.neighbours[0].neighbours[0].neighbours[0], 2f, AssetManager.terraform.get("cometRain"));
                    World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[2].neighbours[2], 2, 0.4f, false, true, 20, null, pTarget, null);
                    World.world.applyForce(pTarget.currentTile.neighbours[3].neighbours[3].neighbours[3].neighbours[3], 2, 0.4f, false, true, 20, null, pTarget, null);

                

                }
                
                    
                return true;
            }
            return false;
        }

        

        public static bool sunGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                PowerLibrary pb = new PowerLibrary();
                
                if (Toolbox.randomChance(0.005f))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                if (Toolbox.randomChance(0.008f))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    pTarget.a.addStatusEffect("burning", 5f);
                    
                }
                if (Toolbox.randomChance(0.02f))
                {
                    pb.divineLightFX(pTarget.a.currentTile, null);
                    EffectsLibrary.spawn("fx_thunder_flash", pSelf.a.currentTile, null, null, 0f, -1f, -1f);
                    pSelf.a.addStatusEffect("caffeinated", 10f);
                    pTarget.a.addStatusEffect("slowness", 10f);

                    
                }
                if (Toolbox.randomChance(0.005f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("lightBallzProjectiles", newPoint, newPoint2, 0.0f);
                }
                //EffectsLibrary.spawnAtTile("fx_multiFlash_dej", pTile, 0.01f);
                
                if (Toolbox.randomChance(0.0001f))
                {
                    //TO BE USED AS IMACT ACTION FOR LIGHT PROJECILES LATER
                    int count = 0;
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos);
                    Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                   
                    EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[2].neighbours[2].neighbours[1].neighbours[1].neighbours[2], null, null, 0f, -1f, -1f);
                    count++;
                    if (count == 1){
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[1].neighbours[1].neighbours[0].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                        count++;
                    }
                    if (count == 2){
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, 1f, -0.2f);
                        count++;
                    }
                    if (count == 3){
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[2].neighbours[1].neighbours[2].neighbours[3].neighbours[1].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                        EffectsLibrary.spawn("fx_napalm_flash", pTarget.a.currentTile.neighbours[1].neighbours[3].neighbours[0].neighbours[1].neighbours[0].neighbours[0].neighbours[0].neighbours[1].neighbours[0].neighbours[0], null, null, 0f, -0.5f, -0.2f);
                        
                    }
                    
                    
                    

                    pSelf.a.addStatusEffect("invincible", 5f);
                }
                

                return true;
            }
            return false;
        }

        public static bool warGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                PowerLibrary pb = new PowerLibrary();
                
                if (Toolbox.randomChance(0.01f))
                {
                    EffectsLibrary.spawnExplosionWave(pSelf.currentTile.posV3, 1f, 1f);
                    pSelf.a.addStatusEffect("WarGodsCry");
                    World.world.startShake(0.3f, 0.01f, 2f, true, true);
                    pSelf.a.addStatusEffect("invincible", 1f);
                    MapAction.damageWorld(pSelf.currentTile, 2, AssetManager.terraform.get("crab_step"), null);
                    pSelf.a.addStatusEffect("invincible", 1f);
                    World.world.applyForce(pSelf.currentTile, 4, 0.4f, false, true, 20, null, pTarget, null);

                }
                if (Toolbox.randomChance(0.03f))
                {
                    Vector2Int pos = pTile.pos; // Position of the Ptile as a Vector 2
		            float pDist = Vector2.Distance(pTarget.currentPosition, pos); // the distance between the target and the pTile
                    Vector3 newPoint = Toolbox.getNewPoint(pSelf.currentPosition.x, pSelf.currentPosition.y, (float)pos.x, (float)pos.y, pDist, true); // the Point of the projectile launcher 
		            Vector3 newPoint2 = Toolbox.getNewPoint(pTarget.currentPosition.x, pTarget.currentPosition.y, (float)pos.x, (float)pos.y, pTarget.a.stats[S.size], true);
                    EffectsLibrary.spawnProjectile("WarAxeProjectile1", newPoint, newPoint2, 0.0f);
                    
                }
                


                return true;
            }
            return false;
        }
        
        public static bool earthGodAttack(BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget != null)
            {
                Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
                PowerLibrary pb = new PowerLibrary();
                
                if (Toolbox.randomChance(0.05f))
                {
                    pb.spawnEarthquake(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(0.1f))
                {
                    pb.spawnCloudRain(pTarget.a.currentTile, null);
                    pb.spawnCloudSnow(pTarget.a.currentTile, null);
                }
                if (Toolbox.randomChance(0.02f))
                {
                    pb.spawnBoulder(pTarget.a.currentTile, null);
                }

                return true;
            }
            return false;
        }


         
        public static bool sunGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of light"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("shiny");
                    pTarget.a.addTrait("agile");
                    pTarget.a.addTrait("fire_proof");
                    pTarget.a.addTrait("fire_blood");
                    pTarget.a.addTrait("weightless");
                    pTarget.a.addTrait("fast");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("light_lamp");
                    pTarget.a.addTrait("immortal");

                }
                

            }
            return true;
        }

        public static bool knowledgeGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of Knowledge"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("genius");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("fire_proof");
                    pTarget.a.addTrait("fire_blood");
                    pTarget.a.addTrait("tough");
                    pTarget.a.addTrait("strong_minded");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("immortal");
                    pTarget.a.addTrait("wise");


                }
                

            }
            return true;
        }

        public static bool darkGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of the Night"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("bloodlust");
                    pTarget.a.addTrait("agile");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("weightless");
                    pTarget.a.addTrait("cold_aura");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("immortal");
                    pTarget.a.addTrait("nightchild");
                    pTarget.a.addTrait("moonchild");

                }
                

            }
            return true;
        }

        public static bool starsGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {
            Actor a = Reflection.GetField(pTarget.GetType(), pTarget, "a") as Actor;
            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of the Stars"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("agile");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("weightless");
                    pTarget.a.addTrait("shiny");
                    pTarget.a.addTrait("energized");
                    pTarget.a.addTrait("immortal");
                    pTarget.a.addTrait("nightchild");
                    pTarget.a.addTrait("moonchild");

                }
                

            }
            return true;
        }

        public static bool earthGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of the Earth"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("giant");
                    pTarget.a.addTrait("strong");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("fat");
                    pTarget.a.addTrait("tough");
                    pTarget.a.addTrait("immortal");

                }
                

            }
            return true;
        }

        public static bool warGodAutoTrait(BaseSimObject pTarget, WorldTile pTile)
        {

            if (pTarget.a != null)
            {
                if (pTarget.a.hasTrait("God Of War"))
                {
                    pTarget.a.addTrait("blessed");
                    pTarget.a.addTrait("strong");
                    pTarget.a.addTrait("frost_proof");
                    pTarget.a.addTrait("ambitious");
                    pTarget.a.addTrait("pyromaniac");
                    pTarget.a.addTrait("veteran");
                    pTarget.a.addTrait("tough");
                    pTarget.a.addTrait("immortal");

                }
                

            }
            return true;
        }


        private static bool sunGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_sun")       //only in age of sun
                {
                    pSelf.a.addStatusEffect("Lights_Prevail"); // add the status I created
                
                }
                else
                {
                    if (pSelf.a.hasStatus("Lights_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool warGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_ash")       //only in age of sun
                {
                    pSelf.a.addStatusEffect("War_Prevail"); // add the status I created
                
                }
                else
                {
                    if (pSelf.a.hasStatus("War_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool starsGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_moon")       //only in age of moon
                {
                    pSelf.a.addStatusEffect("Stars_Prevail"); // add the status I created
                
                }
                else
                {
                    if (pSelf.a.hasStatus("Stars_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool darkGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_dark")       //only in age of dark
                {
                    pSelf.a.addStatusEffect("Nights_Prevail"); // add the status I created
                
                }
                else
                {
                    if (pSelf.a.hasStatus("Nights_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

        private static bool knowledgeGodEraStatus(BaseSimObject pSelf, WorldTile pTile)
        {
            if (pSelf.a != null)
            {
                if (World.world_era.id == "age_wonders")       //only in age of wonder
                {
                    pSelf.a.addStatusEffect("Knowledge_Prevail"); // add the status I created
                
                }
                else
                {
                    if (pSelf.a.hasStatus("Knowledge_Prevail"))          //no other age can have this trait
                    {
                        pSelf.a.finishAllStatusEffects(); // remove the status
                    }
                }


            }
            return true;
        }

         public static void addTraitToLocalizedLibrary(string id, string description)        
         {

            string language = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "language") as string;
      		Dictionary<string, string> localizedText = Reflection.GetField(LocalizedTextManager.instance.GetType(), LocalizedTextManager.instance, "localizedText") as Dictionary<string, string>;
      		localizedText.Add("trait_" + id, id);
      		localizedText.Add("trait_" + id + "_info", description);

        }

        


    }

}
