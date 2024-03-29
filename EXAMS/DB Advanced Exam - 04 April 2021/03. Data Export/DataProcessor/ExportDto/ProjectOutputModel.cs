﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ExportDto
{
    [XmlType("Project")]
    public class ProjectOutputModel
    {
        [XmlAttribute("TasksCount")]
        public int TasksCount { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement("ProjectName")]
        public string ProjectName { get; set; }

        [XmlElement("HasEndDate")]
        public string HasEndDate { get; set; }

        [XmlArray("Tasks")]
        public TaskOutputModel[] Tasks { get; set; }
    }

    [XmlType("Task")]
    public class TaskOutputModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(40)]
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Label")]
        public string Label { get; set; }
    }
}
