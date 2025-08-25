using YGOSharp.OCGWrapper.Enums;
using System.Collections.Generic;
using System.Linq;
using WindBot;
using WindBot.Game;
using WindBot.Game.AI;
using System;

namespace WindBot.Game.AI.Decks
{
   [Deck("Mikanko", "AI_Mikanko")]
   class MikankoExecutor : DefaultExecutor
   {
        public class CardId
        {
          //main code
          //陨石
          public const int NibiruThePrimalBeing = 27204311;
          //灰流丽
          public const int AshBlossom = 14558127;
          public const int NaturalBeast = 33198837;
          public const int NaturalExterio = 99916754;
          public const int ImperialOrder = 61740673;
          public const int SwordsmanLV7 = 37267041;
          public const int RoyalDecree = 51452091;
          public const int Number41BagooskatheTerriblyTiredTapir = 90590303;
          //增殖的g
          public const int MaxxC = 23434538;
          //撒旦老人
          public const int OldMan = 46565218;
          public const int AlbionTheShroudedDragon = 25451383;
          public const int SkillDrain = 82732705;

          //main code
          public const int HuaishouQiailu = 63941210;
          public const int Huaishoulongwang = 48770333;
          public const int Rongyanmoshen = 102380;
          
          public const int MiGreen = 6327734;
          public const int MiEarth = 11161666;
          public const int MiRed = 18377261;
          public const int Yuwudarinv = 81260679;
          public const int Yuwuerguizi = 84550369;
          public const int Yishizhunbei = 96729612;
          public const int Yuwuchuancheng = 44649322;
          public const int Hanyinkaijia = 33114323;
          public const int Tianzizhilun = 40678060;
          public const int Yuwushuiwudao = 43527730;
          public const int MikankoBird = 57736667;
          public const int Yuwuroulunwu = 79912449;
          public const int Yuwuhuocongwu = 80044027;
          public const int Yuwushenyin = 53174748;
          public const int Yuwushenjiao = 78199891;

          //extra code
          public const int Yuwuguirinv = 57566760;

        }
        
        private int RockCount = 0;
        const int hintTimingMainEnd = 0x4;
        const int SetcodeTimeLord = 0x4a;
        List<ClientCard> currentNegateCardList = new List<ClientCard>();
        List<int> notToNegateIdList = new List<int>{
            58699500, 20343502, CardId.AlbionTheShroudedDragon, 19403423
        };

        public MikankoExecutor(GameAI ai, Duel duel): base(ai, duel)
        {
            Console.WriteLine("Hello jinyou!!!!");
            //counter
            AddExecutor(ExecutorType.Activate, CardId.AshBlossom, AshBlossomActivate);
            AddExecutor(ExecutorType.Activate, CardId.MaxxC, MaxxCActivate);
            AddExecutor(ExecutorType.Activate, CardId.NibiruThePrimalBeing, NibiruThePrimalBeingActivate);
            AddExecutor(ExecutorType.Activate, CardId.MikankoBird, MikankoBirdEffect);

            AddExecutor(ExecutorType.Activate, CardId.NaturalBeast , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.NaturalExterio , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.ImperialOrder , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.SwordsmanLV7 , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.RoyalDecree , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.OldMan , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.AlbionTheShroudedDragon , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.SkillDrain , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.HuaishouQiailu , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Huaishoulongwang , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Rongyanmoshen , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.MiGreen , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.MiEarth , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.MiRed , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwudarinv , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwuerguizi , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yishizhunbei , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwuchuancheng , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Hanyinkaijia , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Tianzizhilun , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwushuiwudao , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwuroulunwu , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwuhuocongwu , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwushenyin , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwushenjiao , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Yuwuguirinv , DefaultField);
            AddExecutor(ExecutorType.Activate, CardId.Number41BagooskatheTerriblyTiredTapir , DefaultField);


            AddExecutor(ExecutorType.SpSummon);
            AddExecutor(ExecutorType.SpSummon, CardId.AshBlossom, AshBlossomSpSummon);

            AddExecutor(ExecutorType.Summon);
            AddExecutor(ExecutorType.Summon, CardId.AshBlossom, AshBlossomSummon);
            AddExecutor(ExecutorType.Summon, CardId.OldMan, OldManSummon);
            AddExecutor(ExecutorType.Summon, CardId.NibiruThePrimalBeing, NibiruThePrimalBeingSummon);

        }

