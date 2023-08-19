using ShoppingWEBUI.Core.DTO.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Helper.SessionHelper
{
    public class SessionManager
    {
        public static LoginDTO? LoggedUser 
        
        {
            get => AppHttpContext.Current.Session.GetObject<LoginDTO>("LoginDTO");

            set => AppHttpContext.Current.Session.SetObject("LoginDTO", value);
        } 
        public static string? Token 
        
        {
            get => AppHttpContext.Current.Session.GetObject<string>("Token");

            set => AppHttpContext.Current.Session.SetObject("Token", value);
        }
    }
}
