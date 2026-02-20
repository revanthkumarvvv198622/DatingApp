using System;
using System.Data.Common;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context)
    {
        if (await context.Users.AnyAsync()) return;

        var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);

        if (members == null) {
            Console.WriteLine("No members found in the JSON file."); return;
        }

        foreach (var member in members)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var user = new AppUser
            {
                Id = member.Id,
                Email = member.Email,
                DisplayName = member.DisplayName,
                ImageUrl = member.ImageUrl,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                    Id = member.Id,
                    DateOfBirth = member.DateOfBirth,
                    ImageUrl = member.ImageUrl,
                    DisplayName = member.DisplayName,
                    Created = member.Created,
                    LastActive = member.LastActive,
                    Description = member.Description,
                    Gender = member.Gender,
                    City = member.City,
                    Country = member.Country
                }
            };

            user.Member.Photos =
            [
                new Photo
                {
                    Url = member.ImageUrl!,
                    MemberId = member.Id
                }
            ];
            context.Users.Add(user);
        }

        await context.SaveChangesAsync();
    }
}
