﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TicketSystem.Models;

public partial class TicketComment
{
    [Key]
    public int CommentID { get; set; }

    public int TicketID { get; set; }

    [Required]
    [StringLength(450)]
    public string UserID { get; set; }

    [Required]
    [StringLength(1000)]
    public string CommentText { get; set; }

    public bool IsInternal { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get; set; }

    [ForeignKey("TicketID")]
    [InverseProperty("TicketComments")]
    public virtual Ticket Ticket { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("TicketComments")]
    public virtual AspNetUser User { get; set; }
}