﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UserServices.Models;

[Index("UserId", Name = "IX_AspNetUserClaims_UserId")]
public partial class AspNetUserClaim
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    public string ClaimType { get; set; }

    public string ClaimValue { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserClaims")]
    public virtual AspNetUser User { get; set; }
}