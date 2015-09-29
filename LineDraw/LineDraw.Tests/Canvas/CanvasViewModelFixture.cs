using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LineDraw.Interfaces;
using LineDraw.Models;
using LineDraw.Canvas;
using Moq;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

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
            Assert.IsNull(target.ErrorMessage);
            Assert.IsNull(target.TimeMessage);
            Assert.IsInstanceOfType(target.SelectPointCommand, typeof(ICommand));
            Assert.IsInstanceOfType(target.Lines, typeof(ObservableCollection<Point[]>));
            Assert.AreEqual(PathAlgorithm.BFS, target.PathAlgorithm);
            mockedLineService.VerifyAll();
        }
                
        [TestMethod]
        public void WhenPropertyChanged_PropertyIsUpdated()
        {
            //Prepare
            Size size = new Size { Height = 500, Width = 500 };
            Mock<ILineService> mockedLineService = new Mock<ILineService>();
            mockedLineService.Setup(x => x.GetCanvasSize()).Returns(size).Verifiable();

            CanvasViewModel target = new CanvasViewModel(mockedLineService.Object);

            bool startPointChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "StartPoint")
                {
                    startPointChangedRaised = true;
                }
            };

            bool endPointChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "EndPoint")
                {
                    endPointChangedRaised = true;
                }
            };

            bool pathAlgorithmChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "PathAlgorithm")
                {
                    pathAlgorithmChangedRaised = true;
                }
            };

            bool errorMessageChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "ErrorMessage")
                {
                    errorMessageChangedRaised = true;
                }
            };

            bool timeMessageChangedRaised = false;
            target.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "TimeMessage")
                {
                    timeMessageChangedRaised = true;
                }
            };

            //Act
            target.StartPoint = new Point();
            target.EndPoint = new Point();
            target.PathAlgorithm = PathAlgorithm.AStar;
            target.ErrorMessage = "";
            target.TimeMessage = "";

            //Verify
            Assert.IsTrue(startPointChangedRaised);
            Assert.IsTrue(endPointChangedRaised);
            Assert.IsTrue(pathAlgorithmChangedRaised);
            Assert.IsTrue(errorMessageChangedRaised);
            Assert.IsTrue(timeMessageChangedRaised);
        }

        [TestMethod]
        public void WhenSelectPointCommandExecuted_StartPointSet()
        {
            //Prepare
            Size size = new Size { Height = 500, Width = 500 };
            Mock<ILineService> mockedLineService = new Mock<ILineService>();
            mockedLineService.Setup(x => x.GetCanvasSize()).Returns(size).Verifiable();
            mockedLineService.Setup(x => x.SelectPoint(It.Is<Point>(y => y.X == 0 && y.Y == 0))).
                Returns((Point y) => new PointQueryResult { Result = y, Success = true }).Verifiable();

            CanvasViewModel target = new CanvasViewModel(mockedLineService.Object);

            //Act
            MouseEventArgs e = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left);
            target.SelectPointCommand.Execute(e);

            //Verify
            Assert.AreEqual(0, target.StartPoint.X);
            Assert.AreEqual(0, target.StartPoint.Y);
            Assert.IsNull(target.EndPoint);
            mockedLineService.VerifyAll();
        }

        [TestMethod]
        public void WhenConsecutiveCommandExecuted_StartAndEndPointSet()
        {
            //Prepare
            Size size = new Size { Height = 500, Width = 500 };
            Point[] line = new Point[] { new Point { X = 1, Y = 2 }, new Point { X = 3, Y = 4 } };
            LineQueryResult lineQueryResult = new LineQueryResult { Result = line, Success = true };

            Mock<ILineService> mockedLineService = new Mock<ILineService>();
            mockedLineService.Setup(x => x.GetCanvasSize()).Returns(size).Verifiable();
            mockedLineService.Setup(x => x.AddLineAsync(
                It.Is<Point>(y => y.X == 0 && y.Y == 0),
                It.Is<Point>(y => y.X == 0 && y.Y == 0), 
                It.IsAny<PathAlgorithm>(), 
                It.IsAny<CancellationToken>())).ReturnsAsync(lineQueryResult).Verifiable();

            mockedLineService.Setup(x => x.SelectPoint(It.Is<Point>(y => y.X == 0 && y.Y == 0))).
                Returns((Point y) => new PointQueryResult { Result = y, Success = true }).Verifiable();

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

        [TestMethod]
        public void WhenClearLinesCommandExecuted_LinesCleared()
        {
            //Prepare
            Size size = new Size { Height = 500, Width = 500 };
            Mock<ILineService> mockedLineService = new Mock<ILineService>();
            mockedLineService.Setup(x => x.GetCanvasSize()).Returns(size).Verifiable();
            mockedLineService.Setup(x => x.ClearLines()).Verifiable();

            CanvasViewModel target = new CanvasViewModel(mockedLineService.Object);
            target.StartPoint = new Point();
            target.EndPoint = new Point();
            target.Lines.Add(new Point[] { });
            int before = target.Lines.Count;

            //Act
            target.ClearLinesCommand.Execute(null);

            //Verify
            Assert.IsTrue(0 < before);
            Assert.AreEqual(0, target.Lines.Count);
            Assert.IsNull(target.StartPoint);
            Assert.IsNull(target.EndPoint);
            mockedLineService.VerifyAll();
        }
    }
}
