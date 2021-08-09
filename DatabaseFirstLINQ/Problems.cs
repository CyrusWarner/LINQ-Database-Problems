using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DatabaseFirstLINQ.Models;
using System.Collections.Generic;

namespace DatabaseFirstLINQ
{
    class Problems
    {
        private ECommerceContext _context;

        public Problems()
        {
            _context = new ECommerceContext();
        }
        public void RunLINQQueries()
        {
            //ProblemOne(); COMPLETED
            //ProblemTwo(); COMPLETED
            //ProblemThree(); COMPLETED
            //ProblemFour(); COMPLETED
            //ProblemFive(); COMPLETED
            //ProblemSix(); COMPLETED
            //ProblemSeven(); COMPLETED
            //ProblemEight(); COMPLETED
            //ProblemNine(); COMPLETED
            //ProblemTen(); COMPLETED
            //ProblemEleven(); COMPLETED
            //ProblemTwelve(); COMPLETED
            //ProblemThirteen(); COMPLETED
            //ProblemFourteen(); COMPLETED
            //ProblemFifteen(); COMPLETED 
            //ProblemSixteen(); COMPLETED
            //ProblemSeventeen(); COMPLETED
            //ProblemEighteen(); COMPLETED
            //ProblemNineteen(); COMPLETED 
            //ProblemTwenty(); COMPLETED
            BonusOne();
            //BonusThree();
        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void ProblemOne()
        {
            
            var numberOfUsers = _context.Users.ToList().Count;
            Console.WriteLine(numberOfUsers);
            // Write a LINQ query that returns the number of users in the Users table.
            // HINT: .ToList().Count

        }

        private void ProblemTwo()
        {
            // Write a LINQ query that retrieves the users from the User tables then print each user's email to the console.
            var users = _context.Users;

            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void ProblemThree()
        {
            // Write a LINQ query that gets each product where the products price is greater than $150.
            // Then print the name and price of each product from the above query to the console.
            var products = _context.Products;
            var productsPriceQuery = products.Where(product => product.Price > 150);
            foreach(var product in productsPriceQuery)
            {
                Console.WriteLine($"{product.Name}: {product.Price} dollars");
            }

        }

        private void ProblemFour()
        {
            // Write a LINQ query that gets each product that contains an "s" in the products name.
            // Then print the name of each product from the above query to the console.
            var products = _context.Products;
            var productLetterQuery = products.Where(product => product.Name.Contains("s"));
            foreach(var product in productLetterQuery)
            {
                Console.WriteLine(product.Name);
            }

        }

        private void ProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered BEFORE 2016
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var usersRegistered = users.Where(user => user.RegistrationDate.Value.Year < 2016);
            foreach (var user in usersRegistered)
            {
                Console.WriteLine($"{user.Email}-{user.RegistrationDate}");
            }

        }

        private void ProblemSix()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018
            // Then print each user's email and registration date to the console.
            var users = _context.Users;
            var usersRegisteredBetweenDates = users.Where(user => user.RegistrationDate.Value.Year > 2016).Where(user => user.RegistrationDate.Value.Year < 2018);
            foreach (var user in usersRegisteredBetweenDates)
            {
                Console.WriteLine($"{user.Email}-{user.RegistrationDate}");
            }
        }

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void ProblemSeven()
        {
            // Write a LINQ query that retreives all of the users who are assigned to the role of Customer.
            // Then print the users email and role name to the console.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            foreach (UserRole userRole in customerUsers)
            {
                Console.WriteLine($"Email: {userRole.User.Email} Role: {userRole.Role.RoleName}");
            }
        }

        private void ProblemEight()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var shoppingCartProducts = _context.ShoppingCarts.Include(user => user.User).Include(products => products.Product).Where(user => user.User.Email == "afton@gmail.com");
            foreach (ShoppingCart products in shoppingCartProducts)
            {
                Console.WriteLine($"{products.Product.Name} {products.Product.Price} {products.Quantity}");
            }





        }

        private void ProblemNine()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Then print the total of the shopping cart to the console.
            var shoppingCartProducts = _context.ShoppingCarts.Include(user => user.User).Include(products => products.Product).Where(user => user.User.Email == "afton@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine(shoppingCartProducts);
        }

        private void ProblemTen()
        {
            // Write a LINQ query that retreives all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the user's email as well as the product's name, price, and quantity to the console.
            // COME BACK TO AND TRY AGAIN
            var employees = _context.UserRoles.Where(user => user.Role.RoleName == "Employee").Select(user => user.UserId);
            var shoppingCartEmployeeProducts = _context.ShoppingCarts.Include(sc => sc.Product).Include(sc => sc.User).Where(sc => employees.Contains(sc.UserId));

            foreach(ShoppingCart employee in shoppingCartEmployeeProducts)
            {
                Console.WriteLine($"{employee.User.Email}");
                Console.WriteLine($"Product Name:{employee.Product.Name}, Product Price:{employee.Product.Price}, Product Quantity:{employee.Quantity}");
            }


        }

        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        // <><> C Actions (Create) <><>

