using System;
using Core.Parser;
using Newtonsoft.Json;

namespace RoyalAxe.CoreLevel 
{
    [Serializable]
    public class WaveDestiny
    {
        [JsonIgnore]
        public bool HasDestiny => !string.IsNullOrEmpty(IdDestiny);
        [ColumnName("Destiny")]
        public string IdDestiny;
        [ColumnName("Level_destiny")]
        public int Level;
    }
}