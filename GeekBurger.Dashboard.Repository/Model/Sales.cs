﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GeekBurger.Dashboard.Repository.Model
{
    public class Sales
    {
        [Key]
        public string Id { get; set; }
        public string StoreId { get; set; }
        public string StoreName { get; set; } = "Pasadena";
        public string OrderId { get; set; }

        [JsonProperty(PropertyName = "Total")]
        public decimal Value { get; set; }
        public State State { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// Status do pedido
    /// </summary>
    public enum State
    {
        Open,
        Paid,
        Canceled,          
        Finished
    }
}