using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SChat;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsValidPhoneNumber()
        {
            Assert.IsTrue(Functions.IsValidPhoneNumber("9999194949"));
            Assert.IsTrue(Functions.IsValidPhoneNumber("9994443322"));
            Assert.IsFalse(Functions.IsValidPhoneNumber("99991949499"));
            Assert.IsFalse(Functions.IsValidPhoneNumber("999919494"));
            Assert.IsFalse(Functions.IsValidPhoneNumber("My phone number"));
            Assert.IsFalse(Functions.IsValidPhoneNumber(""));
        }
        [TestMethod]
        public void IsValidEmail()
        {
            Assert.IsTrue(Functions.IsValidEmail("user@gmail.com"));
            Assert.IsTrue(Functions.IsValidEmail("user@mail.ru"));
            Assert.IsFalse(Functions.IsValidEmail("usergmail.com"));
            Assert.IsFalse(Functions.IsValidEmail("usergmailcom"));
            Assert.IsFalse(Functions.IsValidEmail(""));
        }
        [TestMethod]
        public void PasswordEncryptTest()
        {
            string password = "qq";
            string expected = "BED4EB698C6EEEA7F1DDF5397D480D3F2C0FB938";
            Assert.AreEqual(Encrypt.GetHash(password), expected);
        }
        [TestMethod]
        public void LoginTest()
        {
            string login = "kovalev30";
            string password = "kovalev333";
            Assert.IsTrue(Functions.LoginCheck(login, password));
        }
        [TestMethod]
        public void IsValidLoginAndPassword()
        {
            Assert.IsTrue(Functions.IsValidLogAndPass("login3", "password33"));
            Assert.IsTrue(Functions.IsValidLogAndPass("qq", "ww"));
            Assert.IsTrue(Functions.IsValidLogAndPass("laq", "wwadsw"));
            Assert.IsFalse(Functions.IsValidLogAndPass("", ""));
            Assert.IsFalse(Functions.IsValidLogAndPass("", "SimplePass"));
            Assert.IsFalse(Functions.IsValidLogAndPass("SimpleLogin", ""));
        }
        [TestMethod]
        public void IsValidLoginAndPasswordRegister()
        {
            Assert.IsTrue(Functions.IsValidLogAndPassRegister("login3", "password33"));
            Assert.IsFalse(Functions.IsValidLogAndPassRegister("login3", "login3"));
            Assert.IsFalse(Functions.IsValidLogAndPassRegister("qq", "ww"));
            Assert.IsFalse(Functions.IsValidLogAndPassRegister("qqvxfc", "ww"));

        }
        [TestMethod]
        public void IsLoginAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsLoginAlreadyTaken("kovalev30"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("user23"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("F"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken(""));
        }
        [TestMethod]
        public void GetNameOfTransportUsingId()
        {
            int transportId = 1;
            string expected = "Avtobus";
            Assert.AreEqual(Functions.GetNameOfTransport(transportId), expected);
        }
        [TestMethod]
        public void GetNumberPlateUsingId()
        {
            int transportId = 1;
            string expected = "а333аа78";
            Assert.AreEqual(Functions.GetNumberPlate(transportId), expected);
        }
        [TestMethod]
        public void GetRouteNameUsingId()
        {
            int routeId = 1;
            string expected = "Маршрут #1";
            Assert.AreEqual(Functions.GetRouteName(routeId), expected);
        }
        [TestMethod]
        public void GetNameOfPointUsingId()
        {
            int pointId = 1;
            string expected = "Томская";
            Assert.AreEqual(Functions.GetNameOfPoint(pointId), expected);
        }
        [TestMethod]
        public void GetLocationOfPointUsingId()
        {
            int pointId = 1;
            string expected = "Томская, 21";
            Assert.AreEqual(Functions.GetLocationOfPoint(pointId), expected);
        }
        [TestMethod]
        public void IsValidNameAndLocationOfPoint()
        {
            string Name = "Томская";
            string Location = "Томская, 21";
            Assert.IsTrue(Functions.IsValidNameAndLocationOfPoint(Name, Location));
        }
        [TestMethod]
        public void IsValidInfoAboutDriver()
        {
            string IdTransport = "1";
            string name = "Петр";
            string surname = "Некрасов";
            string patronymic = "Антонович";
            Assert.IsTrue(Functions.IsValidInfoAboutDriver(IdTransport, name, surname, patronymic));
        }
        [TestMethod]
        public void IsOnlyDigits()
        {
            string Id = "123";
            Assert.IsTrue(Functions.IsOnlyDigits(Id));
        }
        [TestMethod]
        public void IsValidDateOfBirthday()
        {
            DateTime date1 = new DateTime(2000, 05, 02);
            DateTime date2 = new DateTime(2025, 02, 01);
            Assert.IsTrue(Functions.IsValidDateOfBirthday(date1));
            Assert.IsFalse(Functions.IsValidDateOfBirthday(date2));
        }
        [TestMethod]
        public void IsPhoneNumberAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsPhoneNumberAlreadyTaken("9996963350"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("7776006061"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("7776006062"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("7776006063"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("7776006064"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("7776006065"));
        }
        [TestMethod]
        public void IsEmailAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsEmailAlreadyTaken("rud.kovalev@gmail.com"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("filaks@mail.ru"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("cute@gmail.com"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("user@gmail.com"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("simpleEmail@sibmail.com"));
        }
    }
}