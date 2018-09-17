using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ENIDABackendAPI.Service;
using ENIDABackendAPI.Repository;
using ENIDABackendAPI.Model;

namespace ENIDAServiceTest.Service
{
    [TestFixture]
    class InformationServiceTest
    {
        private Mock<InformationRepository> informationRepositoryMock;
        private InformationService serviceUnderTest;

        [SetUp]
        public void SetUp()
        {
            informationRepositoryMock = new Mock<InformationRepository>();
            serviceUnderTest = new InformationService(informationRepositoryMock.Object);
        }

        [Test]
        public void NoResults()
        {
            informationRepositoryMock.Setup(b => b.GetInformationByImageIdOrderedByOffset(""))
                .Returns(new List<Information>().AsQueryable());

            var result = serviceUnderTest.GetInformationForOffset("", 0, 1);

            Assert.IsEmpty(result);
        }

        [Test]
        public void LessThanLimitedResultsAvailable()
        {
            var information = CreateInformation("", 0);

            informationRepositoryMock.Setup(a => a.GetInformationByImageIdOrderedByOffset(""))
                .Returns(new List<Information>
                {
                    information
                }.AsQueryable()
            );

            var result = serviceUnderTest.GetInformationForOffset("", 0, 1);
            Assert.AreEqual(information, result.First());
        }

        private Information CreateInformation(string imageId, int yoffset)
        {
            return new Information
            {
                Content = "",
                Image = new Image
                {
                    Id = imageId
                },
                Type = InformationType.Text,
                YOffset = yoffset
            };
        }

        [Test]
        public void TheFirst5ItemsReturnedWhenSearchFromStart()
        {
            var infomration = new List<Information>
            {
                CreateInformation("", 0),
                CreateInformation("", 1),
                CreateInformation("", 2),
                CreateInformation("", 3),
                CreateInformation("", 4),
                CreateInformation("", 5)
            }.AsQueryable();

            informationRepositoryMock.Setup(a => a.GetInformationByImageIdOrderedByOffset(""))
                .Returns(infomration);

            var results = serviceUnderTest.GetInformationForOffset("", 0, 5);

            Assert.AreEqual(
                infomration.Take(5)
                , results);
        }

        [Test]
        public void Get5ElementsFromTheMiddleBasedOnYOffset()
        {
            var information = new List<Information>
            {
                CreateInformation("", 0),
                CreateInformation("", 1),
                CreateInformation("", 2),
                CreateInformation("", 3),
                CreateInformation("", 4),
                CreateInformation("", 5),
                CreateInformation("", 6),
                CreateInformation("", 7),
                CreateInformation("", 8),
                CreateInformation("", 9),
                CreateInformation("", 10),
            };

            informationRepositoryMock.Setup(a => a.GetInformationByImageIdOrderedByOffset(""))
                .Returns(information.AsQueryable());

            var result = serviceUnderTest.GetInformationForOffset("", 5, 5);

            Assert.AreEqual(information.GetRange(3,5), result);
           
        }

        [Test]
        public void SearchForRangeWhenNearStartOfSource()
        {
            var information = new List<Information>
            {
                CreateInformation("", 0),
                CreateInformation("", 1),
                CreateInformation("", 2),
                CreateInformation("", 3),
                CreateInformation("", 4),
                CreateInformation("", 5),
                CreateInformation("", 6),
                CreateInformation("", 7),
                CreateInformation("", 8),
                CreateInformation("", 9),
                CreateInformation("", 10),
            }.AsQueryable();

            informationRepositoryMock.Setup(a => a.GetInformationByImageIdOrderedByOffset(""))
                .Returns(information);

            var result = serviceUnderTest.GetInformationForOffset("", 1, 5);

            Assert.AreEqual(information.Take(5), result);
        }

        [Test]
        public void SearchForRangeNearEnd()
        {
            var information = new List<Information>
            {
                CreateInformation("", 0),
                CreateInformation("", 1),
                CreateInformation("", 2),
                CreateInformation("", 3),
                CreateInformation("", 4),
                CreateInformation("", 5),
                CreateInformation("", 6),
                CreateInformation("", 7),
                CreateInformation("", 8),
                CreateInformation("", 9),
                CreateInformation("", 10),
            };

            informationRepositoryMock.Setup(a => a.GetInformationByImageIdOrderedByOffset(""))
                .Returns(information.AsQueryable());

            var result = serviceUnderTest.GetInformationForOffset("", 9, 5);

            Assert.AreEqual(information.GetRange(7, 4), result);
        }

        [Test]
        public void NoImageMatchingIdGenerateEmptyResult()
        {
            var information = new List<Information>
            {
                CreateInformation("1", 0),
                CreateInformation("1", 1),
                CreateInformation("1", 2),
                CreateInformation("1", 3),
            }.AsQueryable();

            informationRepositoryMock.Setup(a => a.GetInformationByImageIdOrderedByOffset("1"))
                .Returns(information);

            var result = serviceUnderTest.GetInformationForOffset("3", 0, 2);

            Assert.AreEqual(new List<Information>(), result);
        }
    }
}
