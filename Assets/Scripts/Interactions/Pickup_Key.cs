public class Pickup_Key : Pickup
{
    public int DoorCode = 0;
    protected override void HandlePickup(ref PlayerInventory inventory)
    {
        base.HandlePickup(ref inventory);
        inventory.AddKey(this);
    }
}
