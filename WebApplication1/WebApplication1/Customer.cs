﻿namespace WebApplication1
{
    using System.ComponentModel.DataAnnotations;

    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(50)]
        public string Name { get; set; }

    }
}