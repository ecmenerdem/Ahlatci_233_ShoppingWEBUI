using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Core.DTO.User
{
    public class UserDTO
    {
        public Guid Guid { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }

        public string EPosta { get; set; }

        public string Tel { get; set; }

        public string Adres { get; set; }
    }
}
