namespace LD38.Gameplay
{
    public class TerritoryGoods
    {
        public GoodsStorage Food { get; } = new GoodsStorage(GoodsType.Food);

        private GoodsStorage[] GoodsStorageSlots { get; }

        public TerritoryGoods()
        {
            GoodsStorageSlots = new GoodsStorage[] 
            {
                Food
            };
        }

        public GoodsStorage Load(GoodsStorage goodsToLoad)
        {
            GoodsStorageSlots.Load(goodsToLoad);
            return goodsToLoad;
        }
    }
}
