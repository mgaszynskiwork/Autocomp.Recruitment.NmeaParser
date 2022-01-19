using Autocomp.Nmea.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NmeaParser.ViewModels;

namespace UnitTest
{
    [TestClass]
    public class UnitTestGLL
    {
        [TestMethod]
        public void TestCorrect()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,4911.405,N,12311.152,W,075444,A,D,*1D");
            Assert.AreEqual(check.Error, false);
        }

        [TestMethod]
        public void TestLatitude()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,a4911.405,N,12311.152,W,075444,A,D,*1D");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestLatitudeDirection()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,4911.405,Na,12311.152,W,075444,A,D,*1D");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestLongitude()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,4911.405,N,a12311.152,W,075444,A,D,*1D");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestLongitudeDirection()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,4911.405,N,12311.152,aW,075444,A,D,*1D");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestUTC()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,4911.405,N,12311.152,W,a075444,A,D,*1D");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestStatus()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,4911.405,N,12311.152,W,075444,aA,D,*1D");
            Assert.AreEqual(check.Error, true);
        }

        [TestMethod]
        public void TestModeIndicator()
        {
            NmeaGLL check = new NmeaGLL("$GPGLL,4911.405,N,12311.152,W,075444,A,aD,*1D");
            Assert.AreEqual(check.Error, true);
        }
    }
}