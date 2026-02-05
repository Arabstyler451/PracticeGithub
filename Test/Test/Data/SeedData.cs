using Test.Models;
using Microsoft.EntityFrameworkCore;
using Test.Data.Migrations;
using Microsoft.AspNetCore.Identity;

namespace Test.Data
{
    public class SeedData
    {
        public static async Task SeedStaffAsync(ApplicationDbContext context)
        {
            if (!await context.Staff.AnyAsync())
            {
                var staff = new List<Models.Staff>
                {
                    new Models.Staff
                    {
                        FirstName = "John",
                        StaffEmail = "johncitypoint@gmail.com",
                        StaffPhoneNumber = "0712358721",
                        Role = "Manager"
                    },
                    new Models.Staff
                    {
                        FirstName = "Jane",
                        StaffEmail = "janecitypoint@gmail.com",
                        StaffPhoneNumber = "0711190901",
                        Role = "Co-Executive"
                    },
                    new Models.Staff
                    {
                        FirstName = "Greg",
                        StaffEmail = "gregcitypoint@gmail.com",
                        StaffPhoneNumber = "0743834333",
                        Role = "Customer Support"
                    },
                    new Models.Staff
                    {
                        FirstName = "Emily",
                        StaffEmail = "emilycitypoint@gmail.com",
                        StaffPhoneNumber = "0723188123",
                        Role = "Customer Support"
                    },
                    new Models.Staff
                    {
                        FirstName = "Adam",
                        StaffEmail = "adamcitypoint@gmail.com",
                        StaffPhoneNumber = "0754647484",
                        Role = "Receptionist"
                    },
                };
                await context.Staff.AddRangeAsync(staff);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedRoomsAsync(ApplicationDbContext context)
        {
            if (!await context.Room.AnyAsync())
            {
                var rooms = new List<Room>
                {
                    new Room
                    {
                        Name = "Conference Room",
                        Capacity = 10,
                        Description = "A spacious conference room equipped with modern amenities.",
                        Equipment = "Projector, Whiteboard, Wi-Fi",
                        PricePerHour = 150,
                        StarRating = 4,
                        GuestRating = 4.5,
                        NumberOfRooms = 5,
                        IsAvailable = true
                        },

                    new Room
                    {
                        Name = "Executive Meeting Room",
                        Capacity = 20,
                        Description = "An executive meeting room designed for high-profile meetings.",
                        Equipment = "Video Conferencing, Sound System, Wi-Fi",
                        PricePerHour = 300,
                        StarRating = 5,
                        GuestRating = 4.8,
                        NumberOfRooms = 2,
                        IsAvailable = true
                        },

                    new Room

                    {
                        Name = "Training Room",
                        Capacity = 30,
                        Description = "A training room suitable for workshops and training sessions.",
                        Equipment = "Computers, Projector, Wi-Fi",
                        PricePerHour = 250,
                        StarRating = 4,
                        GuestRating = 4.3,
                        NumberOfRooms = 3,
                        IsAvailable = true
                        },

                    new Room

                    {
                        Name = "Small Meeting Room",
                        Capacity = 5,
                        Description = "A small meeting room ideal for quick discussions and brainstorming sessions.",
                        Equipment = "Whiteboard, Wi-Fi",
                        PricePerHour = 100,
                        StarRating = 3,
                        GuestRating = 4.0,
                        NumberOfRooms = 4,
                        IsAvailable = false
                        },

                    new Room

                    {
                        Name = "Boardroom",
                        Capacity = 15,
                        Description = "A formal boardroom for corporate meetings and presentations.",
                        Equipment = "Conference Table, Projector, Wi-Fi",
                        PricePerHour = 200,
                        StarRating = 5,
                        GuestRating = 4.7,
                        NumberOfRooms = 2,
                        IsAvailable = false
                    },

                    new Room
                    {
                        Name = "Workshop Room",
                        Capacity = 25,
                        Description = "A versatile workshop room for hands-on activities and group work.",
                        Equipment = "Workbenches, Tools, Wi-Fi",
                        PricePerHour = 180,
                        StarRating = 4,
                        GuestRating = 4.4,
                        NumberOfRooms = 3,
                        IsAvailable = true
                    },

                    new Room
                    {
                        Name = "Training Lab",
                        Capacity = 20,
                        Description = "A training lab equipped with the latest technology for effective learning.",
                        Equipment = "Computers, Projector, Wi-Fi",
                        PricePerHour = 220,
                        StarRating = 4,
                        GuestRating = 4.6,
                        NumberOfRooms = 2,
                        IsAvailable = true

                    },

                    new Room
                    {
                        Name = "Creative Studio",
                        Capacity = 15,
                        Description = "A creative studio designed for brainstorming and innovative thinking.",
                        Equipment = "Whiteboard, Art Supplies, Wi-Fi",
                        PricePerHour = 160,
                        StarRating = 4,
                        GuestRating = 4.2,
                        NumberOfRooms = 2,
                        IsAvailable = false
                    },

                    new Room
                    {
                        Name = "Interview Room",
                        Capacity = 8,
                        Description = "A professional interview room for conducting interviews and assessments.",
                        Equipment = "Table, Chairs, Wi-Fi",
                        PricePerHour = 120,
                        StarRating = 3,
                        GuestRating = 4.1,
                        NumberOfRooms = 3,
                        IsAvailable = true
                    },

                    new Room
                    {
                        Name = "Virtual Meeting Room",
                        Capacity = 12,
                        Description = "A virtual meeting room equipped with advanced video conferencing technology.",
                        Equipment = "Video Conferencing, High-Speed Internet, Wi-Fi",
                        PricePerHour = 280,
                        StarRating = 5,
                        GuestRating = 4.9,
                        NumberOfRooms = 2,
                        IsAvailable = false
                    }


                };
                await context.Room.AddRangeAsync(rooms);
                await context.SaveChangesAsync();

            }

        }
        public static async Task SeedRoles(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "Manager", "User" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    var role = new IdentityRole(roleName);
                    await roleManager.CreateAsync(role);
                }
            }

            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com", EmailConfirmed = true }; //Admin Email
                await userManager.CreateAsync(adminUser, "Admin@123"); //Admin Password
            }

            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}