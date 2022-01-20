using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        //private readonly UserManager<User> _userManager;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            //_userManager = userManager;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            //await _context.Database.EnsureCreatedAsync(); //vai ver se a DB está criada se n tiver cria

            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

            var user = await _userHelper.GetUserByEmailAsync("gonfroes@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Gonçalo",
                    LastName = "Pereira",
                    Email = "gonfroes@gmail.com",
                    UserName = "gonfroes@gmail.com",
                    PhoneNumber = "214832033"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Couldn't create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            } 

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            if (!_context.Products.Any())
            {
                AddProduct("Iphone X", user);
                AddProduct("Michey Mouse", user);
                AddProduct("Iwatch Series 4", user);
                AddProduct("Ipad Mini", user);
                AddProduct("iphone 2", user);

                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100),
                User = user
            });
        }
    }
}
