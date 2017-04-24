using System;
using static Microsoft.Xna.Framework.MathHelper;
using System.Collections.Generic;
using System.Linq;

namespace LD38.Gameplay
{
    public class GoodsStorage
    {
        private const int MinQuantity = 0;
        private const int DefaultMaxQuantity = 100;

        public GoodsType GoodsType { get; private set; }

        private int _maxQuantity = DefaultMaxQuantity;

        public int MaxQuantity
        {
            get => _maxQuantity;
            set
            {
                _maxQuantity = value;
                Quantity = Quantity;
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => _quantity = Clamp(value, MinQuantity, MaxQuantity);
        }
        public bool HasQuantity => Quantity > 0;

        public int AvailableSpace => DefaultMaxQuantity - Quantity;
        public bool HasAvailableSpace => AvailableSpace > 0;

        public GoodsStorage(GoodsType goodsType)
        {
            GoodsType = goodsType;
            Quantity = MinQuantity;
        }

        internal GoodsStorage SetGoodsType(GoodsType type)
        {
            GoodsType = type;
            return this;
        }

        internal GoodsStorage Transfer(GoodsStorage goodsToLoad)
        {
            if (GoodsType != goodsToLoad.GoodsType)
                throw new Exception("trying to transfer goods to slot of different type, use SetGoodsType first");

            int amountThatMayTransfer = Clamp(goodsToLoad.Quantity, 0, this.AvailableSpace);
            goodsToLoad.Quantity -= amountThatMayTransfer;
            Quantity += amountThatMayTransfer;

            return this;
        }
    }

    public static class GoodsStorageExt
    {
        public static GoodsStorage Load(this IEnumerable<GoodsStorage> self, GoodsStorage goodsToLoad)
        {
            // look to see if there is already a slot with this good
            Maybe<GoodsStorage> slot = self.FirstOrDefault(s => s.GoodsType == goodsToLoad.GoodsType);

            // if we have an existing slot try transfering
            if (slot.HasValue && slot.Value.HasAvailableSpace)
                slot.Value.Transfer(goodsToLoad);

            // move to new slot if needed / can 
            while (goodsToLoad.HasQuantity)
            {
                Maybe<GoodsStorage> openSlot = self.FirstOrDefault(sg => sg.GoodsType == GoodsType.Void);
                if (!openSlot.HasValue)
                    break;

                openSlot.Value.SetGoodsType(goodsToLoad.GoodsType);
                openSlot.Value.Transfer(goodsToLoad);
            }

            return goodsToLoad;
        }
    }
}
