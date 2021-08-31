using FundooNotes.Managers.Interface;
using FundooNotes.Models;
using FundooNotes.Repository.Interface;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Managers.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository repository;
        public UserManager(IUserRepository repository)
        {
            this.repository = repository;
        }

        public string Login(LoginModel userData)
        {
            try
            {
                return this.repository.Login(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Register(RegisterModel userData)
        {
            try
            {
                return this.repository.Register(userData);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ForgotPassword(string email)
        {
            try
            {
                bool res =  this.repository.ForgotPassword(email);
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
