using Experimental.System.Messaging;
using FundooNotes.Models;
using FundooNotes.Repository.Interface;
using Models.Models;
using Repository.Context;
using System;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Net;
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
                    userData.Password = EncodePasswordToBase64(userData.Password);
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
        
        public bool ForgotPassword(string email)
        {
            try
            {
                var checkEmail = this.userContext.Users.Where(x => x.Email.Equals(email)).FirstOrDefault();
                if (checkEmail != null)
                {
                    string url = "www.google.com";
                    SendToQueue(url);
                    return ReceiveQueue(email);
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            
        }
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

        public bool ReceiveQueue(string email)
        {
            try
            {
                var receiveQueue = new MessageQueue(@".\Private$\MyQueue");
                var receiveMsg = receiveQueue.Receive();
                receiveMsg.Formatter = new BinaryMessageFormatter();
                string linkToSend = receiveMsg.Body.ToString();
                return SendMail(email, linkToSend);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SendMail(string email,string url)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("cristianomessicrlm0730@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Reset your password";
                mail.Body = $"Click this link to reset your password\n{url}";
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("cristianomessicrlm0730@gmail.com", "CristianoMessi0730");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
                return true;
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
                encData_byte = Encoding.UTF8.GetBytes(password);
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
