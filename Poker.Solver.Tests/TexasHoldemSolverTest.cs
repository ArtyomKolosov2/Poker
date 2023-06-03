using NUnit.Framework;
 
namespace Evolution.Poker.Solver.Tests
{
    [TestFixture]
    public class TexasHoldemSolverTest
    {
        [TestCase]
        public void TestTh5c6dAcAsQs()
        {
            Assert.AreEqual("2cJc Kh4h=Ks4c Kc7h KdJs 6h7d 2hAh", Solver.Process("texas-holdem 5c6dAcAsQs Ks4c KdJs 2hAh Kh4h Kc7h 6h7d 2cJc"));
        }

        [TestCase]
        public void TestTh2h5c8sAsKc()
        {
            Assert.AreEqual("Jc6s Qs9h 3cKh KdQh", Solver.Process("texas-holdem 2h5c8sAsKc Qs9h KdQh 3cKh Jc6s"));
        }
        
        [TestCase]
        public void TestTh3d4s5dJsQd()
        {
            Assert.AreEqual("9h7h 2dTc KcAs 7sJd TsJc Qh8c 5c4h", Solver.Process("texas-holdem 3d4s5dJsQd 5c4h 7sJd KcAs 9h7h 2dTc Qh8c TsJc"));
        }

    }
}
