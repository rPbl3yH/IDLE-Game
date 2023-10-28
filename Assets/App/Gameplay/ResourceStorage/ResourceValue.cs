using System;

namespace App.Gameplay
{
    [Serializable]
    public class ResourceValue
    {
        public int Amount;
        public int MaxAmount;

        public ResourceValue(int amount, int maxAmount)
        {
            Amount = amount;
            MaxAmount = maxAmount;
        }
    }
}