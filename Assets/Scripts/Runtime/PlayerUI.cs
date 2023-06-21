using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Runtime
{
    public class PlayerUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI itemIdText;
        [SerializeField] private TextMeshProUGUI itemNameText;
        
        private Player _player;

        public UnityAction<int> OnClick;
        
        public void Initialize(Player player)
        {
            _player = player;
            
            itemIdText.text = player.PlayerID.ToString();
            itemNameText.text = player.PlayerName;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(_player.PlayerID);
        }
    }
}