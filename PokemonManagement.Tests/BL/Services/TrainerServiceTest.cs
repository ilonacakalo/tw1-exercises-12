using Moq;
using PokemonManagement.BL.Exceptions;
using PokemonManagement.BL.Services;
using PokemonManagement.DAL.Models;
using PokemonManagement.DAL.Repositories;

namespace PokemonManagement.Tests.BL.Services
{
    public class TrainerServiceTest
    {
        [Fact]
        public void Train_ShouldThrowIfMaxLevelPkmn()
        {
            // Arrange
            var pokemonTrainerId = -1;
            var maxLevelOwnedPokemon = new TrainerPokemon
            {
                PokemonId = -1, TrainerId = -1, Level = 100
            };

            var mockRepository = new Mock<ITrainerPokemonRepository>();
            mockRepository
                .Setup(r => r.GetBy(pokemonTrainerId))
                .Returns(maxLevelOwnedPokemon);
            mockRepository
                .Setup(r => r.Update(maxLevelOwnedPokemon));
            // Act
            TrainerService trainerService = new(mockRepository.Object);
            // Assert
            Assert.Throws<PokemonLogicException>(() =>
            {
                trainerService.Train(pokemonTrainerId);
            });
        }

        //nije sretan
        [Fact]
        public void Evolve_ShouldThrowIfNotValidEvolution()
        {
            var mockRepository = new Mock<ITrainerPokemonRepository>();

            TrainerPokemon notEvolvablePkmn = new() 
            {
                Id = -1, 
                Trainer = new Trainer()
                {
                    Id = -1, Candies = 100
                },
                Pokemon = new Pokemon()
                {
                    Id = -1,
                    Name = "Test",
                    EvolvesToId = null,
                    EvolvesTo = null
                }
            };

            TrainerService service = new(mockRepository.Object);

            Assert.Throws<PokemonLogicException>(
                () => service.Evolve(notEvolvablePkmn)
            );
        }

    }
}
