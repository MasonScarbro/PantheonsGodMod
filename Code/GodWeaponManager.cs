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
            Item Item = null;
            if (asset.id.Equals("God Of War"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("AxeOfFury"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }

            if (asset.id.Equals("God Of the Night"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("DarkDagger"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }

            if (asset.id.Equals("God Of light"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("SpearOfLight"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("divine_rune");
                pActor.equipment.setItem(Item, pActor);

            }
            if (asset.id.Equals("God Of Knowledge"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("StaffOfKnowledge"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);

            }
            if (asset.id.Equals("God Of Chaos"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("MaceOfDestruction"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }
            if (asset.id.Equals("God Of the Stars"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("CometScepter"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }

            if (asset.id.Equals("God Of The Lich"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("LichGodsGreatSword"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }


            if (asset.id.Equals("God Of the Earth"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("HammerOfCreation"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);

            }

            if (asset.id.Equals("God Of Love"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("StaffOfLove"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);

            }
            if (asset.id.Equals("God Of Fire"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("HellStaff"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }
            if (asset.id.Equals("God Hunter"))
            {
                Item = World.world.items.generateItem(AssetManager.items.get("GodHuntersScythe"), pActor.kingdom, pActor.name, 1, pActor);
                Item.addMod("eternal");
                pActor.equipment.setItem(Item, pActor);
            }
            Item.data.name = Item.asset.translation_key;
        }
        return true;
    }

}

