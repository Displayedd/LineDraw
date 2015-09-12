using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using LineDraw.Canvas;
using Moq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace LineDraw.Tests.Models
{
    [TestClass]
    public class CanvasViewModelFixture
    {
        [TestMethod]
        public void WhenConstructed_InitializesValues()
        {
            //Prepare
            Size size = new Size { Height = 500, Width = 500 };
            Mock<ILineService> mockedLineService = new Mock<ILineService>();
            mockedLineService.Setup(x => x.GetCanvasSize()).Returns(size).Verifiable();

            //Act
            CanvasViewModel target = new CanvasViewModel(mockedLineService.Object);

            //Verify
            Assert.IsInstanceOfType(target, typeof(CanvasViewModel));
            Assert.AreEqual(size.Height, target.CanvasHeight);
            Assert.AreEqual(size.Width, target.CanvasWidth);
            Assert.IsNull(target.StartPoint);
            Assert.IsNull(target.EndPoint);
            Assert.IsInstanceOfType(target.SelectPointCommand, typeof(ICommand));
            Assert.IsInstanceOfType(target.Lines, typeof(ObservableCollection<Point[]>));
            mockedLineService.VerifyAll();
        }

        [TestMethod]
        public void WhenSelectPointCommandExecuted_StartPointSet()
        {
            //Prepare
            Size size = new Size { Height = 500, Width = 500 };
            Mock<ILineService> mockedLineService = new Mock<ILineService>();
            mockedLineService.Setup(x => x.GetCanvasSize()).Returns(size).Verifiable();
            CanvasViewModel target = new CanvasViewModel(mockedLineService.Object);

            //Act
            MouseEventArgs e = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            target.SelectPointCommand.Execute(e);

            //Verify
            Assert.AreEqual(0, target.StartPoint.Value.X);
            Assert.AreEqual(0, target.StartPoint.Value.Y);
            Assert.IsNull(target.EndPoint);
            mockedLineService.VerifyAll();
        }

        [TestMethod]
        public void WhenConsecutiveCommandExecuted_StartAndEndPointSet()
        {
            //Prepare
            Size size = new Size { Height = 500, Width = 500 };
            Point[] line = new Point[] { new Point {X=1, Y=2}, new Point {X=3, Y=4}};
            LineQueryResult queryResult = new LineQueryResult { Result = line, Success = true };

            Mock<ILineService> mockedLineService = new Mock<ILineService>();
            mockedLineService.Setup(x => x.GetCanvasSize()).Returns(size).Verifiable();
            mockedLineService.Setup(x => x.AddLine(It.Is<Point>(y => y.X == 0 && y.Y==0),
                It.Is<Point>(y => y.X == 0 && y.Y == 0))).Returns(queryResult).Verifiable();

            CanvasViewModel target = new CanvasViewModel(mockedLineService.Object);

            //Act
            MouseEventArgs e = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            target.SelectPointCommand.Execute(e);
            target.SelectPointCommand.Execute(e);

            //Verify
            Assert.IsNull(target.StartPoint);
            Assert.IsNull(target.EndPoint);
            Assert.AreEqual(1, target.Lines.Count);
            Assert.AreEqual(1, target.Lines[0][0].X);
            Assert.AreEqual(2, target.Lines[0][0].Y);
            Assert.AreEqual(3, target.Lines[0][1].X);
            Assert.AreEqual(4, target.Lines[0][1].Y);
            mockedLineService.VerifyAll();
        }
    }
}
