using Autocomp.Nmea.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NmeaParser.ViewModels;

namespace UnitTest
{
    [TestClass]
    public class UnitTestMWV
    {
        [TestMethod]
        public void TestCorrect()
        {
            NmeaMWV check = new NmeaMWV("$WIMWV,214.8,R,0.1,K,A*28");
            Assert.AreEqual(check.Error, false);
        }

        [TestMethod]
        public void TestNumberOfFields()
        {
            NmeaMWV check = new NmeaMWV("$WIMWV,214.8,R,0.1*28");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestWindAngle()
        {
            NmeaMWV check = new NmeaMWV("$WIMWV,a214.8,R,0.1,K,A*28");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestReference()
        {
            NmeaMWV check = new NmeaMWV("$WIMWV,214.8,aR,0.1,K,A*28");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestWindSpeed()
        {
            NmeaMWV check = new NmeaMWV("$WIMWV,214.8,R,a0.1,K,A*28");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestWindSpeedUnits()
        {
            NmeaMWV check = new NmeaMWV("$WIMWV,214.8,R,0.1,aK,A*28");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestStatus()
        {
            NmeaMWV check = new NmeaMWV("$WIMWV,214.8,R,0.1,K,aA*28");
            Assert.AreEqual(check.Error, true);
        }
    }
}