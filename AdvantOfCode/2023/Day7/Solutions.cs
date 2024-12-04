using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdvantOfCode._2023.Day7
{
    public class Solutions
    {



        private int GetRank1(string hand)
        {
            Dictionary<char, int> card = new();

            foreach (var c in hand)
            {
                if (card.ContainsKey(c))
                {
                    card[c]++;
                }
                else
                {
                    card.Add(c, 1);
                }
            }

            if (card.ContainsKey('J'))
            {
                var k = card.OrderByDescending(x => x.Value).Where(x => x.Key != 'J').FirstOrDefault().Key;

                if (k == '\0')
                    return 6;
                else
                {
                    card[k] += card['J'];
                    card.Remove('J');
                }
            }

            if (card.Values.Any(x => x == 5))
                return 6;
            else if (card.Values.Any(x => x == 4) && card.Count() == 2)
                return 5;
            else if (card.Values.Any(x => x == 3) && card.Values.Any(x => x == 2) && card.Count() == 2)
                return 4;
            else if (card.Values.Any(x => x == 3) && card.Values.Any(x => x == 1) && card.Count() == 3)
                return 3;
            else if (card.Values.Any(x => x == 2) && card.Values.Any(x => x == 1) && card.Count() == 3)
                return 2;
            else if (card.Values.Any(x => x == 2) && card.Values.Any(x => x == 1) && card.Count() == 4)
                return 1;
            else
                return 0;
        }

        private class Game
        {
            public Game(string hand, int bid, int rank)
            {
                Hand = hand;
                Bid = bid;
                Rank = rank;
            }

            public string Hand { get; set; }
            public int Bid { get; set; }
            public int Rank { get; set; }
        }


        public void First()
        {
            List<char> cards = new() { 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

            var inputs = File.ReadAllLines(@".\input7.txt");

            var hands = inputs.Select(x => x.Split(" ").First()).ToList();
            var bids = inputs.Select(x => int.Parse(x.Split(" ").Last())).ToList();

            var games = new List<Game>();

            for (int i = 0; i < hands.Count(); i++)
            {
                var curr = hands[i];
                var rank = GetRank1(hands[i]);

                if (curr == "KK677" || curr == "KTJJT")
                {

                }

                var sameGames = games.Where(x => x.Rank == rank);

                if (sameGames.Count() == 0)
                {
                    var g = games.FirstOrDefault(x => x.Rank > rank);
                    if (g is null)
                    {
                        Game game = new(hands[i], bids[i], rank);
                        games.Add(game);
                    }
                    else
                    {
                        var index = games.IndexOf(g);
                        Game game = new(hands[i], bids[i], rank);
                        games.Insert(index, game);
                    }
                }
                else
                {
                    var flag = false;
                    var index = games.IndexOf(sameGames.First());
                    foreach (var sameg in sameGames)
                    {
                        flag = false;
                        for (int j = 0; j < 5; j++)
                        {
                            var currsam = curr;
                            var sam = sameg.Hand;

                            var curr_x = cards.IndexOf(hands[i][j]);
                            var same_y = cards.IndexOf(sameg.Hand[j]);

                            if (curr_x == same_y)
                                continue;

                            if (curr_x > same_y)
                            {
                                flag = true;
                                break;
                            }
                            if (curr_x < same_y)
                            {
                                break;
                            }
                        }


                        if (flag)
                            break;
                        else
                            index++;
                    }

                    Game game = new(hands[i], bids[i], rank);
                    games.Insert(index, game);
                }

            }

            var total = 0;
            for (int i = 0; i < games.Count(); i++)
            {
                total += (games[i].Bid * (i + 1));
            }

            Console.WriteLine(total);
            Utility.PrintList(games);
        }



        private int GetRank2(string hand)
        {
            Dictionary<char, int> card = new();

            foreach (var c in hand)
            {
                if (card.ContainsKey(c))
                {
                    card[c]++;
                }
                else
                {
                    card.Add(c, 1);
                }
            }

            if (card.ContainsKey('J'))
            {
                var k = card.OrderByDescending(x => x.Value).Where(x => x.Key != 'J').FirstOrDefault().Key;

                if (k == '\0')
                    return 6;
                else
                {
                    card[k] += card['J'];
                    card.Remove('J');
                }
            }

            if (card.Values.Any(x => x == 5))
                return 6;
            else if (card.Values.Any(x => x == 4) && card.Count() == 2)
                return 5;
            else if (card.Values.Any(x => x == 3) && card.Values.Any(x => x == 2) && card.Count() == 2)
                return 4;
            else if (card.Values.Any(x => x == 3) && card.Values.Any(x => x == 1) && card.Count() == 3)
                return 3;
            else if (card.Values.Any(x => x == 2) && card.Values.Any(x => x == 1) && card.Count() == 3)
                return 2;
            else if (card.Values.Any(x => x == 2) && card.Values.Any(x => x == 1) && card.Count() == 4)
                return 1;
            else
                return 0;
        }

        public void Secound()
        {
            List<char> cards = new() { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };


            var inputs = File.ReadAllLines(@".\input7.txt");

            var hands = inputs.Select(x => x.Split(" ").First()).ToList();
            var bids = inputs.Select(x => int.Parse(x.Split(" ").Last())).ToList();

            var games = new List<Game>();

            for (int i = 0; i < hands.Count(); i++)
            {
                var curr = hands[i];
                var rank = GetRank2(hands[i]);



                if (curr == "KK677" || curr == "KTJJT")
                {

                }

                var sameGames = games.Where(x => x.Rank == rank);

                if (sameGames.Count() == 0)
                {
                    var g = games.FirstOrDefault(x => x.Rank > rank);
                    if (g is null)
                    {
                        Game game = new(hands[i], bids[i], rank);
                        games.Add(game);
                    }
                    else
                    {
                        var index = games.IndexOf(g);
                        Game game = new(hands[i], bids[i], rank);
                        games.Insert(index, game);
                    }
                }
                else
                {
                    var flag = false;
                    var index = games.IndexOf(sameGames.First());
                    foreach (var sameg in sameGames)
                    {
                        flag = false;
                        for (int j = 0; j < 5; j++)
                        {
                            var currsam = curr;
                            var sam = sameg.Hand;

                            var curr_x = cards.IndexOf(hands[i][j]);
                            var same_y = cards.IndexOf(sameg.Hand[j]);

                            if (curr_x == same_y)
                                continue;

                            if (curr_x > same_y)
                            {
                                flag = true;
                                break;
                            }
                            if (curr_x < same_y)
                            {
                                break;
                            }
                        }


                        if (flag)
                            break;
                        else
                            index++;
                    }

                    Game game = new(hands[i], bids[i], rank);
                    games.Insert(index, game);
                }

            }

            var total = 0;
            for (int i = 0; i < games.Count(); i++)
            {
                total += (games[i].Bid * (i + 1));
            }

            Console.WriteLine(total);
            Utility.PrintList(games);
        }
    }
}
