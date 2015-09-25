using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using Moq;
using LineDraw.Converters;
using System.Windows.Media;
using System.Windows.Data;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class EnumToBooleanConverterFixture
    {
        [TestMethod]
        public void ShouldConvertToBool()
        {
            //Prepare
            EnumToBooleanConverter target = new EnumToBooleanConverter();
            PathAlgorithm algo = PathAlgorithm.BFS;

            //Act
            bool result1 = (bool)target.Convert(algo, null, PathAlgorithm.BFS, null);
            bool result2 = (bool)target.Convert(algo, null, PathAlgorithm.AStar, null);

            //Verify
            Assert.IsTrue(result1);
        }

        [TestMethod]
        public void ShouldConvertToEnum()
        {
            //Prepare
            EnumToBooleanConverter target = new EnumToBooleanConverter();
            PathAlgorithm algo = PathAlgorithm.BFS;

            //Act
            object result1 = target.ConvertBack(true, null, PathAlgorithm.BFS, null);
            object result2 = target.ConvertBack(false, null, PathAlgorithm.BFS, null);

            //Verify
            Assert.AreEqual(PathAlgorithm.BFS, result1);
            Assert.AreEqual(Binding.DoNothing, result2);
        }
    }
}
