﻿namespace SupermarketsChain.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public decimal? PricePerUnit { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime DateOfSale { get; set; }
    }
}
