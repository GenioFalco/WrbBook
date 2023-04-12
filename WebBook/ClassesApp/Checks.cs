using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WebBook.EntityFramework;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WebBook.ClassesApp
{
    class Checks
    {
        public static bool CheckNull(string str, string Pole)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return true;
            }
            MessageBox.Show(Pole + " обязательно для заполнения.");
            return false;
        }

        public static bool ValidateEmail(string emailAddress)
        {
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Match match = Regex.Match(emailAddress, pattern);
            return match.Success;

        }

        public static bool TolkBukva(string TolkBukva , string Pole)
        {
            if (string.IsNullOrEmpty(TolkBukva))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }
            else if (TolkBukva.Contains(" ") || !Regex.IsMatch(TolkBukva, @"^[а-яА-Я]+$"))
            {
                MessageBox.Show(Pole + " поддерживает только символы кирилического алфавита.");
                return false;
            }

            else
            {
                return true;
            }
        }


        public static bool LetteLatinAndCyrillic(string TolkBukva, string Pole)
        {
            if (string.IsNullOrEmpty(TolkBukva))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }
            else if (!Regex.IsMatch(TolkBukva, @"^[а-яА-Я' 'a-zA-Z]+$"))
            {
                MessageBox.Show(Pole + " не поддерживает ввод цифр.");
                return false;
            }

            else
            {
                return true;
            }
        }


        public static bool Email(string email, string Pole)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }
            else if (email.Contains(" ") || !Regex.IsMatch(email, @"^[a-zA-Z@._0-9]+$"))
            {
                MessageBox.Show(Pole + " поддерживает только латинские буквы и символы (@._-).");
                return false;
            }

            else
            {
                return true;
            }
        }

       
      
    }
}
