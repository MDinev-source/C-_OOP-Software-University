﻿namespace PlayersAndMonsters.Core
{

    using Contracts;
    using System.Text;
    using PlayersAndMonsters.Common;
    using PlayersAndMonsters.Repositories;
    using PlayersAndMonsters.Core.Factories;
    using PlayersAndMonsters.Models.BattleFields;
    using PlayersAndMonsters.Repositories.Contracts;
    using PlayersAndMonsters.Core.Factories.Contracts;
    using PlayersAndMonsters.Models.BattleFields.Contracts;

    public class ManagerController : IManagerController
    {
        private IPlayerRepository playerRepository;
        private IPlayerFactory playerFactory;
        private ICardRepository cardRepository;
        private ICardFactory cardFactory;
        private IBattleField battleField;

        public ManagerController()
        {
            this.playerRepository = new PlayerRepository();
            this.playerFactory = new PlayerFactory();
            this.cardRepository = new CardRepository();
            this.cardFactory = new CardFactory();
            this.battleField = new BattleField();
        }

        public string AddPlayer(string type, string username)
        {
            var player = this.playerFactory.CreatePlayer(type, username);

            this.playerRepository.Add(player);

            return string.Format(
                ConstantMessages.SuccessfullyAddedPlayer,
                type,
                username);
        }

        public string AddCard(string type, string name)
        {
            var card = this.cardFactory.CreateCard(type, name);

            this.cardRepository.Add(card);

            return string.Format(
                ConstantMessages.SuccessfullyAddedCard,
                type,
                name);
        }

        public string AddPlayerCard(string username, string cardName)
        {
            var user = this.playerRepository.Find(username);
            var card = this.cardRepository.Find(cardName);

            user.CardRepository.Add(card);

            return string.Format(
                ConstantMessages.SuccessfullyAddedPlayerWithCards,
                cardName,
                username);
        }

        public string Fight(string attackUser, string enemyUser)
        {
            var attacker = this.playerRepository.Find(attackUser);
            var enemy = this.playerRepository.Find(enemyUser);

            this.battleField.Fight(attacker, enemy);

            return string.Format(
                ConstantMessages.FightInfo,
                attacker.Health,
                enemy.Health);
        }

        public string Report()
        {
            var sb = new StringBuilder();

            foreach (var player in this.playerRepository.Players)
            {
                sb.AppendLine(
                    string.Format(
                        ConstantMessages.PlayerReportInfo,
                        player.Username,
                        player.Health,
                        player.CardRepository.Count));

                foreach (var card in player.CardRepository.Cards)
                {
                    sb.AppendLine(
                        string.Format(
                            ConstantMessages.CardReportInfo,
                            card.Name,
                            card.DamagePoints));
                }

                sb.AppendLine(ConstantMessages.DefaultReportSeparator);
            }

            return sb.ToString().TrimEnd();
        }
    }
}