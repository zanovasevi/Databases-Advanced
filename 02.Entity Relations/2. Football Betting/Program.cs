using System;
using Microsoft.EntityFrameworkCore;
using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var context = new FootballBettingContext();

            context.Database.Migrate();
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();
        }
    }
}
