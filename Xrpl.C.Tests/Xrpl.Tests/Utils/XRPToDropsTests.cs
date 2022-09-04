﻿using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ripple.Binary.Codec.Types;
using Xrpl.Client.Exceptions;
using Xrpl.Client.Models.Transactions;
using Xrpl.Utils;
using Xrpl.XrplWallet;
using static Xrpl.Client.Models.Common.Common;

// https://github.com/XRPLF/xrpl.js/blob/main/packages/xrpl/test/utils/dropsToXrp.ts

namespace Xrpl.Tests.Client.Tests
{
    [TestClass]
    public class TestUXrpToDrops
    {
        [TestMethod]
        public void TestXRPToDrops()
        {
            string xrp = XrpConversion.XrpToDrops("2");
            Assert.AreEqual("2000000", xrp);
        }

        [TestMethod]
        public void TestXRPToDropsFractions()
        {
            var xrp = XrpConversion.XrpToDrops("3.456789");
            Assert.AreEqual("3456789", xrp);

            xrp = XrpConversion.XrpToDrops("3.4");
            Assert.AreEqual("3400000", xrp);

            xrp = XrpConversion.XrpToDrops("0.000001");
            Assert.AreEqual("1", xrp);

            xrp = XrpConversion.XrpToDrops("0.0000010");
            Assert.AreEqual("1", xrp);
        }

        [TestMethod]
        public void TestXRPToDropsZero()
        {
            var xrp = XrpConversion.XrpToDrops("0");
            Assert.AreEqual("0", xrp);

            xrp = XrpConversion.XrpToDrops("-0");
            Assert.AreEqual("0", xrp);

            xrp = XrpConversion.XrpToDrops("0.00");
            Assert.AreEqual("0", xrp);

            xrp = XrpConversion.XrpToDrops("000000000");
            Assert.AreEqual("0", xrp);
        }

        [TestMethod]
        public void TestXRPToDropsNegative()
        {
            var xrp = XrpConversion.XrpToDrops("-2");
            Assert.AreEqual("-2000000", xrp);
        }

        [TestMethod]
        public void TestXRPToDropsDecimal()
        {
            var xrp = XrpConversion.XrpToDrops("2.");
            Assert.AreEqual("2000000", xrp);

            xrp = XrpConversion.XrpToDrops("-2.");
            Assert.AreEqual("-2000000", xrp);
        }

        // SKIPPING FROM BIGNUMBER AS WE USE BIGINT

        //[TestMethod]
        //public void TestXRPToDropsDouble()
        //{
        //    var xrp = XrpConversion.XrpToDrops(2000000);
        //    Assert.AreEqual("2", xrp);

        //    xrp = XrpConversion.XrpToDrops(-2000000);
        //    Assert.AreEqual("-2", xrp);
        //}

        [TestMethod]
        public void TestXRPToDropsSCINotation()
        {
            var xrp = XrpConversion.XrpToDrops("1e-6");
            Assert.AreEqual("1", xrp);
        }

        [TestMethod]
        public void TestInvalidXRPToDropsDecimalError()
        {
            Assert.ThrowsException<ValidationError>(() => XrpConversion.XrpToDrops("1.1234567"));
            Assert.ThrowsException<ValidationError>(() => XrpConversion.XrpToDrops("0.0000001"));
        }

        [TestMethod]
        public void TestInvalidXRPToDropsValueError()
        {
            Assert.ThrowsException<FormatException>(() => XrpConversion.XrpToDrops("FOO"));
            Assert.ThrowsException<ValidationError>(() => XrpConversion.XrpToDrops("1e-7"));
            Assert.ThrowsException<FormatException>(() => XrpConversion.XrpToDrops("2,0"));
            Assert.ThrowsException<FormatException>(() => XrpConversion.XrpToDrops("."));
        }

        [TestMethod]
        public void TestInvalidXRPToDropsMultipleDecimalPointError()
        {
            Assert.ThrowsException<ValidationError>(() => XrpConversion.XrpToDrops("1.0.0"));
            Assert.ThrowsException<ValidationError>(() => XrpConversion.XrpToDrops("..."));
        }
    }
}

