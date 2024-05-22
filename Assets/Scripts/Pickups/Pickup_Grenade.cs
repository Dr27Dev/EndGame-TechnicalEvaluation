public class Pickup_Grenade : Pickup
{
    protected override void HandlePickup(ref PlayerInventory inventory)
    {
        base.HandlePickup(ref inventory);
        inventory.EnableGrenade();
    }
}
