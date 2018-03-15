namespace LocalTheatre.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LocalTheatre.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(LocalTheatre.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var user = new ApplicationUser();
            user.UserName = "user@test.com";
            user.Email = "user@test.com";

            string userPass = "Test123$";

            UserManager.Create(user, userPass);

            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            var adminuser = new ApplicationUser();
            adminuser.UserName = "admin@test.com";
            adminuser.Email = "admin@test.com";

            userPass = "Pass123$";

            var checkUser = UserManager.Create(adminuser, userPass);

            if (checkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(adminuser.Id, "Admin");

            }

            if (!roleManager.RoleExists("Restricted"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Restricted";
                roleManager.Create(role);


            }

            var post1 = new Post();
            post1.PostID = 1;
            post1.UserName = user.UserName;
            post1.PostTitle = "IT for good";
            post1.PostDesc = " It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for 'lorem ipsum' will uncover many web sites still in their infancy. Various versions have evolved over the years, sometimes by accident, sometimes on purpose (injected humour and the like).";
            post1.PostDate = DateTime.Now;

            try
            {
                context.Posts.Add(post1);
            }
            catch (SqlException ex)
            {

                var error = ex.InnerException;
            }

            var comment1 = new Comment();
            comment1.CommentID = 1;
            comment1.PostID = 1;
            comment1.commentMain = "good Article!";
            comment1.UserName = user.UserName;
            comment1.CommentDate = DateTime.Now;

            try
            {
                context.Comments.Add(comment1);
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
            }

            var Comment2 = new Comment();
            Comment2.CommentID = 2;
            Comment2.PostID = 1;
            Comment2.commentMain = "nice one";
            Comment2.UserName = user.UserName;
            Comment2.CommentDate = DateTime.Now;

            try
            {
                context.Comments.Add(Comment2);
            }
            catch (SqlException ex)
            {
                var error = ex.InnerException;
            }

            var post2 = new Post();
            post2.PostID = 2;
            post2.UserName = user.UserName;
            post2.PostTitle = " test2";
            post2.PostDesc = " Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock, a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure Latin words, consectetur, from a Lorem Ipsum passage, and going through the cites of the word in classical literature, discovered the undoubtable source. Lorem Ipsum comes from sections 1.10.32 and 1.10.33 of  (The Extremes of Good and Evil) by Cicero, written in 45 BC. This book is a treatise on the theory of ethics, very popular during the Renaissance. The first line of Lorem Ipsum, ";
            post2.PostDate = DateTime.Now;

            try
            {

                context.Posts.Add(post2);
            }
            catch (SqlException ex)
            {
                var error = ex.InnerException;
            }
            var restricted = new IdentityRole();
            restricted.Name = "restricted";
            roleManager.Create(restricted);


        }
    }
}
