using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class GameImportModel
    {
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Required]
        public string ReleaseDate { get; set; }

        [Required]
        public string Developer { get; set; }

        [Required]
        public string Genre { get; set; }

        public IEnumerable<string> Tags { get; set; }
    }
}
//      "Name": "MONSTER HUNTER: WORLD",
//		"Price": 59.99,
//		"ReleaseDate": "2018-08-09",
//		"Developer": "CAPCOM Co., Ltd.",
//		"Genre": "Action",
//		"Tags": [
//			"Single-player",
//			"Multi-player",
//			"Co-op",
//			"Steam Achievements",
//			"Partial Controller Support",
//			"Stats"
//		] 