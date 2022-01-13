using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ProjectTaskInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        public TaskInputModel[] Tasks { get; set; }
    }

    [XmlType("Task")]
    public class TaskInputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [Required]
        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [EnumDataType(typeof(ExecutionType))]
        [XmlElement("ExecutionType")]
        public int ExecutionType { get; set; }

        [EnumDataType(typeof(LabelType))]
        [XmlElement("LabelType")]
        public int LabelType { get; set; }
    }
}
