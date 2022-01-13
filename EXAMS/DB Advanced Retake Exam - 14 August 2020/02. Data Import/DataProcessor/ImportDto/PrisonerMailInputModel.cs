using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SoftJail.Data.Models;

namespace SoftJail.DataProcessor.ImportDto
{
    public class PrisonerMailInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^The [A-Z]{1}[a-z]+$")]
        public string Nickname { get; set; }

        [Range(18, 65)]
        public int Age { get; set; }

        [Required]
        public string IncarcerationDate { get; set; }

        public string ReleaseDate { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal? Bail { get; set; }

        [ForeignKey(nameof(Cell))]
        public int? CellId { get; set; }

        public MailInputModel[] Mails { get; set; }
    }

    public class MailInputModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Sender { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z\s0-9]+ str.$")]
        public string Address { get; set; }
    }
}

