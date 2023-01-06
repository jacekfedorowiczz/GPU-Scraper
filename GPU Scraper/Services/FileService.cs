﻿using GPU_Scraper.Entities;
using GPUScraper.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;

namespace GPUScraper.Services
{
    public class FileService : IFileService
    {
        private const string filePath = @"..\GPU Scraper\JSON\GPUs.json";
        private readonly GPUScraperDbContext _dbContext;

        public FileService(GPUScraperDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SerializeToJson()
        {
            if (!File.Exists(filePath))
            {
                var GPUs = _dbContext.GPUs.ToList();
                var serialized = JsonConvert.SerializeObject(GPUs);

                File.WriteAllText(filePath, serialized);
            }
        }
    }
}
