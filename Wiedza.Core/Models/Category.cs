﻿namespace Wiedza.Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Category? ParentCategory { get; set; }
    public Guid? ParentCategoryId { get; set; }
}