﻿using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }
}