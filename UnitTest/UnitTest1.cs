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
            Assert.IsTrue(Functions.IsValidPhoneNumber("89009205001"));
            Assert.IsTrue(Functions.IsValidPhoneNumber("89995213322"));
            Assert.IsFalse(Functions.IsValidPhoneNumber("899995959559"));
            Assert.IsFalse(Functions.IsValidPhoneNumber("89993"));
            Assert.IsFalse(Functions.IsValidPhoneNumber("My phone number"));
            Assert.IsFalse(Functions.IsValidPhoneNumber(""));
        }
        [TestMethod]
        public void IsValidEmail()
        {
            Assert.IsTrue(Functions.IsValidEmail("Matrix@gmail.com"));
            Assert.IsTrue(Functions.IsValidEmail("Imagine@mail.ru"));
            Assert.IsFalse(Functions.IsValidEmail("usermail.com"));
            Assert.IsFalse(Functions.IsValidEmail("usermailcom"));
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
            string login = "Matrix";
            string password = "meme3";
            Assert.IsTrue(Functions.LoginCheck(login, password));
        }
        [TestMethod]
        public void IsValidLoginAndPassword()
        {
            Assert.IsTrue(Functions.IsValidLogAndPass("Matrix", "meme3"));
            Assert.IsTrue(Functions.IsValidLogAndPass("Imagine", "pizza"));
            Assert.IsTrue(Functions.IsValidLogAndPass("Login???", "p@ssw0rd"));
            Assert.IsFalse(Functions.IsValidLogAndPass("", ""));
            Assert.IsFalse(Functions.IsValidLogAndPass("", "SimplePass"));
            Assert.IsFalse(Functions.IsValidLogAndPass("SimpleLogin", ""));
        }
        [TestMethod]
        public void IsLoginAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsLoginAlreadyTaken("Matrix"));
            Assert.IsTrue(Functions.IsLoginAlreadyTaken("Imagine"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("SimpleLogin"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken("Login?"));
            Assert.IsFalse(Functions.IsLoginAlreadyTaken(""));
        }

        [TestMethod]
        public void IsValidDateOfBirthday()
        {
            DateTime date1 = new DateTime(1999, 01, 01);
            DateTime date2 = new DateTime(2025, 12, 31);
            Assert.IsTrue(Functions.IsValidDateOfBirthday(date1));
            Assert.IsFalse(Functions.IsValidDateOfBirthday(date2));
        }
        [TestMethod]
        public void IsPhoneNumberAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsPhoneNumberAlreadyTaken("8002992921"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("8002992920"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("8002992919"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("2222992950"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("9992992921"));
            Assert.IsFalse(Functions.IsPhoneNumberAlreadyTaken("9022992020"));
        }
        [TestMethod]
        public void IsEmailAlreadyTaken()
        {
            Assert.IsTrue(Functions.IsEmailAlreadyTaken("Matrix@gmail.com"));
            Assert.IsTrue(Functions.IsEmailAlreadyTaken("Imagine@gmail.com"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("user3@mail.ru"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("user42@gmail.com"));
            Assert.IsFalse(Functions.IsEmailAlreadyTaken("s0mpleEmail@sibmail.com"));
        }
        [TestMethod]
        public void IsLogEqualPass()
        {
            Assert.IsFalse(Functions.IsLogEqualPass("Matrix", "Matrix"));
            Assert.IsTrue(Functions.IsLogEqualPass("Matrix", "meme3"));
        }
        [TestMethod]
        public void IsValidLength()
        {
            Assert.IsTrue(Functions.IsValidLength("Matrix"));
            Assert.IsTrue(Functions.IsValidLength("Matrwerwewe"));
            Assert.IsFalse(Functions.IsValidLength("Ma"));
            Assert.IsFalse(Functions.IsValidLength(""));
        }
        [TestMethod]
        public void IsChatAlreadyCreated()
        {
            Assert.IsTrue(Functions.IsChatAlreadyCreated("Pizza"));
            Assert.IsFalse(Functions.IsChatAlreadyCreated("Welcome to the club, buddy"));
            Assert.IsFalse(Functions.IsChatAlreadyCreated(""));
        }
    }
}