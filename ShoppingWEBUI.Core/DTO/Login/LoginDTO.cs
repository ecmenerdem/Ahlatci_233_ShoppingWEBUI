using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.DTO.Login
{
    public class LoginDTO
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Token { get; set; }
        public string AdSoyad { get; set; }

        public string EPosta { get; set; }

        public string Adres { get; set; }

    }
}
