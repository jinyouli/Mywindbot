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
          public const int 原始生命态尼比鲁 = 27204311;
          public const int 坏星坏兽席兹奇埃鲁 = 63941210;
          public const int 雷击坏兽雷鸣龙王 = 48770333;
          public const int 熔岩魔神 = 102380;
          public const int 撒旦老人 = 46565218;
          public const int 革命同调士 = 97682931;
          public const int 灰流丽 = 14558127;
          public const int 剑之御巫波礼 = 18377261;
          public const int 珠之御巫狐理 = 6327734;
          public const int 御巫奉佐那伎 = 11161666;
          public const int 增殖的G = 23434538;
          public const int 宣告者的神巫 = 92919429;
          public const int 大日女之御巫 = 81260679;
          public const int 大风暴 = 19613556;
          public const int 仪式的准备 = 96729612;
          public const int 御巫神舞二贵子 = 84550369;
          public const int 超融合 = 48130397;
          public const int 传承的大御巫 = 44649322;
          public const int 折断的竹光 = 41587307;
          public const int 罕银铠甲 = 33114323;
          public const int 愚钝之斧 = 19578592;
          public const int 脆刃之剑 = 41927278;
          public const int 天子的指轮 = 40678060;
          public const int 御巫的火丛舞 = 80044027;
          public const int 御巫的水舞蹈 = 43527730;
          public const int 御巫的诱轮舞 = 79912449;
          public const int 御巫舞踊迷惑鸟 = 57736667;
          public const int 御巫神较 = 78199891;
          public const int 御巫神隐 = 53174748;

          //extra code
          public const int 共命之翼迦楼罗 = 11765832;
          public const int 旧神努茨 = 80532587;
          public const int 沼地的泥龙王 = 54757758;
          public const int 鲜花女男爵 = 84815190;
          public const int 动力工具勇气龙 = 63265554;
          public const int 虹光之宣告者 = 79606837;
          public const int 奥特玛雅卓尔金 = 1686814;
          public const int 灾厄之星提丰 = 93039339;
          public const int 交血鬼吸血鬼谢里丹 = 32302078;
          public const int 贵日女之御巫 = 57566760;
          public const int 梦幻崩影凤凰 = 2857636;
          public const int SP小夜 = 29301450;
        }

        private int RockCount = 0;

        public MikankoExecutor(GameAI ai, Duel duel): base(ai, duel)
        {
            Console.WriteLine("Hello jinyou!!!!");

            AddExecutor(ExecutorType.SpSummon);
            AddExecutor(ExecutorType.Summon);
            AddExecutor(ExecutorType.Repos, DefaultMonsterRepos);
            AddExecutor(ExecutorType.Activate, DefaultField);
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
                case CardId.原始生命态尼比鲁:
                    return Bot.GetRemainingCount(CardId.原始生命态尼比鲁, 1);
                case CardId.坏星坏兽席兹奇埃鲁:
                    return Bot.GetRemainingCount(CardId.坏星坏兽席兹奇埃鲁, 1);
                case CardId.雷击坏兽雷鸣龙王:
                    return Bot.GetRemainingCount(CardId.雷击坏兽雷鸣龙王, 1);
                case CardId.熔岩魔神:
                    return Bot.GetRemainingCount(CardId.熔岩魔神, 1);
                case CardId.撒旦老人:
                    return Bot.GetRemainingCount(CardId.撒旦老人, 1);
                case CardId.革命同调士:
                    return Bot.GetRemainingCount(CardId.革命同调士, 1);
                case CardId.灰流丽:
                    return Bot.GetRemainingCount(CardId.灰流丽, 2);
                case CardId.剑之御巫波礼:
                    return Bot.GetRemainingCount(CardId.剑之御巫波礼, 2);
                case CardId.珠之御巫狐理:
                    return Bot.GetRemainingCount(CardId.珠之御巫狐理, 2);
                case CardId.御巫奉佐那伎:
                    return Bot.GetRemainingCount(CardId.御巫奉佐那伎, 2);
                case CardId.增殖的G:
                    return Bot.GetRemainingCount(CardId.增殖的G, 2);
                case CardId.宣告者的神巫:
                    return Bot.GetRemainingCount(CardId.宣告者的神巫, 2);
                case CardId.大日女之御巫:
                    return Bot.GetRemainingCount(CardId.大日女之御巫, 2);
                case CardId.大风暴:
                    return Bot.GetRemainingCount(CardId.大风暴, 1);
                case CardId.仪式的准备:
                    return Bot.GetRemainingCount(CardId.仪式的准备, 2);
                case CardId.御巫神舞二贵子:
                    return Bot.GetRemainingCount(CardId.御巫神舞二贵子, 2);
                case CardId.超融合:
                    return Bot.GetRemainingCount(CardId.超融合, 1);
                case CardId.传承的大御巫:
                    return Bot.GetRemainingCount(CardId.传承的大御巫, 2);
                case CardId.折断的竹光:
                    return Bot.GetRemainingCount(CardId.折断的竹光, 1);
                case CardId.罕银铠甲:
                    return Bot.GetRemainingCount(CardId.罕银铠甲, 1);
                case CardId.愚钝之斧:
                    return Bot.GetRemainingCount(CardId.愚钝之斧, 1);
                case CardId.脆刃之剑:
                    return Bot.GetRemainingCount(CardId.脆刃之剑, 1);
                case CardId.天子的指轮:
                    return Bot.GetRemainingCount(CardId.天子的指轮, 1);
                case CardId.御巫的火丛舞:
                    return Bot.GetRemainingCount(CardId.御巫的火丛舞, 1);
                case CardId.御巫的水舞蹈:
                    return Bot.GetRemainingCount(CardId.御巫的水舞蹈, 2);
                case CardId.御巫的诱轮舞:
                    return Bot.GetRemainingCount(CardId.御巫的诱轮舞, 1);
                case CardId.御巫舞踊迷惑鸟:
                    return Bot.GetRemainingCount(CardId.御巫舞踊迷惑鸟, 1);
                case CardId.御巫神较:
                    return Bot.GetRemainingCount(CardId.御巫神较, 1);
                case CardId.御巫神隐:
                    return Bot.GetRemainingCount(CardId.御巫神隐, 1);
                default:
                    return 0;
            }
      }
   }
}
