public class Pickup_HealingCross : Pickup
{
    protected override void HandlePickup(ref PlayerInventory inventory)
    {
        base.HandlePickup(ref inventory);
        inventory.GetComponent<PlayerStats>().Heal();
    }
}
