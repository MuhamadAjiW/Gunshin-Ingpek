using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Core.Game.Data.Currency
{
    [Serializable]
    public class CurrencyData
    {
        [Serializable]
        public class Transaction
        {
            public int amount;
            public String description;
        }
        
        public int balance = 100; 
        public List<Transaction> transactions = new();

        public void AddTransaction(int amount, string description)
        {
            if(balance + amount < 0)
            {
                return;
            }
            
            transactions.Add(new Transaction { amount = amount, description = description });
            balance += amount;
        }
        public void Load(string json)
        {
            CurrencyData data = JsonUtility.FromJson<CurrencyData>(json);
            if (data != null)
            {
                transactions.Clear();
                if (data.transactions != null)
                {
                    foreach (Transaction transaction in data.transactions)
                    {
                        transactions.Add(new Transaction { amount = transaction.amount, description = transaction.description });
                    }
                }
                balance = data.balance;
            }
        }

        public string SaveToJson()
        {
            return JsonUtility.ToJson(this);
        }
    }
}