// FILE : Log.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// model that handles logs to the database

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MistAPI.Models.Entities
{
    public class Log
    {
        [Key]
        [Column("LogsID")]
        public int LogId { get; set; }

        [Column("UserID")]
        public int UserID { get; set; }

        [Required]
        [Column("LogDate")]
        public DateTime LogDate { get; set; }

        [MaxLength(50)]
        [Column("LogType")]
        public string? LogType { get; set; }

        [MaxLength(200)]
        [Column("LogInformation")]
        public string? LogInformation { get; set; }

        public Log() { }

        public Log(int userID, DateTime logDate, string logType, string logInformation)
        {
            UserID = userID;
            LogDate = logDate;
            LogType = logType;
            LogInformation = logInformation;
        }

        public Log(DateTime logDate, string logType, string logInformation)
        {
            LogDate = logDate;
            LogType = logType;
            LogInformation = logInformation;
        }
    }
}