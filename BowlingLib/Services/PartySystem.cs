using AccountabilityLib.Interfaces;
using AccountabilityLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BowlingLib.Services
{
    public class PartySystem
    {
        private readonly IPartyRepository _partyRepository;
        private readonly string _accountabilityOrigin;

        public PartySystem(IPartyRepository partyRepository, string accountabilityOrigin)
        {
            _partyRepository = partyRepository;
            _accountabilityOrigin = accountabilityOrigin;
        }

        public async Task<Party> RegisterNewPlayer(string name, string legalId)
        {
            Party newPlayer = await _partyRepository.CreateParty(name, legalId);
            Party origin = await _partyRepository.GetParty(_accountabilityOrigin);
            AccountabilityType type = await _partyRepository.GetAccountabilityType("Customer");
            if (type != null && origin != null)
            {
                await _partyRepository.CreateAccountability(newPlayer, origin, type);
                return newPlayer;
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
