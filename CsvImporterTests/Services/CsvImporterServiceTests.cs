using AutoMapper;
using CsvImporter.Data;
using CsvImporter.Domain;
using CsvImporter.Models;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsvImporter.Services.Tests
{
    [TestFixture()]
    public class CsvImporterServiceTests
    {
        [Test()]
        public async Task ImportAsyncTestAsync()
        {
            var csvContent = "PointOfSale;Product;Date;Stock\r\n121017;17240503103734;2019-08-17;2";
            var mockLogger = new Mock<ILogger<Worker>>();
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(new HttpResponseMessage { 
                    StatusCode = (System.Net.HttpStatusCode)200,
                    Content = new StreamContent(new MemoryStream(Encoding.ASCII.GetBytes(csvContent))) 
                }));
            var mockhttpClient = new HttpClient(mockHttpMessageHandler.Object);
            var mockRepository = new Mock<ICsvImporterRepository>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<List<Sale>>(It.IsAny<List<SaleDto>>())).Returns(new List<Sale>() { new Sale() });

            var csvImporterService = new CsvImporterService(mockLogger.Object, mockhttpClient, mockMapper.Object, mockRepository.Object);


            await csvImporterService.ImportAsync("http://www.url.com", 1);

            mockMapper.Verify(x => x.Map<List<Sale>>(It.IsAny<List<SaleDto>>()));
            mockRepository.Verify(x => x.BulkInsert(It.IsAny<List<Sale>>()));
        }
    }
}