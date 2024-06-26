﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace WebApp.Infrastructure
{
    public class FileRepository : IFileRepository
    {
        private readonly BlobContainerClient _container;
        private const string ContainerName = "sample";

        public FileRepository(BlobServiceClient blobServiceClient)
        {
            _container = blobServiceClient.GetBlobContainerClient(ContainerName);
        }

        public async Task UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var blockBlob = _container.GetBlobClient(fileName);
            await blockBlob.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });
        }

        public async Task<byte[]> DownloadAsync(string fileName)
        {
            var blockBlob = _container.GetBlobClient(fileName);
            var response = await blockBlob.DownloadContentAsync();
            return response.Value.Content.ToArray();
        }

        public async Task<IList<string>> GetAsync()
        {
            var result = new List<string>();
            await foreach (var blobItem in _container.GetBlobsAsync())
            {
                result.Add(blobItem.Name);
            }
            return result;
        }
    }
}
