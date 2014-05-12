using NUnit.Framework;
using OpenTK;

namespace BirdSimulatorTests
{
    [TestFixture]
    public class UtilitiesTests
    {
        [Test]
        public void ShouldProperlyCalculateXRotation()
        {
            var vector = new Vector3(0, 0, 1);
            var rotations = Utils.VectorToRotation(vector);
            Assert.AreEqual(rotations.X, 0, 5);
            Assert.AreEqual(rotations.Y, 0, 5);
            Assert.AreEqual(rotations.Z, 0, 5);
        }

        [Test]
        public void ShouldProperlyCalculateYRotation()
        {
            var vector = new Vector3(1, 0, 0);
            var rotations = Utils.VectorToRotation(vector);
            Assert.AreEqual(rotations.X, 0, 5);
            Assert.AreEqual(rotations.Y, 0, 5);
            Assert.AreEqual(rotations.Z, 0, 5);
        }

        [Test]
        public void ShouldProperlyCalculateZRotation()
        {
            var vector = new Vector3(1, 0, 0);
            var rotations = Utils.VectorToRotation(vector);
            Assert.AreEqual(rotations.X, 0, 5);
            Assert.AreEqual(rotations.Y, 0, 5);
            Assert.AreEqual(rotations.Z, 0, 5);
        }
    }
}