        private void ProblemEleven()
        {
            // Create a new User object and add that user to the Users table using LINQ.
            User newUser = new User()
            {
                Email = "david@gmail.com",
                Password = "DavidsPass123"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        private void ProblemTwelve()
        {
            // Create a new Product object and add that product to the Products table using LINQ.
            Product newProduct = new Product()
            {
                Name = "Toilet Paper",
                Description = "Everyone needs it",
                Price = 5
            };
            _context.Products.Add(newProduct);
            _context.SaveChanges();

        }

        private void ProblemThirteen()
        {
            // Add the role of "Customer" to the user we just created in the UserRoles junction table using LINQ.
            var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            UserRole newUserRole = new UserRole()
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        private void ProblemFourteen()
        {
            // Add the product you create to the user we created in the ShoppingCart junction table using LINQ.
            var productId = _context.Products.Where(p => p.Name == "Toilet Paper").Select(p => p.Id).SingleOrDefault();
            var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();

            ShoppingCart newUserProduct = new ShoppingCart()
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1,
            };
            _context.ShoppingCarts.Add(newUserProduct);
            _context.SaveChanges();

        }

        // <><> U Actions (Update) <><>

        private void ProblemFifteen()
        {
            // Update the email of the user we created to "mike@gmail.com"
            var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            user.Email = "mike@gmail.com";
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        private void ProblemSixteen()
        {
            // Update the price of the product you created to something different using LINQ.
            var products = _context.Products;
            var product = products.Where(p => p.Id == 9).SingleOrDefault();
            product.Name = "Chicken";
            _context.Products.Update(product);
            _context.SaveChanges();

        }

        private void ProblemSeventeen()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new UserRole object and add it to the UserRoles table
            // See problem eighteen as an example of removing a role relationship
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "mike@gmail.com").SingleOrDefault();
            _context.UserRoles.Remove(userRole);
            UserRole newUserRole = new UserRole()
            {
                UserId = _context.Users.Where(u => u.Email == "mike@gmail.com").Select(u => u.Id).SingleOrDefault(),
                RoleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault()
            };
            _context.UserRoles.Add(newUserRole);
            _context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void ProblemEighteen()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            var userRole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com");
            foreach(UserRole userRoleRelationship in userRole)
             {
                _context.UserRoles.Remove(userRoleRelationship);
            }
            _context.SaveChanges();

        }

        private void ProblemNineteen()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Loop
            var shoppingCartProducts = _context.ShoppingCarts.Where(sc => sc.User.Email == "oda@gmail.com");
            foreach (ShoppingCart userProductRelationship in shoppingCartProducts)
            {
                _context.ShoppingCarts.Remove(userProductRelationship);
            }
            _context.SaveChanges();
        }

        private void ProblemTwenty()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            var user = _context.Users.Where(u => u.Email == "oda@gmail.com");
            foreach (User userRelationship in user)
            {
                _context.Users.Remove(userRelationship);
            }
            _context.SaveChanges();
            

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            Console.WriteLine("Please enter your email");
            string userEmail = Console.ReadLine();
            Console.WriteLine("Please enter your password");
            string userPassword = Console.ReadLine(); 
            var users = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword);
            foreach (User user in users)
            {
              Console.WriteLine("Signed in");
                return;
            }
            Console.WriteLine("Invalid Email or Password");
            BonusOne();
        }

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the toals to the console.
        }

        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console COMPLETED
            // 2. If the user succesfully signs in COMPLETED
            // a. Give them a menu where they perform the following actions within the console COMPLETED
            // View the products in their shopping cart
            // View all products in the Products table
            // Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // Remove a product from their shopping cart
            // 3. If the user does not succesfully sing in COMPLETED
            // a. Display "Invalid Email or Password" COMPLETED
            // b. Re-prompt the user for credentials COMPLETED
            List<string> options = new List<string>()
            {
                "View products in your shopping cart",
                "View all products ",
                "Add a product to the shopping cart",
                "remove a product from the shopping cart",
            };
            Console.WriteLine("Please Enter Your Email");
            string userEmail = Console.ReadLine();
            Console.WriteLine("Please Enter Your Password");
            string userPassword = Console.ReadLine();
            var users = _context.Users.Where(user => user.Email == userEmail && user.Password == userPassword);
            foreach (User user in users)
            {
                if (user.Email != null)
                {
                    {
                        Console.WriteLine("Signed in");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Email Or Password");
                    BonusThree();
                }
            }

        }

    }
}
