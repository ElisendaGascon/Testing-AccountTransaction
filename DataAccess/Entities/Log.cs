using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccess.Enums;

namespace DataAccess.Entities
{
    public class Log : IUpdateableEntity
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
        public DateTime LastUpdated { get; set; }

        [NotMapped]
        public LogLevels LogLevel
        {
            get => (LogLevels)Enum.Parse(typeof(LogLevels), Level);
            set => Level = value.ToString();
        }
    }
}
