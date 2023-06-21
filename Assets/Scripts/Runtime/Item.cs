using UnityEngine;

namespace Runtime
{
    [System.Serializable]
    public class Item
    {
        [field: SerializeField] public int ItemID { get; private set; }
        [field: SerializeField] public string ItemType { get; private set; }
        [field: SerializeField] public string ItemName { get; private set; }

        public Item(int id, string type, string name)
        {
            ItemID = id;
            ItemType = type;
            ItemName = name;
        }
    }
}