        private bool NibiruThePrimalBeingSummon()
        {
            return false;
        }

        private bool MikankoBirdEffect()
        {
            if (Duel.Phase == DuelPhase.BattleStart)
            {
                if (Enemy.GetMonsterCount() > 0)
                {
                    AI.SelectCard(Enemy.GetMonsters());
                    return true;
                }

                ClientCard target = null;
                foreach (ClientCard card in Enemy.GetSpells())
                {
                    if (card != null)
                    {
                        target = card;
                    }
                }

                if (target != null)
                {
                    AI.SelectCard(target);
                    return true;
                }
            }
            else{
                if(Bot.GetMonsters().Count == 0){
                    return DefaultField();;
                }
            }
            return DefaultField();
        }

        public bool MaxxCActivate()
        {
            Console.WriteLine("增殖 =");
            Console.WriteLine(Duel.LastChainPlayer);
            if (CheckWhetherNegated(true) || Duel.LastChainPlayer == -1) 
            {
                return false;
            }else{
                return DefaultMaxxC();
            }
        }

        /// <summary>
        /// Check whether'll be negated
        /// </summary>
        /// <param name="isCounter">check whether card itself is disabled.</param>
        public bool CheckWhetherNegated(bool disablecheck = true, bool toFieldCheck = false, CardType type = 0)
        {
            bool isMonster = type == 0 && Card.IsMonster();
            isMonster |= ((int)type & (int)CardType.Monster) != 0;
            bool isSpellOrTrap = type == 0 && (Card.IsSpell() || Card.IsTrap());
            isSpellOrTrap |= (((int)type & (int)CardType.Spell) != 0) || (((int)type & (int)CardType.Trap) != 0);
            bool isCounter = ((int)type & (int)CardType.Counter) != 0;
            if (isSpellOrTrap && toFieldCheck && CheckSpellWillBeNegate(isCounter))
                return true;
            if (DefaultCheckWhetherCardIsNegated(Card)) return true;
            if (isMonster && (toFieldCheck || Card.Location == CardLocation.MonsterZone))
            {
                if ((toFieldCheck && (((int)type & (int)CardType.Link) != 0)) || Card.IsDefense())
                {
                    if (Enemy.MonsterZone.Any(card => CheckNumber41(card)) || Bot.MonsterZone.Any(card => CheckNumber41(card))) return true;
                }
                if (Enemy.HasInSpellZone(CardId.SkillDrain, true, true)) return true;
            }
            if (disablecheck) return (Card.Location == CardLocation.MonsterZone || Card.Location == CardLocation.SpellZone) && Card.IsDisabled() && Card.IsFaceup();
            return false;
        }

        /// <summary>
        /// Check whether last chain card should be disabled.
        /// </summary>
        public bool CheckLastChainShouldNegated()
        {
            ClientCard lastcard = Util.GetLastChainCard();
            if (lastcard == null || lastcard.Controller != 1) return false;
            if (lastcard.IsMonster() && lastcard.HasSetcode(SetcodeTimeLord) && Duel.Phase == DuelPhase.Standby) return false;
            if (notToNegateIdList.Contains(lastcard.Id)) return false;
            if (DefaultCheckWhetherCardIsNegated(lastcard)) return false;
            if (Duel.Turn == 1 && lastcard.IsCode(_CardId.MaxxC)) return false;

            return true;
        }

        public bool AshBlossomActivate()
        {
            if (CheckWhetherNegated() || !CheckLastChainShouldNegated()) return false;

            if (Util.GetLastChainCard().IsCode(_CardId.MaxxC)) return false;

            if (Duel.LastChainPlayer == -1) 
            {
                return false;
            }

            if (DefaultAshBlossomAndJoyousSpring())
            {
                ClientCard lastChainCard = Util.GetLastChainCard();
                if (lastChainCard.Location == CardLocation.MonsterZone || lastChainCard.Location == CardLocation.SpellZone) currentNegateCardList.Add(Util.GetLastChainCard());
                return true;
            }
            return false;
        }

