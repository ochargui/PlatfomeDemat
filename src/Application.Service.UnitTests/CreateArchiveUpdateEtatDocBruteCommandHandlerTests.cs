using DEMAT.ApplicationServices.UseCases.CreateArchiveUpdateEtatDocBrute;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using DEMAT.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Service.UnitTests
{
 public   class CreateArchiveUpdateEtatDocBruteCommandHandlerTests
    {
        #region Variables
        private CreateArchiveUpdateEtatDocBruteCommandHandler _queryHandler;
        private Mock<IArchiveReadRepository> mockArchive;
        Guid packeId = new Guid("4877fb32-3873-4443-aecb-535980717212");
        
        #endregion

        #region Methods
        private string CreateArchiveUpdateEtatDocBrute()
        {
            return "testUnitaire";

        }

     

        [Fact]
        public async Task Test_Notnull()
        {
            // Arrange
           // _mockReadRepository = new Mock<IFonctionReadRepository>();
            mockArchive = new Mock<IArchiveReadRepository>();
            mockArchive.Setup(r => r.InsertArchiveUpdateEtatPacket(packeId, It.IsAny<CancellationToken>())).ReturnsAsync(CreateArchiveUpdateEtatDocBrute());
            // Act
            _queryHandler = new CreateArchiveUpdateEtatDocBruteCommandHandler(mockArchive.Object);
            string typeMat = await _queryHandler.Handle(new CreateArchiveUpdateEtatDocBruteCommand(packeId), new CancellationToken());

            // Assert
            Assert.NotNull(typeMat);

        }
        #endregion


    }
}
