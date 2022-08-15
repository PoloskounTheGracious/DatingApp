using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context) 
        {
            //If context.Users contains any elements, return, otherwise, proceed.
            if (await context.Users.AnyAsync()) return; 

            //Grab seed data from UserSeedData.json and store in var userData, 
            //then turn the seed data in var userData into List<AppUser> and store as var users.
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData); 

            foreach (var user in users) 
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                //Don't forget that the "Pa$$w0rd" ordeal is obviously for use on seed data only...
                //We do this because we don't want random passwords for seed users as it would
                //be difficult to login with such passwords, so we want a consistent memorable test password
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                //We don't use "await" here because we are just adding each "user" to be tracked by EF!
                context.Users.Add(user);
            }

            //We use "await" here because we are making a database operation!
            await context.SaveChangesAsync(); 
        }
    }
}