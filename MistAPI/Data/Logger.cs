// FILE : logger.cs
// PROJECT : SENG2020 - MistApp
// PROGRAMMER : Zemmat Hagos, Will Jessel, Eric Moutoux
// FIRST VERSION : 2026-3-10
// DESCRIPTION :
// the class that handles logging to a server

using MistAPI.Models.Entities;

namespace MistAPI.Data
{
    public class logger
    {
        private readonly AppDbContext AppDbContext;
        public logger(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }
        /// <summary>
        /// logs to the database
        /// </summary>
        /// <param name="log">the item to log</param>
        public async Task LogToDb(Log log)
        {
            try
            {
                //sends the user data to the server
                AppDbContext.logs.Add(log);
                await AppDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}