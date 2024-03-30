using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using SmartTradeAdvisor.Data.DbContexts;
using SmartTradeAdvisor.Data.Entities;
using SmartTradeAdvisor.Data.Repositories;

namespace SmartTradeAdvisor.Data.Tests.RepositoriesTests;
public class IndexesRepositoryTests
{
    private IndexesRepository? _indexesRepository;
    private Mock<IndexDbContext>? _mockContext;
    private Mock<DbSet<MarketIndex>>? _mockDbSet;

    [SetUp]
    public void SetUp()
    {
        // Inicjalizacja repozytorium i fałszywej bazy danych
        _mockContext = new Mock<IndexDbContext>();
        _mockDbSet = new Mock<DbSet<MarketIndex>>();
        _mockContext.Setup(x => x.Set<MarketIndex>()).Returns(_mockDbSet.Object);

        _indexesRepository = new IndexesRepository(_mockContext.Object);
    }

    [Test]
    public void GetAll_ShouldReturnAllProducts()
    {
        // Arrange
        var products = new List<MarketIndex>
        {
            new MarketIndex {
                Id = Guid.NewGuid(),
                Name = "Index1",
                Description = "Description1",
                IsSymbol = false,
                MarketIndexValues = [],
            },
            new MarketIndex {
                Id = Guid.NewGuid(),
                Name = "Index2",
                Description = "Description2",
                IsSymbol = false,
                MarketIndexValues = [],
            }
        };
        _mockDbSet?.Setup(x => x.ToList()).Returns(products);

        // Act
        var result = _indexesRepository.GetAll();

        // Assert
        Assert.That(products.Count, Is.EqualTo(result.Count()));
    }

    [Test]
    public void GetById_ExistingId_ShouldReturnProduct()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        var index = new MarketIndex
        {
            Id = id,
            Name = "Index1",
            Description = "Description1",
            IsSymbol = false,
            MarketIndexValues = [],
        };
        _mockDbSet.Setup(x => x.Find(id)).Returns(index);

        // Act
        var result = _indexesRepository.GetById(id);

        // Assert
        Assert.That(result != null);
        Assert.That(index, Is.EqualTo(result));
    }

    [Test]
    public void GetById_NonExistingId_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        _mockDbSet.Setup<MarketIndex>(x => x.Find(99)).Returns((MarketIndex)null);
        var wrongGuid = new Guid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _indexesRepository.GetById(wrongGuid));
    }

    [Test]
    public void Add_ShouldAddProduct()
    {
        // Arrange
        var product = new MarketIndex
        {
            Id = Guid.NewGuid(),
            Name = "Index1",
            Description = "Description1",
            IsSymbol = false,
            MarketIndexValues = [],
        };

        // Act
        _indexesRepository.Add(product);

        // Assert
        _mockDbSet.Verify(x => x.Add(product), Times.Once);
    }

    [Test]
    public void Update_ShouldUpdateProduct()
    {
        // Arrange
        var index = new MarketIndex
        {
            Id = Guid.NewGuid(),
            Name = "Index1",
            Description = "Description1",
            IsSymbol = false,
            MarketIndexValues = [],
        };

        // Act
        _indexesRepository.Update(index);

        // Assert
        _mockDbSet.Verify(x => x.Attach(index), Times.Once);
        _mockContext.Verify(x => x.Entry(index), Times.Once);
    }

    [Test]
    public void Delete_ShouldDeleteProduct()
    {
        // Arrange
        var index = new MarketIndex
        {
            Id = Guid.NewGuid(),
            Name = "Index1",
            Description = "Description1",
            IsSymbol = false,
            MarketIndexValues = [],
        };

        // Act
        _indexesRepository.Delete(index);

        // Assert
        _mockDbSet.Verify(x => x.Remove(index), Times.Once);
    }

    [Test]
    public void Save_ShouldSaveChanges()
    {
        // Act
        _indexesRepository.Save();

        // Assert
        _mockContext.Verify(x => x.SaveChanges(), Times.Once);
    }
}
