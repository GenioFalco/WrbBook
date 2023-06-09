using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;
using WebBook.ClassesApp.Models;
using WebBook.EntityFramework;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace WebBook.ClassesApp
{
    class Checks
    {

       
        public static bool CheckNull(string TolkBukva, string Pole, int lenght = 50)
        {
            if (string.IsNullOrEmpty(TolkBukva))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }

            if (TolkBukva.Length > lenght)
            {
                MessageBox.Show(Pole + $" не должно превышать {lenght} символов");
                return false;
            }

            if (string.IsNullOrWhiteSpace(TolkBukva))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }

            if (!Regex.IsMatch(TolkBukva, @"^(?!.*\s$)(?!.*\s{2})(?!^\s).*$"))
            {
                MessageBox.Show(Pole + " уберите лишние пробелы.");
                return false;
            }

            else
            {
                return true;
            }
        }

        public static bool ValidateEmail(string emailAddress, int lenght = 250)
        {
            if (string.IsNullOrEmpty(emailAddress))
            {
                MessageBox.Show("Поле почта обязательно для заполнения.");
                return false;
            }


            if (emailAddress.Length > lenght)
            {
                MessageBox.Show($" не должно превышать {lenght} символов");
                return false;
            }
            string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Match match = Regex.Match(emailAddress, pattern);
            return match.Success;
        }

        public static bool TolkBukva(string TolkBukva , string Pole, int lenght = 30)
        {
            if (string.IsNullOrEmpty(TolkBukva))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }

            if (TolkBukva.Length > lenght)
            {
                MessageBox.Show(Pole + $" не должно превышать {lenght} символов");
                return false;
            }

           
            if (!Regex.IsMatch(TolkBukva, @"^[а-я' 'А-Я]+$"))
            {
                MessageBox.Show(Pole + " поддерживает только символы кирилического алфавита.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(TolkBukva))
            {
                MessageBox.Show(Pole + " не должно быть пустое.");
                return false;
            }

            if (!Regex.IsMatch(TolkBukva, @"^(?!.*\s$)(?!.*\s{2})(?!^\s).*$"))
            {
                MessageBox.Show(Pole + " уберите лишние пробелы.");
                return false;
            }

            else
            {
                return true;
            }
        }



        public static bool LetteLatinAndCyrillic(string TolkBukva, string Pole, int lenght = 100)
        {
            if (string.IsNullOrEmpty(TolkBukva))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }

            if (TolkBukva.Length > lenght)
            {
                MessageBox.Show(Pole + $" не должно превышать {lenght} символов");
                return false;
            }
          
            if (!Regex.IsMatch(TolkBukva, @"^[а-яА-Я' 'a-zA-Z,.?-]+$"))
            {
                MessageBox.Show(Pole + " не поддерживает ввод цифр.");
                return false;
            }
            if(string.IsNullOrWhiteSpace(TolkBukva))
            {
                MessageBox.Show(Pole + " не должно быть пустое.");
                return false;
            }

            if (!Regex.IsMatch(TolkBukva, @"^(?!.*\s$)(?!.*\s{2})(?!^\s).*$"))
            {
                MessageBox.Show(Pole + " уберите лишние пробелы.");
                return false;
            }

            else
            {
                return true;
            }
        }


        public static bool Email(string email, string Pole, int lenght = 250)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }

            if (email.Length > lenght)
            {
                MessageBox.Show(Pole + $" не должно превышать {lenght} символов");
                return false;
            }

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

        public static bool Number(string email, string Pole,int lenght = 1)
        {
            if (email.Length > lenght)
            {
                MessageBox.Show(Pole + $" не должно превышать {lenght} символов");
                return false;
            }

            if (!Regex.IsMatch(email, @"^[0-9]+$"))
            {
                MessageBox.Show(Pole + " поддерживает только цифры");
                return false;
            }

             if(string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show(Pole + " не должно быть пустое.");
                return false;
            }

            if (!Regex.IsMatch(email, @"^(?!.*\s$)(?!.*\s{2})(?!^\s).*$"))
            {
                MessageBox.Show(Pole + " уберите лишние пробелы.");
                return false;
            }
            else
            {
                return true;
            }
        }


        public static bool WordAndNumber(string TolkBukva, string Pole, int lenght = 15)
        {
            if (string.IsNullOrEmpty(TolkBukva))
            {
                MessageBox.Show(Pole + " обязательно для заполнения.");
                return false;
            }

            if (TolkBukva.Length > lenght)
            {
                MessageBox.Show(Pole + $" не должно превышать {lenght} символов");
                return false;
            }

            if (!Regex.IsMatch(TolkBukva, @"^[0-9а-яА-Я' 'a-zA-Z-]+$"))
            {
                MessageBox.Show(Pole + " не поддерживает ввод цифр.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(TolkBukva))
            {
                MessageBox.Show(Pole + " не должно быть пустое.");
                return false;
            }

            if (!Regex.IsMatch(TolkBukva, @"^(?!.*\s$)(?!.*\s{2})(?!^\s).*$"))
            {
                MessageBox.Show(Pole + " уберите лишние пробелы.");
                return false;
            }

            else
            {
                return true;
            }
        }

        public static bool WordAndNumber2(List<QuestionModel> questionModels, string Pole, int lenght = 50)
        {

            foreach (var question in questionModels)
            {
                if (string.IsNullOrEmpty(question.Title))
                {
                    MessageBox.Show("Вопрос" + " " + question.Id + Pole + " обязательно для заполнения.");
                    return false; // Если поле Title пустое, устанавливаем флаг, выводим сообщение и выходим из цикла.
                }

                if (string.IsNullOrWhiteSpace(question.Title))
                {
                    MessageBox.Show("Вопрос"+ " " + question.Id + Pole + " не должно быть пустое.");
                    return false;
                }

                if (question.Title.Length > lenght)
                {
                    MessageBox.Show("Вопрос" + " " + question.Id + Pole + $" не должно превышать {lenght} символов");
                    return false;
                }

                if (!Regex.IsMatch(question.Title, @"^(?!.*\s$)(?!.*\s{2})(?!^\s).*$"))
                {
                    MessageBox.Show("Вопрос" + " " + question.Id + Pole + " уберите лишние пробелы.");
                    return false;
                }
                if (!Regex.IsMatch(question.Title, @"^[a-zA-Z?.,!;:-]"))
                {
                    MessageBox.Show("Вопрос" + " " + question.Id + Pole + " допустимы только буквы а также символы ?.,!;:-.");
                    return false;
                }
            }
            return true;


        }

        public static bool WordAndNumber3(List<AnswerModel> answerModels, string Pole, int lenght = 50)
        {

            foreach (var question in answerModels)
            {
                if (string.IsNullOrEmpty(question.Title))
                {
                    MessageBox.Show(Pole + " обязательно для заполнения.");
                    return false; // Если поле Title пустое, устанавливаем флаг, выводим сообщение и выходим из цикла.
                }

                if (string.IsNullOrWhiteSpace(question.Title))
                {
                    MessageBox.Show(Pole + " не должно быть пустое.");
                    return false;
                }

                if (question.Title.Length > lenght)
                {
                    MessageBox.Show(Pole + $" не должно превышать {lenght} символов");
                    return false;
                }

                if (!Regex.IsMatch(question.Title, @"^(?!.*\s$)(?!.*\s{2})(?!^\s).*$"))
                {
                    MessageBox.Show(Pole + " уберите лишние пробелы.");
                    return false;
                }
                if (!Regex.IsMatch(question.Title, @"^[a-zA-Z?.,!;:-]"))
                {
                    MessageBox.Show(Pole + " допустимы только буквы а также символы ?.,!;:-.");
                    return false;
                }

            }
            return true;


        }




    }
}