        private bool OldManSummon()
        {
            return false;
        }

        private bool AshBlossomSummon()
        {
            return false;
        }

        private bool AshBlossomSpSummon()
        {
            return false;
        }

        /// <summary>
        /// Whether spell or trap will be negate. If so, return true.
        /// </summary>
        /// <param name="isCounter">is counter trap</param>
        /// <param name="target">check target</param>
        /// <returns></returns>
        public bool CheckSpellWillBeNegate(bool isCounter = false, ClientCard target = null)
        {
            // target default set
            if (target == null) target = Card;
            // won't negate if not on field
            if (target.Location != CardLocation.SpellZone && target.Location != CardLocation.Hand) return false;

            // negate judge
            if (Enemy.HasInMonstersZone(CardId.NaturalExterio, true) && !isCounter) return true;
            if (target.IsSpell())
            {
                if (Enemy.HasInMonstersZone(CardId.NaturalBeast, true)) return true;
                if (Enemy.HasInSpellZone(CardId.ImperialOrder, true) || Bot.HasInSpellZone(CardId.ImperialOrder, true)) return true;
                if (Enemy.HasInMonstersZone(CardId.SwordsmanLV7, true) || Bot.HasInMonstersZone(CardId.SwordsmanLV7, true)) return true;
            }
            if (target.IsTrap())
            {
                if (Enemy.HasInSpellZone(CardId.RoyalDecree, true) || Bot.HasInSpellZone(CardId.RoyalDecree, true)) return true;
            }
            // how to get here?
            return false;
        }

        public bool CheckNumber41(ClientCard card)
        {
            return card != null && card.IsFaceup() && card.IsCode(CardId.Number41BagooskatheTerriblyTiredTapir) && card.IsDefense() && !card.IsDisabled();
        }

