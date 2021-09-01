﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRepository.cs" company="TVSnxt">
//   Copyright © 2021 Company="TVSnxt"
// </copyright>
// <creator name="Diwakar"/>
// ----------------------------------------------------------------------------------------------------------

namespace FundooNotes.Repository.Repository
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Net.Mail;
    using System.Security.Claims;
    using System.Text;
    using Experimental.System.Messaging;
    using FundooNotes.Models;
    using FundooNotes.Repository.Interface;
    using global::Models.Models;
    using global::Repository.Context;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// user repository class
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// The user context
        /// </summary>
        private readonly UserContext userContext;

        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository" /> class
        /// </summary>
        /// <param name="userContext">Initializes objects</param>
        public UserRepository(UserContext userContext, IConfiguration configuration)
        {
            this.userContext = userContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Method to encrypt the password
        /// </summary>
        /// <param name="password">the password</param>
        /// <returns>encrypted password</returns>
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        /// <summary>
        /// method to register
        /// </summary>
        /// <param name="userData">user data to register</param>
        /// <returns>successfully register or not</returns>
        public string Register(RegisterModel userData)
        {
            try
            {
                var checkEmail = this.userContext.Users.Where(x => x.Email.Equals(userData.Email)).FirstOrDefault();
                if(checkEmail==null)
                {
                    if (userData != null)
                    {
                        userData.Password = EncodePasswordToBase64(userData.Password);
                        this.userContext.Users.Add(userData);
                        this.userContext.SaveChanges();
                        return "Registration Successfull";
                    }
                    return "Registration UnSuccessfull";
                }

                return "Email Id already exist";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// method to login
        /// </summary>
        /// <param name="userData">user data to login</param>
        /// <returns>successfully login or not</returns>
        public string Login(CredentialModel userData)
        {
            try
            { 
                string email = userData.Email;
                string encodedPassword = EncodePasswordToBase64(userData.Password);
                var login = this.userContext.Users.Where(x => x.Email.Equals(email) && x.Password.Equals(encodedPassword)).FirstOrDefault();
                return login != null ? "Login Success" : "Login failed!!Email or password wrong";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Method to Forgot password.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>Reset email sent or not</returns>
        public bool ForgotPassword(string email)
        {
            try
            {
                var checkEmail = this.userContext.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();
                if (checkEmail != null)
                {
                    string url = "www.google.com";
                    this.SendToQueue(url);
                    return this.ReceiveQueue(email);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sends to queue.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void SendToQueue(string url)
        {
            try
            {
                MessageQueue msgQueue;
                if (MessageQueue.Exists(@".\Private$\MyQueue"))
                {
                    msgQueue = new MessageQueue(@".\Private$\MyQueue");
                }
                else
                {
                    msgQueue = MessageQueue.Create(@".\Private$\MyQueue");
                }

                Message message = new Message();
                message.Formatter = new BinaryMessageFormatter();
                message.Body = url;
                msgQueue.Label = "Url Link";
                msgQueue.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Receives from the queue.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>returns boolean value</returns>
        public bool ReceiveQueue(string email)
        {
            try
            {
                var receiveQueue = new MessageQueue(@".\Private$\MyQueue");
                var receiveMsg = receiveQueue.Receive();
                receiveMsg.Formatter = new BinaryMessageFormatter();
                string linkToSend = receiveMsg.Body.ToString();
                return this.SendMail(email, linkToSend);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="url">The URL.</param>
        /// <returns>boolean value whether mail sent or not</returns>
        public bool SendMail(string email, string url)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("cristianomessicrlm0730@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Reset your password";
                mail.Body = $"Click this link to reset your password\n{url}";
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("cristianomessicrlm0730@gmail.com", "CristianoMessi0730");
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="userData">The user data.</param>
        /// <returns>
        /// returns the boolean value
        /// </returns>
        public bool ResetPassword(CredentialModel userData)
        {
            try
            {
                var checkEmail = this.userContext.Users.Where(x => x.Email.Equals(userData.Email)).FirstOrDefault();
                if (checkEmail != null)
                {
                    checkEmail.Password = EncodePasswordToBase64(userData.Password);
                    this.userContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenrateJwtToken(string email)
        {
            byte[] key = Convert.FromBase64String(this.configuration["SecretKey"]);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, email)
            }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }
    }
}
