using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Runtime
{
    public class ItemSceneManager : MonoBehaviour
    {
        [SerializeField] private List<Item> itemList;
        [SerializeField] private List<ItemUI> itemUIList;
        
        [SerializeField] private RectTransform itemListParent;

        [SerializeField] private ItemUI itemUIPrefab;
        
        [SerializeField] private List<Player> playerList;
        [SerializeField] private List<PlayerUI> playerUIList;
        
        [SerializeField] private RectTransform playerListParent;

        [SerializeField] private PlayerUI playerUIPrefab;
        
        [SerializeField] private TMP_InputField itemIdInputField;
        [SerializeField] private TMP_InputField itemTypeInputField;
        [SerializeField] private TMP_InputField itemNameInputField;

        [SerializeField] private int itemGiveTargetPlayerID;

        [SerializeField] private GameObject itemGivePanel;
        
        [SerializeField] private TMP_InputField giveItemIDInputField;

        [SerializeField] private TextMeshProUGUI errorText;

        private void Awake()
        {
            itemList = new List<Item>();
            itemUIList = new List<ItemUI>();
        }

        private async void Start()
        {
            itemGivePanel.SetActive(false);
            
            var itemsResult = await ItemWebHandler.GetItems();

            if (itemsResult.success)
            {
                itemList = itemsResult.items;
                foreach (var item in itemList)
                {
                    CreateItemUI(item);
                }
            }
            
            var playersResult = await PlayerWebHandler.GetPlayerData();

            if (playersResult.success)
            {
                playerList = playersResult.players;
                foreach (var player in playerList)
                {
                    CreatePlayerUI(player);
                }
            }
        }

        public async void CreateItem()
        {
            var itemID = Convert.ToInt32(itemIdInputField.text);
            var itemType = itemTypeInputField.text;
            var itemName = itemNameInputField.text;
            
            var item = new Item(itemID, itemType, itemName);
            
            var result = await ItemWebHandler.CreateItem(item);
            if (result.success)
            {
                Debug.Log(result.response);
                
                CreateItemUI(item);
                OnSuccessCreateItem();
            }
            else
            {
                Debug.LogError(result.response);
                errorText.text = result.response;
            }
        }

        public async void GiveItem()
        {
            var itemID = Convert.ToInt32(giveItemIDInputField.text);
            var playerID = itemGiveTargetPlayerID;
            
            var result = await PlayerWebHandler.GiveItem(playerID, itemID);
            if (result.success)
            {
                Debug.Log(result.response);
                
                OnSuccessGiveItem();
            }
            else
            {
                Debug.LogError(result.response);
                errorText.text = result.response;
            }
        }

        private void OnSuccessCreateItem()
        {
            itemIdInputField.text = "";
            itemTypeInputField.text = "";
            itemNameInputField.text = "";
        }
        
        private void OnOpenItemGivePanel(int playerID)
        {
            itemGivePanel.SetActive(true);
            itemGiveTargetPlayerID = playerID;
        }

        private void OnSuccessGiveItem()
        {
            itemGivePanel.SetActive(false);
        }
        
        private void CreateItemUI(Item item)
        {
            var itemUI = Instantiate(itemUIPrefab, itemListParent);
            itemUI.Initialize(item);
            itemUI.gameObject.SetActive(true);
                    
            itemUIList.Add(itemUI);
        }
        
        private void CreatePlayerUI(Player player)
        {
            var playerUI = Instantiate(playerUIPrefab, playerListParent);
            playerUI.Initialize(player);
            playerUI.OnClick = OnOpenItemGivePanel;
            playerUI.gameObject.SetActive(true);
                    
            playerUIList.Add(playerUI);
        }
    }
}