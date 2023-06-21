using System;
using TMPro;
using UnityEngine;

namespace Runtime
{
    public class AccountUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        
        [SerializeField] private AccountSO accountSO;

        private void OnEnable()
        {
            usernameInputField.onValueChanged.AddListener(accountSO.SetUsername);
            emailInputField.onValueChanged.AddListener(accountSO.SetEmail);
            passwordInputField.onValueChanged.AddListener(accountSO.SetPassword);

            usernameInputField.text = accountSO.Username;
            emailInputField.text = accountSO.Email;
            passwordInputField.text = accountSO.Password;
        }
        
        private void OnDisable()
        {
            usernameInputField.onValueChanged.RemoveListener(accountSO.SetUsername);
            emailInputField.onValueChanged.RemoveListener(accountSO.SetEmail);
            passwordInputField.onValueChanged.RemoveListener(accountSO.SetPassword);
        }
    }
}