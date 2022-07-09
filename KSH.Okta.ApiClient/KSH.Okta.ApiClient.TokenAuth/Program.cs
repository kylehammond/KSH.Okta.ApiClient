using System;
using KSH.Okta.ApiClient.TokenAuth.Models;
using KSH.Okta.ApiClient.TokenAuth.Services;

namespace KSH.Okta.ApiClient.TokenAuth
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                //// ======== get all users ======== 
                //Console.WriteLine("Getting all users...");
                //GetUsersTest();

                //// ======== get by id/login ======== 
                //Console.WriteLine("Getting user...");
                //GetUserTest("kyle.hammond"); // kyle
                //GetUserTest("00uf3g4k1rU7UNMLO0h7"); // kyle
                //GetUserTest("testguy.mccloud"); // testguy mccloud
                //GetUserTest("00uf5t2yvgq58KuN80h7"); // testguy mccloud

                //======== update a first name ======== 
                UpdateUserProfileTest("#.#@email.com");

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                Console.WriteLine(e);
                Console.ReadLine();
                throw;
            }
        }

        private static void UpdateUserProfileTest(string email)
        {
            var user = new OktaClient().GetUserAsync(email).GetAwaiter().GetResult();
            user.Profile.FirstName = "FirstName";
            Console.WriteLine("Attempting to update with email '" + email + "'...");
            var updatedUser = new OktaClient().UpdateUserProfileAsync(user).GetAwaiter().GetResult();
            WriteUser(updatedUser);
        }

        private static void GetUsersTest()
        {
            var users = new OktaClient().GetUsersAsync().GetAwaiter().GetResult(); // all users

            Console.WriteLine("Users found: " + users.Count);
            Console.WriteLine("Users:");
            foreach (var user in users) WriteUser(user);

            Console.WriteLine("===============================");
            Console.WriteLine(string.Empty);
        }

        private static void GetUserTest(string id)
        {
            var user = new OktaClient().GetUserAsync(id).GetAwaiter().GetResult();
            Console.WriteLine("User (searching on '" + id + "'):");
            WriteUser(user);
            Console.WriteLine("===============================");
            Console.WriteLine(string.Empty);
        }

        private static void WriteUser(User user)
        {
            Console.WriteLine("");

            Console.WriteLine("Id:  " + user.Id);
            Console.WriteLine("Status:  " + user.Status);
            Console.WriteLine("Created:  " + user.Created);
            Console.WriteLine("Activated:  " + user.Activated);
            Console.WriteLine("StatusChanged:  " + user.StatusChanged);
            Console.WriteLine("LastLogin:  " + user.LastLogin);
            Console.WriteLine("LastUpdated:  " + user.LastUpdated);
            Console.WriteLine("PasswordChanged:  " + user.PasswordChanged);
            Console.WriteLine("Login:  " + user.Profile.Login);
            Console.WriteLine("Email:  " + user.Profile.Email);
            Console.WriteLine("SecondEmail:  " + user.Profile.SecondEmail);
            Console.WriteLine("FirstName:  " + user.Profile.FirstName);
            Console.WriteLine("LastName:  " + user.Profile.LastName);
            Console.WriteLine("MiddleName:  " + user.Profile.MiddleName);
            Console.WriteLine("HonorificPrefix:  " + user.Profile.HonorificPrefix);
            Console.WriteLine("HonorificSuffix:  " + user.Profile.HonorificSuffix);
            Console.WriteLine("Title:  " + user.Profile.Title);
            Console.WriteLine("DisplayName:  " + user.Profile.DisplayName);
            Console.WriteLine("NickName:  " + user.Profile.NickName);
            Console.WriteLine("ProfileUrl:  " + user.Profile.ProfileUrl);
            Console.WriteLine("PrimaryPhone:  " + user.Profile.PrimaryPhone);
            Console.WriteLine("MobilePhone:  " + user.Profile.MobilePhone);
            Console.WriteLine("StreetAddress:  " + user.Profile.StreetAddress);
            Console.WriteLine("City:  " + user.Profile.City);
            Console.WriteLine("State:  " + user.Profile.State);
            Console.WriteLine("ZipCode:  " + user.Profile.ZipCode);
            Console.WriteLine("CountryCode:  " + user.Profile.CountryCode);
            Console.WriteLine("PostalAddress:  " + user.Profile.PostalAddress);
            Console.WriteLine("PreferredLanguage:  " + user.Profile.PreferredLanguage);
            Console.WriteLine("Locale:  " + user.Profile.Locale);
            Console.WriteLine("Timezone:  " + user.Profile.Timezone);
            Console.WriteLine("UserType:  " + user.Profile.UserType);
            Console.WriteLine("EmployeeNumber:  " + user.Profile.EmployeeNumber);
            Console.WriteLine("CostCenter:  " + user.Profile.CostCenter);
            Console.WriteLine("Organization:  " + user.Profile.Organization);
            Console.WriteLine("Division:  " + user.Profile.Division);
            Console.WriteLine("Department:  " + user.Profile.Department);
            Console.WriteLine("ManagerId:  " + user.Profile.ManagerId);
            Console.WriteLine("Manager:  " + user.Profile.Manager);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}
