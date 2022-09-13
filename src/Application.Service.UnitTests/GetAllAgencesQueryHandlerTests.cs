using DEMAT.ApplicationServices.UseCases.GetAllAgences;
using DEMAT.Domain.Interfaces;
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
    public class GetAllAgencesQueryHandlerTests
    {
        #region Varriables 
        private GetAllAgencesQueryHandler _queryHandler;
        private Mock<IAgenceReadRepository> mockAgence;
     
        #endregion

        #region Methodes 

        private IEnumerable<AgenceModel> GetAllAgences()
        {
            var List = new List<AgenceModel>() {
                new AgenceModel()
                {
                  Id = new Guid("787e2e08-df51-4b6b-9f1c-bd4adfb65b50"),
                  CodeAgence=100,
                  NomAgence ="Tunis",
                  Adresse="Habib bourguiba ",
                  ZoneAgenceId= new Guid("997e2e08-df51-4b6b-9f1c-bd4adfb65b50")
                }, 
                new AgenceModel()
                { Id = new Guid("786e2e08-df51-4b6b-9f1c-bd4adfb65b50"),
                  CodeAgence=101,
                  NomAgence ="Djerba",
                  Adresse="Djerba Midoun ",
                  ZoneAgenceId= new Guid("967e2e08-df51-4b6b-9f1c-bd4adfb65b50")
                 }
             };
            return List;
        }

        [Fact]
        public async Task Test_Notnull()
        {
            // Arrange
            // _mockReadRepository = new Mock<IFonctionReadRepository>();
            mockAgence = new Mock<IAgenceReadRepository>();
            mockAgence.Setup(r => r.GetAllAgences( It.IsAny<CancellationToken>())).ReturnsAsync(GetAllAgences());
            // Act
            _queryHandler = new GetAllAgencesQueryHandler(mockAgence.Object);
            IEnumerable<AgenceModel> agences = await _queryHandler.Handle(new GetAllAgencesQuery(),new CancellationToken());

            // Assert
            Assert.NotNull(agences);

        }





        #endregion
    }
}
