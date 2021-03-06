﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimplonAdventure.Helpers;

namespace SimplonAdventure
{
    public class Carte
    {
        private readonly int _dimension;

        private Lieu[,] _carte;

        public int PosX { get; set; }

        public int PosY { get; set; }

        public Lieu Position => _carte[PosX, PosY];
        
        
        public Carte(int dimension)
        {
            _dimension = dimension;
            _carte = new Lieu[dimension, dimension];
        }

        public void Generer()
        {
            int nombreHeal = 0;
            int nombreMonstres = 0;
            int nombreVide = 0;

            for (int i = 0; i < _dimension; i++)
            {
                for (int j = 0; j < _dimension; j++)
                {
                     var lieu = new Lieu();
                    int res=lieu.Generer();
                    _carte[i, j] = lieu;
                    //Code de log
                    switch (res)
                    {
                        case 0:
                            nombreVide++;
                            break;
                        case 1:
                            nombreMonstres++;
                                break;
                        case 2:
                            nombreHeal++;
                            break;
                    }
                }
            }

            var finX = RandomHelper.GetRandom(0, _dimension - 1);
            var finY = RandomHelper.GetRandom(0, _dimension - 1);
            var marchandX = RandomHelper.GetRandom(0, _dimension - 1);
            var marchandY = RandomHelper.GetRandom(0, _dimension - 1);
             PosX = RandomHelper.GetRandom(0, _dimension - 1);
             PosY = RandomHelper.GetRandom(0, _dimension - 1);

            while (finX == PosX && finY == PosY)
            {
                PosX = RandomHelper.GetRandom(0, _dimension - 1);
                PosY = RandomHelper.GetRandom(0, _dimension - 1);
            }

            while (finX == marchandX && finY == marchandY && PosX == marchandX && PosY == marchandY)
            {
                marchandX = RandomHelper.GetRandom(0, _dimension - 1);
                marchandY = RandomHelper.GetRandom(0, _dimension - 1);
            }


            Lieu fin = _carte[finX, finY];
            fin.EstFin = true;
            fin.Monstre=new Monstre
            {
                Pv = 250,
                DegatMax = 25,
                Nom = "George Abitbol"
            };

            Lieu marchand = _carte[marchandX, marchandY];
            marchand.EstMarchant = true;
            marchand.EstCaseSoin = false;

            Console.WriteLine($"La carte a été générée avec {nombreVide} cases vides, {nombreMonstres} monstres et {nombreHeal} cases de soin");
            Console.WriteLine($"Départ à la case [{PosX},{PosY}], princesse à la case [{finX},{finY}]");

        }

        public bool Deplacer(string direction)
        {
            switch (direction.ToLower())
            {
                case "n":
                case "z":
                    if (PosY > 0)
                    {
                         PosY--;
                        return true;
                    }
                    break;
                case "d":
                case "e":
                    if (PosX < _dimension-1)
                    {
                        PosX++;
                        return true;
                    }
                    break;
                case "s":
                    if (PosY < _dimension-1)
                    {
                        PosY++;
                        return true;
                    }
                    break;
                case "q":
                case "o":
                    if (PosX > 0)
                    {
                        PosX--;
                        return true;
                    }
                    break;
                default:
                    throw new InvalidOperationException();
            }
            return false;
        }


        public override string ToString()
        {
            var carte = string.Empty;
            carte += "     ";

            //Première ligne 
            for (int i = 0; i < _dimension; i++)
            {
                if (i == 0) carte += '|';

                carte += $" {(i < 10 ? $" {i}" : $"{i}")} |";
            }

            //Contenu de la carte
            for (int j = 0; j < _dimension; j++)
            {
                carte += $"\n| {(j < 10 ? $"{j} " : $"{j}")} |";

                for (int k = 0; k < _dimension; k++)
                {
                    carte += $"  {(j == PosY && k == PosX ? "X" : _carte[k,j].ToString())} |";
                }
            }

            return carte;
        }

        public void RevealCarte()
        {
            for (int j = 0; j < _dimension; j++)
            {
               for (int k = 0; k < _dimension; k++)
               {
                   _carte[j, k].EstVisible = true;
               }
            }
        }
    }
}