        public bool NibiruThePrimalBeingActivate()
        {
            if (CheckWhetherNegated()) return false;
            if (Duel.Player == 0 || Bot.GetMonsters().Any(card => card.IsFaceup() && card.HasType(CardType.Synchro)))
            {
                return false;
            }
            
            if (Util.GetBestAttack(Enemy) > Util.GetBestAttack(Bot))
            {
                // end main phase
                if ((CurrentTiming & hintTimingMainEnd) != 0)
                {
                    SelectNibiruPosition();
                    return true;
                }
                
                // avoid Baronne de Fleur
                List<ClientCard> tunerList = Enemy.GetMonsters().Where(card => card.IsFaceup() && card.IsTuner() && !card.HasType(CardType.Xyz | CardType.Link)).ToList();
                List<ClientCard> nonTunerList = Enemy.GetMonsters().Where(card => card.IsFaceup() && !card.IsTuner() && !card.HasType(CardType.Xyz | CardType.Link)).ToList();
                foreach (ClientCard tuner in tunerList)
                {
                    foreach (ClientCard nonTuner in nonTunerList)
                    {
                        if (tuner.Level + nonTuner.Level == 10)
                        {
                            SelectNibiruPosition();
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void SelectNibiruPosition()
        {
            int totalAttack = Bot.GetMonsters().Where(card => card.IsFaceup()).Sum(m => (int?)m.Attack) ?? 0;
            totalAttack += Enemy.GetMonsters().Where(card => card.IsFaceup()).Sum(m => (int?)m.Attack) ?? 0;
            Logger.DebugWriteLine("Nibiru token attack: " + totalAttack.ToString());
            if (totalAttack >= 3000)
            {
                AI.SelectPosition(CardPosition.FaceUpDefence);
                AI.SelectPosition(CardPosition.FaceUpDefence);
            } else {
                AI.SelectPosition(CardPosition.FaceUpAttack);
                AI.SelectPosition(CardPosition.FaceUpAttack);
            }
        }

        /// <summary>
        /// Check whether'll be negated
        /// </summary>
        /// <param name="isCounter">check whether card itself is disabled.</param>
        public bool CheckWhetherNegated()
        {
            if ((Card.IsSpell() || Card.IsTrap()) && CheckSpellWillBeNegate()){
                return true;
            }
            if (DefaultCheckWhetherCardIsNegated(Card)) {
                return true;
            }
            if (Card.IsMonster() && Card.Location == CardLocation.MonsterZone && Card.IsDefense())
            {
                if (Enemy.MonsterZone.Any(card => CheckNumber41(card)) || Bot.MonsterZone.Any(card => CheckNumber41(card)))
                {
                    return true;
                }
            }
            return false;
        }

        public override CardPosition OnSelectPosition(int cardId, IList<CardPosition> positions)
        {
            return CardPosition.FaceUpAttack;
        }


        public override BattlePhaseAction OnSelectAttackTarget(ClientCard attacker, IList<ClientCard> defenders)
        {
            for (int i = 0; i < defenders.Count; ++i)
            {
                ClientCard defender = defenders[i];
                attacker.RealPower = attacker.Attack;
                defender.RealPower = defender.GetDefensePower();
                if (!attacker.IsDisabled())
                    return AI.Attack(attacker, defender);
            }

            if (attacker.CanDirectAttack && (Enemy.GetMonsterCount() == 0 || !attacker.IsDisabled()))
                return AI.Attack(attacker, null);

            return null;
        }

        public override int OnRockPaperScissors()
        {
            RockCount++;
            if (RockCount <= 3)
                return 2;
            else
                return base.OnRockPaperScissors();
        }


        private int CheckRemainInDeck(int id)
        {
            switch (id)
            {
                case CardId.NibiruThePrimalBeing:
                    return Bot.GetRemainingCount(CardId.NibiruThePrimalBeing, 1);
                case CardId.HuaishouQiailu:
                    return Bot.GetRemainingCount(CardId.HuaishouQiailu, 1);
                case CardId.Huaishoulongwang:
                    return Bot.GetRemainingCount(CardId.Huaishoulongwang, 1);
                case CardId.Rongyanmoshen:
                    return Bot.GetRemainingCount(CardId.Rongyanmoshen, 1);
                case CardId.OldMan:
                    return Bot.GetRemainingCount(CardId.OldMan, 1);
                case CardId.AshBlossom:
                    return Bot.GetRemainingCount(CardId.AshBlossom, 3);
                case CardId.MiGreen:
                    return Bot.GetRemainingCount(CardId.MiGreen, 2);
                case CardId.MiEarth:
                    return Bot.GetRemainingCount(CardId.MiEarth, 2);
                case CardId.MiRed:
                    return Bot.GetRemainingCount(CardId.MiRed, 2);
                case CardId.MaxxC:
                    return Bot.GetRemainingCount(CardId.MaxxC, 3);
                case CardId.Yuwudarinv:
                    return Bot.GetRemainingCount(CardId.Yuwudarinv, 2);
                case CardId.Yuwuerguizi:
                    return Bot.GetRemainingCount(CardId.Yuwuerguizi, 2);
                case CardId.Yishizhunbei:
                    return Bot.GetRemainingCount(CardId.Yishizhunbei, 2);
                case CardId.Yuwuchuancheng:
                    return Bot.GetRemainingCount(CardId.Yuwuchuancheng, 2);
                case CardId.Hanyinkaijia:
                    return Bot.GetRemainingCount(CardId.Hanyinkaijia, 2);
                case CardId.Tianzizhilun:
                    return Bot.GetRemainingCount(CardId.Tianzizhilun, 1);
                case CardId.Yuwushuiwudao:
                    return Bot.GetRemainingCount(CardId.Yuwushuiwudao, 2);
                case CardId.MikankoBird:
                    return Bot.GetRemainingCount(CardId.MikankoBird, 2);
                case CardId.Yuwuroulunwu:
                    return Bot.GetRemainingCount(CardId.Yuwuroulunwu, 1);
                case CardId.Yuwuhuocongwu:
                    return Bot.GetRemainingCount(CardId.Yuwuhuocongwu, 1);
                case CardId.Yuwushenyin:
                    return Bot.GetRemainingCount(CardId.Yuwushenyin, 1);
                case CardId.Yuwushenjiao:
                    return Bot.GetRemainingCount(CardId.Yuwushenjiao, 1);
                default:
                    return 0;
            }
      }
   }
}
