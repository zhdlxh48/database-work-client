using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runtime
{
    public class AccountSceneManager : MonoBehaviour
    {
        [SerializeField] private AccountSO accountSO;
        
        [SerializeField] private TextMeshProUGUI errorText;
        
        public async void Login()
        {
            var result = await AccountWebHandler.Login(accountSO.Email, accountSO.Password);

            if (result.success)
            {
                Debug.Log(result.response);
                OnAuthSuccess();
            }
            else
            {
                Debug.LogError(result.response);
                errorText.text = result.response;
            }
        }
        
        public async void Register()
        {
            var result = await AccountWebHandler.Register(accountSO.Username, accountSO.Email, accountSO.Password);

            if (result.success)
            {
                Debug.Log(result.response);
                OnAuthSuccess();
            }
            else
            {
                Debug.LogError(result.response);
                errorText.text = result.response;
            }
        }
        
        private void OnAuthSuccess()
        {
            SceneManager.LoadScene("ItemScene");
        }
    }
}