using System;
using System.Collections.Generic;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using Moq;

namespace GloboTicket.TicketManagement.Application.UnitTest.Mocks
{
    public class RepositoryMocks
    {
        public static Mock<IAsyncRepository<Category>> GetCategoryRepository()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    CategoryId = Guid.Parse("{9D61A73A-B2C8-48BB-9A04-685DA73FA393}"),
                    Name = "Concerts"
                },                
                new Category
                {
                    CategoryId = Guid.Parse("{79EC569D-009C-40B5-83D4-EFCC3B9B52FB}"),
                    Name = "Musicals"
                },               
                new Category
                {
                    CategoryId = Guid.Parse("{4A16A266-78B8-49F7-B5FA-2A92B562EEA9}"),
                    Name = "Conferences"
                },                
                new Category
                {
                    CategoryId = Guid.Parse("{BAA828E3-0384-481F-BE79-121E9E043EF8}"),
                    Name = "Plays"
                },
            };
            var mockCategoryRepository = new Mock<IAsyncRepository<Category>>();

            mockCategoryRepository.Setup(repo => repo.ListAllAsync()).ReturnsAsync(categories);

            mockCategoryRepository.Setup(repo => repo.AddAsync(It.IsAny<Category>())).ReturnsAsync(
                (Category category) =>
                {
                    categories.Add(category);
                    return category;
                });

            return mockCategoryRepository;
        }
    }
}