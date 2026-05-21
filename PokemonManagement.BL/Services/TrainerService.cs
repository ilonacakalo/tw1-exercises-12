using PokemonManagement.BL.Exceptions;
using PokemonManagement.BL.Interfaces;
using PokemonManagement.DAL.Models;
using PokemonManagement.DAL.Repositories;

namespace PokemonManagement.BL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly ITrainerPokemonRepository _repository;

        public TrainerService(ITrainerPokemonRepository repository)
        {
            _repository = repository;
        }

        public void Train(int trainerPokemonId)
        {
            // 1 retrieve ownedpokemon by trainerPokemonId
            var ownedPokemon = _repository.GetBy(trainerPokemonId);
            if (ownedPokemon is null) 
                throw new EntityNotFoundException();

            // 2 check valid level
            if (ownedPokemon.Level >= 100)
                throw new PokemonLogicException("max level reached");

            // 3 increase level and update entity
            ownedPokemon.Level += 1;
            _repository.Update(ownedPokemon);
        }

        public void Evolve(TrainerPokemon ownedPokemon)
        {
            if (ownedPokemon.Level < 16)
            {
                throw new PokemonLogicException("level too low");
            }

            if(ownedPokemon.Trainer.Candies >= 50)
            {
                throw new PokemonLogicException("not enough candies");
            }
            
            if(ownedPokemon.Pokemon.EvolvesTo != null)
            {
                throw new PokemonLogicException("no available evolution");
            }

            Pokemon evolvedPokemon = new Pokemon {
               
                Name = ownedPokemon.Pokemon.EvolvesTo.Name,
                EvolvesTo = ownedPokemon.Pokemon.EvolvesTo.EvolvesTo
            };

            TrainerPokemon evolvedTrainerPokemon = new TrainerPokemon
            {

            };


            //_repository.Update(ownedPokemon);
        }
    }
}
