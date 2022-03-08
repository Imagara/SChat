using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SChat
{
    public class Functions
    {
        // Валидация номера телефона
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            foreach (char c in phoneNumber)
                if (!char.IsDigit(c))
                    return false;
            if (phoneNumber.Length != 10)
                return false;
            return true;
        }
        // Валидация электронной почты
        public static bool IsValidEmail(string email)
        {
            if (Regex.IsMatch(email, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
                return true;
            else
                return false;
        }
        // Валидация дня рождения
        public static bool IsValidDateOfBirthday(DateTime Date)
        {
            if (Date > DateTime.Now)
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsValidLogAndPass(string login, string password)
        {
            if (login == "" || password == "")
                return false;
            else
                return true;
        }
        // Валидация логина и пароля
        public static bool IsValidLogAndPassRegister(string login, string password)
        {
            if (login.Length < 5 || password.Length < 5)
                return false;
            if (login == password)
                return false;
            else
                return true;
        }
        //// Получение названия маршрута по его id
        //public static string GetRouteName(int routeId)
        //{
        //    return cnt.db.Routes.Where(item => item.IdRoute == routeId).Select(item => item.Name).FirstOrDefault();
        //}

        // Проверка на правильность введеных данных при входе
        public static bool LoginCheck(string login, string password)
        {
            if (cnt.db.User.Select(item => item.NickName + item.Password).Contains(login + Encrypt.GetHash(password)))
                return true;
            else
                return false;
        }
        // Проверка на уникальность логина
        public static bool IsLoginAlreadyTaken(string login)
        {
            return cnt.db.User.Select(item => item.NickName).Contains(login);
        }
        //// Проверка на уникальность номера телефона
        //public static bool IsPhoneNumberAlreadyTaken(string PhoneNum)
        //{
        //    return cnt.db.User.Select(item => item.PhoneNumber).Contains(PhoneNum);
        //}
        // Проверка на уникальность электронной почты
        public static bool IsEmailAlreadyTaken(string Email)
        {
            return cnt.db.User.Select(item => item.NickName).Contains(Email);
        }
        //// Получение названия транспорта по его id 
        //public static string GetNameOfTransport(int transportId)
        //{
        //    return cnt.db.Transport.Where(item => item.IdTransport == transportId).Select(item => item.NameOfTransport).FirstOrDefault();
        //}
        //// Получение номерного знака по его id
        //public static string GetNumberPlate(int transportId)
        //{
        //    return cnt.db.Transport.Where(item => item.IdTransport == transportId).Select(item => item.NumberPlate).FirstOrDefault();
        //}
        //// Получение названия точки по ее id
        //public static string GetNameOfPoint(int pointId)
        //{
        //    return cnt.db.Points.Where(item => item.IdPoint == pointId).Select(item => item.Name).FirstOrDefault();
        //}
        //// Получение локации точки по ее id
        //public static string GetLocationOfPoint(int pointId)
        //{
        //    return cnt.db.Points.Where(item => item.IdPoint == pointId).Select(item => item.location).FirstOrDefault();
        //}
        // Проверка на валидность название и локацию остановки
        public static bool IsValidNameAndLocationOfPoint(string name, string location)
        {
            if (name != "" && location != "")
                return true;
            else
                return false;
        }
        // Проверка на валидность информации о водителе
        public static bool IsValidInfoAboutDriver(string idTransport, string name, string surname, string patronymic)
        {
            if (IsOnlyDigits(idTransport) && idTransport != "" && name != "" && surname != "" && patronymic != "")
                return true;
            else
                return false;
        }
        public static bool IsOnlyDigits(string str)
        {
            foreach (char c in str)
                if (!char.IsDigit(c))
                    return false;
            return true;
        }
    }
}
