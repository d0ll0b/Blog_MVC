﻿using System;
namespace Blog_MVC.Models
{
	public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}

