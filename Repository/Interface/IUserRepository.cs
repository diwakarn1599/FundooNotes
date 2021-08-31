﻿using FundooNotes.Models;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Repository.Interface
{
    public interface IUserRepository
    {
        bool Register(RegisterModel userData);
        string Login(LoginModel userData);
        bool ForgotPassword(string email);
    }
}
