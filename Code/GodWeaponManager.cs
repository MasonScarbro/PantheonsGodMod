/* 
AUTHOR: MASON SCARBRO
VERSION: 1.0.0
*/


public static class GodWeaponManager
{
    public static bool godGiveWeapon(NanoObject pTarget, BaseAugmentationAsset asset)
    {
        Actor pActor = (Actor)pTarget;
        if (pActor.asset.use_items)
        {
            pActor.equipment.weapon?.throwOutItem();
            if (asset.id.Equals("God Of War"))
            {
                Item axeOfFuryD = World.world.items.generateItem(AssetManager.items.get("AxeOfFury"), pActor.kingdom, pActor.name, 1, pActor);
                axeOfFuryD.addMod("eternal");
                pActor.equipment.setItem(axeOfFuryD, pActor);
            }

            if (asset.id.Equals("God Of the Night"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("DarkDagger"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }

            if (asset.id.Equals("God Of light"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("SpearOfLight"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("divine_rune");
                pActor.equipment.setItem(Item, pActor);

            }
            if (asset.id.Equals("God Of Knowledge"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("StaffOfKnowledge"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);

            }
            if (asset.id.Equals("God Of Chaos"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("MaceOfDestruction"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }
            if (asset.id.Equals("God Of the Stars"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("CometScepter"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }

            if (asset.id.Equals("God Of The Lich"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("LichGodsGreatSword"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }


            if (asset.id.Equals("God Of the Earth"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("HammerOfCreation"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);

            }

            if (asset.id.Equals("God Of Love"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("StaffOfLove"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);

            }
            if (asset.id.Equals("God Of Fire"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("HellStaff"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }
            if (asset.id.Equals("God Hunter"))
            {
                Item Item = World.world.items.generateItem(AssetManager.items.get("GodHuntersScythe"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }
        }
        return true;
    }

}

