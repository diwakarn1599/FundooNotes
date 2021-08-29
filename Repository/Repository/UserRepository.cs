using FundooNotes.Models;
using FundooNotes.Repository.Interface;
using Models.Models;
using Repository.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Repository.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext userContext;
        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }
        /// <summary>
        /// method to register
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public bool Register(RegisterModel userData)
        {
            try
            {
                if (userData != null)
                {
                    userData.password = EncodePasswordToBase64(userData.password);
                    this.userContext.Users.Add(userData);
                    this.userContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Function to login
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        public string Login(LoginModel userData)
        {
            try
            { 
                string email = userData.email;
                string encodedPassword = EncodePasswordToBase64(userData.password);
                var login = this.userContext.Users.Where(x => x.email.Equals(email) && x.password.Equals(encodedPassword)).FirstOrDefault();
                return login != null ? "Login Success" : "Login failed!!Email or password wrong";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Method to encrypt the password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
    }
}
