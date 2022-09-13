using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DEMAT.ApplicationServices.Helper;
using DEMAT.ApplicationServices.UseCases.DigitizationAgent.Requests;
using DEMAT.Domain.Entities;
using DEMAT.Domain.Entities.Documents;
using DEMAT.Domain.Interfaces;
using DEMAT.Infrastructure;
using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DEMAT.ApplicationServices.UseCases.DigitizationAgent.Handlers.Commands
{
    public class UploadRawDocumentCommandHandler : IRequestHandler<UploadRawDocumentsCommand, DocumentPicture>
    {
        private readonly IAmenUnitOfWork _amenUnitOfWork;
        private readonly IDematContext _amenBankContext;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public UploadRawDocumentCommandHandler(IAmenUnitOfWork amenUnitOfWork, IDematContext amenBankContext , IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _amenUnitOfWork = amenUnitOfWork;
            _amenBankContext = amenBankContext;
            _cloudinaryConfig = cloudinaryConfig;

            Account account = new Account
            (
               
                "gatewayapp",
                "268949899546168",
                "dQ4Dh7jgmsoMsp7zy4wFpqvM-uA"
            );

            _cloudinary = new Cloudinary(account);
        }

        public async Task<DocumentPicture> Handle(UploadRawDocumentsCommand request, CancellationToken cancellationToken)
        {
            string json = JsonConvert.SerializeObject(request.File, Formatting.Indented);
            Console.WriteLine($"From Service {json}");
            Console.WriteLine($"From Service {request.AgenceCode}");
            Console.WriteLine($"From Service {request.AccountingDay}");

            var file = request.File;

            if(file.ContentType == "application/pdf")
            {
                return new DocumentPicture();
            }


            // We will store here the result that we will get from cloudinary 
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0 )
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            //Creating the lot object 
            var lot = new Lot
            {
                Name = request.AgenceCode + request.AccountingDay
            };

            //Creating the document picture object 
            var documentPicture = new DocumentPicture
            {
               Url = uploadResult.Url.ToString(),
               PublicId = uploadResult.PublicId
            };

            //Creating the raw document object 
            var rawDocument = new RawDocument
            {
                State = StateTypes.ValidatedByOCR,
                DocumentType = DocumentType.NotRecognized,
                DocumentPicture = documentPicture,
                Lot = lot
            };

            // Saving to db
            await _amenUnitOfWork.RawDocumentRepository.AddAsync(rawDocument, cancellationToken);
            await _amenUnitOfWork.SaveAsync(cancellationToken);

            return documentPicture;
        }
    }
}
