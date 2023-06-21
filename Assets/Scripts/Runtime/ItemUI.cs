using TMPro;
using UnityEngine;

namespace Runtime
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemIdText;
        [SerializeField] private TextMeshProUGUI itemTypeText;
        [SerializeField] private TextMeshProUGUI itemNameText;
        
        public void Initialize(Item item)
        {
            itemIdText.text = item.ItemID.ToString();
            itemTypeText.text = item.ItemType;
            itemNameText.text = item.ItemName;
        }
    }
}