﻿using System.ComponentModel.DataAnnotations;

namespace BlazorBootcamp_DataAccess
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
