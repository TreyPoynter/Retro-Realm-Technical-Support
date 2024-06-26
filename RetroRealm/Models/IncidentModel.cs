﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using RetroRealm.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace RetroRealm.Models
{
    public class IncidentModel
    {
        public int? IncidentModelId { get; set; } = 0;
        [Required (ErrorMessage = "A title is required!")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "A description is required!")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Open date is required!")]
        [ValidateDate]
        public DateTime? DateOpened { get; set; }
        public DateTime? DateClosed { get; set; }

        [Required (ErrorMessage = "A customer is required!")]
        public int? CustomerModelId { get; set; }
        [Required(ErrorMessage = "A game is required!")]
        public int? GameModelId { get; set; }
        [Required(ErrorMessage = "A technician is required!")]
        public int? TechnicianModelId { get; set; }

        [ValidateNever]
        public CustomerModel Customer { get; set; }
        [ValidateNever]
        public TechnicianModel Technician { get; set; }
        [ValidateNever]
        public GameModel Game { get; set; }

    }
}
