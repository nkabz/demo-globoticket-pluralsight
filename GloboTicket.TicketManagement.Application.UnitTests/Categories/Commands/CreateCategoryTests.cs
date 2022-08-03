using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Application.Features.Categories.Commands.CreateCategory;
using GloboTicket.TicketManagement.Application.Profiles;
using GloboTicket.TicketManagement.Application.UnitTest.Mocks;
using GloboTicket.TicketManagement.Domain.Entities;
using Moq;
using Shouldly;
using Xunit;

namespace GloboTicket.TicketManagement.Application.UnitTest.Categories.Commands
{
    public class CreateCategoryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IAsyncRepository<Category>> _categoryRepository;

        public CreateCategoryTests()
        {
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            
            _mapper = configurationProvider.CreateMapper();
            
            _categoryRepository = RepositoryMocks.GetCategoryRepository();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreateCategoryCommandHandler(_categoryRepository.Object, _mapper);
        
            await handler.Handle(new CreateCategoryCommand {Name = "Test"}, CancellationToken.None);
        
            var allCategories = await _categoryRepository.Object.ListAllAsync();
            
            allCategories.Count.ShouldBe(5);
        }
    }
}